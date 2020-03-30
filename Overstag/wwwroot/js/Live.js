var OverstagLive = function () {
    return {
        websocket: null, handler: null, url: null, closehandler: null,
        init: function (url, handler, closehandler, autoreconnect = false) {
            this.url = url;
            this.handler = handler;
            this.closehandler = closehandler;
            this.websocket = new WebSocket(url);
            this.mapEvents();

            if (autoreconnect) {
                setInterval(function () {
                    if (OverstagLive.websocket.readyState === WebSocket.CLOSED) {
                        OverstagLive.websocket = new WebSocket(OverstagLive.url);
                        OverstagLive.mapEvents();
                    }
                }, 10000);
            }
        },
        mapEvents: function () {
            this.websocket.onerror = function(e) {
                M.toast({ html: e.data + '', classes: 'red' });
            };

            this.websocket.onopen = function (e) {
                M.toast({ html: 'Successvol verbonden', classes: 'green' });
            };

            this.websocket.onclose = function (e) {
                M.toast({ html: 'Verbinding verbroken', classes: 'orange' });
                if (OverstagLive.closehandler !== null && OverstagLive.closehandler !== undefined)
                    OverstagLive.closehandler();
            };

            this.websocket.onmessage = function (e) {
                if (OverstagLive.handler !== null && OverstagLive.handler !== undefined)
                    OverstagLive.handler(e.data);
                else
                    console.log('Handler not set');
            };
        },
        sendObject: function (type, data) {
            this.websocket.send(JSON.stringify({ t: type, data: JSON.stringify(data) }));
        },
        send: function (type, data) {
            this.websocket.send(JSON.stringify({ t: type, data: data }));
        }
    };
}();