using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Overstag.Middlewares;
using Overstag.Classes;

namespace Overstag.Services
{
    public class ConnectionData
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Metadata { get; set; }
        public byte Type { get; set; }

        /* Types
         * 0: all
         * 1: lasergame
         */
    }


    public class SocketHandler
    {
        private ConcurrentDictionary<ConnectionData, WebSocket> _connections = new ConcurrentDictionary<ConnectionData, WebSocket>();
        private ConcurrentBag<ConnectionPool> _pools = new ConcurrentBag<ConnectionPool>();
        private int _counter = 0;

        /// <summary>
        /// Generate random unique ID hash
        /// </summary>
        /// <returns>Random hash</returns>
        public string GenerateID() => Encryption.Random.rHash("Socket-" + _counter++);

        public ConnectionData Find(WebSocket socket) => _connections.FirstOrDefault(f => f.Value == socket).Key;
        public ConnectionData Find(string id) => _connections.FirstOrDefault(f => f.Key.Id == id).Key;

        public ConnectionPool FindPool(WebSocket socket) => FindPool(Find(socket).Type);
        public ConnectionPool FindPool(int type)
        {
            ConnectionPool p = _pools.FirstOrDefault(f => f.Type == type);
            return (p == null) ? new ConnectionPool((byte)type) : p;
        }

        public string FindId(WebSocket socket) => Find(socket).Id;
        public WebSocket FindSocket(string id) => _connections.FirstOrDefault(f => f.Key.Id == id).Value;

        public async Task Add(WebSocket socket, byte type = 0, bool broadcast = false, string name = "Anonymous") => await Add(socket, new ConnectionData() { Username = name, Type = type}, broadcast);
        public async Task Add(WebSocket socket, ConnectionData data, bool broadcast)
        {
            await Task.Run(() => {
                data.Id = GenerateID();
                if (!_connections.TryAdd(data, socket))
                    return;
            });

            //Send data to joined client 
            await Send(socket, new { t = "fillData", data = FindPool(socket).Data });

            //Broadcast userJoined if has to do
            if (broadcast)
                await Broadcast(new { t = "userJoined", data = data.Username }, data.Type);
        }

        public async Task Remove(string id, bool broadcast = false) => await Remove(Find(id), broadcast);
        public async Task Remove(WebSocket socket, bool broadcast = false) => await Remove(Find(socket), broadcast);
        public async Task Remove(ConnectionData data, bool broadcast)
        {
            _connections.TryRemove(data, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed connection (server)", CancellationToken.None);

            //Broadcast that user left. Receivers can ignore if they want
            if (broadcast)
                await Broadcast(new { t = "userLeft", data = data.Username }, data.Type);
        } 

        public async Task Broadcast(byte[] data)
        {
            foreach (var socket in _connections.Select(f => f.Value).ToList())
                await Send(socket, data);
        }

        public async Task Broadcast(byte[] data, int type)
        {
            foreach (var socket in _connections.Where(g => g.Key.Type == type).Select(f => f.Value).ToList())
                await Send(socket, data);
        }

        public async Task Broadcast(object data) => await Broadcast(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
        public async Task Broadcast(object data, int type) => await Broadcast(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)), type);

        public async Task Send(WebSocket socket, byte[] data)
            => await socket.SendAsync(new ArraySegment<byte>(data, 0, data.Length), WebSocketMessageType.Text, true, CancellationToken.None);

        public async Task Send(WebSocket socket, object data)
            => await Send(socket, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));

        public async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            byte[] data = new byte[result.Count];
            Buffer.BlockCopy(buffer, 0, data, 0, result.Count);

            (ConnectionPool, object) value = FindPool(socket).HandleMessage(data);

            //Update list or insert
            var pool = value.Item1;
            var p = _pools.FirstOrDefault(f => f.Type == pool.Type);
            if (p == null)
                _pools.Add(pool);

            p = pool;

            //Broadcast result if not null
            object rdata = value.Item2;
            if (rdata != null)
                await Broadcast(rdata, p.Type);
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
