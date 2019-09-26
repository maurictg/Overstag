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
        }()
    };
}();

$(function() {
    OverstagJS.init();
});