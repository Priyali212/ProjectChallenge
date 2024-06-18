$(document).ready(function () {
    //onclick btn-slide-add-role-div open
    $('.btn-slide-add-role-div').click(function (evt) {
        evt.stopPropagation();
        $('.slide-add-role-div').addClass("open");
    });

    $('.btn-popup-add-banner-div').click(function (evt) {
        /* evt.stopPropagation();*/
        $('.popup-add-role-div').slideToggle();
    });

    $('#btnRoleSave').click(function () {
        ValidateRoleForm();
    })

    $('#btnCancel').click(function () {
        ClearAllForm();
    });

    $('.btn-back-setting').click(function () {
        $('.slide-add-role-div,.overlay').removeClass("open");
    });
    $("body").click(function (evt) {
        if (!$(evt.target).is('.slide-add-role-div')) {
            $('.slide-add-role-div,.overlay').removeClass("open");
        }

        if (!$(evt.target).is('.slide-role-setting-div')) {
            $('.slide-role-setting-div,.overlay').removeClass("open");
        }
    });
    $('.btn-back-setting').click(function () {
        $('.slide-role-setting-div,.overlay').removeClass("open");
    });
    $('.bulk-btn').click(function (evt) {
        evt.stopPropagation();
        $('.slide-bulk-div').addClass("open");
    });
    //$("body").click(function (evt) {
    //    if (!$(evt.target).is('.slide-role-setting-div')) {
    //        $('.slide-role-setting-div,.overlay').removeClass("open");
    //    }
    //});
    // click btn-popup-add-role-div
    $('.btn-popup-add-role-div').click(function () {
        $('.popup-add-role-div, .overlay').slideToggle();
        //$('.setting-div').removeClass('open');
    });
    //click btn-popup-add-role-div close
    $('.popup-add-role-div h3 a').click(function () {
        $('.popup-add-role-div, .overlay').slideUp();
    });
    // click btn-popup-edit-role-div
    $('.btn-popup-edit-role-div').click(function () {
        $('.popup-edit-role-div, .overlay').slideToggle();
        //$('.setting-div').removeClass('open');
    });
    //click btn-popup-edit-role-div close
    $('.popup-edit-role-div h3 a').click(function () {
        $('.popup-edit-role-div, .overlay').slideUp();
    });
    // click btn-popup-role-access-div
    $('.btn-popup-role-access-div').click(function () {

        //$('.popup-role-access-div').html('');
        var url = apiUrl + 'RolesManagementAPI';
        $.get(url + '/GetRoleWiseAccessMetrixList', { RoleId: $(this).closest('.slide-role-setting-div').attr('role-id').trim() }, function (data) {
            if (data != null) {
                $('#Div-Role-Table').attr('role-id', data.RoleId.toString().trim());

                BuildRoleWiseAccessTable(data);

                $('.padder').slideUp();
                $('.popup-role-access-div, .overlay').slideToggle();
               
                $('.setting-div').removeClass('open');

            }
            //$('.popup-role-access-div').html(data);



            $('#btnSave').click(function () {

                var objRoleWiseAccess = {};
                objRoleWiseAccess.RoleId = $('#Div-Role-Table').attr('role-id').trim();
                var lstRoleWiseAccess = [];
                $('#table-access').find('tbody tr').each(function () {

                    var AccessCount = 0;
                    var EditFound = 0;
                    $(this).find('td').each(function () {
                        if ($(this).attr('act') != undefined && $(this).attr('act') != null && $(this).attr('act').trim() == 'edit') {
                            var RoleWiseAccessData = {};
                            RoleWiseAccessData.RoleId = objRoleWiseAccess.RoleId;
                            RoleWiseAccessData.ActionId = $(this).find('select').val();
                            RoleWiseAccessData.AccessId = $('#table-access').find('thead').find('th').eq(AccessCount).attr('accesstype-id').trim();
                            RoleWiseAccessData.PageId = $(this).closest('tr').find('td').eq(0).attr('page-id').trim();
                            lstRoleWiseAccess.push(RoleWiseAccessData);
                            EditFound++;
                        }

                        AccessCount++;
                    });



                });
                objRoleWiseAccess.lstRoleWiseAccess = lstRoleWiseAccess;
                var url = apiUrl + 'RolesManagementAPI/SaveAccessesForRole'
                
                $.ajax({
                    url: url,
                    type: 'post',
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (response) {
                        if (response.status) {
                            alert('Access update successfully');
                            $('.popup-role-access-div, .overlay').slideUp();
                            $('.padder').show('2000');
                            $('#table-access').find('thead').children().remove();
                        }
                        else {
                            alert(response.msg);
                        }
                    },
                    data: JSON.stringify(objRoleWiseAccess)
                });
            });

        });
    });

    $('#lnkDelete').click(function () {
        DeleteRole($(this));
    })
    ShowRolesList();
});

function BuildRoleWiseAccessTable(data) {
    $('#table-access').find('thead').children().remove();
    //creating Table Head Start
    var i = 0;
    var trHead = document.createElement('tr');
    $(trHead).append('<th>Page Name</th>');

    for (i = 0; i < data.lstAccessType.length; i++) {
        var th = document.createElement('th');

        $(th).attr('accesstype-id', data.lstAccessType[i].Id);
        $(th).text(data.lstAccessType[i].AccessTypeName);
        $(trHead).append($(th));
    }
    $('#table-access').find('thead').append($(trHead));
    //creating Table Head End

    //Creating Table Body Start
    for (i = 0; i < data.lstCategory.length; i++) {
        var bodyTr = document.createElement('tr');
        var PageNameTd = document.createElement('td');
        $(PageNameTd).text(data.lstCategory[i].CategoryName);
        $(PageNameTd).attr('page-id', data.lstCategory[i].Id);

        $(bodyTr).append(PageNameTd);

        var j = 0;
        for (j = 0; j < data.lstAccessType.length; j++) {
            var TbodyTD = document.createElement('td');

            //Creating Access Type Dropdown start
            var k = 0;
            var selectAccess = document.createElement('select');
            $(selectAccess).addClass('f-input');
            $(selectAccess).attr('disabled', 'disabled');

            var CurrentSelection = 0;
            var index = -1;
            if (data.lstRoleWiseAccessData != null && data.lstRoleWiseAccessData.length > 0) {
                //index = data.lstRoleWiseAccessData.findIndex(x => x.PageId === data.lstCategory[i].Id && x.AccessId === data.lstAccessType[j].Id);

                //index = data.lstRoleWiseAccessData[0].findIndex(function (item, i) {
                //    return (item.PageId === data.lstCategory[i].Id && item.PaAccessIdgeId === data.lstAccessType[j].Id)
                //});
                var ind = 0;
                for (ind = 0; ind < data.lstRoleWiseAccessData.length; ind++) {
                    if (data.lstRoleWiseAccessData[ind].PageId == data.lstCategory[i].Id && data.lstRoleWiseAccessData[ind].AccessId == data.lstAccessType[j].Id) {
                        index = ind;
                        break;
                    }
                }

            }

            if (index > -1) {
                CurrentSelection = data.lstRoleWiseAccessData[index].ActionId;
            }

            for (k = 0; k < data.lstRoleAccess.length; k++) {

                var options = document.createElement('option');
                $(options).attr('value', data.lstRoleAccess[k].AccessId);
                $(options).text(data.lstRoleAccess[k].AccessName);

                if (CurrentSelection == data.lstRoleAccess[k].AccessId)
                    $(options).attr('selected', 'selected');

                $(selectAccess).append(options);
            }
            $(TbodyTD).attr('access-id', CurrentSelection.toString());
            $(TbodyTD).append(selectAccess);
            //Creating Access Type Dropdown end

            $(bodyTr).append(TbodyTD);
        }
        $('#table-access').find('tbody').append($(bodyTr));
    }

    //Creating Table Body End

    $('#table-access').find('tbody tr td select').on('change', function () {
        if ($(this).closest('td').attr('access-id').trim() != $(this).val()) {
            $(this).closest('td').attr('act', 'edit');
        }
        else {
            $(this).closest('td').removeAttr('act');
        }
    });

    // table-access enabled and disabled
    $('#table-access tbody select').attr("disabled", true);
    $('.btn-edit-access-select-dropdown').click(function () {
        $('#table-access tbody select').attr("disabled", false);
    });

    //click btn-popup-role-access-div close
    $('.popup-role-access-div h3 a').click(function () {
        $('.popup-role-access-div, .overlay').slideUp();
        $('#table-access').find('tbody').children().remove();
        $('.padder').show('2000');
    });
}

function ShowRolesList() {
    var url = apiUrl + 'RolesManagementAPI';
    $.get(url, function (response) {
        if (response != null && response.length > 0) {
            var i = 0;
            for (i = 0; i < response.length; i++) {
                var Tr = document.createElement('tr');

                var tdCheck = document.createElement('td');
                var checkbox = document.createElement('input');
                $(checkbox).attr('type', 'checkbox');
                $(checkbox).attr('role-id', response[i].RolesId);
                $(tdCheck).append(checkbox);
                $(Tr).append(tdCheck);

                var tdName = document.createElement('td');
                $(tdName).attr('data-th', 'Name');
                $(tdName).text(response[i].RoleName);
                $(Tr).append(tdName);

                var tdDescription = document.createElement('td');
                $(tdDescription).attr('data-th', 'Description');
                $(tdDescription).text(response[i].RoleDescriptioin);
                $(Tr).append(tdDescription);

                var tdButton = document.createElement('td');
                $(tdButton).attr('data-th', 'Actions');
                $(tdButton).addClass('td-btn-align');
                var ActButton = document.createElement('button');
                $(ActButton).addClass('button blue-btn btn-slide-role-setting-div');
                $(ActButton).html('<i class="fa fa-cog"></i>');
                $(tdButton).append(ActButton);

                $(Tr).append(tdButton);

                $('#tbl-Category').find('tbody').append(Tr);

            }
            $('.btn-slide-role-setting-div').click(function (evt) {
                evt.stopPropagation();
                $('.slide-role-setting-div').addClass("open");
                $('.slide-role-setting-div').attr("role-id", $(this).closest('tr').find('input[type=checkbox]').attr('role-id'));
            });
            
        }
    });


}

function ValidateRoleForm() {
    var status = 1;

    if (CheckField($('#txtRoleName'), '', 'Please Enter Role Name'))
        status = 0;

    if (status == 1) {
        SaveRole();
    }
}

function SaveRole() {
    var ObjRole = {};
    ObjRole.RoleName = $('#txtRoleName').val();
    ObjRole.Description = $('#txtRoleDescription').val();


    var url = apiUrl + 'RolesManagementAPI/SaveRole';
    $.ajax({
        url: url,
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.msg);
                $('.popup-add-role-div, .overlay').slideUp();
                ClearAllForm();
                ShowRolesList();
            }
            else {
                alert(response.msg);
            }
        },
        data: JSON.stringify(ObjRole)
    });
}

function ClearAllForm() {
    $('.main-content-div, .overlay').slideUp();
    $('#Roletop').find('input[type=text],textarea').val('');
    $('select').val('0');
    $('#table-access').find('thead').children().remove();

}

function DeleteRole(btn) {

    var RoleId = $(btn).closest('div.slide-role-setting-div').attr('role-id');

    var url = apiUrl + 'RolesManagementAPI/DeleteRole';
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.msg);
                $('.popup-add-role-div, .overlay').slideUp();
                ClearAllForm();
                ShowRolesList();
            }
            else {
                alert(response.msg);
            }
        },
        data: { RoleId: RoleId }
    });
}