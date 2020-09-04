/// <reference path="../theams/plugins/jquery/jquery.min.js" />
/// <reference path="../theams/toaster/toasterjs/toastr.js" />

var token = localStorage.getItem('token');
$(document).ready(function () {
    GetAllTestCategory();
    var obj = null;
    if (sessionStorage.testData !== undefined) {
        obj = JSON.parse(sessionStorage.testData);
    }
    if (obj !== null) {
        $('#txtid').val(obj.id);
        $('#ddlCategoryId').attr("data-value", obj.catetoryId);
        $('#txtnameId').val(obj.name);
        $('#txtnameId').prop('readonly', true);
        $('#txtnoOfQuestionId').val(obj.noOfQuestion);
        $('#startDateString').val(obj.statrDate);
        $('#EndDateString').val(obj.endDate);
        $('#txtDescriptionId').val(obj.description);
        sessionStorage.removeItem('testData');
        sessionStorage.clear();
        $('#btnadd').text("Update");
    }
});

var uploadComplete = false;
var openFile = function () {
    var id = $("#txtid").val() ? $("#txtid").val() : 0;
    var categoryId = $("#ddlCategoryId").val();
    var nameId = $("#txtnameId").val();
    var noOfQuestion = $("#txtnoOfQuestionId").val();
    var discriptionId = $("#txtDescriptionId").val();
    if (categoryId === null || categoryId === undefined || categoryId === "") {
        alert('Please select Category');
        return false;
    }
    if (nameId === null || nameId === undefined || nameId === "") {
        alert('Please enter name');
        return false;
    }
    if (noOfQuestion === null || noOfQuestion === undefined || noOfQuestion === "") {
        alert('Please enter no of question');
        return false;
    }

    var formData = new FormData();
    formData.append("Id", id);
    formData.append("CatetoryId", categoryId);
    formData.append("Name", nameId);
    formData.append("NoOfQuestion", noOfQuestion);
    formData.append("Description", discriptionId);
    datasave('test/createtest', formData).then(data => {
        if (data.data !== null) {
            alert(data.data.message);
            cancelUploding();
            window.location = "/Account/TestDetail";
        }
    });
}

function cancelUploding() {
    $("#ddlCategoryId").val('');
    $("#txtnameId").val('');
    $("#txtDescriptionId").val('');
    $("#txtnoOfQuestionId").val('');
    $("#startDateString").val('');
    $("#EndDateString").val('');
    window.location = "/Account/TestDetail";
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
    $('#testdetailtable').DataTable().destroy();
    var categoryId = $("#ddlCategoryId").val();
    $.ajax({
        url: baseurl + 'Test/gettestbycategoryid',
        headers: {
            "Authorization": "Bearer " + token
        },
        async: false,
        data: { CategoryId: categoryId },
        method: 'GET',
        contentType: "json",
        success: function (data) {
            var testdata = "";
            if (data !== null) {
                for (var i = 0; i < data.data.length; i++) {
                    testdata += `<tr>
                                        <td></td>
                                         <td>`+ data.data[i].name + `</td>
                                         <td><a href="#" class="btn btn-info btn-sm" onclick=UserDetailPopUp("` + data.data[i].id + `")>Assign Test</a>&nbsp<a class="btn btn-primary btn-sm" style="background-color: #007B9F;color: white;border-color: #007B9F;" onclick=EditTest("` + data.data[i].id + `"); ><i class="fa fa-pencil" aria-hidden="true"></i> Edit</a>&nbsp<a class="btn btn-danger btn-sm" style="color: white;" onclick=DeleteTest("` + data.data[i].id + `"); ><i class="fa fa-trash" aria-hidden="true"></i> Delete</a></td>
                                      </tr>`
                }
                $('#loadtestdata').html(testdata);
                FormatTable();
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function EditTest(id) {
    var baseURL = baseurl + 'Test/gettestbyid?testid=' + id;
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            if (result.status) {
                sessionStorage.setItem('testData', JSON.stringify(result.data));
                window.location.href = '/Account/TestConfigure';
            } else {
                alert(result.message);
            }
            
        }
    });
}
function FormatTable() {
    var t = $('#testdetailtable').DataTable({
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

function questioncount(ctr) {
    var catid = parseInt($(ctr).val());
    if (isNaN(catid)) {
        catid = ctr;
    }
    $.ajax({
        url: baseurl + 'Test/getquestioncountbycategoryid',
        headers: {
            "Authorization": "Bearer " + token
        },
        async: false,
        data: { CategoryId: catid },
        method: 'GET',
        contentType: "json",
        success: function (data) {
            if (data !== null) {
                $('#txtquestioncount').text(data.data.count);
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function validatequestion() {
    var count = $('#txtquestioncount').text();
    var noofquestion = $('#txtnoOfQuestionId').val();
    if (count === 0) {
        alert("Cannot Create test!")
        $('#btnadd').prop("disabled", true);
    }
    if (parseInt(noofquestion) > parseInt(count)) {
        alert("Cannot enter more than Available questions in Category!");
        $('#txtnoOfQuestionId').val('');
        $('#txtnoOfQuestionId').css('border-color', 'Red');
        return false;
    }
    else {
        $('#txtnoOfQuestionId').css('border-color', 'lightgrey');
        $('#btnadd').prop("disabled", false);
    }
}

function DeleteTest(id) {
    bootbox.confirm({
        title: "<span style='font-weight:600;'>Delete Question</span>",
        message: 'Are you sure want to Delete this Test?',
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

            var baseURL = baseurl + 'test/deletetest';
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
                            alert(result.message);
                            window.location = 'TestDetail';
                        } else {
                            alert(result.message);
                        }
                    }
                });
            }
        }
    });
}

function UserDetailPopUp(ctr) {
    $('#btnSubmit').val(ctr);
    var testid = ctr;
    $.ajax({
        url: baseurl + 'test/getalluser',
        headers: {
            "Authorization": "Bearer " + token
        },
        async: false,
        data: { 'TestId': testid },
        method: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);
            var userdata = "";
            if (data.data !== null && data.data !== undefined && data.data != "") {
                userdata += `<table id="userdetailtable" class="table table-bordered table-striped tblbackground dataTable dtr-inline" role="grid">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Name</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>`
                for (var i = 0; i < data.data.length; i++) {
                    userdata += `<tr>
                                        <td></td>
                                         <td>`+ data.data[i].fullName + `</td>
                                         <td><input type="hidden" name="UserId" value="` + data.data[i].userId + `" />`
                    if (data.data[i].testId !== null && data.data[i].testId > 0) {
                        userdata += `<input type="checkbox" value="` + data.data[i].testId + `" name="VideoUserId[]" id="goalChkId" class="videouserchk" title="Select" checked="checked" />`
                    } else {
                        userdata += `<input type="checkbox" name="VideoUserId[]" id="goalChkId" class="videouserchk" title="Select" />`
                    }
                    userdata += `</td ></td>
                                      </tr>`
                }
                userdata += `</tbody>
                    </table>`
                $('#studendata').html(userdata);
                $('#datamodel').modal('show');
                FormatUserTable();
            }
            else {
                $('#tbluserdata').html("No Data Present");
                $('#datamodel').modal('show');
            }
        },
        error: function (xhr, textStatus, err) {
            //$("#usertbl").unblock();
        }
    })
}
function getUnique(array) {
    var uniqueArray = [];
    for (i = 0; i < array.length; i++) {
        if (uniqueArray.indexOf(array[i]) === -1) {
            uniqueArray.push(array[i]);
        }
    }
    return uniqueArray;
}
function saveUserVideoMappigdata(ctr) {
    var userarr = [];
    $('[name="VideoUserId[]"]').each(function () {
        var currentvalue = $(this).parent().find("input[type='hidden']").val();
        var currentUserVideovalue = parseInt($(this).val());
        if ($(this).prop("checked") === true && isNaN(parseInt(currentUserVideovalue))) {
            userarr.push({ 'UserId': currentvalue, 'IsSelected': 'true', 'UserTestId': currentUserVideovalue });
        } else {
            if ($(this).prop("checked") === false && currentUserVideovalue > 0) {
                userarr.push({ 'UserId': currentvalue, 'IsSelected': 'true', 'UserTestId': currentUserVideovalue });
            }
        }
    });
    var myJSON = JSON.stringify(getUnique(userarr));
    var formData = new FormData();
    formData.append("testId", parseInt($(ctr).val()));
    formData.append("userArr", myJSON);
    datasave('test/usertestmapping', formData).then(data => {
        if (data.data !== "Udated") {
            alert(data.data.message);
            $('#datamodel').modal('hide');
            userarr = [];
        } else {
            alert(data.data);
            $('#datamodel').modal('hide');
            userarr = [];
        }
    });
}
function FormatUserTable() {
    var t = $('#userdetailtable').DataTable({
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "iDisplayLength": 25
    });
    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

function ExistingTestNameCheck() {
    $("#overlay1").show();
    var token = localStorage.getItem('token');
    var url = 'franchisee/existingtestnamecheck';
    let baseURL = baseurl + url;
    var testname = $('#txtnameId').val();
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        async: false,
        data: { 'TestName': testname },
        method: 'GET',
        contentType: "json",
        success: function (data) {
            debugger;
            if (data.errorTypeCode == 101) {
                $("#overlay1").hide();
                $("#error_testname").show();
                $("#error_testname").html('Test with this Testname is already created, please try other</font>');
                $("#txtnameId").css("border-color", "Red");
                $("#btnadd").attr("disabled", true);
            }
            else {
                $("#overlay1").hide();
                $("#error_testname").html('<font color="Green">Testname is avalible!</font>');
                //$("#error_email").hide();
                $("#txtnameId").css("border-color", "Green");
                $("#error_testname").show();
                $("#btnadd").attr("disabled", false);
            }
        },
        error: function () {
            $("#overlay1").hide();
            alert('Error!');
        }
    });
};