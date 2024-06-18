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

        ShowStatusList();

    });
});


function ShowStatusList() {


    var UserId = $('#txtByResName').val();
    var FromDate = $('#txtFrmdt').val();
    var ToDate = $('#txtTodt').val();


    var url = apiUrl + 'CheckStatusAPI/CheckStatus';

    $.get(url, { UserId: UserId, FromDate: FromDate, ToDate: ToDate },

        function (resonse) {
            if (resonse != null) {
                $('#dvCheckList').find('table tbody').empty();

                $(resonse).each(function () {
                    var tr = document.createElement('tr');
                    //var tdCheckBox = document.createElement('td');
                    //$(tdCheckBox).html('<input type="checkbox" value="' + $(this)[0].TasksheetId + '">');
                    //$(tr).append(tdCheckBox);


                    var tdresourcesname = document.createElement('td');
                    $(tdresourcesname).text($(this)[0].resourcesname);
                    $(tdresourcesname).attr('data-th', 'resourcesname');
                    $(tr).append(tdresourcesname);


                    var tdWorkedDate = document.createElement('td');
                    $(tdWorkedDate).text($(this)[0].WorkedDate);
                    $(tdWorkedDate).attr('data-th', 'WorkedDate');
                    $(tr).append(tdWorkedDate);

                   


                    $('#dvCheckList').find('table tbody').append(tr);
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