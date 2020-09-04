function LoadPagedata() {
    var url = 'EndUseTest/getendusertests'
    GetProfile(url).then(data => {
        var testhtml = "";
        if (data.data !== null) {
            for (var i = 0; i < data.data.length; i++) {
                testhtml += `<tr>
                                    <td></td>
                                    <td>`+ data.data[i].test + `</td>
                                    <td>`
                if (data.data[i].isTestDone > 0) {
                    testhtml += `<a id="link_` + i + `" href="result?testid=` + data.data[i].testId + `"><i class="fa fa-building"></i>View Result</span></a>` 
                } else {
                    testhtml += `<a id="link_` + i + `" href="quiz?testid=` + data.data[i].testId + `"><i class="fa fa-building"></i>Start Test</a>`
                }
                testhtml += `</td></tr>`;
            }
            $('#tbodydataid').html(testhtml);
            FormatTable();
        }
    })
}
function FormatTable() {
    var t = $('#testtable').DataTable({
        "columnDefs": [{
            "responsive": true,
            "autoWidth": false
        }],
        "iDisplayLength": 25
    });
    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}