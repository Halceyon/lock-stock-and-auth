
require('./styles.css');
require('bootstrap');

import AspnetAuth from 'aspnet-auth';
import $ from 'jquery';

window.aspnetAuth = new AspnetAuth({
    url: 'http://localhost:52387'
});

const logOutput = function (json) {
    $('#codeResult').html(JSON.stringify(json, null, 4));
};
const isBusy = function (busy) {
    if (busy) {
        $('#rowProgress').show();
    } else {        
        $('#rowProgress').hide();
    }
}

$('#formRegister').on('submit', function (e) {
    e.preventDefault();
    var username = $("#formRegister input[name=username]").val();
    var password = $("#formRegister input[name=password]").val();
    var confirmPassword = $("#formRegister input[name=confirmPassword]").val();
    isBusy(true);
    aspnetAuth.register(username, password)
    .then(function (result){
        logOutput(result);
        isBusy(false);    
    })
    .catch(function (e) {
        logOutput(e);
    });
});
$('#formLogin').on('submit', function (e) {
    e.preventDefault();
    var username = $("#formLogin input[name=username]").val();
    var password = $("#formLogin input[name=password]").val();
    isBusy(true);
    
    aspnetAuth.login(username, password)
    .then(function (result){
        logOutput(result);
        isBusy(false);
    })
    .catch(function (e) {
        logOutput(e);
        isBusy(false);
    });
});
$("#btnLogOut").click(function () {
    aspnetAuth.logout().then(function (result){
        logOutput(result);
    });
});
$('#btnRefreshToken').click(function () {
    aspnetAuth.refreshToken().then(function (result){
        logOutput(result);
    });
});