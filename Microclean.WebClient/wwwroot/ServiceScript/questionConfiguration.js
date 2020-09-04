var token = localStorage.getItem('token');

$(document).ready(function () {
    var obj = JSON.parse(sessionStorage.questionData);
    if (obj !== null) {
        $('#ddlCategoryId').attr("data-value", obj.categoryId);
        $('#hdnOptionLength').val(obj.options.length);
        $('#txtquestionid').val(obj.name);
        $('#hdnQuestionId').val(obj.id);
        $('#drpnoofoptions').val(obj.noOfOption);
        $("#ddlduration option:contains(" + obj.durationId+")").attr('selected', 'selected');
        const alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split('');
        var htmls = "";
        for (var i = 0; i < obj.options.length; i++) {
            var pp = obj.options[i].isMatched === true ? 1 : 0;
            htmls += ` <div class="form-group optioncls">
                            <div class="form-check required">                                
                                <input type="hidden" id="ismatchedhdn_`+ i + `" name="Options[` + i + `"].IsMatched" value=` + pp + `" />`;

            if (obj.options[i].isMatched === true) {
                htmls += `<input class="form-check-input" id="ismatched_` + i + `" onclick="AsignValue(` + i + `);" type="checkbox" checked="checked">` +
                    `<label class="form-check-label"> <b>(` + alphabet[i] + `)</b> This Answer Option is correct</label>`;
            }
            else {
                htmls += `<input class="form-check-input" id="ismatched_` + i + `" onclick="AsignValue(` + i + `);" type="checkbox" >` +
                    `<label class="form-check-label"> <b>(` + alphabet[i] + `)</b> This Answer Option is correct</label>`;
            }
            htmls += `</div><input type="hidden" id="optionId_` + i + `" value="` + obj.options[i].id + `" />` +
                `<textarea class="textarea textareaeditor" id="option_` + i + `" name="Options[` + i + `].Name" placeholder="Place some text here">` + obj.options[i].name + `</textarea></div>`;

            $('#optoionsdivid').html(htmls)
        }
    }
});

function cancelUploding() {
    $("#drpcategory").val('');
    $("#txtnameId").val('');
    $("#txtDescriptionId").val('');
    $("#txtnoOfQuestionId").val('');
    $("#startDateString").val('');
    $("#EndDateString").val('');
    window.location = "/Admin/QuestionDetail";
}

function dateValidation() {
    var strtDate = $('#startDateString').val();
    var endDate = $('#EndDateString').val();
    if (strtDate >= endDate) {
        alert("Invalid Time : EndDateTime cannot be less than or equal to StartDateTime. ");
        $("#EndDateString").val('');
        return false;
    }
}

function researchData() {
    $('#tblQuestionList').DataTable().destroy();
    var categoryId = $("#ddlCategoryId").val();
    var baseURL = baseurl + 'Question/getquestions';
    $.ajax({
        url: baseURL,
        async: false,
        data: { CategoryId: categoryId },
        method: 'GET',
        contentType: "json",
        success: function (data) {
            var questionData = "";
            if (data.data !== null) {
                for (var i = 0; i < data.data.length; i++) {
                    questionData += `<tr>
                                        <td>`+ i + 1 + `</td>   
                                        <td>`+ data.data[i].category + `</td> 
                                        <td>`+ data.data[i].question + `</td>
                                         <td><a class="btn btn-primary btn-sm" style="background-color: #007B9F;color: white;border-color: #007B9F;" onclick=EditQuestion("` + data.data[i].id + `"); ><i class="fa fa-pencil" aria-hidden="true"></i> Edit</a><a class="btn btn-danger btn-sm" style="color: white;" onclick=DeleteQuestion("` + data.data[i].id + `"); ><i class="fa fa-trash" aria-hidden="true"></i> Delete</a></td>
                                      </tr>`
                }
                $('#loadQuestionData').html(questionData);
                FormatTable();
            } else {
                $('#loadQuestionData').html(questionData);
                FormatTable();
            }

        },
        error: function (response) {
            console.log(response);
        }
    });
}


function EditQuestion(id) {
    var baseURL = baseurl + 'Question/getquestionbyid?questionid=' + id;
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            sessionStorage.setItem('questionData', JSON.stringify(result.data));
            window.location.href = '/Admin/QuestionEdit';
        }
    });
}

function FormatTable() {
    var t = $('#tblQuestionList').DataTable({
        "paging": true,
        "responsive": true,
        "autoWidth": false,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "iDisplayLength": 10,
    });
    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

var uploadComplete = false;
var openFile = function () {
    var categoryval = $("#ddlCategoryId").val();
    var durationval = $("#ddlduration :selected").text();
    var questionval = $("#txtquestionid").val();
    var NoOfOptionVal = $("#drpnoofoptions :selected").val();
    var questionId = $("#hdnQuestionId").val();
    var isOptionValid = false;
    $('.optioncls').each(function (i) {
        if ($('#option_' + i + '').val() === null || $('#option_' + i + '').val() === undefined || $('#option_' + i + '').val() === "") {
            isOptionValid = true;
            return false;
        }
    });
    if (isOptionValid) {
        alert('Please enter options');
        return false;
    }
    if (!$('div.form-check.required :checkbox:checked').length > 0) {
        alert('Please seelct atleast one correct answer.!!');
        return false;
    }
    var formData = new FormData();
    formData.append("CategoryId", categoryval);
    formData.append("DurationId", durationval);
    formData.append("Name", questionval);
    formData.append("Id", questionId);
    formData.append("NoOfOption", NoOfOptionVal);
    $('.optioncls').each(function (i) {
        var checkedid = $('#ismatched_' + i + '').is(":checked");
        formData.append("Options[" + i + "].Id", $('#optionId_' + i + '').val());
        formData.append("Options[" + i + "].Name", $('#option_' + i + '').val());
        formData.append("Options[" + i + "].IsMatched", checkedid);
    });
    if (isOptionValid) {
        alert('Please enter options');
        return false;
    }
    datasave('Question/createquestion', formData).then(data => {
        if (data.data !== null) {
            if (data.status) {
                alert("Updated");
                window.location = "/Admin/QuestionDetail";
            }
        }
    });
}


function DeleteQuestion(id) {
    bootbox.confirm({
        title: "<span style='font-weight:600;'>Delete Question</span>",
        message: 'Are you sure want to Delete this Question?',
        buttons: {
            'cancel': {
                label: 'No',
                className: 'btn-default pull-right'
            },
            'confirm': {
                label: 'Yes',
                className: 'btn-danger margin-right-5'
            }
        },
        callback: function (result) {
            var baseURL = baseurl + 'question/deletequestion';
            if (result) {
                var Details = new Object();
                Details.Id = id;
                Details.IsApproved = false;
                $.ajax({
                    url: baseURL,
                    headers: {
                        "Authorization": "Bearer " + token
                    },
                    type: 'POST',
                    data: (Details),
                    dataType: 'json',
                    success: function (result) {
                        if (result.status) {
                            window.location = 'QuestionDetail';
                            alert(result.message);
                        } else {
                            alert(result.message);
                        }
                    }
                });
            }
        }
    });
}