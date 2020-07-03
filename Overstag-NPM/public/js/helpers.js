/**
 * Helper functions
 */

/**
 * 
 * @param {Object} o: Options {method: string, url: string, data: object, responseType: string, requestType: string, [optional:] onComplete, onError, onProgress(loaded, total, percentage)}
 * @param {Function} cb: Callback function(data, statusCode, statusText)
 */
H.ajax = function(o, cb) {
    if(!o || !cb) return false;
    let x = new XMLHttpRequest();
    x.onreadystatechange = function() {
        if(this.readyState === 4) cb(this.response, this.status, this.statusText);
    };

    x.onload = o.onComplete;
    x.onerror = o.onError;
    x.responseType = o.responseType;
    o.method = o.method.toLowerCase();

    x.open(o.method, o.url, true);
    if((o.method === 'post' || o.method === 'put') && o.data && o.requestType) {
        let sendString;
        switch (o.requestType) {
            case 'formencoded':
            case 'urlencoded':
                sendString = true;
                o.dataString = (typeof o.data === 'object') ? H.serializeJSON(o.data) : o.data;
                o.requestType = 'application/x-www-form-urlencoded';
                break;
            case 'json':
                sendString = true;
                o.requestType = 'application/json;charset=UTF-8';
                o.dataString = JSON.stringify(o.data);
            default:
                break;
        }

        if(o.onProgress) x.onprogress = (e) => o.onProgress(e.loaded, e.total, Math.ceil((e.loaded / e.total) * 100));
        x.setRequestHeader('Content-Type', o.requestType);
        x.send((sendString) ? o.dataString : o.data);
    }
    else {
        x.send();
    }
}

H.post = (url, data, callback, resType = 'json', reqType = 'urlencoded') => H.ajax({url: url, method: 'POST', data: data, responseType: resType, requestType: reqType}, callback);
H.get = (url, callback, resType = 'text') => H.ajax({url: url, method: 'GET', responseType: resType}, callback);
H.getJSON = (url, cb) => H.get(url, cb, 'json');

//Serialize JSON to url-encoded
H.serializeJSON = (json) => {
    let d = [];
    for (n in json) d.push(encodeURIComponent(n) + '=' + encodeURIComponent(json[n]));
    return d.join('&').replace(/%20/g, '+');
}