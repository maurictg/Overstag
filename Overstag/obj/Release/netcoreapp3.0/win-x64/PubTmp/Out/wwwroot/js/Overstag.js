var OverstagJS = function () {
    return {
        init: function () {
            this.Theme.init();
        },
        Loader: function () {
            return {
                init: function () {
                    $.get('/html/loader.html', function (r) {
                        $('#_data').html(r);
                    }).done(function () {
                        $('.modal').modal();
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
                    $('.card-panel, .card, .card-content, .card-reveal, .card-image').addClass('white-text grey darken-3');
                    $(':text, :password, textarea').addClass('white-text');
                    $('tr.grey').removeClass('lighten-4').addClass('darken-1');
                    $('img.blue, div.blue').removeClass('blue lighten-4').addClass('grey darken-3');
                    $('.modal, .modal-footer, .collapsible-header, .collection-item, body').addClass('white-text grey darken-4');
                    $('.blue-dark-text').removeClass('blue-dark-text').addClass('white-text bdt');
                    localStorage.setItem('darktheme', 'true');
                },
                setLight: function () {
                    $('.card-panel, .card, .card-content, .card-reveal, .card-image').removeClass('white-text grey darken-3');
                    $(':text, :password, textarea').removeClass('white-text');
                    $('.modal, .modal-footer, .collapsible-header, .collection-item, body').removeClass('white-text grey darken-4');
                    $('img.grey, div.grey').removeClass('grey darken-3').addClass('blue lighten-4');
                    $('tr.grey').removeClass('darken-1').addClass('lighten-4');
                    $('.bdt').removeClass('white-text').addClass('blue-dark-text');
                    localStorage.setItem('darktheme', 'false');
                },
                init: function () {
                    if (localStorage.getItem('darktheme')) {
                        if (localStorage.getItem('darktheme') === 'true')
                            this.setDark();
                    }
                    else {
                        localStorage.setItem('darktheme', 'false');
                    }
                }
            };
        }(),
        General: function () {
            return {
                getIEVersion: function () {
                    var ua = window.navigator.userAgent;
                    var msie = ua.indexOf('MSIE ');
                    if (msie > 0) {
                        // IE 10 or older => return version number
                        return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
                    }

                    var trident = ua.indexOf('Trident/');
                    if (trident > 0) {
                        // IE 11 => return version number
                        var rv = ua.indexOf('rv:');
                        return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
                    }

                    var edge = ua.indexOf('Edge/');
                    if (edge > 0) {
                        // Edge (IE 12+) => return version number
                        return parseInt(ua.substring(edge + 5, ua.indexOf('.', edge)), 10);
                    }

                    // other browser
                    return false;
                },
                detectIE: function () {
                    var version = this.getIEVersion();
                    if (version === false) {
                        console.log('Goed bezig! Geen IE');
                    } else if (version >= 12) {
                        console.log('User gebruikt Edge ' + version);
                    } else {
                        console.log('User gebruikt IE ' + version);
                        M.toast({ html: '<b>Overstag support geen Internet Explorer</b>', classes: 'red', displayLength: 10000 });
                        M.toast({ html: 'Gebruik in plaats van dit Edge, Chrome of beter: Firefox', classes: 'blue' });
                    }
                }
            };
        }(),
        Security: function () {
            return {
                init: function () {
                    if (localStorage.getItem('remember')) {
                        if (localStorage.getItem('remember') === 'yes') {
                            this.setRemember();
                        }
                    }
                },
                Login: function () {
                    if (localStorage.getItem('remember')) {
                        var r = localStorage.getItem('remember');
                        if (r.length > 3) {
                            $.get('/Register/tryRestore/' + r, function (res) {
                                if (res.status === 'success') {
                                    console.log('Welcome back!');
                                    localStorage.setItem('remember', res.newtoken);
                                    window.location.href = '/User';
                                } else {
                                    console.log(res.error);
                                    localStorage.removeItem('remember');
                                }
                            }, 'json');
                        }
                    }
                },
                Logout: function () {
                    console.log('Logging out...');
                    this.removeRemember();
                },
                setRemember: function () {
                    console.log('Ill remember you');
                    $.get('/User/setRemember', function (r) {
                        localStorage.setItem('remember', r);
                    }, 'json');
                },
                removeRemember: function () {
                    if (localStorage.getItem('remember')) {
                        var r = localStorage.getItem('remember');
                        if (r !== 'yes' && r !== '') {
                            $.get('/User/removeRemember/' + r, function (res) {
                                if (res.status === 'success') {
                                    console.log('Bye!');
                                    localStorage.removeItem('remember');
                                } else {
                                    console.log('Error status');
                                }
                            }).fail(function() {
                                M.toast({ html: 'Er gaat iets fout', classes: 'red' });
                                localStorage.removeItem('remember');
                            }).always(function () {
                                OverstagJS.Security.redirect();
                            });
                        } else {
                            localStorage.removeItem('remember');
                        }
                    }
                    else {
                        this.redirect();
                    }

                },
                redirect: function () {
                    localStorage.setItem('logout', '1');
                    localStorage.removeItem('login');
                    window.location.href = '/Register/Logoff';
                }
            };
        }()
    };
}();

$(function() {
    OverstagJS.init();
});