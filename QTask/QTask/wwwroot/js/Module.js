$(document).ready(function () {
    $('.btn-slide-add-role-div').click(function (evt) {
        evt.stopPropagation();
        $('.slide-add-role-div').addClass("open");
    });

    $('.btn-popup-add-banner-div').click(function (evt) {
        /* evt.stopPropagation();*/
        $('.popup-add-role-div').slideToggle();
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
    $('#btnSearch').click(function () {
        ShowModuleList();
    })
    $('#btnModuleSave').click(function () {
        Validate();
    });
    $('#lnkDelete').click(function () {
        DeleteModule($(this));
    })
    ShowModuleList();
});


function ShowModuleList() {
    $('#tbl-Category').find('tbody').find('tr').remove();
    var url = apiUrl + 'ModuleAPI/GetModuleList';
    var Keyword = $('#txtKeyword').val();
    $.get(url, { 'keyword': Keyword } ,function (response) {
        if (response != null && response.length > 0) {
            var i = 0;
            for (i = 0; i < response.length; i++) {
                var Tr = document.createElement('tr');

                var tdCheck = document.createElement('td');
                var checkbox = document.createElement('input');
                $(checkbox).attr('type', 'checkbox');
                $(checkbox).attr('module-id', response[i].ModuleId);
                $(tdCheck).append(checkbox);
                $(Tr).append(tdCheck);

                var tdName = document.createElement('td');
                $(tdName).attr('data-th', 'Name');
                $(tdName).text(response[i].ModuleName);
                $(Tr).append(tdName);

                //var tdDescription = document.createElement('td');
                //$(tdDescription).attr('data-th', 'Description');
                //$(tdDescription).text(response[i].RoleDescriptioin);
                //$(Tr).append(tdDescription);

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
                $('.slide-role-setting-div').attr("module-id", $(this).closest('tr').find('input[type=checkbox]').attr('module-id'));
            });
            //$('div.table-design-div').load('/roles/_RolesAccessList', { 'lstRolesModel': response }, function (data) {
            //    $('.btn-slide-role-setting-div').click(function (evt) {
            //        evt.stopPropagation();
            //        $('.slide-role-setting-div').addClass("open");
            //        $('.slide-role-setting-div').attr("role-id", $(this).closest('tr').find('input[type=checkbox]').attr('role-id'));
            //    });
            //})
        }
    });


}

function Validate() {
    var status = 1;
    if (CheckField($('#txtModuleName'), '', 'Please Enter Module Name'))
        status = 0;

    if (status == 1)
        SaveModule();
}

function SaveModule() {
    var objModule = {};
    objModule.ModuleName = $('#txtModuleName').val();


    var url = apiUrl + 'ModuleAPI/SaveModule';
    $.ajax({
        url: url,
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.msg);
                $('.popup-add-role-div, .overlay').slideUp();
                ShowModuleList();
            }
            else {
                alert(response.msg);
            }
        },
        data: JSON.stringify(objModule)
    });
}

function DeleteModule(btn) {
    //var objModule = {};
    //objModule.ModuleId = $(btn).closest('div.slide-role-setting-div').attr('module-id');

    var ModuleId = $(btn).closest('div.slide-role-setting-div').attr('module-id');

    var url = apiUrl + 'ModuleAPI/DeleteModule';
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        /* contentType: 'application/json',*/
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.status) {
                alert(response.msg);
                $('.popup-add-role-div, .overlay').slideUp();
                ShowModuleList();
            }
            else {
                alert(response.msg);
            }
        },
        data: { 'ModuleId': parseInt(ModuleId) }
    });
}
