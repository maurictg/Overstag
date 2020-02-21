using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Invokes websocket async
        /// </summary>
        /// <param name="context">Given HttpContext from middleware</param>
        /// <returns>Task</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                byte type = (!string.IsNullOrEmpty(context.Request.Query["type"])) ? Convert.ToByte(context.Request.Query["type"]) : (byte)0;
                bool broadcast = string.IsNullOrEmpty(context.Request.Query["noBroadcast"]);
                string name = (!string.IsNullOrEmpty(context.Request.Query["name"])) ? context.Request.Query["name"].ToString() : "Anonymous";
                bool authenticated = false;

                if (context.Session.GetInt32("Type") != null)
                    if (context.Session.GetInt32("Type") > 1)
                        authenticated = true;

                WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
                await Handler.Add(ws, type, broadcast, authenticated, name);
                await Receive(ws, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                        await Handler.Receive(ws, result, buffer);
                    else if (result.MessageType == WebSocketMessageType.Close)
                        await Handler.Remove(ws, true);
                });
            }
        }

        /// <summary>
        /// Handle when websocket receives data
        /// </summary>
        /// <param name="socket">The socket</param>
        /// <param name="messageHandler">The function that handles the event</param>
        /// <returns>Task</returns>
        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> messageHandler)
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
