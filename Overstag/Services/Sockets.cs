using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Overstag.Middlewares;
using System.Text;

namespace Overstag.Services
{
    public class ConnectionData
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Metadata { get; set; }
    }

    public class SocketHandler
    {
        private ConcurrentDictionary<ConnectionData, WebSocket> _connections = new ConcurrentDictionary<ConnectionData, WebSocket>();
        private int _counter = 0;

        public SocketHandler() { }

        public string GenerateID() => Encryption.Random.rHash("Socket-" + _counter++);

        public ConnectionData Find(WebSocket socket) => _connections.FirstOrDefault(f => f.Value == socket).Key;
        public ConnectionData Find(string id) => _connections.FirstOrDefault(f => f.Key.Id == id).Key;
        public string FindId(WebSocket socket) => Find(socket).Id;
        public WebSocket FindSocket(string id) => _connections.FirstOrDefault(f => f.Key.Id == id).Value;

        public async Task Add(WebSocket socket, bool broadcast = false) => await Add(socket, "Anonymous", broadcast);
        public async Task Add(WebSocket socket, string name, bool broadcast = false) => await Add(socket, new ConnectionData() { Username = name}, broadcast);
        public async Task Add(WebSocket socket, ConnectionData data, bool broadcast)
        {
            await Task.Run(() => {
                data.Id = GenerateID();
                if (!_connections.TryAdd(data, socket))
                    return;
            });
            
            if(broadcast)
                await Broadcast(Encoding.UTF8.GetBytes($"{data.Username} joined at {DateTime.Now.ToLongTimeString()}"));
        }

        public async Task Remove(string id, bool broadcast = false) => await Remove(Find(id), broadcast);
        public async Task Remove(WebSocket socket, bool broadcast = false) => await Remove(Find(socket), broadcast);
        public async Task Remove(ConnectionData data, bool broadcast)
        {
            _connections.TryRemove(data, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed connection (server)", CancellationToken.None);
            if (broadcast)
                await Broadcast(Encoding.UTF8.GetBytes($"{data.Username} left at {DateTime.Now.ToLongTimeString()}"));
        } 

        public async Task Broadcast(byte[] data)
        {
            foreach (var socket in _connections.Select(f => f.Value).ToList())
                await socket.SendAsync(new ArraySegment<byte>(data, 0, data.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            byte[] data = new byte[result.Count];
            Buffer.BlockCopy(buffer, 0, data, 0, result.Count);
            await Broadcast(data);
        }
    }

    public static class SocketExtensions
    {
        public static IServiceCollection AddSocketHandler(this IServiceCollection collection)
        {
            collection.AddTransient<SocketHandler>();
            foreach (var item in Assembly.GetEntryAssembly().ExportedTypes.Where(f => f.GetTypeInfo().GetType() == typeof(SocketHandler)))
                collection.AddSingleton(item);
            return collection;
        }

        public static IApplicationBuilder MapSocketHandler(this IApplicationBuilder app, PathString path, SocketHandler handler)
            => app.Map(path, (f) => f.UseMiddleware<SocketMiddleware>(handler));
    }
}
