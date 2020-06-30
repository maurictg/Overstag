var Overstag = function () {
    return {
        init: function () {
            this.Theme.init();
            this.mapEvents();
        },
        mapEvents: function () {
            $('#btnsnow').on('click', this.Theme.letItSnow);
        },
        Loader: function () {
            return {
                init: function(callback) {
                    $.get('/html/loader.html', function(r) {
                        $('#_data').html(r);
                    }).done(function () {
                        $('.modal').modal();
                        if (callback !== undefined && callback !== null)
                            callback();
                    }).fail(function () {
                        console.log('Failed to load!');
                    });
                },
                show: function () {
                    M.Modal.getInstance($('#_loader')).open();
                },
                hide: function () {
                    M.Modal.getInstance($('#_loader')).close();
                }
            };
        }(),
        Theme: function () {
            return {
                setDark: function () {
                    $('body').addClass('overstag-dark');
                    localStorage.setItem('darktheme', 'true');
                },
                setLight: function () {
                    $('body').removeClass('overstag-dark');
                    localStorage.setItem('darktheme', 'false');
                },
                init: function () {
                    if (localStorage.getItem('darktheme')) {
                        if (localStorage.getItem('darktheme') === 'true')
                            this.setDark();
                        else
                            this.setLight();
                    }
                    else {
                        localStorage.setItem('darktheme', 'false');
                    }
                }
            };
        }(),
        General: function () {
            return {
                rememberMe: function (token) {
                    $.post('/Auth/Register', { token: token }, function (r) {
                        if (r.status === 'success') {
                            localStorage.setItem('remember', r.token);
                            console.log('Ill remember you');
                        } else {
                            console.log('Remembering failed');
                        }
                    });
                },
                restoreMe: function (callback, params, successcallback) {
                    if (localStorage.getItem('remember')) {
                        $.post('/Auth/Login', { token: localStorage.getItem('remember') }, function (r) {
                            if (r.status === 'success') {
                                if (successcallback !== undefined && successcallback !== null) {
                                    successcallback();
                                }
                                console.log('Login restored!');
                            } else {
                                console.log('Cant restore login');
                                localStorage.removeItem('remember');
                            }
                        }).done(function () {
                            if (callback !== undefined && callback !== null) {
                                callback(params);
                            }
                            return true;
                        }).fail(function () {
                            if (callback !== undefined && callback !== null) {
                                callback(params);
                            }
                            return false;
                        });
                    }
                    else {
                        if (callback !== undefined && callback !== null)
                            callback(params);
                        return false;
                    }
                },
                logout: function(nomaterial) {
                    var token = localStorage.getItem('remember');
                    localStorage.setItem('logout', true);
                    localStorage.removeItem('remember');
                    $.get('/Register/Logout', { token: token }, function (r){
                        if (r.status === 'error') {
                            if (!nomaterial)
                                M.toast({ html: 'Er is iets fout gegaan met uitloggen', classes: 'red' });

                            window.setTimeout(function () {
                                window.location.href = '/Home';
                            }, 500);
                        } else {
                            window.location.href = '/Home';
                        }
                    },'json');
                }
            };
        }(),
    };
}();

var Core = function(){
    return {
        doReload: function (interval) {
            setTimeout(window.location.reload.bind(window.location), interval);
        }
    };
}();

$(function() {
    Overstag.init();
});