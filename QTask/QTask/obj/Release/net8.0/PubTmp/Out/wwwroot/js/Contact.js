$(document).ready(function () {
    // click filter-btn
    $('.popup-filter-btn').click(function () {
        $('.popup-filter-div, .overlay').slideToggle();
        $('.setting-div').removeClass('open');
    });
    //click filter-close-btn
    $('.filter-div h3 a').click(function () {
        $('.popup-filter-div, .overlay').slideUp();
    });
    // click filter-btn
    $('.add-new-btn').click(function () {
        $('#dvCreateUsers').children().remove();
        GetCreateUserForm(null);
    });

    // click popup-approve-btn
    $('.popup-approve-btn').click(function () {
        $('.popup-approve-div, .overlay').slideToggle();
        $('.setting-div').removeClass('open');
    });
    //click popup-approve-close-btn
    $('.popup-approve-div h3 a').click(function () {
        $('.popup-approve-div, .overlay').slideUp();
    });

    // click popup-myprofile-btn
    $('.popup-myprofile-btn').click(function () {
        $('.view-detail-sec, .overlay').slideToggle();
        $('.setting-div').removeClass('open');
    });
    //click popup-myprofile-close-btn
    $('.view-detail-sec h3 a').click(function () {
        $('.view-detail-sec, .overlay').slideUp();
    });
    // click popup-edit-profile-btn
    $('.popup-edit-profile-btn').click(function () {
        $('.view-detail-sec').slideUp();
        $('.popup-edit-profile-div, .overlay').slideToggle();
        $('.setting-div').removeClass('open');
    });
    //click popup-edit-profile-close-btn
    $('.popup-edit-profile-div a').click(function () {
        $('.popup-edit-profile-div, .overlay').slideUp();
    });
    $('#btnCancel').click(function () {
        ClearAllForm();
    }); 

    //add-filter-btn
    $('.add-filter-btn').click(function () {
        $(".filter-form-div").append('<div class="filter-form gap-1 grid grid-col-4 tab-grid-col-2 mob-grid-col-1 pt-1 pbt-1">' +
            '<div class="f-input-div filter-col">' +
            '<label>Field Name</label>' +
            '<select class="f-input">' +
            '<option></option>' +
            '</select>' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Operator</label>' +
            '<select class="f-input">' +
            '<option></option>' +
            '</select>' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Min Value</label>' +
            '<input type="text" class="f-input">' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Max Value</label>' +
            '<div class="div-flex gap-1">' +
            '<input type="text" class="f-input">' +
            '<button class="red-btn delete"><i class="fa fa-trash"></i></button>' +
            '</div>' +
            '</div>' +
            '</div>');
        $('.filter-form .delete').on("click", function (e) {
            $(this).closest('.filter-form').remove();
        });
    });
    //
    //add-filter-btn
    $('.add-filter-order-btn').click(function () {
        $(".add-filter-order-div").append('<div class="filter-order-div gap-1 grid grid-col-2 mob-grid-col-1 pbt-1 pt-05">' +
            // '<div class="f-input-div filter-col">'+
            // '<label>Name</label>'+
            // '<input type="text" class="f-input">'+
            // '</div>'+
            '<div class="f-input-div filter-col">' +
            '<label>Order by column</label>' +
            '<select class="f-input">' +
            '<option>Select</option>' +
            '<option>1</option>' +
            '<option>2</option>' +
            '<option>3</option>' +
            '<option>4</option>' +
            '</select>' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Direction</label>' +
            '<div class="div-flex gap-1">' +
            '<select class="f-input">' +
            '<option>Select</option>' +
            '<option>Ascending</option>' +
            '<option>Descending</option>' +
            '</select>' +
            '<button class="red-btn delete">' +
            '<i class="fa fa-trash"></i>' +
            '</button>' +
            '</div>' +
            '</div>' +
            '</div>');
        $('.filter-order-div .delete').on("click", function (e) {
            $(this).closest('.filter-order-div').remove();
        });
    });
    $('#lnkbulkDelete').click(function () {
        if ($('#table-one').find('tr > td > input[type=checkbox]:checked').length > 0) {

            var objDelete = {};

            var UserList = [];
            $('#table-one').find('tr > td > input[type=checkbox]:checked').each(function () {
                //objDelete.lstUserList
                var usr = {};
                usr.Id = $(this).val().trim();
                UserList.push(usr);
            });
            objDelete.lstUserList = UserList;
            var url = apiUrl + 'contactapi/DeleteUser';
            $.ajax({
                url: url,
                method: 'POST',
                datatype: 'json',
                contentType: 'application/json',
                success: function (response) {
                    if (response.status) {
                        alert(response.Msg);
                        ShowUserList('1', '20');
                    }
                    else {
                        alert(response.Msg);
                    }

                },
                data: JSON.stringify(objDelete)
            });
            //$.post(url, { objDelete: objDelete }, function (data) {
            //    if (data.status) {
            //        alert(data.Msg);
            //        ShowUserList('1', '20');
            //    }
            //    else {
            //        alert(data.Msg);
            //    }
            //});
        }
        else {
            alert('atleast select one record');
        }
    });

    $('#lnkDelete').click(function () {

        var Id = $(this).closest('.slide-action-div').attr('user-id').trim();

        var url = apiUrl + 'contactapi/DeleteUser';
        $.ajax({
            url: url,
            method: 'GET',
            datatype: 'json',
            contentType: 'application/json',
            success: function (response) {
                if (response.status) {
                    alert(response.Msg);
                    ShowUserList('1', '20');
                }
                else {
                    alert(response.Msg);
                }

            },
            data: { 'Id': Id }
        });


        //$.post(url, { objDelete: objDelete }, function (data) {
        //    if (data.status) {
        //        alert(data.Msg);
        //        ShowUserList('1', '20');
        //    }
        //    else {
        //        alert(data.Msg);
        //    }
        //});


    });

    $('#btnSearch').click(function () {
        ShowUserList(1, 20);
    })

    ShowUserList(1, 20);
    //bindTableEvents();
    bindCreateUserEvents();
    bindTableEvents();
});

function bindTableEvents() {
    $('button.next').click(function () {
        var PageId = parseInt($(this).closest('div').find('span.pg-no').attr('page-id').trim());
        PageId = PageId + 1;
        var PageSize = 20;

        ShowUserList(PageId, PageSize);
    })

    $('button.prev').click(function () {
        var PageId = parseInt($(this).closest('div').find('span.pg-no').attr('page-id').trim());
        PageId = PageId - 1;
        var PageSize = 20;

        ShowUserList(PageId, PageSize);
    })

    $('button.first').click(function () {
        PageId = 1;
        var PageSize = 20;

        ShowUserList(PageId, PageSize);
    })

    $('button.last').click(function () {
        var Total = parseInt($(this).closest('div').find('span.pg-total').text().trim());
        var PageId = parseInt(Total / 20);
        if ((Total % 20) > 0) {
            PageId = PageId + 1;
        }
        var PageSize = 20;

        ShowUserList(PageId, PageSize);
    })

    //check all checkbox
    $("#checkAll").change(function () {
        $('.slide-bulk-div').addClass("open");
        $('#table-1 input:checkbox').not(this).prop('checked', this.checked);
        $('#table-1 input:checkbox').closest('tr').toggleClass("highlight", this.checked);
        if ($('#table-1 tr').hasClass('highlight')) {
            $('#delete').prop("disabled", false);
        }
        else {
            $('#delete').prop("disabled", true);
        }
        if ($(this).is(":checked")) {
            $(".slide-bulk-div").addClass('open');
        } else {
            $(".slide-bulk-div").removeClass('open');
        }
    });

    //onclick setting div open
    $('.bulk-btn').click(function (evt) {
        evt.stopPropagation();
        $('.slide-bulk-div').addClass("open");
    });

}

function bindCreateUserEvents() {
    $('.add-email-btn').click(function () {
        $(".add-email-input-div").append('<div class="add-email-div">' +
            '<div class="add-new-form mbt-1 gap-1 grid grid-col-3 tab-grid-col-2 mob-grid-col-1 pt-05">' +
            '<div class="f-input-div filter-col">' +
            '<label>Type Of Email</label>' +
            '<select class="f-input" name="ddnEmailType">' +
            '<option value="0">Select</option>' +
            '<option value="personal">Personal</option>' +
            '<option value="official">Official</option>' +
            '<option value="other">Other</option>' +
            '</select>' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Email</label>' +
            '<input type="text" class="f-input" name="txtEmail">' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Opt In</label>' +
            '<div class="div-flex gap-1">' +
            '<select class="f-input" name="ddnOptIn">' +
            '<option value="0">Select</option>' +
            '<option value="1">Yes</option>' +
            '<option value="2">No</option>' +
            '</select>' +
            '<button class="red-btn email-delete-btn"><i class="fa fa-trash"></i></button>' +

            '</div>' +
            '</div>' +
            '</div>' +

            '</div>');
        $('.email-delete-btn').on("click", function (e) {
            $(this).closest('.add-email-div').remove();
        });
    });
    $('.email-delete-btn').on("click", function (e) {
        $(this).closest('.add-email-div').remove();
    });

    //add address 
    $('.add-add-btn').click(function () {
        $(".add-add-input-div").append('<div class="add-div pbt-1">' +
            '<div class="add-new-form gap-1 grid grid-col-3 tab-grid-col-2 mob-grid-col-1 pt-1 pbt-1">' +
            '<div class="f-input-div filter-col">' +
            '<label>Address Street</label>' +
            '<input type="text" class="f-input" name="ddnAddress">' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Address Postal Code</label>' +
            '<input type="text" class="f-input num-only" name="txtPostal">' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Address City</label>' +
            '<input type="text" class="f-input" name="txtCity">' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Address State</label>' +
            '<input type="text" class="f-input" name="txtState">' +
            '</div>' +
            '<div class="f-input-div filter-col">' +
            '<label>Address Country</label>' +
            '<input type="text" class="f-input" name="txtCountry">' +
            '</div>' +
            '<div class="f-input-div filter-col align-bt">' +
            '<button class="red-btn delete"><i class="fa fa-trash"></i></button>' +
            '</div>' +
            '</div>' +
            // '<div class="align-right">'+
            // '<button class="red-btn delete"><i class="fa fa-trash"></i></button>'+
            // '</div>'+
            '</div>');
        $('.add-add-input-div button.delete').on("click", function (e) {
            $(this).closest('.add-div').remove();
            // alert('hi');
        });
    });
    $('.add-add-input-div button.delete').on("click", function (e) {
        $(this).closest('.add-div').remove();
        // alert('hi');
    });
}

function ShowUserList(PageIndex, PageSize) {
    //$.get('/contact/_UserList', { PageIndex: PageIndex, PageSize: PageSize }, function (resonse) {
    //    if (resonse != null && resonse.trim() != '') {
    //        $('#dvUserList').children().remove();
    //        $('#dvUserList').append(resonse);
    //        bindTableEvents();
    //    }
    //});
    var name = $('#txtKeyword').val();

    var url = apiUrl + 'ContactAPI/GetUser';
    $.get(url, { Name: name, PageIndex: PageIndex, PageSize: PageSize }, function (resonse) {
        if (resonse != null) {
            $('#dvUserList').find('table tbody').empty();
            $(resonse.contactLists).each(function () {
                var tr = document.createElement('tr');
                var tdCheckBox = document.createElement('td');
                $(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].ID + '">');
                $(tr).append(tdCheckBox);

                var tdName = document.createElement('td');
                $(tdName).text($(this)[0].UserFullName);
                $(tdName).attr('data-th', 'Name');
                $(tr).append(tdName);

                var tdDescription = document.createElement('td');
                $(tdDescription).text($(this)[0].UserName);
                $(tdDescription).attr('data-th', 'UserId');
                $(tr).append(tdDescription);

                var tdTitle = document.createElement('td');
                $(tdTitle).text($(this)[0].GroupName);
                $(tdTitle).attr('data-th', 'GroupName');
                $(tr).append(tdTitle);

                var tdDepartment = document.createElement('td');
                $(tdDepartment).text($(this)[0].IsAdmin);
                $(tdDepartment).attr('data-th', 'IsAdmin');
                $(tr).append(tdDepartment);

                var tdAction = document.createElement('td');
                $(tdAction).attr('data-th', 'Action');
                $(tdAction).addClass('td-btn-align')
                $(tdAction).html('<button class="blue-btn slide-action-btn"><i class="fa fa-cog"></i></button>');
                $(tr).append(tdAction);
                $('#dvUserList').find('table tbody').append(tr);
            })
            $('#dvUserList').find('span.pg-no').attr('page-id', resonse.PageIndex.toString()).text((((parseInt(resonse.PageIndex.toString()) - 1) * 20) + 1).toString());
            $('#dvUserList').find('span.pg-current-total').text((parseInt(resonse.PageIndex) * 20) > parseInt(resonse.TotalCount) ? resonse.TotalCount.toString() : (parseInt(PageIndex) * 20).toString());
            $('#dvUserList').find('span.pg-total').text(resonse.TotalCount.toString());

            if (parseInt(resonse.PageIndex) < 2) {
                $('#dvUserList').find('button.prev').attr('disabled', 'disabled');
                $('#dvUserList').find('button.first').attr('disabled', 'disabled');
            }
            else {
                $('#dvUserList').find('button.prev').removeAttr('disabled');
                $('#dvUserList').find('button.first').removeAttr('disabled');
            }
            //if (((Convert.ToInt32(Model.TotalCount.ToString()) % 20) == 0 && (Convert.ToInt32(Model.TotalCount.ToString()) / 20 > Model.PageIndex)) || ((Convert.ToInt32(Model.TotalCount.ToString()) % 20) != 0 && (Convert.ToInt32(Model.TotalCount.ToString()) / 20 >= Model.PageIndex)))
            if (((parseInt(resonse.TotalCount) % 20) == 0 && (parseFloat(resonse.TotalCount) / 20 > parseFloat(resonse.PageIndex))) || ((parseInt(resonse.TotalCount) % 20) != 0 && (parseFloat(resonse.TotalCount) / 20 >= parseFloat(resonse.PageIndex)))) {
                $('#dvUserList').find('button.next').removeAttr('disabled');
                $('#dvUserList').find('button.last').removeAttr('disabled');
            }
            else {
                $('#dvUserList').find('button.next').attr('disabled', 'disabled');
                $('#dvUserList').find('button.last').attr('disabled', 'disabled');
            }

            //slide-action-div
            $(".slide-action-btn").click(function (evt) {
                evt.stopPropagation();
                $('.slide-action-div').attr('user-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
                $('.slide-action-div').addClass('open');
            });
            //$('#dvUserList').children().remove();
            //$('#dvUserList').append(resonse);
            //bindTableEvents();
        }
    });
}

function ValidateUserCreation(type) {
    var status = 1;


    if (CheckField($('#txtUserName'), '', 'Please Enter user Name'))
        status = 0;

    if (CheckField($('#txtUserFullName'), '', 'Please Enter Full Name'))
        status = 0;


    if (CheckField($('#ddnGroupName'), '0', 'Please Assign Group Name'))
        status = 0;

    if (status == 1) {
        if (type == 'update')
            UpdateUser();
        else
            if (type == 'create')
                SaveCreateUser();
            else
                alert('Not able to identify the Action');
    }
}

function SaveCreateUser() {
    var objCreateUser = {};

    objCreateUser.ID = '0';
    objCreateUser.UserName = $('#txtUserName').val();
    objCreateUser.UserFullName = $('#txtUserFullName').val();
    objCreateUser.GroupId = $('#ddnGroupName').val();

    //Emails details start



    //Emails Details End

    //Address Details start


    var url = apiUrl + 'contactapi/CreateUsers';
    $.ajax({
        url: url,
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.Msg);
                $('.popup-create-div, .overlay').slideUp();
                ShowUserList('1', '20');
            }
            else {
                alert(response.Msg);
            }
        },
        data: JSON.stringify(objCreateUser)
    });

}

function GetSalesManagerList(selVal) {
    var url = apiUrl + 'RolesManagementAPI/GetRoleWiseUserList';
    $.get(url, { 'RoleName': 'Sales Manager' }, function (resonse) {
        if (resonse != null) {
            $(resonse).each(function () {
                $('#ddnReportTo').append('<option value="' + $(this)[0].Userid + '">' + $(this)[0].FirstName + ' ' + $(this)[0].LastName + '</option>')
            })
            $('#ddnReportTo').val(selVal);
        }
    })
}


function GetGroupList(selVal) {
    var url = apiUrl + 'RolesManagementAPI';
    $.get(url, function (resonse) {
        if (resonse != null) {
            $(resonse).each(function () {
                $('#ddnGroupName').append('<option value="' + $(this)[0].RolesId + '">' + $(this)[0].RoleName + '</option>');
            })
            $('#ddnGroupName').val(selVal);
        }
    })
}

function GetCreateUserForm(obj) {
    //$('#dvCreateUsers').append($('.add-new-div popup-create-div').clone());
    var GroupId = '0';
    if (obj != null) {
        GroupId = obj.GroupId.toString();

        $('#txtUserName').val(obj.UserName);
        $('#txtUserFullName').val(obj.UserFullName);
        // GroupId = obj.GroupId;
        // $('#ddnGroupName').val(obj.GroupId);
        $('#btnUpdate').attr('user-id', obj.ID);
        $('#btnUpdate').show();
        $('#btnSave').hide();
    }
    else {
        $('#btnUpdate').hide();
        $('#btnSave').show();
    }
    // GetSalesManagerList(SaleManager);
    GetGroupList(GroupId);
    $('.popup-create-div, .overlay').slideToggle();
    $('.setting-div').removeClass('open');
    //click filter-close-btn
    $('.add-new-div h3 a').click(function () {
        $('.add-add-input-div').children().remove();
        $('.add-email-input-div').children().remove();
        $('.popup-create-div, .overlay').slideUp();
    });
}

function EditUserDetails(obj) {

    var Id = $(obj).closest('div.slide-action-div').attr('user-id');
    var url = apiUrl + 'ContactAPI/GetUserDetails';
    $.get(url, { 'Id': Id }, function (data) {
        if (data != null) {
            GetCreateUserForm(data);
        }


        //$('.popup-create-div, .overlay').slideToggle();
        //$('.setting-div').removeClass('open');
        ////click filter-close-btn
        //$('.add-new-div h3 a').click(function () {
        //    $('#dvCreateUsers').children().remove();
        //    $('.popup-create-div, .overlay').slideUp();
        //});

        //bindCreateUserEvents();
    });

}
function ClearAllForm() {
    $('.popup-create-div, .overlay').slideUp();
    $('#Canceltop').find('input[type=text],textarea').val('');
    $('select').val('0');

}
function UpdateUser() {
    var objCreateUser = {};

    objCreateUser.ID = $('#btnUpdate').attr('user-id').trim();
    objCreateUser.UserName = $('#txtUserName').val();
    objCreateUser.UserFullName = $('#txtUserFullName').val();
    objCreateUser.GroupId = $('#ddnGroupName').val();



    var url = apiUrl + 'contactapi/UpdateUsers';
    $.ajax({
        url: url,
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.Msg);
                $('.popup-create-div, .overlay').slideUp();
                ShowUserList('1', '20');
            }
            else {
                alert(response.Msg);
            }
        },
        data: JSON.stringify(objCreateUser)
    });

}