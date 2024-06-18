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


    $('#btnDownload').click(function () {

        DownloadExcel();

    });


    $('#btnSearch').click(function () {

        ShowGenerateReportList();

    });



});




function DownloadExcel() {


    var UserId = $('#txtByResName').val();
    var FromDate = $('#txtFrmdt').val();
    var ToDate = $('#txtTodt').val();
    //var UserId = {};
   
    
    // model.AssetClass = $('#ddnSchemeAssetClass').val();
    var url = apiUrl + 'GenerateReportAPI/GenerateReport';
    $('#btnDownload').attr('disabled', 'disabled');
    $('#ImgUploadExcel').css('display', 'inline');
    
    $.get(url, { UserId: UserId, FromDate: FromDate, ToDate: ToDate }, function (resonse) {
        if (resonse != null) {
            $('#dvGenerateList').find('table tbody').empty();
           
                ExportToExcel();
                

          
            

            //slide-action-div
            $(".slide-action-btn").click(function (evt) {
                evt.stopPropagation();
                $('.slide-action-div').attr('Notice-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
                $('.slide-action-div').addClass('open');
            });
        }
    });
    
}



function ShowGenerateReportList() {


    var UserId = $('#txtByResName').val();
    var FromDate = $('#txtFrmdt').val();
    var ToDate = $('#txtTodt').val();
   
   
    var url = apiUrl + 'GenerateReportAPI/GenerateReport';
   
    $.get(url, { UserId: UserId, FromDate: FromDate, ToDate: ToDate },

        function (resonse) {
        if (resonse != null) {
            $('#dvGenerateList').find('table tbody').empty();

            $(resonse).each(function () {
                var tr = document.createElement('tr');
                //var tdCheckBox = document.createElement('td');
                //$(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].TasksheetId + '">');
                //$(tr).append(tdCheckBox);
                

                var tdresourcesname = document.createElement('td');
                $(tdresourcesname).text($(this)[0].resourcesname);
                $(tdresourcesname).attr('data-th', 'resourcesname');
                $(tr).append(tdresourcesname);  


                var tdTtotalRecord = document.createElement('td');
                $(tdTtotalRecord).text($(this)[0].totalRecord);
                $(tdTtotalRecord).attr('data-th', 'totalRecord');
                $(tr).append(tdTtotalRecord);
                
              
               

                var tdOpenTask = document.createElement('td');
                $(tdOpenTask).text($(this)[0].OpenTask);
                $(tdOpenTask).attr('data-th', 'OpenTask');
                $(tr).append(tdOpenTask);

                var tdClosedTask = document.createElement('td');
                $(tdClosedTask).text($(this)[0].ClosedTask);
                $(tdClosedTask).attr('data-th', 'ClosedTask');
                $(tr).append(tdClosedTask);


                $('#dvGenerateList').find('table tbody').append(tr);
            })





            //slide-action-div
            $(".slide-action-btn").click(function (evt) {
                evt.stopPropagation();
                $('.slide-action-div').attr('Notice-id', $(this).closest('tr').find('td').eq(0).find('input[type=checkbox]').eq(0).val().trim());
                $('.slide-action-div').addClass('open');
            });
        }
    });

}


function ExportToExcel() {
    var UserId = $('#txtByResName').val();
    var FromDate = $('#txtFrmdt').val();
    var ToDate = $('#txtTodt').val();

    var a = document.createElement('a');

    a.href = '/api/GenerateReportAPI/ExportToExcel?UserId=' + UserId + "&FromDate=" + FromDate + "&ToDate=" + ToDate;

    a.download = 'exported_data.xlsx';

    document.body.appendChild(a);

    a.click();

    document.body.removeChild(a);
    $('#ImgUploadExcel').hide();
}