$(document).ready(function () {
    getuserinfo();
    GetAllState();
    GetAllStateClient();
    EnableDigitValidation();   
});
function getuserinfo() {
    $("#overlay1").show();
    var obj = JSON.parse(sessionStorage.userData);
    console.log(obj);
    $('#hdnUserId').val(obj.user.id);
    $('#txtCompanyName').val(obj.companyName);
    $('#txtFirstName').val(obj.user.firstName);
    $('#txtMiddleName').val(obj.user.middleName);
    $('#txtLastName').val(obj.user.lastName);
    $('#txtEmail').val(obj.user.email);
    $('#txtPhone').val(obj.user.phoneNumber);
    $('#txtAlternateEmail').val(obj.user.userDetail.alternateEmail);
    $('#txtAlternateNumber').val(obj.user.userDetail.alternateNumber);
    $('#hdnfrnUserDetailId').val(obj.user.userDetail.id);
    if (obj.user.addresses.length > 0) {
        $('#hdnfrnAddressId').val(obj.user.addresses[0].addressId);
        $('#ddlState').attr("data-value", obj.user.addresses[0].stateId);
        $('#ddldistrictid').attr("data-value", obj.user.addresses[0].districtId);
        $('#ddlcitylocation').attr("data-value", obj.user.addresses[0].cityLocationId);
        $('#txtAddress').val(obj.user.addresses[0].fullAddress);
        $('#txtPincode').val(obj.user.addresses[0].userZip);
    }
    if (obj.user.feeDetails.length > 0) {
        $("#txtFee_0").val(obj.user.feeDetails[0].feeValue);
        $("#txtFee_1").val(obj.user.feeDetails[1].feeValue);
        $("#txtFee_2").val(obj.user.feeDetails[2].feeValue);
        $("#txtFee_3").val(obj.user.feeDetails[3].feeValue);
        $("#txtPaymentTerms_0").val(obj.user.feeDetails[0].paymentTerms);
        $("#txtPaymentDueDate_0").val(obj.user.feeDetails[0].paymentDueDate);
        $("#txtTotalFee_0").val(obj.user.totalFee);
        $('#hddnFeeValId_0').val(obj.user.feeDetails[0].feeId);
        $('#hddnFeeValId_1').val(obj.user.feeDetails[1].feeId);
        $('#hddnFeeValId_2').val(obj.user.feeDetails[2].feeId);
        $('#hddnFeeValId_3').val(obj.user.feeDetails[3].feeId);
    }
    $('#hdnCompanyId').val(obj.companyId);
    $('#txtCompanyEmail').val(obj.companyEmail);
    $('#txtCompanyPhoneno').val(obj.companyContactNo);

    $('#ddlStateClient').attr("data-value", obj.stateId);
    $('#ddldistrictidClient').attr("data-value", obj.districtId);
    $('#ddlcitylocationClient').attr("data-value", obj.cityLocationId);

    $('#txtcompanyAddress').val(obj.companyAddress);
    $('#txtCompanyPincode').val(obj.companyZip);
    $("#hdnfrnUserDetailId").val(obj.user.userDetail.id);
    $("#overlay1").hide();
}

function franchiseeUpdateByAdmin() {
    $("#overlay1").show();
    debugger;
    let userDetailId = $('#hdnfrnUserDetailId').val();
    let userId = $('#hdnUserId').val();

    var formData = new FormData();
    let userfirstnameval = $('#txtFirstName').val();
    let usermiddlenameval = $('#txtMiddleName').val();
    let userlastnameval = $('#txtLastName').val();
    let useremailval = $('#txtEmail').val();
    let userphoneval = $('#txtPhone').val();
    let userAlternateEmail = $('#txtAlternateEmail').val();
    let userAlternateNo = $('#txtAlternateNumber').val();
    //-----------------------------------------------------------------------------------------
    let userAddressId = $('#hdnfrnAddressId').val();
    let userState = $("#ddlState :selected").val() != undefined ? $("#ddlState :selected").val() : 0;
    let userCity = $("#ddlcitylocation :selected").val();
    let userPincode = $("#txtPincode").val() != "" ? $('#txtPincode').val() : 0;
    let userAddress = $('#txtAddress').val();
    //-----------------------------------------------------------------------------
    let companyId = $('#hdnCompanyId').val();
    let companyName = $('#txtCompanyName').val();
    let companyEmail = $('#txtCompanyEmail').val();
    let companyPhoneNo = $("#txtCompanyPhoneno").val() != "" ? $('#txtCompanyPhoneno').val() : 0;
    let companyCityLocation = $("#ddlcitylocationClient :selected").val() != undefined ? $("#ddlcitylocationClient :selected").val() : 0;
    let companyPincode = $("#txtCompanyPincode").val() != "" ? $('#txtCompanyPincode').val() : 0;
    let companyAddress = $('#txtcompanyAddress').val();

    //------------------------------------------------------------------------------------------------------
    formData.append("UserId", userId);
    formData.append("FirstName", userfirstnameval);
    formData.append("MiddleName", usermiddlenameval);
    formData.append("LastName", userlastnameval);
    formData.append("Email", useremailval);
    formData.append("PhoneNumber", userphoneval);

    formData.append("UserDetail.Id", userDetailId !== "" ? userDetailId : 0);
    formData.append("UserDetail.AlternateEmail", userAlternateEmail);
    formData.append("UserDetail.AlternateNumber", userAlternateNo);

    formData.append("Address.AddressId", userAddressId !== "" ? userAddressId : 0);
    formData.append("Address.StateId", userState !== "" ? userState : 0);
    formData.append("Address.CityLocationId", userCity !== "" ? userCity : 0);
    formData.append("Address.ZipCode", userPincode !== "" ? userPincode : 0);
    formData.append("Address.FullAdrrss", userAddress);

    formData.append("CompanyId", companyId);
    formData.append("CompayName", companyName);
    formData.append("CompayEmail", companyEmail);
    formData.append("CompayPhone", companyPhoneNo);
    formData.append("CompanyAddressCity", companyCityLocation);
    formData.append("CompanyAddress", companyAddress);
    formData.append("CompanyPincode", companyPincode);

    var feeTotal = $('#txtTotalFee_0').val();
    var feePaymentTerms = $('#txtPaymentTerms_0').val();
    var feePaymentDueDate = $('#txtPaymentDueDate_0').val();
    for (var i = 0; i < 4; i++) {
        var feeType = $('#hdnFeeType_' + i).val();
        var feeValId = $('#hddnFeeValId_' + i).val();
        var feeval = $('#txtFee_' + i).val();
        formData.append("FranchiseeFeeCommands[" + i + "].FeeTypeId", feeType);
        formData.append("FranchiseeFeeCommands[" + i + "].FeeId", feeValId);
        formData.append("FranchiseeFeeCommands[" + i + "].FeeValue", parseFloat(feeval));
        formData.append("FranchiseeFeeCommands[" + i + "].TotalFee", parseFloat(feeTotal));
        formData.append("FranchiseeFeeCommands[" + i + "].PaymentTerms", feePaymentTerms);
        formData.append("FranchiseeFeeCommands[" + i + "].PaymentDueDate", feePaymentDueDate);
    }

    datasave('Franchisee/franchiseeitselfprofileupdate', formData).then(data => {
        debugger;
        $("#overlay1").hide();
        if (data.data.status === true && data.data.message === "Updated Successfully") {
            alert(data.data.message);
            window.location.href = "/Admin/Index";
        }
    });
}

function FirstSteap() {
    $("#activity").hide();
    $("#timeline").show();
    $("#settings").hide();
    $("#time_line").addClass("active");
    $("#presonal_information").removeClass("active");
    $("#setting").removeClass("active");
}
function SecondSteap() {
    $("#activity").hide();
    $("#timeline").hide();
    $("#settings").show();
    $("#time_line").removeClass("active");
    $("#presonal_information").removeClass("active");
    $("#setting").addClass("active");
}
function backTofirstSteap() {
    $("#activity").show();
    $("#timeline").hide();
    $("#settings").hide();
    $("#time_line").removeClass("active");
    $("#presonal_information").addClass("active");
    $("#setting").removeClass("active");
}
function backToSecondSteap() {
    $("#activity").hide();
    $("#timeline").show();
    $("#settings").hide();
    $("#time_line").addClass("active");
    $("#presonal_information").removeClass("active");
    $("#setting").removeClass("active");
}
function Personal() {
    $("#activity").show();
    $("#timeline").hide();
    $("#settings").hide();
}
function Timeline() {
    $("#activity").hide();
    $("#timeline").show();
    $("#settings").hide();
}
function Commercial() {
    $("#activity").hide();
    $("#timeline").hide();
    $("#settings").show();
}

$(function () {
    $("#txtFee_3").change(function () {
        if ($('#txtFee_0').val() !== undefined || $('#txtFee_0').val() !== "") {
            var fiftypercentoffee = (parseFloat($('#txtFee_0').val() * 50) / 100);
            var totalFee = parseFloat($('#txtFee_0').val()) + (parseFloat($('#txtFee_1').val()));
            if (fiftypercentoffee > parseFloat($("#txtFee_3").val())) {
                alert('Threshold Amount not less then 50% of Franchisee Fee. ' + fiftypercentoffee + '');
                $("#txtFee_3").val(0);
                $('#btnSave').attr("disabled", true);
                return false;
            } else {
                if (totalFee < parseFloat($("#txtFee_3").val())) {
                    alert('Threshold Amount should not be more than from Total Due Amount.');
                    $("#txtFee_3").val(0);
                    $('#btnSave').attr("disabled", true);
                    return false;
                }
            }
            $('#btnSave').attr("disabled", false);
        }
    });
});