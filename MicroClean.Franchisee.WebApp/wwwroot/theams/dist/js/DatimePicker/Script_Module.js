
$(document).ready(function () {
    $.E2Edatetimepicker.setLocale('en');

    var nowDate = new Date();

    var curr_date = nowDate.getDate();
    var curr_month = nowDate.getMonth() + 1;
    var curr_year = nowDate.getFullYear();
    var formatteddate = curr_month + '/' + curr_date + '/' + curr_year;
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $('#startDateString').E2Edatetimepicker({ format: 'd-m-Y H:i', minDate: today, autoclose: true });
    //$('#InterviewScheduleDate').E2Edatetimepicker({ format: 'd-m-Y', minDate: today ,autoclose: true});

    $("#startDateString").on("change", function (e) {
        var val1 = $(this).val();
        var dateonly = val1.split(" ");
        var temd = dateonly[0].split("-");
        var tempTime = dateonly[1];
        var d = temd[0];
        var m = temd[1];
        var y = temd[2];
        var finaldate = new Date(y + '-' + m + '-' + d);
        var customdate = new Date(finaldate);
        $('#EndDateString').E2Edatetimepicker({
            format: 'd-m-Y H:i',
            minDate: customdate
        });
        var newdate = dateonly[0].split("-").reverse().join("-");
        var a = new Date(newdate + " " + tempTime);
        a.setMinutes(a.getMinutes() + 30);
        var hh = a.getHours();
        var mm = a.getMinutes();
        var finaldateEnd = new Date(y + '-' + m + '-' + d + " " + hh + ":" + mm);
        $('#EndDateString').E2Edatetimepicker({ format: 'd-m-Y H:i', value: finaldateEnd, autoclose: true });
    });
});