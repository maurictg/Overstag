let H = function() {
    'use strict';

    /* Constructor _c */
    let _c = function(s) {
        if(!s) return;
        
        //Store nodelist in prototype.E (E means Elements)
        if(typeof s === 'string') _c.prototype.E = document.querySelectorAll(s);
        else if(s instanceof NodeList) _c.prototype.E = s;
        else if(s instanceof _c) console.log(s);
        else return;

        _c.prototype.Q = s; //Store query in prototype.Q
        //Copy prototype objects to main object for console logout and array usage
        this.E.forEach((v, k) => {
            this[k] = v;
        });
    }

    /* Shorthand */
    let fn = _c.prototype;

    /**
     * Chaining functions
     */

    /* Each callback(e, i) */
    fn.each = function(cb) {
        if (!cb || typeof cb !== 'function') return;
        for (var i = 0; i < this.E.length; i++) {
            cb(this.E[i], i);
        }
        return this;
    }

    fn.addClass = function(c) {
        this.each((e) => e.classList.add(c));
        return this;
    }

    fn.rmClass = function(c) {
        this.each((e) => e.classList.remove(c));
        return this;
    }

    fn.hide = function() {
        this.css('display', 'none');
        return this;
    }

    fn.show = function() {
        this.css('display', 'block');
        return this;
    }

    fn.replace = function(e) {
        if(!e) return;
        this.each((x) => x.replaceWith(e));
        return this;
    }

    fn.on = function(e, handler) {
        this.each((x) => x.addEventListener(e, handler));
        return this;
    }

    fn.off = function() {
        this.each((x, i) => {
            let n = x.cloneNode(true);
            this.E[i].parentNode.replaceChild(n, x);
        });
        //Return a new instance, to refresh node list
        return new _c(this.Q);
    }
    
    /**
     * Non-chaining functions (getters/setters)
     */

    fn.html = function(h) {
        if(h) this.each((e) => e.innerHTML = h);
        else return this.E[0].innerHTML;
    }
    
    fn.text = function(t) {
        if(t) this.each((e) => e.textContent = t);
        else return this.E[0].innerText;
    }

    fn.css = function(n, v) {
        if(!n) return;
        if(v) this.each((e) => e.style[n] = v);
        else return this.E[0].style[n];
    }

    fn.attr = function(n, v) {
        if(!n) return;
        if(v) this.each((e) => e.setAttribute(n, v));
        else return this.E[0].getAttribute(n);
    }

    fn.rmAttr = function(n) {
        if(n) this.each((e) => e.removeAttribute(n));
        else return;
    }

    fn.data = function(n, v) {
        if(!n) return;
        return this.attr('data-'+n, v);
    }

    fn.rmData = function(n) {
        if(!n) return;
        return this.rmAttr('data-'+n);
    }

    fn.destroy = function() {
        this.each((e) => e.parentNode.removeChild(e));
    }

    fn.val = function(v) {
        if(v) this.each((e) => e.value = v);
        else return this.E[0].value;
    }

    fn.clone = function() {
        return this.E[0].cloneNode(true);
    }
    
    fn.first = function() {
        return this.E[0];
    }

    //Serialize form to url-encoded formdata or JSON
    fn.serialize = function(json = false) {
        let d = {};
        let form = this.E[0];
        Array.prototype.slice.call(form.elements).forEach(function(f) {
            if (!f.name || f.disabled || ['file', 'reset', 'submit', 'button'].indexOf(f.type) > -1) return;
            if (f.type === 'select-multiple') {
                Array.prototype.slice.call(f.options).forEach(function (option) {
                    if (!option.selected) return;
                    d[f.name] = option.value;
                });
                return;
            }
            if (['checkbox', 'radio'].indexOf(f.type) > -1 && !f.checked) return;
            d[f.name] = f.value;
        });
        return (json) ? d : $.serializeJSON(d);
    }

    /* Return initalized constructor */
    return (s) => new _c(s);
}();

//Set symbol
window.$ = H;