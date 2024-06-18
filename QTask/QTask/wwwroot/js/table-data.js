$(document).ready(function () {
    // table json
    var data = [
        { "name": "Tiger Nixon", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$920800" },
        { "name": "Garrett Winters", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$820800" },
        { "name": "Ashton Cox", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$720800" },
        { "name": "Cedric Kelly", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$620800" },
        { "name": "Airi Satou", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$520800" },
        { "name": "Brielle Williamson", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$420800" },
        { "name": "Herrod Chandler", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$320800" },
        { "name": "Rhona Davidson", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$220800" },
        { "name": "Colleen Hurst", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$120800" },
        { "name": "Sonya Frost", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$090800" },
        { "name": "Jena Gaines", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$080800" },
        { "name": "Quinn Flynn", "position": "System Architect", "office": "Edinburgh", "age": "61", "startdate": "2011-04-25", "salary": "$070800" }
    ];
    var tbody = document.getElementById('table-1');
    //data.forEach(function (object) {
    //    var tr = document.createElement('tr');
    //    tr.innerHTML = '<td>' + '<input type="checkbox">' + '</td>' +
    //        '<td data-th="Name">' + object.name + '</td>' +
    //        '<td data-th="Position">' + object.position + '</td>' +
    //        '<td data-th="Office">' + object.office + '</td>' +
    //        '<td data-th="Age">' + object.age + '</td>' +
    //        '<td data-th="Start date">' + object.startdate + '</td>' +
    //        '<td data-th="Salary">' + object.salary + '</td>' +
    //        '<td data-th="Action" class="td-btn-align">' + '<button class="blue-btn slide-action-btn"><i class="fa fa-cog"></i></button>' + '</td>'
    //        ;
    //    tbody.appendChild(tr);
    //});

    

    //addClass highlight in table row
    $("#table-1 input:checkbox").change(function () {
        if ($(this).is(":checked")) {
            $(this).closest('tr').addClass('highlight');
            $('#delete').prop("disabled", false);
        }
        else {
            $(this).closest('tr').removeClass('highlight');
            $('#delete').prop("disabled", true);
        }
    });
    //onclick delete table row
    $('#delete').click(function () {
        $('#table-1 tr.highlight').remove();
        if ($('#table-1 tr').hasClass('highlight')) {
            $(this).prop("disabled", false);
        }
        else {
            $(this).prop("disabled", true);
        }
    });
    
    $("body").click(function (evt) {
        if (!$(evt.target).is('.slide-action-div')) {
            $('.slide-action-div').removeClass("open");
        }
    });
});
