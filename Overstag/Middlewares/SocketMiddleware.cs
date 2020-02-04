using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Overstag.Services;

namespace Overstag.Middlewares
{
    public class SocketMiddleware
    {
        public readonly RequestDelegate _next;
        public SocketHandler Handler { get; set; }

        public SocketMiddleware(RequestDelegate next, SocketHandler handler)
        {
            _next = next;
            Handler = handler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
                await Handler.Add(ws, true);
                await Receive(ws, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                        await Handler.Receive(ws, result, buffer);
                    else if (result.MessageType == WebSocketMessageType.Close)
                        await Handler.Remove(ws, true);
                });
            }
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult,byte[]> messageHandler)
        {
            var buffer = new byte[1024 * 4];
            while(socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
                messageHandler(result, buffer);
            }
        }
    }
}
