let H = function() {
    'use strict';

    /* Constructor _c */
    let _c = function(s) {
        if(!s) return;

        let nodes = [];
        if(typeof s === 'string') nodes = document.querySelectorAll(s);
        else if(s instanceof NodeList || s instanceof HTMLCollection || s instanceof _c || (s instanceof Array && s.length > 0 && s[0] instanceof HTMLElement)) nodes = s;
        else if(s instanceof HTMLElement) nodes[0] = s;
        else if(s instanceof Function) {
            if (document.readyState === "complete" || (document.readyState !== "loading" && !document.documentElement.doScroll)) s();
            else document.addEventListener("DOMContentLoaded", s);
            return;
        }
        
        this.length = (s instanceof _c) ? s.length : nodes.length;

        //Store query in prototype.Q
        _c.prototype.Q = s; 

        //Store objects in this
        for (let i = 0; i < this.length; i++) this[i] = nodes[i];
    }

    /* Shorthand */
    let fn = _c.prototype;

    /**
     * Chaining functions
     */

    /* Each callback(e, i) */
    fn.each = function(cb) {
        if (!cb || typeof cb !== 'function') return;
        for (var i = 0; i < this.length; i++) cb(this[i], i);
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
            this[i].parentNode.replaceChild(n, x);
            this[i] = n; //Also replace in selector
        });
        return this;
    }

    fn.skip = function(n) {
        if(this.length < (n + 1)) return;
        return new _c(Array.prototype.slice.call(this, n));
    }

    fn.take = function(n) {
        return new _c(Array.prototype.slice.call(this, 0, Math.min(this.length, n)));
    } 

    fn.children = function() {
        return new _c(this[0].children);
    }

    fn.focus = function() {
        this[0].focus();
        return this;
    }
    
    /**
     * Non-chaining functions (getters/setters)
     */

    fn.toArray = function() {
        return Array.prototype.slice.call(this);
    }

    fn.html = function(h) {
        if(h || h === '') this.each((e) => e.innerHTML = h);
        else return this[0].innerHTML;
    }
    
    fn.text = function(t) {
        if(t || t === '') this.each((e) => e.textContent = t);
        else return this[0].innerText;
    }

    fn.css = function(n, v) {
        if(!n) return;
        if(v) this.each((e) => e.style[n] = v);
        else return this[0].style[n];
    }

    fn.attr = function(n, v) {
        if(!n) return;
        if(v) this.each((e) => e.setAttribute(n, v));
        else return this[0].getAttribute(n);
    }

    fn.rmAttr = function(n) {
        if(!n) return;
        else this.each((e) => e.removeAttribute(n));
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
        if(v || v === '') this.each((e) => e.value = v);
        else return this[0].value;
    }

    fn.clone = function() {
        return this[0].cloneNode(true);
    }

    //Serialize form to url-encoded formdata or JSON
    fn.serialize = function(json = false) {
        let d = {};
        let form = this[0];
        Array.prototype.slice.call(form.elements).forEach((f) => {
            if (!f.name || f.disabled || ['file', 'reset', 'submit', 'button'].indexOf(f.type) > -1) return;
            if (f.type === 'select-multiple') {
                Array.prototype.slice.call(f.options).forEach((o) => {
                    if (!o.selected) return;
                    d[f.name] = o.value;
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