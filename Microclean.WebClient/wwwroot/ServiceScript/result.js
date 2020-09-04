function GetResult(id) {
    var quizresulthtml = "";
    var url = 'EndUseTest/gettestresultbytestid'
    GetQuizForTest(url, id).then(data => {
        const alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split('');
        quizresulthtml += `
                        <ul class="result">
                            <li> <label for="total">TOTAL `+ data.data.totalQuestion + `</label></li>
                            <li><label for="total">ATTEMPTED `+ data.data.totalAtemptQuestion + `</label></li>
                            <li><label for="total"><span class="blink">CORRECT `+ data.data.totalCorrenctAnswer + `</span></label></li>
                            <li><label for="total"><span class="blink">WRONG `+ data.data.totalWorngAnswer + `</span></label></li>
                            <li><label for="total">`+ data.data.userName + `</label></li>
                        </ul>
                        <div class="card-body">
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Test<div class="secondh">` + data.data.testName + `</div></th>
                                    </tr>
                                </thead>
                                <tbody>
                            `
        for (var i = 0; i < data.data.questions.length; i++) {
            quizresulthtml += ` <tr>
                                        <th>(`+ (i + 1) + `.)` + data.data.questions[i].question + `</th>
                                    </tr>
                                    `
            let optioncount = 0;
            for (var j = 0; j < data.data.questions[i].options.length; j++) {
                quizresulthtml += `<tr>`
                if (data.data.questions[i].options[j].isCorrect === null) {
                    quizresulthtml += `<td class="">(` + alphabet[j] + `.)` + data.data.questions[i].options[j].option + `</td>`
                } else {
                    if ((data.data.questions[i].options[j].isCorrect === 1) && (data.data.questions[i].options[j].isSelect === 1)) {
                        quizresulthtml += `<td class="icheck-correct">(` + alphabet[j] + `.)` + data.data.questions[i].options[j].option + `</td>`
                    } else {
                        if ((data.data.questions[i].options[j].isCorrect === 1) && (data.data.questions[i].options[j].isSelect === 0)) {
                            quizresulthtml += `<td class="icheck-wrong">(` + alphabet[j] + `.)` + data.data.questions[i].options[j].option + `</td>`
                        }
                    }

                }
                quizresulthtml += `</tr>`;
                optioncount++;
            }
        }
        quizresulthtml += `</tbody>
                                <tfoot>
                                </tfoot>
                            </table>
                        </div>
                    `
        $('#quizresultdatatab').html(quizresulthtml);
        FormatTable();
    });
}
function FormatTable() {
    $("#example1").DataTable({
        "responsive": true,
        "autoWidth": false,
        "sorting": false,
        "bInfo": false,
        "lengthMenu": [[25, 50, -1], [25, 50, "All"]],
    });
}
