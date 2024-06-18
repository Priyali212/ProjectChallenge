$(document).ready(function () {
    // left-panel-div Toggle
    $('.toggle-btn').click(function () {
        $('.left-panel-div, .right-panel-div').toggleClass("open");
    });
    // profile-panel-div Toggle
    $('.profile-link').click(function (event) {
        event.stopPropagation();
        $('.slide-profile-panel-div').toggleClass("toggle");
        $('.slide-bulk-div, .setting-div, .slide-action-div').removeClass("open");
    });

    $("body").click(function (evt) {
        if (!$(evt.target).is('.slide-profile-panel-div')) {
            $('.slide-profile-panel-div').removeClass("toggle");
        }
    });

    // sub-menu-div Toggle
    $('.dropdown-anchor').click(function () {
        var openSubMenu = $(this).closest('.dropdown-li');
        openSubMenu.siblings().find('.sub-menu-div').slideUp();
        openSubMenu.find('.sub-menu-div').slideToggle('slow');
        $(this).children('.fa-angle-left').toggleClass('fa-angle-down');
        $(this).children('.fa-angle-down').toggleClass('fa-angle-left');
        if (openSubMenu.find('.sub-menu-div').css('display') == 'none') {
            
             $(this).parent().find('a').children('.fa-angle-down').addClass('fa-angle-left').removeClass('fa-angle-down');
        }
        
        //else {
        //    $(this).parent().find('a').children('.fa-angle-down').addClass('fa-angle-left').removeClass('fa-angle-down');
        //}
        // openSubMenu.find('.dropdown-anchor .fa-angle-left').addClass('rotate');
        // openSubMenu.siblings().find('.dropdown-anchor .fa-angle-left').removeClass('rotate');
    });

    // // table json
    // var data = [
    //     { "name":"Tiger Nixon", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$920800" },
    //     { "name":"Garrett Winters", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$820800" },
    //     { "name":"Ashton Cox", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$720800" },
    //     { "name":"Cedric Kelly", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$620800" },
    //     { "name":"Airi Satou", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$520800" },
    //     { "name":"Brielle Williamson", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$420800" },
    //     { "name":"Herrod Chandler", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$320800" },
    //     { "name":"Rhona Davidson", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$220800" },
    //     { "name":"Colleen Hurst", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$120800" },
    //     { "name":"Sonya Frost", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$090800" },
    //     { "name":"Jena Gaines", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$080800" },
    //     { "name":"Quinn Flynn", "position":"System Architect", "office":"Edinburgh", "age":"61", "startdate":"2011-04-25","salary":"$070800" }
    //   ];
    //   var tbody = document.getElementById('table-1');
    //   data.forEach(function(object) {
    //     var tr = document.createElement('tr');
    //     tr.innerHTML = '<td>' + '<input type="checkbox">' + '</td>' +
    //     '<td data-th="Name">' + object.name + '</td>' +
    //       '<td data-th="Position">' + object.position + '</td>' +
    //       '<td data-th="Office">' + object.office + '</td>' +
    //       '<td data-th="Age">' + object.age + '</td>' +
    //       '<td data-th="Start date">' + object.startdate + '</td>' +
    //       '<td data-th="Salary">' + object.salary + '</td>' +
    //       '<td data-th="Action" class="td-btn-align">' + '<button class="blue-btn slide-action-btn"><i class="fa fa-cog"></i></button>' + '</td>'
    //       ;
    //       tbody.appendChild(tr);
    //   });

    // //check all checkbox
    // $("#checkAll").change(function () {
    //   $('.slide-bulk-div').addClass("open");
    //   $('#table-1 input:checkbox').not(this).prop('checked', this.checked);
    //   $('#table-1 input:checkbox').closest('tr').toggleClass("highlight", this.checked);
    //   if($('#table-1 tr').hasClass('highlight')){
    //     $('#delete').prop("disabled", false);
    //     }
    //     else{
    //       $('#delete').prop("disabled", true);
    //     }
    //     if($(this).is(":checked")) {
    //       $(".slide-bulk-div").addClass('open');
    //   } else {
    //       $(".slide-bulk-div").removeClass('open');
    //   }
    // });

    // //addClass highlight in table row
    // $("#table-1 input:checkbox").change(function () {
    // if($(this).is(":checked")) {
    //   $(this).closest('tr').addClass('highlight');
    //   $('#delete').prop("disabled", false);
    // }
    // else{
    //   $(this).closest('tr').removeClass('highlight');
    //   $('#delete').prop("disabled", true); 
    // }
    // });
    // //onclick delete table row
    // $('#delete').click(function(){
    // $('#table-1 tr.highlight').remove();
    // if($('#table-1 tr').hasClass('highlight')){
    // $(this).prop("disabled", false);
    // }
    // else{
    //   $(this).prop("disabled", true);
    // }
    // });

    //onclick setting div open
    $('.btn-setting').click(function (evt) {
        evt.stopPropagation();
        $('.setting-div').addClass("open");
    });
    $('.btn-back-setting').click(function () {
        $('.setting-div').removeClass("open");
    });
    $("body").click(function (evt) {
        if (!$(evt.target).is('.setting-div')) {
            $('.setting-div').removeClass("open");
        }
    });
    
    $('.btn-back-setting').click(function () {
        $('.slide-bulk-div,.slide-action-div').removeClass("open");
    });
    $("body").click(function (evt) {
        if (!$(evt.target).is('.slide-bulk-div')) {
            $('.slide-bulk-div').removeClass("open");
        }
    });
    //onclick popup-view-detail-btn
    $('.popup-view-detail-btn').click(function () {
        //evt.stopPropagation();
        $('.view-detail-sec').slideDown();
    });
    $('.view-detail-sec h3 a').click(function () {
        $('.view-detail-sec').slideUp();
    });

    // view-other-detail-2 ul hide and show
    $('.view-other-detail-2 .view-card h3.heading').click(function () {
        $(this).next().slideToggle();
        $(this).children('i.fa').toggleClass('fa-plus').toggleClass('fa-minus');
    });
});