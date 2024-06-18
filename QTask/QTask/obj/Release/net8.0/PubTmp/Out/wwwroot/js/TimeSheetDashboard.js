$(document).ready(function () {

    //calender
    $('#txtfrmdate').datetimepicker({
        step: 30, format: 'd-m-y '
    });
    $('#txttodate').datetimepicker({
        step: 30, format: 'd-m-y '
    });
        AddError($('#dvTimesheetList'), 'Work In Process');
});