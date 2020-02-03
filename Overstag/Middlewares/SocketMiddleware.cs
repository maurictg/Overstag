using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Overstag.Services.Sockets;

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
                await Receive(ws, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Binary)
                        await Handler.Receive(ws, result, buffer);
                    else if (result.MessageType == WebSocketMessageType.Close)
                        await Handler.OnDisconnected(ws);
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
                //https://www.youtube.com/watch?v=YC3rqAmbxiU
            }

        }

        public static void UseTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
                    await HandleWSTest(context, ws);
                }
                else
                    context.Response.StatusCode = 400;
            });
        }

        public static async Task HandleWSTest(HttpContext context, WebSocket socket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                string message = Encoding.UTF8.GetString(buffer).Trim('\0');
                message = "You said: " + message;

                byte[] bytes = new byte[message.Length * sizeof(char)];
                Buffer.BlockCopy(message.ToCharArray(), 0, bytes, 0, bytes.Length);

                await socket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length), result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None);
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            }
            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, System.Threading.CancellationToken.None);
        }

        /* I can make that too! Make class including list of sockets, and use HandleWSTest to broadcast to all
         * 
         * 
         */
    }
}
