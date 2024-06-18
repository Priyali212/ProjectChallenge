var apiUrl = 'http://localhost:5197/api/';

//var apiUrl = 'https://192.168.141.160/api/';

$(document).ready(function () {

})


function CheckField(obj, check, msg) {
    if ($(obj) != undefined && $(obj) != null) {
        ClearError(obj);
        if ($(obj).val() == check) {
            AddError($(obj), msg);
            return true
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function AddError(obj, msg) {
    if ($(obj).closest('div').find('div.err').length>0) {
        $(obj).closest('div').find('div.err').text(msg);
    }
    else {
        var Err = document.createElement('div');
        $(Err).addClass('err');
        $(Err).text(msg);
        $(obj).closest('div').append(Err);
    }
    return;
}

function ClearError(obj) {
    if ($(obj) != undefined && $(obj) != null) {
        $(obj).closest('div').find('div.err').remove();
    }
}

function ShowModal(obj) {
    if ($(obj).length > 0) {
        $('.modal-background').show();
        $(obj).show(300);
        $(obj).find('button.close').click(function () {
            CloseModal(obj);
        });
    }
}

function CloseModal(obj) {
    if ($(obj).length > 0) {
        $('.modal-background').hide();
        $(obj).hide(300);
    }
}

function CloseExplicitModal(obj) {
    if ($(obj).closest('.modal-box').length > 0) {
        $('.modal-background').hide();
        $(obj).closest('.modal-box').hide(300);
    }
}

function AddComma(Num) {
    if (isNaN(Num)) {
        return '';
    }

    Num = Num.toString();
    var afterPoint = '';
    if (Num.indexOf('.') > 0)
        afterPoint = Num.substring(Num.indexOf('.'), Num.length);
    Num = Math.floor(Num);
    Num = Num.toString();
    var lastThree = Num.substring(Num.length - 3);
    var otherNumbers = Num.substring(0, Num.length - 3);
    if (otherNumbers != '')
        lastThree = ',' + lastThree;
    if (afterPoint.length == 2)
        afterPoint += '0'
    var res = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree + afterPoint;
    return res;
}


function CheckNumOnly() {
    $('input.num-only').on('keypress', function (event) {

        if ((isNaN(event.key.trim()) || event.key.trim() == '') && event.key.toLowerCase() != '.' && event.key.toLowerCase() != 'backspace' && event.key.toLowerCase() != 'delete'
            && event.key.toLowerCase() != 'arrowleft' && event.key.toLowerCase() != 'arrowright'
            && event.key.toLowerCase() != 'home' && event.key.toLowerCase() != 'tab' && event.key.toLowerCase() != 'end')
            return false;
    });
}

function CommaSepratedValue() {
    $('input.comma-seprated').on('keyup', function () {
        var amt = $(this).val();

        var semiAmt = AddComma(amt.replace(/,/g, ''));
        $(this).val(semiAmt);
    });
}


function FileUpload(FileData, Path) {
    var httpxml = new XMLHttpRequest();
    httpxml.onreadystatechange = function () {
        if (httpxml.readyState === 4 && httpxml.status === 200) {
            var RetStatus = JSON.parse(httpxml.responseText);
            if (RetStatus.status) {
                FileData[0].setAttribute('vl', RetStatus.OrignalFileName);
                FileData[0].setAttribute('fn', RetStatus.NewFileName);
            }
            else {
                alert(RetStatus.msg);
            }
        }
    }
    httpxml.open("POST", apiUrl + "UploadFilesAPI/FileUpload", true);
   // httpxml.setRequestHeader("Content-Type", "multipart/form-data");
    var formData = new FormData();
    formData.append("file", FileData[0].files[0]);
    formData.append("path", Path);
    httpxml.send(formData);

}