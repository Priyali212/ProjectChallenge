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





    //calender
    $('#txtFrmdt').datetimepicker({
        step: 30, format: 'Y-m-d',
    });


    $('#txtTodt').datetimepicker({
        step: 30, format: 'Y-m-d',
    });


    $('#btnSearch').click(function () {

        ShowAdminList(1);

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
    ShowAdminList(1);



});

$(function () {
    $("#ddlPageList").on("change", function () {
        if (!isNaN($(this).val())) {
            ShowAdminList($(this).val());
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


function ShowAdminList(PageIndex) {


    var UserId = $('#txtByResName').val();
    var FromDate = $('#txtFrmdt').val();
    var ToDate = $('#txtTodt').val();
    //var UserId = {};
    var TotalPageCount = "";
    var PreviousPageIndex = "";
    var NextPageIndex = "";
    // model.AssetClass = $('#ddnSchemeAssetClass').val();
    var url = apiUrl + 'TimesheetAdminAPI/GetTimesheet';
    var TotalHours = 0.00;
    $.get(url, { UserId: UserId, FromDate: FromDate, ToDate: ToDate, PageIndex: PageIndex }, function (resonse) {
        if (resonse != null) {
            $('#dvAdminDetails').find('table tbody').empty();
            $(resonse).each(function () {

                TotalPageCount = $(this)[0].paginationModels.TotalPageCount;
                PreviousPageIndex = $(this)[0].paginationModels.PreviousPageIndex;
                NextPageIndex = $(this)[0].paginationModels.NextPageIndex;
                PageIndex = $(this)[0].paginationModels.PageIndex;
                $('#hdnTotalPageCount').val(TotalPageCount);

                var tr = document.createElement('tr');
                var tdCheckBox = document.createElement('td');
                $(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].UserId + '">');
                $(tr).append(tdCheckBox);

                var tdJiraID = document.createElement('td');
                $(tdJiraID).text($(this)[0].JiraId);
                $(tdJiraID).attr('data-th', 'JiraId');
                $(tr).append(tdJiraID);

                var tdDescription = document.createElement('td');
                $(tdDescription).text($(this)[0].Description);
                $(tdDescription).attr('data-th', 'Description');
                $(tr).append(tdDescription);

                var tdWorkedDate = document.createElement('td');
                $(tdWorkedDate).text($(this)[0].WorkedDate);
                $(tdWorkedDate).attr('data-th', 'WorkedDate');
                $(tr).append(tdWorkedDate);

                var tdMinSpend = document.createElement('td');
                $(tdMinSpend).text($(this)[0].MinSpend);
                $(tdMinSpend).attr('data-th', 'MinSpend');
                $(tr).append(tdMinSpend);

                //var tdAction = document.createElement('td');
                //$(tdAction).attr('data-th', 'Action');
                //$(tdAction).addClass('td-btn-align')
                //$(tdAction).html('<div class="td-flex"><button class="blue-btn slide-action-btn"><i class="fa fa-cog"></i></button> </div>');
                //$(tr).append(tdAction);
                $('#dvAdminDetails').find('table tbody').append(tr);
                TotalHours = $(this)[0].HourSpend;
                GetPageList(PageIndex);
            })
            $('#txttlhrs').val(TotalHours);

            //slide-action-div
            $(".slide-action-btn").click(function (evt) {
                evt.stopPropagation();
                $('.slide-action-div').attr('Notice-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
                $('.slide-action-div').addClass('open');
            });
        }
    });
    var paginationHtml = "";
    paginationHtml += "<p style=\"padding-bottom: 0;\">";
    if (PreviousPageIndex >= 0) {
        paginationHtml += "    <a href=\"javascript:void(0);\" class=\"lft-btn\" onclick=\"ShowAdminList(" + PreviousPageIndex + ")\">";
        //paginationHtml += "        <img src=\"/assets/images/artical-left-arrow-blue.webp\" alt=\"\"> Prev";
        paginationHtml += "    </a>";
    }
    paginationHtml += "    <span class=\"mdl-text\"><span class=\"active-num\">" + PageIndex + "</span> / " + TotalPageCount + "</span>";
    if (NextPageIndex >= 0) {
        paginationHtml += "    <a href=\"javascript:void(0);\" class=\"rgt-btn\" onclick=\"ShowAdminList(" + NextPageIndex + ")\">Next";
        //paginationHtml += "        <img src=\"/assets/images/artical-right-arrow.webp\" alt=\"\">";
        paginationHtml += "    </a>";
    }
    paginationHtml += "</p>";

    $("#divPagination").html("");
    $("#divPagination").html(paginationHtml);
    $("html, body").animate({ scrollTop: 500 }, 1000);

    $("#divPageDropdown").show();
}
