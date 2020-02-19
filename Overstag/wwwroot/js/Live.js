var OverstagLive = function () {
    return {
        websocket: null, handler: null,
        init: function (url, handler) {
            this.handler = handler;
            this.websocket = new WebSocket(url);
            this.mapEvents();
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
            };

            this.websocket.onmessage = function (e) {
                this.handler(e.data);
            };
        }
    };
}();