$(function () {
    $("#txtMinimumThresholdDueAmount").change(function () {
        if ($('#txtFranchiseeFee').val() !== undefined || $('#txtFranchiseeFee').val() !== "") {
            var fiftypercentoffee = (parseFloat($('#txtFranchiseeFee').val() * 50) / 100);
            var totalFee = parseFloat($('#txtFranchiseeFee').val()) + (parseFloat($('#txtOtherFee').val()));
            if (fiftypercentoffee > parseFloat($("#txtMinimumThresholdDueAmount").val())) {
                alert('Threshold Amount not less then 50% of Franchisee Fee. ' + fiftypercentoffee + '');
                $("#txtMinimumThresholdDueAmount").val('')
                return false;
            } else {
                if (totalFee < parseFloat($("#txtMinimumThresholdDueAmount").val())) {
                    alert('Threshold Amount should not be more than from Total Due Amount.');
                    $("#txtMinimumThresholdDueAmount").val('')
                    return false;
                }
            }
        }
    });
});

function franchiseeRegistrationByAdmin() {
    $("#overlay1").show();
    var formData = new FormData();
    let userfirstnameval = $('#txtFirstName').val();
    let usermiddlenameval = $('#txtMiddleName').val();
    let userlastnameval = $('#txtLastName').val();
    let useremailval = $('#txtEmail').val();
    let userphoneval = $('#txtPhone').val();
    let userAlternateEmail = $('#txtAlternateEmail').val();
    let userAlternateNo = $('#txtAlternateNumber').val();
    let userpasswordval = $('#txtPassword').val();
    let userreinterpasswordval = $('#txtreinterpassword').val();
    let userCityLocation = $("#ddlcitylocation :selected").val() !== undefined ? $("#ddlcitylocation :selected").val() : 0;
    let userPincode = $("#txtPincode").val() !== "" ? $('#txtPincode').val() : 0;
    let userAddress = $('#txtAddress').val();

    let companyName = $('#txtCompanyName').val();
    let companyEmail = $('#txtCompanyEmail').val();
    let companyPhoneNo = $("#txtCompanyPhoneno").val() !== "" ? $('#txtCompanyPhoneno').val() : 0;
    let companyCityLocation = $("#ddlcitylocationClient :selected").val() !== undefined ? $("#ddlcitylocationClient :selected").val() : 0;
    let companyPincode = $("#txtCompanyPincode").val() !== "" ? $('#txtCompanyPincode').val() : 0;
    let companyAddress = $('#txtcompanyAddress').val();

    let commercialFranchFee = $('#txtFranchiseeFee').val() !== "" ? $('#txtFranchiseeFee').val() : 0;
    let commercialOtherFee = $('#txtOtherFee').val() !== "" ? $('#txtOtherFee').val() : 0;
    let commercialTotalFee = $('#txtTotalFee').val() !== "" ? $('#txtTotalFee').val() : 0;
    let commercialMonthlyFixedFee = $('#txtMonthlyFixedFee').val() !== "" ? $('#txtMonthlyFixedFee').val() : 0;
    let commercialMinimumThreshold = $('#txtMinimumThresholdDueAmount').val() !== "" ? $('#txtMinimumThresholdDueAmount').val() : 0;
    let commercialPaymentTerms = $("#txtPaymentTerms").val();
    let commercialPaymentDueDate = $('#txtPaymentDueDate').val() !== "" ? $('#txtPaymentDueDate').val() : "";
    let franchiseefeeidval = $('#hdnFranchiseeFee').val();
    let otherfeeidval = $('#hdnOtherFee').val();
    let fixedfeeidval = $('#hdnFixedMonthlyFee').val();
    let thresholdidval = $('#hdnThresholdAmount').val();

    formData.append("FirstName", userfirstnameval);
    formData.append("MiddleName", usermiddlenameval);
    formData.append("LastName", userlastnameval);
    formData.append("Email", useremailval);
    formData.append("PhoneNumber", userphoneval);
    formData.append("AlternateEmail", userAlternateEmail);
    formData.append("AlternatePhone", userAlternateNo);
    formData.append("PasswordHash", userpasswordval);
    formData.append("ConfirmPassword", userreinterpasswordval);
    formData.append("Address.CityLocationId", userCityLocation);
    formData.append("Address.ZipCode", userPincode);
    formData.append("Address.FullAdrrss", userAddress);

    formData.append("CompanyName", companyName);
    formData.append("CompanyEmail", companyEmail);
    formData.append("CompanyPhone", companyPhoneNo);
    formData.append("CityLocationId", companyCityLocation);
    formData.append("CompanyAddress", companyAddress);
    formData.append("Pincode", companyPincode);

    formData.append("FranchiseeFee.FranchiseeFee", parseFloat(commercialFranchFee));
    formData.append("FranchiseeFee.OtherFee", parseFloat(commercialOtherFee));
    formData.append("FranchiseeFee.TotalAmmount", parseFloat(commercialTotalFee));
    formData.append("FranchiseeFee.PaymentTerms", commercialPaymentTerms);
    formData.append("FranchiseeFee.PaymentDueDate", commercialPaymentDueDate);
    formData.append("FranchiseeFee.FixedMonthlyFee", parseFloat(commercialMonthlyFixedFee));
    formData.append("FranchiseeFee.ThresholdAmount", parseFloat(commercialMinimumThreshold));

    formData.append("FranchiseeFee.FranchiseeFeeId", franchiseefeeidval);
    formData.append("FranchiseeFee.OtherFeeId", otherfeeidval);
    formData.append("FranchiseeFee.FixedMonthlyFeeId", fixedfeeidval);
    formData.append("FranchiseeFee.ThresholdAmountId", thresholdidval);
    formData.append("UserTypeId", 2);

    datasave('company/createfranchisee', formData, 'admin').then(data => {
        $("#overlay1").hide();
        if (data.data !== null) {
            if (data.data.status === false) {
                alert(data.data.message);
            } else {
                alert(data.data.message);
            }
            window.location = "/Admin/Index";
        }
        else {
            alert("Error in saving data!");
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
function saveFirstSteap() {
    if (CheckRequiredFields()) {
        franchiseeRegistrationByAdmin();
    }
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
function saveSecondSteap() {
    
    franchiseeRegistrationByAdmin();
}
function savefinalSteap() {
    franchiseeRegistrationByAdmin();
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

