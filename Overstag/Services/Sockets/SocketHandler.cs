using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Overstag.Middlewares;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading.Tasks;

namespace Overstag.Services.Sockets
{
    public abstract class SocketHandler
    {
        public SocketConnections Connections { get; set; }

        public SocketHandler(SocketConnections connectionManager)
        {
            this.Connections = connectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            await Task.Run(() => { Connections.AddSocket(socket); });
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await Connections.RemoveAsync(Connections.GetId(socket));
        }

        public async Task Send(WebSocket socket, byte[] data)
        {
            if (socket.State != WebSocketState.Open)
                return;
            await socket.SendAsync(new ArraySegment<byte>(data, 0, data.Length), WebSocketMessageType.Binary, true, System.Threading.CancellationToken.None);
        }

        public async Task Send(string id, byte[] data)
            => await Send(Connections.GetSocket(id), data);

        public async Task Broadcast(byte[] data)
        {
            foreach (var connection in Connections.GetConnections())
                await Send(connection.Value, data);
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }

    public static class SocketsExtension 
    {
        public static IServiceCollection AddSocketManager(this IServiceCollection services)
        {
            services.AddTransient<SocketConnections>();
            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                    services.AddSingleton(type);
            }
            return services;
        }

        public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString path, SocketHandler handler)
        {
            return app.Map(path, (x) => x.UseMiddleware<SocketMiddleware>(handler));
        }
    }
}
