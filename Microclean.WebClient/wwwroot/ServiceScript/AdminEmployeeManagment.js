var token = localStorage.getItem('token');

function addNewEmployeeByAdmin() {
    $("#overlay1").show();
    if (CheckRequiredFields()) {
        var formData = new FormData();
        let userfirstnameval = $('#txtFirstName').val();
        let usermiddlenameval = $('#txtMiddleName').val();
        let userlastnameval = $('#txtLastName').val();
        let useremailval = $('#txtEmail').val();
        let userphoneval = $('#txtPhone').val();
        let userpasswordval = $('#txtPassword').val();
        let userreinterpasswordval = $('#txtconfpassword').val();
        let userstateval = $("#ddlState :selected").val();
        let usercityval = $("#ddlcitylocation :selected").val();
        let userpincodeval = $('#txtPincode').val();
        let userAlternateEmail = $('#txtAlternateEmail').val();
        let userAlternateNo = $('#txtAlternateNumber').val();
        let userCityLocation = $("#ddlcitylocation :selected").val() !== undefined ? $("#ddlcitylocation :selected").val() : 0;
        let userPincode = $("#txtPincode").val() !== "" ? $('#txtPincode').val() : 0;
        let userAddress = $('#txtAddress').val();
        formData.append("FirstName", userfirstnameval);
        formData.append("MiddleName", usermiddlenameval);
        formData.append("LastName", userlastnameval);
        formData.append("Email", useremailval);
        formData.append("PhoneNumber", userphoneval);
        formData.append("PasswordHash", userpasswordval);
        formData.append("ConfirmPassword", userreinterpasswordval);
        
        formData.append("AlternateEmail", userAlternateEmail);
        formData.append("AlternatePhone", userAlternateNo);
        formData.append("PasswordHash", userpasswordval);
        formData.append("ConfirmPassword", userreinterpasswordval);
        formData.append("Address.CityLocationId", userCityLocation);
        formData.append("Address.ZipCode", userPincode);
        formData.append("Address.FullAdrrss", userAddress);
        formData.append("UserType", 4);
        datasave('Company/createsupervisorbyadmin', formData).then(data => {
            $("#overlay1").hide();
            if (data.data !== null) {
                if (data.data.status === false) {
                    alert(data.data.message);
                } else {
                    alert(data.data.message);
                }
                window.location = "/Admin/EmployeeList";
            }
        });
    }
}

function getEmployeeViewById(id) {
    $("#overlay1").show();
    var baseURL = baseurl + 'Company/getuserdetailbyid?userId=' + id;
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $("#overlay1").hide();
            sessionStorage.setItem('employeeData', JSON.stringify(result.data));
            window.location.href = '/Admin/EmployeeView';
        }
    });
}

function getEmployeeForEditById(id) {
    $("#overlay1").show();
    var baseURL = baseurl + 'Company/getuserdetailbyid?userId=' + id;
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $("#overlay1").hide();
            sessionStorage.setItem('employeeData', JSON.stringify(result.data));
            window.location.href = '/Admin/EditEmployee';
        }
    });
}

function getEmployeeView() {
    $("#overlay1").show();
    var obj = JSON.parse(sessionStorage.employeeData);
    if (obj.user.userDetail !== null) {
        $('#fullName').text(obj.user.userDetail.fullName);
        $('#alternate_email').text(obj.user.userDetail.alternateEmail);
        $('#alternate_phoneno').text(obj.user.userDetail.alternateNumber);
        $('#email').text(obj.user.email);
        $('#phoneno').text(obj.user.phoneNumber);
        if (obj.user.addresses.length > 0) {
            $('#address').text(obj.user.addresses[0].fullAddress);
            $('#pincode').text(obj.user.addresses[0].userZip);
        }
    }
    $("#overlay1").hide();
}

function getEmployeeForEdit() {
    $("#overlay1").show();
    var obj = JSON.parse(sessionStorage.employeeData);
    console.log(obj);
    $('#hdnUserId').val(obj.user.id);
    $('#txtFirstName').val(obj.user.firstName);
    $('#txtMiddleName').val(obj.user.middleName);
    $('#txtLastName').val(obj.user.lastName);
    $('#txtEmail').val(obj.user.email);
    $('#txtPhone').val(obj.user.phoneNumber);
    $('#txtAlternateEmail').val(obj.user.userDetail.alternateEmail);
    $('#txtAlternateNumber').val(obj.user.userDetail.alternateNumber);
    $('#hdnUserDetailId').val(obj.user.userDetail.id);
    if (obj.user.addresses.length > 0) {
        $('#hdnAddressId').val(obj.user.addresses[0].addressId);
        $('#ddlState').attr("data-value", obj.user.addresses[0].stateId);
        $('#ddldistrictid').attr("data-value", obj.user.addresses[0].districtId);
        $('#ddlcitylocation').attr("data-value", obj.user.addresses[0].cityLocationId);
        $('#txtAddress').val(obj.user.addresses[0].fullAddress);
        $('#txtPincode').val(obj.user.addresses[0].userZip);
    }
    $("#overlay1").hide();
}

function updateEmployeeByAdmin() {
    $("#overlay1").show();
    var formData = new FormData();
    let userFirstName = $('#txtFirstName').val();
    let userMiddleName = $('#txtMiddleName').val();
    let userLastName = $('#txtLastName').val();
    let userEmail = $('#txtEmail').val();
    let userPhone = $('#txtPhone').val();
    let userDetailId = $('#hdnUserDetailId').val();
    let userAlternateEmail = $('#txtAlternateEmail').val();
    let userAlternateNo = $('#txtAlternateNumber').val();
    let userAddressId = $('#hdnAddressId').val();
    let userState = $("#ddlState :selected").val();
    let userCity = $("#ddlcitylocation :selected").val();
    let userPincode = $('#txtPincode').val();
    let userAddress = $('#txtAddress').val();
    let userId = $('#hdnUserId').val(); 

    formData.append("UserId", userId);
    formData.append("FirstName", userFirstName);
    formData.append("MiddleName", userMiddleName);
    formData.append("LastName", userLastName);
    formData.append("Email", userEmail);
    formData.append("PhoneNumber", userPhone);
    formData.append("UserDetail.Id", userDetailId !== "" ? userDetailId : 0);
    formData.append("UserDetail.AlternateEmail", userAlternateEmail);
    formData.append("UserDetail.AlternateNumber", userAlternateNo);
    formData.append("Address.AddressId", userAddressId !== "" ? userAddressId : 0);
    formData.append("Address.StateId", userState !== "" ? userState : 0);
    formData.append("Address.CityLocationId", userCity !== "" ? userCity : 0);
    formData.append("Address.ZipCode", userPincode !== "" ? userPincode : 0);
    formData.append("Address.FullAdrrss", userAddress);

    datasave('Company/updateadminemployee', formData).then(data => {
        $("#overlay1").hide();
        if (data.data.status === true && data.data.message === "Updated Successfully") {
            alert(data.data.message);
            window.location = "/Admin/EmployeeList";
        }
    });
}


