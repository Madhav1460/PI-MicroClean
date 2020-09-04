var pincodearry = "";
var pincodearryclient = "";
function GetAllState() {
    var token = localStorage.getItem('token');
    var url = 'common/getallstate';
    let URL = baseurl + url;
    var options = $('#ddlState');
    options.append($("<option></option>").val('').html('-Select State-'));
    $.ajax({
        url: URL,
        type: 'GET',
        headers: {
            "Authorization": "Bearer " + token
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.data.length; i++) {
                options.append($("<option></option>").val(result.data[i].stateId).html(result.data[i].staeName));
            }
            var selectValue = $("#ddlState").attr("data-value");
            if (selectValue !== undefined && selectValue !== "") {
                $("#ddlState").val(selectValue);
                getdistrictbystateid(selectValue);
            }
        },
        error: function () {
            alert('Error!');
        }
    });
};
function getdistrictbystateid(ctr) {
    var stateId = parseInt($(ctr).val());
    var token = localStorage.getItem('token');
    if (isNaN(stateId)) {
        stateId = ctr;
    }
    var options = $('#ddldistrictid');
    options.append($("<option></option>").val('').html('Please wait ...'));
    var url = 'common/getalldistrictbyStateId';
    let URL = baseurl + url;
    $.ajax({
        url: URL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        data: { stateid: stateId },
        success: function (result) {
            options.empty(); // Clear the plese wait
            options.append($("<option></option>").val('').html('-Select City-'));
            for (var i = 0; i < result.data.length; i++) {
                options.append($("<option></option>").val(result.data[i].id).html(result.data[i].name));
            }
            var selectValue = $("#ddldistrictid").attr("data-value");
            if (selectValue !== undefined && selectValue !== "") {
                $("#ddldistrictid").val(selectValue);
                GetCityLocationbyDistrictid(selectValue);
            }
        },
        error: function () {
            alert('Error!');
        }
    });
};
function GetCityLocationbyDistrictid(ctr) {
    var token = localStorage.getItem('token');
    var districtidval = parseInt($(ctr).val());
    if (isNaN(districtidval)) {
        districtidval = ctr;
    }
    var options = $('#ddlcitylocation');
    options.append($("<option></option>").val('').html('Please wait ...'));
    var url = 'common/getallcitylocationbydistrictid';
    let URL = baseurl + url;
    $.ajax({
        url: URL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        data: { districtid: districtidval },
        success: function (result) {
            if (result.data !== null) {
                options.empty();
                options.append($("<option></option>").val('').html('-Select City-'));
                for (var i = 0; i < result.data.length; i++) {
                    options.append($("<option></option>").val(result.data[i].id).html(result.data[i].cityLocation));
                }
                pincodearry = result.data;
                var selectValue = $("#ddlcitylocation").attr("data-value");
                if (selectValue !== undefined && selectValue !== "")
                    $("#ddlcitylocation").val(selectValue);
            }
        },
        error: function () {
            alert('Error!');
        }
    });
};
function GetPincode(ctr) {
    if (ctr.selectedIndex !== undefined) {
        $('#txtPincode').val(pincodearry[ctr.selectedIndex - 1].pincode);
        $('#txtAddress').val(pincodearry[ctr.selectedIndex - 1].extendedCityLocation);
    }
}
function GetAllStateClient() {
    var token = localStorage.getItem('token');
    var url = 'common/getallstate';
    let URL = baseurl + url;
    var options = $('#ddlStateClient');
    options.append($("<option></option>").val('').html('-Select State-'));
    $.ajax({
        url: URL,
        type: 'GET',
        headers: {
            "Authorization": "Bearer " + token
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.data.length; i++) {
                options.append($("<option></option>").val(result.data[i].stateId).html(result.data[i].staeName));
            }
            var selectValue = $("#ddlStateClient").attr("data-value");
            if (selectValue !== undefined && selectValue !== "") {
                $("#ddlStateClient").val(selectValue);
                getdistrictbystateidClient(selectValue);
            }
        },
        error: function () {
            alert('Error!');
        }
    });
};
function getdistrictbystateidClient(ctr) {
    var stateId = parseInt($(ctr).val());
    var token = localStorage.getItem('token');
    if (isNaN(stateId)) {
        stateId = ctr;
    }
    var options = $('#ddldistrictidClient');
    options.append($("<option></option>").val('').html('Please wait ...'));
    var url = 'common/getalldistrictbyStateId';
    let URL = baseurl + url;
    $.ajax({
        url: URL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        data: { stateid: stateId },
        success: function (result) {
            options.empty();
            options.append($("<option></option>").val('').html('-Select City-'));
            for (var i = 0; i < result.data.length; i++) {
                options.append($("<option></option>").val(result.data[i].id).html(result.data[i].name));
            }
            var selectValue = $("#ddldistrictidClient").attr("data-value");
            if (selectValue !== undefined && selectValue !== "") {
                $("#ddldistrictidClient").val(selectValue);
                GetCityLocationbyDistrictidClient(selectValue);
            }
        },
        error: function () {
            alert('Error!');
        }
    });
};
function GetCityLocationbyDistrictidClient(ctr) {
    var token = localStorage.getItem('token');
    var districtidval = parseInt($(ctr).val());
    if (isNaN(districtidval)) {
        districtidval = ctr;
    }
    var options = $('#ddlcitylocationClient');
    options.append($("<option></option>").val('').html('Please wait ...'));
    var url = 'common/getallcitylocationbydistrictid';
    let URL = baseurl + url;
    $.ajax({
        url: URL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        data: { districtid: districtidval },
        success: function (result) {

            options.empty();
            if (result.data !== null) {
                options.append($("<option></option>").val('').html('-Select City-'));
                for (var i = 0; i < result.data.length; i++) {
                    options.append($("<option></option>").val(result.data[i].id).html(result.data[i].cityLocation));
                }
                var selectValue = $("#ddlcitylocationClient").attr("data-value");
                if (selectValue !== undefined && selectValue !== "")
                    $("#ddlcitylocationClient").val(selectValue);

                pincodearryclient = result.data;
            }
        },
        error: function () {
            alert('Error!');
        }
    });
};
function GetPincodeClient(ctr) {
    if (ctr.selectedIndex !== undefined) {
        $('#txtCompanyPincode').val(pincodearryclient[ctr.selectedIndex - 1].pincode);
        $('#txtcompanyAddress').val(pincodearryclient[ctr.selectedIndex - 1].extendedCityLocation);
    }
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
                    options.append($("<option></option>").val(result.data[i].id).html(result.data[i].cityLocation));
                }
                var selectValue = $("#ddlTest").attr("data-value");
                if (selectValue !== undefined && selectValue !== "")
                    $("#ddlTest").val(selectValue);
            }
        },
        error: function () {
            alert('Error!');
        }
    });
}

function GetAllTestCategory() {
    var token = localStorage.getItem('token');
    var url = 'common/getalltestcategory';
    let URL = baseurl + url;
    var options = $('#ddlCategoryId');
    options.append($("<option></option>").val('0').html('-Select Category-'));
    $.ajax({
        url: URL,
        type: 'GET',
        headers: {
            "Authorization": "Bearer " + token
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.data.length; i++) {
                options.append($("<option></option>").val(result.data[i].id).html(result.data[i].categoryName));
            }
            var selectValue = $("#ddlCategoryId").attr("data-value");
            if (selectValue !== undefined && selectValue !== "") {
                $("#ddlCategoryId").val(selectValue);
            }
        },
        error: function () {
            alert('Error!');
        }
    });
};

function ExistingEmailCheck() {
    $("#overlay1").show();
    var token = localStorage.getItem('token');
    var url = 'common/existingemailcheck';
    let baseURL = baseurl + url;
    var email = $('#txtEmail').val();
    $.ajax({
        url: baseURL,
        async: false,
        data: { 'Email': email },
        method: 'GET',
        contentType: "json",
        success: function (data) {
            if (data.errorTypeCode === 101) {
                $("#overlay1").hide();
                $("#error_email").show();
                $("#error_email").html('This email is already registered, please try other</font>');
                $("#txtEmail").css("border-color", "Red");
                $('#txtEmail').val("");
            }
            else {
                $("#overlay1").hide();
                $("#error_email").html('<font color="Green">Email is avalible!</font>');
                $("#txtEmail").css("border-color", "Green");
                $("#error_email").show();
            }
        },
        error: function () {
            $("#overlay1").hide();
            alert('Error!');
        }
    });
};
