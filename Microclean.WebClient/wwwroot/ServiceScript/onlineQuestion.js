function AsignValue(count) {
    $('#ismatchedhdn_' + count + '').val(true);
}
function LoadQuestionAndOption() {
    const alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split('');
    var optionscountval = $('#drpnoofoptions').val()
    var optionshtml = "";
    for (var i = 0; i < optionscountval; i++) {
        var optionval = $('#option_' + i + '').val();
        if (optionval == undefined) {
            optionval = '';
        }
        optionshtml += `<div class="form-group optioncls">
                                                            <div class="form-check required">
                                                                <input type="hidden" id="ismatchedhdn_`+ i + `" value="false" name="Options[` + i + `].IsMatched" />
                                                                <input class="form-check-input" id="ismatched_`+ i + `" onclick="AsignValue(` + i + `);" type="checkbox">
                                                                <label class="form-check-label"> <b>(`+ alphabet[i] + `)</b> This Answer Option is correct</label>
                                                            </div>
                                                            <textarea class="textarea textareaeditor" id="option_` + i + `" name="Options[` + i + `].Name" placeholder="Place some text here">` + optionval + `</textarea>
                                                        </div>`;
    }
    $('#optoionsdivid').html(optionshtml);
    $('.textarea').summernote();
}

function cancelUploding() {
    $("#ddlCategoryId").val('');
    $("#txtnameId").val('');
    $("#txtDescriptionId").val('');
    $("#txtnoOfQuestionId").val('');
    $("#startDateString").val('');
    $("#EndDateString").val('');
    window.location = "/Admin/QuestionDetail";
}
var uploadComplete = false;
var openFile = function () {
    var categoryval = $("#ddlCategoryId").val();
    var durationval = $("#ddlduration :selected").text();
    var questionval = $("#txtquestionid").val();
    var NoOfOptionVal = $("#drpnoofoptions :selected").val();
    var questionId = $("#hdnQuestionId").val() ? $("#hdnQuestionId").val() : 0;

    if (categoryval == null || categoryval == undefined || categoryval == "") {
        alert('Please select category');
        return false;
    }
    if (questionval == null || questionval == undefined || questionval == "") {
        alert('Please enter question');
        return false;
    }
    var isOptionValid = false;
    $('.optioncls').each(function (i) {
        if ($('#option_' + i + '').val() == null || $('#option_' + i + '').val() == undefined || $('#option_' + i + '').val() == "") {
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
        formData.append("Options[" + i + "].Name", $('#option_' + i + '').val());
        formData.append("Options[" + i + "].IsMatched", $('#ismatchedhdn_' + i + '').val());
    });
    datasave('Question/createquestion', formData).then(data => {
        if (data.data !== null) {
            if (data.data.status === true) {
                alert(data.data.message);
                $('#txtquestionid').val('').summernote('reset');
                $('.optioncls').each(function (i) {
                    $('#option_' + i + '').summernote('reset');
                    $('#ismatched_' + i + '').prop("checked", false);
                });
            }
        }
    });
}
function getTestById(ctr) {
    var token = localStorage.getItem('token');
    var categoryVal = parseInt($(ctr).val());
    if (isNaN(categoryVal)) {
        categoryVal = ctr;
    }
    var options = $('#ddlTest');
    options.append($("<option></option>").val('').html('Please wait ...'));
    var url = 'Test/gettestbycategoryid';
    let URL = baseurl + url;
    $.ajax({
        url: URL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        data: { CategoryId: categoryVal },
        success: function (result) {
            options.empty();
            if (result.data !== null) {
                options.append($("<option></option>").val('').html('-Select Test-'));
                for (var i = 0; i < result.data.length; i++) {
                    options.append($("<option></option>").val(result.data[i].id).html(result.data[i].name));
                }
                var selectValue = $("#ddlTest").attr("data-value");
                if (selectValue !== undefined && selectValue !== "") {
                    $("#ddlTest").val(selectValue);
                }
            }
        },
        error: function () {
            alert('Error!');
        }
    });
    function GetTestList() {
        var topictID = $('#drptopic').val();
        $.ajax({
            url: "/Onlinetest/GetTestListForEdit",
            type: "GET",
            data: { 'topictId': topictID },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data !== null) {
                    var dropdownString = " <option value=''>All</option>";
                    for (var _data = 0; _data < data.value.length; _data++) {
                        dropdownString += "<option value='" + data.value[_data].id + "'>" + data.value[_data].name + "</option>";
                    }
                    $("#drptest").html(dropdownString);

                    var selectValue = $("#drptest").attr("data-value");

                    if (selectValue !== undefined && selectValue !== "")
                        $("#drptest").val(selectValue);
                }
            },
            error: function (response) {
                console.log(response);
            }
        });
    }
}