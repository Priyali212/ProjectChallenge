$(document).ready(function () {


    var urlForresourcesList = apiUrl + "TaskSheetAPI/GetResourcesList";

    $.ajax({
        type: "Get",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: urlForresourcesList,
        data: {},
        success: function (result) {
            if (result != null) {
                $('#txtByName').children().remove();
                $('#txtByName').append("<option value=''>Select Name</option>");
                $(result).each(function () {
                    var option = document.createElement('option');
                    option.value = $(this)[0].resourcesid;
                    option.innerText = $(this)[0].resourcesname;

                    $('#txtByName').append(option);
                });
                ShowTasksheetList('1');

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
        TaskSheetDetails(null);
    });

    $('.add-new-btn').click(function () {
        EditHistory(0);
    });
    $('.add-new-btn').click(function () {
        GetJiraIdRecord(0);
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
        $('.edit').slideUp();
        $('.popup-edit-profile-div, .overlay').slideToggle();
        $('.setting-div').removeClass('open');
    });
    //click popup-edit-profile-close-btn
    $('.popup-edit-profile-div a').click(function () {
        $('.popup-edit-profile-div, .overlay').slideUp();
    });
    //click popup-cancle-profile-close-btn
    $('.other-div button blue-btn i').click(function () {
        $('.popup-create-div, .overlay').slideUp();
    });





    //calender

    $('#txtReqFormdt').datetimepicker({

        step: 30, format: 'Y-m-d',
        changeMonth: true,
        changeYear: true,

    });
    $("#txtReqFormdt").val($.datepicker.formatDate("yy-mm-dd", new Date()));
    $("#txtReqFormdt").val($.datepicker.formatDate("yy-mm-dd", new Date()));


    $('#txtEstPlanDt').datetimepicker({

        step: 30, format: 'Y-m-d',
        timepicker: false

    });
    $("#txtEstPlanDt").val($.datepicker.formatDate("yy-mm-dd", new Date()));
    $("#txtEstPlanDt").val($.datepicker.formatDate("yy-mm-dd", new Date()));


    $('#txtEstCompDt').datetimepicker({

        step: 30, format: 'Y-m-d',
        timepicker: false

    });
    $("#txtEstCompDt").val($.datepicker.formatDate("yy-mm-dd", new Date()));
    $("#txtEstCompDt").val($.datepicker.formatDate("yy-mm-dd", new Date()));


    //$('#txtActstartDt').datetimepicker({

    //    step: 30, format: 'Y-m-d',
    //    timepicker: false

    //});
    //$("#txtActstartDt").val($.datepicker.formatDate("dd-mm-yy", new Date()));
    //$("#txtActstartDt").val($.datepicker.formatDate("dd-mm-yy", new Date()));


    //$('#txtActendDt').datetimepicker({

    //    step: 30, format: 'Y-m-d',
    //    timepicker: false

    //});
    //$("#txtActendDt").val($.datepicker.formatDate("dd-mm-yy", new Date()));
    //$("#txtActendDt").val($.datepicker.formatDate("dd-mm-yy", new Date()));



    //$('#txtReqFormdt').datetimepicker({
    //    step: 30, format: 'Y-m-d'
    //});
    //$('#txtEstPlanDt').datetimepicker({
    //    step: 30, format: 'Y-m-d'
    //});
    //$('#txtEstCompDt').datetimepicker({
    //    step: 30, format: 'Y-m-d'
    //});
    $('#txtActstartDt').datetimepicker({
        step: 30, format: 'Y-m-d'
    });
    $('#txtActendDt').datetimepicker({
        step: 30, format: 'Y-m-d'
    });
    //$('#txtDateflt').datetimepicker({
    //    step: 30, format: 'Y-m-d'
    //});
    //$('#txtAddedDate').datetimepicker({
    //    step: 30, format: 'y-m-d'
    //});
    //$('#txtModOn').datetimepicker({
    //    step: 30, format: 'y-m-d'
    //});

    $('#btnSearch').click(function () {

        ShowTasksheetList(1);

    });



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

});
$(function () {
    $("#ddlPageList").on("change", function () {
        if (!isNaN($(this).val())) {
            ShowTasksheetList($(this).val());
        }
    });
});

function GetPageList(PageIndex) {

    var TotalPageCount = $("#hdnTotalPageCount").val();

    var selectval = $('#ddlPageList').val();
    if ($("#hdnTotalPageCount").val() > 0) {
        var TotalPageCount = $("#hdnTotalPageCount").val();
        $("#ddlPageList").empty();

        for (let i = 1; i <= TotalPageCount; i++) {
            if (selectval == i) {
                $("#ddlPageList").append('<option value="' + i + '" selected>' + i + '</option>');
            }
            else {
                $("#ddlPageList").append('<option value="' + i + '">' + i + '</option>');
            }
            //$("#ddlPageList").append('<option value="' + i + '">' + i + '</option>');
            /*$("#ddlPageList").append('<li value="' + i + '">' + i + '</li>');*/
        }

        if ($("#hdnCurrentPageIndex").val() > 0) {
            $("#ddlPageList").val($("#hdnCurrentPageIndex").val());
        }
    }
}

function ShowJiraId(obj) {


    if (obj != null) {
        $('#dvJiraList').find('table tbody').empty();
        $(obj).each(function () {
            var tr = document.createElement('tr');

            var tdCheckBox = document.createElement('td');
            $('#btnJiraid').attr('Jira-id', obj.timesheetid);
            //var tdCheckBox = document.createElement('td');
            //$(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].TasksheetId + '">');
            $(tr).append(tdCheckBox);

            var tdTasksheetId = document.createElement('td');
            $(tdTasksheetId).text($(this)[0].JiraId);
            $(tdTasksheetId).attr('data-th', 'JiraId');
            $(tr).append(tdTasksheetId);

            var tdWorkedDate = document.createElement('td');
            $(tdWorkedDate).text($(this)[0].WorkedDate);
            $(tdWorkedDate).attr('data-th', 'WorkedDate');
            $(tr).append(tdWorkedDate);

            var tdDescription = document.createElement('td');
            $(tdDescription).text($(this)[0].Description);
            $(tdDescription).attr('data-th', 'Description');
            $(tr).append(tdDescription);

            var tdminutesSpent = document.createElement('td');
            $(tdminutesSpent).text($(this)[0].minutesSpent);
            $(tdminutesSpent).attr('data-th', 'minutesSpent');
            $(tr).append(tdminutesSpent);

            //var tdUserId = document.createElement('td');
            //$(tdUserId).text($(this)[0].UserId);
            //$(tdUserId).attr('data-th', 'UserId');
            //$(tr).append(tdUserId);

            var tdAddeddate = document.createElement('td');
            $(tdAddeddate).text($(this)[0].Addeddate);
            $(tdAddeddate).attr('data-th', 'Addeddate');
            $(tr).append(tdAddeddate);

            var tdmodifiedby = document.createElement('td');
            $(tdmodifiedby).text($(this)[0].modifiedby);
            $(tdmodifiedby).attr('data-th', 'modifiedby');
            $(tr).append(tdmodifiedby);


            $('#dvJiraList').find('table tbody').append(tr);
        })
        //slide-action-div

        $(".slide-action-btn").click(function (evt) {
            evt.stopPropagation();
            $('.slide-action-div').attr('Jira-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
            $('.slide-action-div').addClass('open');
        });
    }

    $('#task-management-sec').slideUp();
    /*$('#header').hide(200);*/
    $('#timesheet-detail-sec').slideDown();

    $('.time-detail-back-btn').click(function () {
        $('#task-management-sec').slideDown();
        $('#timesheet-detail-sec').slideUp();
        $('.slide-action-div,.setting-div').removeClass('open');
    });
}


function ShowGetTaskHistory(obj) {


    if (obj != null) {
        $('#dvTaskheetList').find('table tbody').empty();
        $(obj).each(function () {
            var tr = document.createElement('tr');

            var tdCheckBox = document.createElement('td');
            $('#btnHistory').attr('Timesheet-id', obj.TasksheetId);
            //var tdCheckBox = document.createElement('td');
            //$(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].TasksheetId + '">');
            $(tr).append(tdCheckBox);

            var tdJiraID = document.createElement('td');
            $(tdJiraID).text($(this)[0].JiraID);
            $(tdJiraID).attr('data-th', 'JiraID');
            $(tr).append(tdJiraID);

            //var tdTask = document.createElement('td');
            //$(tdTask).text($(this)[0].Task);
            //$(tdTask).attr('data-th', 'Task');
            //$(tr).append(tdTask);

            var tdProjectName = document.createElement('td');
            $(tdProjectName).text($(this)[0].ProjectName);
            $(tdProjectName).attr('data-th', 'ProjectName');
            $(tr).append(tdProjectName);

            var tdresourcesname = document.createElement('td');
            $(tdresourcesname).text($(this)[0].resourcesname);
            $(tdresourcesname).attr('data-th', 'resourcesname');
            $(tr).append(tdresourcesname);

            var tdEstimatePlanDate = document.createElement('td');
            $(tdEstimatePlanDate).text($(this)[0].EstimatePlanDate);
            $(tdEstimatePlanDate).attr('data-th', 'EstimatePlanDate');
            $(tr).append(tdEstimatePlanDate);

            var tdEstimatecompletionDate = document.createElement('td');
            $(tdEstimatecompletionDate).text($(this)[0].EstimatecompletionDate);
            $(tdEstimatecompletionDate).attr('data-th', 'EstimatecompletionDate');
            $(tr).append(tdEstimatecompletionDate);

            var tdEstimatedhours = document.createElement('td');
            $(tdEstimatedhours).text($(this)[0].Estimatedhours);
            $(tdEstimatedhours).attr('data-th', 'Estimatedhours');
            $(tr).append(tdEstimatedhours);

            var tdActualstartdate = document.createElement('td');
            $(tdActualstartdate).text($(this)[0].Actualstartdate);
            $(tdActualstartdate).attr('data-th', 'Actualstartdate');
            $(tr).append(tdActualstartdate);

            var tdActualenddate = document.createElement('td');
            $(tdActualenddate).text($(this)[0].Actualenddate);
            $(tdActualenddate).attr('data-th', 'Actualenddate');
            $(tr).append(tdActualenddate);

            var tdActualhours = document.createElement('td');
            $(tdActualhours).text($(this)[0].Actualhours);
            $(tdActualhours).attr('data-th', 'Actualhours');
            $(tr).append(tdActualhours);

            var tdModifiedby = document.createElement('td');
            $(tdModifiedby).text($(this)[0].Modifiedby);
            $(tdModifiedby).attr('data-th', 'Modifiedby');
            $(tr).append(tdModifiedby);




            $('#dvTaskheetList').find('table tbody').append(tr);
        })
        //slide-action-div

        $(".slide-action-btn").click(function (evt) {
            evt.stopPropagation();
            $('.slide-action-div').attr('Timesheet-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
            $('.slide-action-div').addClass('open');
        });
    }
    $('#task-management-sec').slideUp();
    /*$('#header').hide(200);*/
    $('#history-detail-sec').slideDown();

    $('.time-detail-back-btn').click(function () {
        $('#task-management-sec').slideDown();
        $('#history-detail-sec').slideUp();
        $('.slide-action-div,.setting-div').removeClass('open');
    });


}

function ShowTasksheetList(PageIndex) {

    var ProjectName = $('#ddProject').val();
    var AssignedTo = $('#txtByName').val();
    var JiraID = $('#txtJiraId').val();
    var Status = $('#txtlststatus').val();
    // var PageIndex = 1;
    var TotalPageCount = "";
    var PreviousPageIndex = "";
    var NextPageIndex = "";


    var url = apiUrl + 'TaskSheetAPI/GetTimesheetList';
    $.get(url, { JiraID: JiraID, ProjectName: ProjectName, AssignedTo: AssignedTo, Status: Status, PageIndex: PageIndex },
        function (resonse) {
            if (resonse != null) {
                $('#dvTimesheetList').find('table tbody').empty();
                if (resonse.length == 0) {
                    AddError($('#task-management-sec'), 'Record not found for this user');
                }
                $(resonse).each(function () {

                    TotalPageCount = $(this)[0].paginationModels.TotalPageCount;
                    PreviousPageIndex = $(this)[0].paginationModels.PreviousPageIndex;
                    NextPageIndex = $(this)[0].paginationModels.NextPageIndex;
                    PageIndex = $(this)[0].paginationModels.PageIndex;
                    $('#hdnTotalPageCount').val(TotalPageCount);
                    tr = document.createElement('tr');
                    //var tdActualenddate = $(this)[0].EstimatecompletionDate;
                    var estHr = $(this)[0].Estimatedhours;
                    var actualHr = $(this)[0].Actualhours;
                    var date = $(this)[0].EstimatecompletionDate;
                    var cdate = $(this)[0].Actualenddate;
                    var today = new Date();
                    var dd = String(today.getDate()).padStart(2, '0'); var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!var
                    var yyyy = today.getFullYear();

                    var today1 = yyyy + '-' + mm + '-' + dd;

                    // var tr = document.createElement('tr');
                    var tdCheckBox = document.createElement('td');
                    $(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].TasksheetId + '" Jira-Id="' + $(this)[0].JiraID + '">');
                    $(tr).append(tdCheckBox);

                    var tdJiraID = document.createElement('td');
                    $(tdJiraID).text($(this)[0].JiraID);
                    $(tdJiraID).attr('data-th', 'JiraID');
                    $(tr).append(tdJiraID);

                    var tdTask = document.createElement('td');
                    $(tdTask).text($(this)[0].Task);
                    $(tdTask).attr('data-th', 'Task');
                    $(tr).append(tdTask);

                    var tdProjectName = document.createElement('td');
                    $(tdProjectName).text($(this)[0].ProjectName);
                    $(tdProjectName).attr('data-th', 'ProjectName');
                    $(tr).append(tdProjectName);

                    var tdresourcesname = document.createElement('td');
                    $(tdresourcesname).text($(this)[0].resourcesname);
                    $(tdresourcesname).attr('data-th', 'resourcesname');
                    $(tr).append(tdresourcesname);

                    var tdEstimatePlanDate = document.createElement('td');
                    $(tdEstimatePlanDate).text($(this)[0].EstimatePlanDate);
                    $(tdEstimatePlanDate).attr('data-th', 'EstimatePlanDate');
                    $(tr).append(tdEstimatePlanDate);


                    var tdEstimatecompletionDate = document.createElement('td');
                    $(tdEstimatecompletionDate).text($(this)[0].EstimatecompletionDate);
                    $(tdEstimatecompletionDate).attr('data-th', 'EstimatecompletionDate');
                    $(tr).append(tdEstimatecompletionDate);


                    var tdEstimatedhours = document.createElement('td');
                    $(tdEstimatedhours).text($(this)[0].Estimatedhours);
                    $(tdEstimatedhours).attr('data-th', 'Estimatedhours');
                    $(tr).append(tdEstimatedhours);

                    var tdActualstartdate = document.createElement('td');
                    $(tdActualstartdate).text($(this)[0].Actualstartdate);
                    $(tdActualstartdate).attr('data-th', 'Actualstartdate');
                    $(tr).append(tdActualstartdate);


                    var tdActualenddate = document.createElement('td');
                    $(tdActualenddate).text($(this)[0].Actualenddate);
                    $(tdActualenddate).attr('data-th', 'Actualenddate');
                    $(tr).append(tdActualenddate);
                    if (cdate != "" && cdate != null) {
                        if (date < cdate) {

                            tdActualenddate.style.backgroundColor = "red";
                        }
                    }
                    else {
                        if (date < today1) {
                            tdEstimatecompletionDate.style.backgroundColor = "red";
                        }
                    }

                    var tdActualhours = document.createElement('td');
                    $(tdActualhours).text($(this)[0].Actualhours);
                    $(tdActualhours).attr('data-th', 'Actualhours');
                    $(tr).append(tdActualhours);

                    if (actualHr != "" && actualHr != null && actualHr != 0) {
                        if (estHr < actualHr) {
                            tdActualhours.style.backgroundColor = "red";
                        }
                    }

                    var tdAction = document.createElement('td');
                    $(tdAction).attr('data-th', 'Action');
                    $(tdAction).addClass('td-btn-align')


                    $(tdAction).html('<div class="td-flex"><button class="blue-btn slide-action-btn"><i class="fa fa-cog"></i></button> </div>');
                    $(tr).append(tdAction);
                    $('#dvTimesheetList').find('table tbody').append(tr);
                    GetPageList(PageIndex);
                })

                //slide-action-div

                $(".slide-action-btn").click(function (evt) {
                    evt.stopPropagation();
                    $('.slide-action-div').attr('Timesheet-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
                    $('.slide-action-div').attr('Jira-Id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).attr('Jira-Id').trim());

                    $('.slide-action-div').addClass('open');
                });
            }


        });

    var paginationHtml = "";
    paginationHtml += "<p style=\"padding-bottom: 0;\">";
    if (PreviousPageIndex >= 0) {
        paginationHtml += "    <a href=\"javascript:void(0);\" class=\"lft-btn\" onclick=\"ShowTasksheetList(" + PreviousPageIndex + ")\">";
        //paginationHtml += "        <img src=\"/assets/images/artical-left-arrow-blue.webp\" alt=\"\"> Prev";
        paginationHtml += "    </a>";
    }
    paginationHtml += "    <span class=\"mdl-text\"><span class=\"active-num\">" + PageIndex + "</span> / " + TotalPageCount + "</span>";
    if (NextPageIndex >= 0) {
        paginationHtml += "    <a href=\"javascript:void(0);\" class=\"rgt-btn\" onclick=\"ShowTasksheetList(" + NextPageIndex + ")\">Next";
        //paginationHtml += "        <img src=\"/assets/images/artical-right-arrow.webp\" alt=\"\">";
        paginationHtml += "    </a>";
    }
    paginationHtml += "</p>";

    $("#divPagination").html("");
    $("#divPagination").html(paginationHtml);
    $("html, body").animate({ scrollTop: 500 }, 1000);

    $("#divPageDropdown").show();
}




function TaskSheetDetails(obj) {


    if (obj != null) {

        $('#btnSave').attr('Timesheet-id', obj.TasksheetId);

        $('#ddProjectlst').val(obj.ProjectName);
        $('#txtJira').val(obj.JiraID);
        //$('#txtTask').val(obj.Task);
        //$('#txtRemarks').val(obj.Remarks);
        $('#txtReqForm').val(obj.ReqFrom);
        $('#txtReqFormdt').val(obj.ReqDate);
        $('#txtEstPlanDt').val(obj.EstimatePlanDate);
        $('#txtEstCompDt').val(obj.EstimatecompletionDate);
        $('#txtEsthrs').val(obj.Estimatedhours);
        $('#txtActstartDt').val(obj.Actualstartdate);
        $('#txtActendDt').val(obj.Actualenddate);
        $('#ddStatus').val(obj.Status);
        $('#txtActhrs').val(obj.Actualhours);
        //$('#txtAddedDate').val(obj.Addeddate);
        //$('#txtAssign').val(obj.AssignedTo);
        $('#txtdescription').val(obj.Discription);

    }

    else {

        $('#btnSave').show();
    }


    //Resources List

    var urlresourcesList = apiUrl + "TaskSheetAPI/GetResourcesList";
    $.ajax({
        type: "Get",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: urlresourcesList,
        data: {},
        success: function (result) {
            if (result != null) {
                $('#txtAssign').children().remove();
                $('#txtAssign').append("<option value='0'>Select Name</option>");
                $(result).each(function () {


                    var option = document.createElement('option');
                    option.value = $(this)[0].resourcesid;
                    option.innerText = $(this)[0].resourcesname;


                    $('#txtAssign').append(option);
                });
                $('#txtAssign').val(obj.AssignedTo);
            }

        }
    });



    //User List

    var urlUserList = apiUrl + "TaskSheetAPI/GetUserList";
    $.ajax({
        type: "Get",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: urlUserList,
        data: {},
        success: function (result) {
            if (result != null) {
                $('#txtReqForm').children().remove();
                $('#txtReqForm').append("<option value='0'>Select Name</option>");
                $(result).each(function () {

                    var option = document.createElement('option');
                    option.value = $(this)[0].id;
                    option.innerText = $(this)[0].UserFullName;


                    $('#txtReqForm').append(option);
                });
            }

        }
    });


    $('.popup-create-div, .overlay').slideToggle();
    $('.setting-div').removeClass('open');
    $('.popup-multi-lang').eq(0).show();
    //click filter-close-btn
    $('.add-new-div h3 a').click(function () {
        ClearAllForm();
    });

    $('#btnCancel').click(function () {
        ClearAllForm();
    });
}


function validatetasksheet() {
    $('.err').remove();
    var status = 1;
    if (CheckField($('#txtdescription'), '', ' Please Enter Description'))
        status = 0;


    if (CheckField($('#ddStatus'), '', ' Please Enter Status'))
        status = 0;


    if (CheckField($('#txtEsthrs'), '', ' Please Enter Estimated Hours'))
        status = 0;


    if (CheckField($('#txtEstPlanDt'), '', ' Please Enter Estimated Plan Date'))
        status = 0;


    if (CheckField($('#txtEstCompDt'), '', ' Please Enter Est Completion Date'))
        status = 0;


    if (CheckField($('#txtAssign'), '0', ' Please Enter Assigned To'))
        status = 0;



    if (CheckField($('#ddProjectlst'), '', ' Please Enter Project Name'))
        status = 0;


    if ($('#ddStatus').val() != "Yet To Start") {
        if (CheckField($('#txtActstartDt'), '', ' Please Enter StartDate'))
            status = 0;
    }

    if (status == 1) {

        CheckJiraId();
        //  SaveTasksheet();
    }
}
function SaveTasksheet() {

    var obj = {};

    if ($('#btnSave').attr('Timesheet-id') != null && $('#btnSave').attr('Timesheet-id') != undefined && $('#btnSave').attr('Timesheet-id') != "undefined" && $('#btnSave').attr('Timesheet-id').trim() != "")
        obj.TasksheetId = $('#btnSave').attr('Timesheet-id').trim();

    obj.JiraID = $('#txtJira').val();
    obj.ProjectName = $('#ddProjectlst').val();
    //obj.Task = $('#txtTask').val();
    obj.ReqFrom = $('#txtReqForm').val();
    obj.ReqDate = $('#txtReqFormdt').val();
    obj.EstimatePlanDate = $('#txtEstPlanDt').val();
    obj.EstimatecompletionDate = $('#txtEstCompDt').val();
    obj.Estimatedhours = $('#txtEsthrs').val();
    obj.Actualstartdate = $('#txtActstartDt').val();
    obj.Actualenddate = $('#txtActendDt').val();
    obj.status = $('#ddStatus').val();
    obj.Actualhours = $('#txtActhrs').val() == '' ? null : parseInt($('#txtActhrs').val());
    //obj.Addeddate = $('#txtAddedDate').val();
    obj.AssignedTo = $('#txtAssign').val();
    //obj.modifiedby = $('#txtModBy').val();
    //obj.ModifiedON = $('#txtModOn').val();
    obj.Discription = $('#txtdescription').val();


    var Resources = [];
    $('#txtAssign option:selected').each(function () {
        var Res = {};
        Res.resourcesid = $(this).val();
        Res.resourcesname = $(this).text();

        Resources.push(Res);
    });

    obj.ResourcesList = Resources;

    var url = apiUrl + 'TaskSheetAPI/SaveTaskSheet';
    $.ajax({
        url: url,
        type: 'post',
        data: JSON.stringify(obj),
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response.status) {
                alert(response.msg);
                //$('.popup-create-div, .overlay').slideUp();
                //$('#task-management-sec, .overlay').slideDown();
                $('#history-detail-sec').hide();
                $('#task-management-sec').show(200);
                ClearAllForm();
                ShowTasksheetList(1);
            }
            else {
                alert(response.msg);
            }
        },

    });

}



//function ConvIntoDateFormat(date) {
//    var day = parseInt(date.substring(6, 8));
//    var month = parseInt(date.substring(3, 5));
//    var year = parseInt('20'+ date.substring(0, 2));
//    var from_date = new Date(year, month - 1, day);

//    return from_date;

//}


function CheckJiraId() {
    //  obj.JiraID = $('#txtJira').val();
    var JiraId = $('#txtJira').val();
    var url = apiUrl + 'TaskSheetAPI/CheckJiraId';


    var TasksheetId = $('#btnSave').attr('Timesheet-id').trim();

    if (TasksheetId != null && TasksheetId != 0 && TasksheetId != "") {
        SaveTasksheet();
    }
    else {


        $.get(url, { 'JiraId': JiraId }, function (data) {
            if (data.msg == 'Please Enter Valid JiraId') {

                AddError($('#txtJira'), 'Please Enter valid jira Id');

            }
            else if (data.msg == 'JiraId Already Exist in Task sheet') {

                AddError($('#txtJira'), 'JiraId Already Exist in Task sheet');

            }

            else {
                SaveTasksheet();
            }
        });
    }
}

function CheckJiraId1() {

    var obj = {};

    if ($('#btnSave').attr('Timesheet-id') != null && $('#btnSave').attr('Timesheet-id') != undefined && $('#btnSave').attr('Timesheet-id') != "undefined" && $('#btnSave').attr('Timesheet-id').trim() != "")
        obj.TasksheetId = $('#btnSave').attr('Timesheet-id').trim();

    obj.JiraID = $('#txtJira').val();


    var url = apiUrl + 'TaskSheetAPI/CheckJiraId';
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
                ShowTasksheetList(1);
            }
            else {
                alert(response.msg);
            }
        },

    });

}

function ClearAllForm() {
    $('.popup-create-div, .overlay').slideUp();
    $('#Bannertop').find('input[type=text],textarea').val('');
    $('select').val('0');

}


$(function () {


    $("#txtJiraId").autocomplete({

        source: function (request, response) {

            var Search = $('#txtJiraId').val();
            console.log("2")
            var url = apiUrl + 'TaskSheetAPI/GetAutoCompleteSearchData';
            $.ajax({
                url: url,
                data: { searchValue: Search },
                //  data:  $('#txtAutoComplete').val(),
                dataType: "json",
                type: "Get",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger;
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
            $(this).val(i.item.val);
            $("#txtJiraId").submit();
        },
        minLength: 1
    });
});



$(function () {


    $("#txtJira").autocomplete({

        source: function (request, response) {

            var Search = $('#txtJira').val();
            console.log("1")
            var url = apiUrl + 'TaskSheetAPI/GetAutoCompleteSearchData';
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
            $(this).val(i.item.val);
            $("#txtJira").submit();
        },
        minLength: 1
    });
});


$(function () {


    $("#txtReqForm").autocomplete({

        source: function (request, response) {

            var Search = $('#txtReqForm').val();
            var url = apiUrl + 'TaskSheetAPI/GetSearchUserLst';
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
            $(this).val(i.item.val);
            $("#txtReqForm").submit();
        },
        minLength: 1
    });
});





function EditTasksheet(obj) {

    var Id = $(obj).closest('div.slide-action-div').attr('Timesheet-id');
    var url = apiUrl + 'TaskSheetAPI/GetSingleTimesheet';
    $.get(url, { 'TasksheetId': Id }, function (data) {
        if (data != null) {
            TaskSheetDetails(data);
        }
    });

}



function EditHistory(obj) {

    var Id = $(obj).closest('div.slide-action-div').attr('Timesheet-id');
    var url = apiUrl + 'TaskSheetAPI/GetTaskHistory';

    $.get(url, { 'TasksheetId': Id }, function (data) {
        if (data != null) {
            ShowGetTaskHistory(data);
        }
    });

}



function GetJiraIdRecord(obj) {

    var JiraId = $(obj).closest('div.slide-action-div').attr('Jira-id');
    var url = apiUrl + 'TaskSheetAPI/GetJiraRecord';

    $.get(url, { 'JiraId': JiraId }, function (data) {
        if (data != null) {
            ShowJiraId(data);
        }
    });

}


