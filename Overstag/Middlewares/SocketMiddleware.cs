using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Overstag.Middlewares
{
    public class SocketMiddleware
    {
        public static void Handle(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
                    await HandleWS(context, ws);
                }
                else
                    context.Response.StatusCode = 400;
            });
        }

        public static async Task HandleWS(HttpContext context, WebSocket socket)
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
    }
}
