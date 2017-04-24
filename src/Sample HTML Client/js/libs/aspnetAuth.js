/*!
 * Lock stock and auth
 * http://www.codehq.co.za
 * Copyright 2017 Lock Stock and Auth Authors
 * Copyright 2017 Code HQ (Pty) (Ltd)
 * Licensed under MIT (https://github.com/Halceyon/lock-stock-and-auth/blob/master/LICENSE)
 */
; (function (root, factory) {

    if (typeof define === "function" && define.amd) {
        define(["jquery"], factory);
    } else if (typeof exports === "object") {
        module.exports = factory(require("jquery"));
    } else {
        root.aspnetAuth = factory(root.jQuery);
    }

}(this, function (jquery) {
    if (jquery === undefined) {
        alert("aspnet authentication requires jQuery");
    }
    
    function createCookie(name, value, seconds) {
        var expires;

        if (seconds) {
            var date = new Date();
            date.setTime(date.getTime() + (seconds * 1000));
            expires = "; expires=" + date.toGMTString();
        } else {
            expires = "";
        }
        document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
    }

    function readCookie(name) {
        var nameEQ = encodeURIComponent(name) + "=";
        var ca = document.cookie.split(";");
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === " ") c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0) return decodeURIComponent(c.substring(nameEQ.length, c.length));
        }
        return null;
    }

    function eraseCookie(name) {
        createCookie(name, "", -1);
    }

    function saveAuth(result) {
        createCookie("aspnetAuthToken", result.access_token);
        createCookie("apnetAuth", JSON.stringify(result));
    }

    var aspnetAuth = {
        authentication: null,
        user: {},
        url: "",
        register: function (username, password, cb) {
            console.log(this);
            $.ajax({
                    method: "POST",
                    url: this.url + "/api/account/register",
                    data: {
                        Username: username,
                        Password: password,
                        ConfirmPassword: password
                    }
                })
                .done(function (result) {
                    cb({
                        result: "success",
                        message: result
                    });
                })
                .error(function(jqXhr) {
                    cb({
                        result: "failed",
                        message: jqXhr.responseText
                    });
                });
        },
        login: function (username, password, cb) {
            $.ajax({
                url: this.url + "/token",
                method: "POST",
                data: {
                    username: username,
                    password: password,
                    grant_type: "password",
                    client_id: "jsApp"
                },
                success: function (result) {
                    saveAuth(result);
                    fillAuth();
                    cb({
                        result: "success",
                        message: "User logged in successfully"
                    });
                },
                error: function (result) {
                    cb({
                        result: "failed",
                        message: result.responseJSON.error_description
                    });
                }
            });
        },
        logout: function(cb) {
            eraseCookie("aspnetAuthToken");
            eraseCookie("apnetAuth");
            cb({
                result: "success",
                message: "User logged out successfully"
            });
        },
        refreshToken: function (cb) {
            var data = {
                grant_type: "refresh_token",
                refresh_token: this.authentication.refresh_token,
                client_id: "jsApp"
            };
            console.log(data);
            $.ajax({
                url: this.url + "/token",
                method: "POST",
                data: {
                    grant_type: "refresh_token",
                    refresh_token: this.authentication.refresh_token,
                    client_id: "jsApp"
                },
                success: function (result) {
                    saveAuth(result);
                    fillAuth();
                    cb({
                        result: "success",
                        message: "Token refreshed"
                    });
                },
                error: function (result) {
                    cb({
                        result: "failed",
                        message: result.responseJSON.error_description
                    });
                }
            });
        },
        fillAuth: function() {
            fillAuth();
        },
        web: {
            get: function(url, data, cb) {
                $.ajax({
                    url: aspnetAuth.url + url,
                    data: data,
                    headers: {
                        'Authorization': 'Bearer ' + readCookie("aspnetAuthToken")
                    },
                    method: "GET",
                    success: function (result) {
                        cb(result);
                    },
                    error: function (result) {
                        cb(result);
                    }
                });
            }
        }
    };

    function fillAuth() {
        var expires = Date.parse(aspnetAuth.authentication[".expires"]);
        var now = Date.parse(new Date());
        if (expires < now) {
            eraseCookie("apnetAuth");
        } else {
            aspnetAuth.authentication = JSON.parse(readCookie("apnetAuth"));
        }

    }

    fillAuth();
    return aspnetAuth;
}));