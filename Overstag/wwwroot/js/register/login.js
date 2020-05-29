const Login = function () {
    return {
        init() {
            $('#_progbar').show();
            $('#_reg, #_mreg').addClass('active');
            $('#progbar2, #mailhelper').hide();
            this.mapEvents();
            OverstagJS.General.restoreMe(Login.toggleWith, true, Login.redirect);
        },
        mapEvents() {
            $('#Password').keydown((e) => {
                if (e.which === 13)
                    Login.login();
            });

            $('#Username').keydown((e) => {
                if (e.which === 13)
                    $('#Password').focus();
            });


            $('#lUsername').click(() => $('#Username').focus());
            $('#lPassword').click(() => $('#Password').focus());
            $('#btnlogin').click(() => Login.login());
            $('#tbrmail').on('keyup', () => $('#mailhelper').hide());

            $('#sendmailreset').click(() => {
                if ($('#tbrmail').val() !== '') {
                    $('#mailhelper').hide();
                    Login.sendmail();
                } else {
                    $('#mailhelper').show();
                }
            });

            $('#2facode').on('paste', (e) => {
                let t = e.originalEvent.clipboardData.getData('text');
                if (!t || t === null || t.length < 1)
                    t = $('#2facode').val();
                else
                    $('#2facode').val(t);

                if (t.length >= 6)
                    Login.validate2fa();
            });

            $('#2facode').on('keyup', () => {
                if ($('#2facode').val().length >= 6)
                    Login.validate2fa();
            });
        },
        toggleWith(enable) {
            if (enable)
                $('#logincard').fadeIn('slow');
            Login.toggle(enable);
        },
        toggle(enable) {
            if (!enable) {
                $('#btnlogin').addClass('disabled');
                $('#Username, #Password').attr('disabled', true);
                $('#_progbar').show();
            } else {
                $('#_progbar, #lblmoment').hide();
                $('#btnlogin').removeClass('disabled');
                $('#Username, #Password').removeAttr('disabled');
                $('#Username').focus();
            }
        },
        isValid() {
            let isValid = true;
            $('.checki').each(function() {
                if ($.trim($(this).val()) === '')
                    isValid = false;
            });
            return isValid;
        },
        login() {
            if (this.isValid()) {
                this.toggle(false);
                $.post('/Register/postLogin', { Username: $('#Username').val(), Password: $('#Password').val() }, (r) => {
                    console.log(r);
                    if (r.status === 'success') {
                        if (r.type === 3)
                            localStorage.setItem('redirect', '/Admin');

                        if (r.type === 2)
                            localStorage.setItem('redirect', '/Mentor');

                        if (r.twofactor === 'yes') {
                            Login.open2fa();
                            $('#2facode').focus();
                            token = r.token;
                        }
                        else {
                            Login.redirect();
                            localStorage.setItem('remember', r.remember);
                        }
                    }
                    else {
                        M.toast({ html: r.error, classes: 'red' });
                        $('.checki').val('');
                    }
                }, 'json'
                ).done(function () {
                    Login.toggle(true);
                    $('#Username').focus();
                }).fail(function () {
                    $('#_progbar').hide();
                    M.toast({ html: 'Inloggen mislukt door interne fout', classes: 'red' });
                });
            } else {
                M.toast({ html: 'Vul a.u.b. alle velden in', classes: 'orange' });
            }
        },
        redirect() {
            localStorage.setItem('login', true);
            if (redirectUrl !== '') {
                if (redirectUrl === 'RELOAD') {
                    window.location.reload();
                } else {
                    console.log('Redirecting to: ' + redirectUrl);
                    window.location.href = redirectUrl;
                }
                return;
            }

            if (localStorage.getItem('redirect')) {
                $(location).attr('href', localStorage.getItem('redirect'));
                localStorage.removeItem('redirect');
            }
            else {
                $(location).attr('href', '/User');
            }
        },

        //To be refactored
        sendmail() {
            $('#progbar2').show();
            if ($('#tbrmail').val() === '') {
                M.toast({ html: 'Vul a.u.b. alle velden in', classes: 'orange' });
                return;
            }
            $.post('/Register/postMailreset', { Email: $('#tbrmail').val() }, function (response) {
                if (response.status === 'success') {
                    $('#tbrmail').val('')
                    M.toast({ html: 'Mail verzonden. Check je email a.u.b. <br><b> Controleer ook je spam map</b>', classes: 'green ' });
                    Login.cancelMail();
                }
                else {
                    M.toast({ html: response.error, classes: 'red ' });
                    $('#tbrmail').val('');
                }
            }, 'json').done(() => {
                $('#progbar2').hide();
                M.Modal.getInstance($('#memailreset')).close();
            });
        },

        cancelMail() {
            $('#tbremail').val('');
            $('#memailreset').hide();
            $('#logincard').fadeIn('slow');
            $('#Username').focus();
        },

        open2fa() {
            $('#logincard').hide();
            $('#2facard').fadeIn('slow');
            $('#2facode').focus();
        },

        openMailReset() {
            $('#logincard').hide();
            $('#memailreset').fadeIn('slow');
        },

        validate2fa() {
            if ($('#2facode').val().length !== 6 || isNaN($('#2facode').val())) {
                M.toast({ html: 'Type aub een geldige code in', classes: 'orange' });
                $('#2facode').val('');
            }
            else {
                var now = new Date();
                var timestring = now.getDate().toString().padStart(2, '0') + '-' + (now.getMonth() + 1).toString().padStart(2, '0') + '-' + now.getFullYear() + ' ' + now.getHours().toString().padStart(2, '0') + ':' + now.getMinutes().toString().padStart(2, '0') + ':' + now.getSeconds().toString().padStart(2, '0');
                $.post('/Register/Validate2FA', { token: token, code: $('#2facode').val(), datetime: timestring }, function (response) {
                    if (response.status === 'success') {
                        localStorage.setItem('remember', response.remember);
                        Login.cancel2fa();
                        Login.redirect();
                    }
                    else if (response.status === 'timeout') {
                        M.toast({ html: 'Uw klok staat niet goed.', classes: 'orange' });
                    } else {
                        M.toast({ html: 'Validatie mislukt.', classes: 'red' });
                        $('#2facode').val('');
                        wrong2fa++;
                        if (wrong2fa > 5) {
                            M.toast({ html: 'Te veel pogingen. Probeer het later opnieuw', classes: 'orange' });
                            Login.cancel2fa();
                        }
                    }
                }, 'json').fail(function () {
                    console.log('Er gaat iets fout');
                    errorcnt++;
                    if (errorcnt > 3) {
                        M.toast({ html: 'Er is iets fout gegaan', classes: 'red' });
                    }
                });
            }
        },

        cancel2fa() {
            $('#Username').val('');
            $('#Password').val('');
            $('#2facode').val('');
            $('#2facard').hide();
            $('#logincard').fadeIn('slow');
            $('#Username').focus();
        },

        restore2fa() {
            if ($('#tfarest').val() === '')
                M.toast({ html: 'Vul a.u.b. alle velden in!', classes: 'orange' });
            else {
                $.get('/Register/Restore2FA/' + token + '/' + encodeURIComponent($('#tfarest').val()), function (r) {
                    if (r.status === 'success') {
                        M.Modal.getInstance($('#tfarestore')).close();
                        Login.cancel2fa();
                        M.toast({ html: 'Code is juist. Log a.u.b. opnieuw in', classes: 'green' });
                    } else
                        M.toast({ html: 'Code onjuist', classes: 'red' });

                    $('#tfarest').val('');
                }, 'json').fail(function () {
                    M.toast({ html: 'Er is iets fout gegaan', classes: 'red' });
                });
            }
        },
    };
}();