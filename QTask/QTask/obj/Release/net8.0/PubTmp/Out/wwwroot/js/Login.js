$(document).ready(function () {
    $('#btnLogin').click(function () {
        ValidateLogin('no');
    });
    $('#btnForceLogin').click(function () {        
        ValidateLogin('yes');
    })
})


function ValidateLogin(Frc) {
    var status = 1;
    if (CheckField($('#txtUserName'), '', 'Enter UserName')) {
        status = 0;
    }

    if (CheckField($('#txtPassword'), '', 'Enter Password')) {
        status = 0;
    }

    if (status == 1) {
        LoginNow(Frc);        
    }
}

function LoginNow(Frc) {    
    $('#btnLogin').attr('disabled', true).html('<i class="fa fa-spinner fa-spin"></i> Loading');
    var objLogin = {};
    objLogin.UserName = $('#txtUserName').val();
    objLogin.Password = $('#txtPassword').val();
    objLogin.Force = Frc;

    //$.post('/Login/ValidateLogin', { objLogin: objLogin }, function (response) {
    //    if (!response.result) {
    //        $('#btnLogin').attr('disabled', false).html('<i class="fa fa-sign-in-alt"></i> Login');
    //        if (response.msg.indexOf('logged in') > -1) {
    //            $('#concurrent-modal').modal({
    //                fadeDuration: 100
    //            });
    //        }
    //        else
    //            alert(response.msg);
    //    }
    //    else {
    //        location.href = '/dashboard/dashboard';
    //    }
    //});
    var url = apiUrl + 'loginapi';
    $.ajax({
        url: url,
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (!response.status) {
                $('#btnLogin').attr('disabled', false).html('<i class="fa fa-sign-in-alt"></i> Login');
                if (response.MsgResponse.indexOf('logged in') > -1) {
                    $('#concurrent-modal').modal({
                        fadeDuration: 100
                    });
                }
                else
                    alert(response.MsgResponse);
            }
            else {
                GoForward(response);
            }
        },
        data: JSON.stringify(objLogin)
    });
}

function GoForward(objData) {

    var objResp = {}
    objResp.id = objData.id;
    objResp.UserName = objData.UserName;
    objResp.Password = objData.Password;
    objResp.first_name = objData.first_name;
    objResp.last_name = objData.last_name;
    objResp.is_admin = objData.is_admin;
    objResp.receive_notifications = objData.receive_notifications;
    objResp.title = objData.title;
    objResp.department = objData.department;
    objResp.phone_mobile = objData.phone_mobile;
    objResp.Activestatus = objData.Activestatus;
    objResp.reports_to_id = objData.reports_to_id;
    objResp.is_group = objData.is_group;
    objResp.UserSession = objData.UserSession;
    objResp.Photo = objData.Photo;
    objResp.MsgResponse = objData.MsgResponse;
    objResp.status = objData.status;
    objResp.Token = objData.Token;
    objResp.lstUserAccess = objData.lstUserAccess;
    var url = '/Login/CheckLogin';
    $.post(url, { objResp: objResp }, function (response) {
            if (response.result) {
                location.href = response.retUrl;
            }
            else {
                alert(response.msg);
            }        
    });
    //$.ajax({
    //    url: url,
    //    type: 'POST',
    //    dataType: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    async: true,
    //    success: function (response) {
    //        if (response.result) {
    //            location.href = retUrl;
    //        }
    //        else {
    //            alert(response.msg);
    //        }
    //    },
    //    data: JSON.stringify({ 'Name': 'kalpesh','Dept':'IT' })
    //});
}

//var app = angular.module('crmApp', []);
//app.controller('loginController', function ($scope, $http) {
//    $scope.loginNow = function () {
//        var objLogin = {};
//        objLogin.UserName = $scope.UserName;
//        objLogin.Password = $scope.Password;

//        $http.post('/Login/ValidateLogin', JSON.stringify(objLogin)).then(function (response) {
//            debugger;
//            if (!response.data.result) {
//                alert(response.data.msg);
//            }
//            else {
//                location.href = '/dashboard/dashboard';
//            }
//        });


//        //var post = $http({
//        //    method: "POST",
//        //    url: "/Login/ValidateLogin",
//        //    dataType: 'application/json',
//        //    data: objLogin,      
//        //    headers: { "Content-Type": "application/json" }
//        //}).then(function (response) {
//        //    if (!response.result)
//        //        alert(response.msg);
//        //});
//    }
//});