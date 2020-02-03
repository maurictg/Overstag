using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;

namespace Overstag.Services.Sockets
{
    public class SocketConnections
    {
        private ConcurrentDictionary<string, WebSocket> _connections = new ConcurrentDictionary<string, WebSocket>();
        private int _index = 0;

        public WebSocket GetSocket(string id)
            => _connections.FirstOrDefault(f => f.Key == id).Value;

        public Dictionary<string, WebSocket> GetConnections()
            => _connections.ToDictionary(f => f.Key, f => f.Value);

        public string GetId(WebSocket socket)
            => _connections.FirstOrDefault(f => f.Value == socket).Key;

        public async Task RemoveAsync(string id)
        {
            _connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server closed connection.", System.Threading.CancellationToken.None);
        }

        public void AddSocket(WebSocket socket)
        {
            _connections.TryAdd(Encryption.Random.rHash("WebSocket" + _index++), socket);
        }
    }
}
