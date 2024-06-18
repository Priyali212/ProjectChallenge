$(document).ready(function () {
    //GetFAQCategory();


    var urlForresourcesList = apiUrl + "TaskSheetAPI/GetResourcesList";

    $.ajax({
        type: "Get",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: urlForresourcesList,
        data: {},
        success: function (result) {
            if (result != null) {
                $('#txtByResName').children().remove();
                $('#txtByResName').append("<option value=''>Select Name</option>");
                $(result).each(function () {
                    var option = document.createElement('option');
                    option.value = $(this)[0].resourcesid;
                    option.innerText = $(this)[0].resourcesname;

                    $('#txtByResName').append(option);
                });
            }
        }
    });



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
        $('.popup-create-div, .overlay').slideDown();
    });


    $('.add-new-btn').click(function () {
        GetJiraIdRecord(0);
    });
    //$('.add-new-btn').click(function () {

    //    EditFAQ(0);
    //});
    // click popup-approve-btn
    $('.popup-approve-btn').click(function () {
        $('.popup-approve-div, .overlay').slideToggle();
        $('.setting-div').removeClass('open');
    });
    //click popup-approve-close-btn
    $('.popup-approve-div h3 a').click(function () {
        $('.popup-approve-div, .overlay').slideUp();
    });
    // click popup-edit-profile-btn
    $('.popup-edit-profile-btn').click(function () {
        $('.edit').slideUp();
        $('.popup-edit-profile-div, .overlay').slideToggle();
        $('.setting-div').removeClass('open');
    });
    //click popup-edit-profile-close-btn
    $('.popup-edit-profile-div a').click(function () {
        $('.popup-edit-profile-div, .overlay').slideUp();
    });
    //calender
    $('#txtSelectDate').datetimepicker({

        step: 30, format: 'Y-m-d',
        timepicker: false

    });
    $("#txtSelectDate").val($.datepicker.formatDate("yy-mm-dd", new Date()));
    $("#txtSelectDate").val($.datepicker.formatDate("yy-mm-dd", new Date()));


    //faq tab
    $('#lnkAddCat').click(function () {
        if (!$('#liFAQCat').is(':visible')) {
            $('#liFAQCat').show();
            $('#lnkAddCat').text('Hide Category Form');
            $('#ddlCategory').val(0);
        }
        else {
            $('#liFAQCat').hide();
            $('#lnkAddCat').text('Add New Category');
        }
    });

    $('.faq-eng-btn').click(function () {
        $('.faq-hindi-form-div,.faq-tamil-form-div').slideUp();
        $('.faq-eng-form-div').slideDown();
        $(this).addClass('active');
        $('.faq-hindi-btn,.faq-tamil-btn').removeClass('active');
    });
    $('.faq-hindi-btn').click(function () {
        $('.faq-eng-form-div,.faq-tamil-form-div').slideUp();
        $('.faq-hindi-form-div').slideDown();
        $(this).addClass('active');
        $('.faq-eng-btn,.faq-tamil-btn').removeClass('active');
    });
    $('.faq-tamil-btn').click(function () {
        $('.faq-hindi-form-div,.faq-eng-form-div').slideUp();
        $('.faq-tamil-form-div').slideDown();
        $(this).addClass('active');
        $('.faq-eng-btn,.faq-hindi-btn').removeClass('active');
    });
    //add-filter-btn

    $('#btnSearch').click(function () {

        ShowFAQList(0, 20);

    });

    //$('#lnkDelete').click(function () {
    //    DeleteTimesheet($(this));
    //})
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
            //'<button class="red-btn delete" id="lnkDeleted"><i class="fa fa-trash"></i></button>' +
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
    //ShowFAQList('1', '20');

    //onclick setting div open
    $('.bulk-btn').click(function (evt) {
        evt.stopPropagation();
        $('.slide-bulk-div').addClass("open");
    });

    $('.lang-div button').click(function () {

        var ind = $(this).index();
        $('.popup-multi-lang').hide(500);
        $('.popup-multi-lang').eq(ind).show(500);
    });
    ShowTimesheetList($('#txtSelectDate').val());

});







function ClearAllForm() {
    $('.popup-create-div, .overlay').slideUp();
    $('#MediaNewstop').find('input[type=text],textarea').val('');
    $('select').val('0');

}
//check all checkbox
$("#checkAll").change(function () {
    $('.slide-bulk-div').addClass("open");
    $('#tbl-FAQ-body input:checkbox').not(this).prop('checked', this.checked);
    $('#tbl-FAQ-body input:checkbox').closest('tr').toggleClass("highlight", this.checked);
    if ($('#tbl-FAQ-body tr').hasClass('highlight')) {
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



function ValidateTimeSheetDetails() {
    CheckJiraId();
}

function SaveTimeSheet() {
    debugger;
    var obj = {};

    if ($('#btnSave').attr('TimeSheetId') != null && $('#btnSave').attr('TimeSheetId') != undefined && $('#btnSave').attr('TimeSheetId') != "undefined" && $('#btnSave').attr('TimeSheetId').trim() != "")
        obj.JiraId = $('#btnSave').attr('TimeSheetId').trim();

    obj.JiraId = $('#txtJiraId').val();
    obj.WorkedDate = $('#txtSelectDate').val();
    obj.Description = $('#txtDesc').val();
    obj.MinSpend = $('#txtSpend').val();



    var url = apiUrl + 'TimesheetAPI/SaveTimesheet';
    $.ajax({
        url: url,
        type: 'post',
        data: JSON.stringify(obj),
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.msg);
                // $('.popup-create-div, .overlay').slideUp();

                ShowTimesheetList($('#txtSelectDate').val());
                $('#txtJiraId').val("");
                $('#txtDesc').val("");
                $('#txtDesc').val("");
                $('#txtSpend').val("");
            }
            else {
                alert(response.msg);
            }
        },

    });



}


//$('#dvTimesheetList').find('table tfoot').empty();

$('#txtSelectDate').change(function () {
    var WorkedDate = $('#txtSelectDate').val();
    ShowTimesheetList(WorkedDate);
});
function ShowTimesheetList(WorkedDate) {
    var url = apiUrl + 'TimesheetAPI/ShowTimesheetList';
    var TotalHours = 0.00;
    $.get(url, { WorkedDate: WorkedDate }, function (resonse) {
        if (resonse != null) {
            $('#dvTimesheetList').find('table tbody').empty();
            $(resonse).each(function () {
                var tr = document.createElement('tr');
                //var tFootTr = document.createElement('tr');
                var tdCheckBox = document.createElement('td');
                $(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].TimesheetId + '">');
                $(tr).append(tdCheckBox);

                var tdJiraID = document.createElement('td');
                $(tdJiraID).text($(this)[0].JiraId);
                $(tdJiraID).attr('data-th', 'JiraId');
                $(tr).append(tdJiraID);

                var tdTask = document.createElement('td');
                $(tdTask).text($(this)[0].Task);
                $(tdTask).attr('data-th', 'Task');
                $(tr).append(tdTask);

                var tdWorkedDate = document.createElement('td');
                $(tdWorkedDate).text($(this)[0].WorkedDate);
                $(tdWorkedDate).attr('data-th', 'WorkedDate');
                $(tr).append(tdWorkedDate);

                var tdMinSpend = document.createElement('td');
                $(tdMinSpend).text($(this)[0].MinSpend);
                $(tdMinSpend).attr('data-th', 'Time in Min');
                $(tr).append(tdMinSpend);

                var tdDescription = document.createElement('td');
                $(tdDescription).text($(this)[0].Description);
                $(tdDescription).attr('data-th', 'Description');
                $(tr).append(tdDescription);

                var tdAction = document.createElement('td');
                $(tdAction).attr('data-th', 'Action');
                $(tdAction).addClass('td-btn-align')
                $(tdAction).html('<div class="td-flex"> <button  id="lnkDeleted"class="red-btn " onclick="DeleteTimesheet(' + $(this)[0].TimesheetId + ')"><i class="fa fa-trash"></i></button> </div>');
                $(tr).append(tdAction);
                $('#dvTimesheetList').find('table tbody').append(tr);
                TotalHours = $(this)[0].HourSpend;
                $('#dvTimesheetList').find('table tfoot').html('<tr><td colspan="4" class="align-right">Total</td>' +
                    '<td colspan="4"><span>' + TotalHours + '</span></td>' +
                    '</tr>'
                );
            });

            //$('#lnkDeleted').click(function () {
            //    DeleteTimesheet($(this));
            //});


            //$('#dvTimesheetList table tfoot').show();
            //$('#totalHr').text(TotalHours);

            //slide-action-div
            $(".slide-action-btn").click(function (evt) {
                evt.stopPropagation();
                $('.slide-action-div').attr('Notice-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
                $('.slide-action-div').addClass('open');
            });
        }
    });
}



function CheckJiraId1() {

    var obj = {};

    if ($('#btnSave').attr('Timesheet-id') != null && $('#btnSave').attr('Timesheet-id') != undefined && $('#btnSave').attr('Timesheet-id') != "undefined" && $('#btnSave').attr('Timesheet-id').trim() != "")
        obj.Timesheetid = $('#btnSave').attr('Timesheet-id').trim();

    obj.JiraID = $('#txtJiraId').val();


    var url = apiUrl + 'TimesheetAPI/CheckJiraId';
    $.ajax({
        url: url,
        type: 'post',
        data: JSON.stringify(obj.JiraID),
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.msg);
                $('.popup-create-div, .overlay').slideUp();
                ClearAllForm();
                ShowTimesheetList();
            }
            else {
                alert(response.msg);
            }
        },

    });

}
function CheckJiraId() {
    //  obj.JiraID = $('#txtJira').val();
    var JiraId = $('#txtJiraId').val();
    var url = apiUrl + 'TimesheetAPI/CheckJiraId';

    $.get(url, { 'JiraId': JiraId }, function (data) {
        if (data == 'false') {
            //     (CheckField($('#txtJira'), '', 'Please Enter valid jira Id'))
            AddError($('#txtJiraId'), 'Please Enter valid jira Id');
            //  ShowJiraId(data);
        }
        else {
            SaveTimeSheet();
        }
    });

}

$(function () {


    $("#txtJiraId").autocomplete({

        source: function (request, response) {

            var Search = $('#txtJiraId').val();
            var url = apiUrl + 'TimesheetAPI/GetAutoCompleteSearchData';
            $.ajax({
                url: url,
                data: { searchValue: Search },
                //  data:  $('#txtAutoComplete').val(),
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.msg, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    //     alert(response.responseText);
                },
                failure: function (response) {
                    //     alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            0
            $(this).val(i.item.val);
            $("#txtJiraId").submit();
        },
        minLength: 1
    });
});


function DeleteTimesheet(TimesheetId) {

    /*var TimesheetId = $(btn).closest('div.timesheet-table-div').attr('TimesheetId');*/

    var url = apiUrl + 'TimeSheetAPI/DeleteTimesheet';
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
                ShowTimesheetList();
            }
            else {
                alert(response.msg);
            }
        },
        data: { 'TimesheetId': parseInt(TimesheetId) }
    });
}

