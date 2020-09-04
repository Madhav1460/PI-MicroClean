function getuserinfo() {
    $("#overlay1").show();
    var url = 'Franchisee/getuserdetailbyId'
    GetProfile(url).then(data => {
        console.log(data);
        GetAllState();
        GetAllStateClient();
        //------------------------------------------------------------------------------------------------------//
        debugger;
        var html = '';
        var a = 1;
        if (data.data.paymentDetails != null) {
            for (var i = 0; i < data.data.paymentDetails.length; i++) {
                html += '<tr>';
                html += '<td>' + a++ + '</td>';
                html += '<td>' + data.data.paymentDetails[i].paymentDate + '</td>';
                html += '<td>' + data.data.paymentDetails[i].paymentRef + '</td>';
                html += '<td>' + data.data.paymentDetails[i].paymentType + '</td>';
                html += '<td>' + parseFloat(data.data.paymentDetails[i].paidAmout).toFixed(2) + '</td>';
                html += '</tr>';
            }
        } else {
            html += `<span style="text-align:center;">No Payment Details Found!</span>`;
        }
        $('.tbody').html(html);
        //------------------------------------------------------------------------------------------------------//
        $('#txtFirstName').val(data.data.user.firstName);
        $('#txtMiddleName').val(data.data.user.middleName);
        $('#txtLastName').val(data.data.user.lastName);
        $('#txtEmail').val(data.data.user.email);
        $('#txtPhone').val(data.data.user.phoneNumber);
        $('#txtAlternateEmail').val(data.data.user.userDetail.alternateEmail);
        $('#txtAlternateNumber').val(data.data.user.userDetail.alternateNumber);
        $('#hdnfrnUserDetailId').val(data.data.user.userDetail.id);
        if (data.data.user.addresses.length > 0) {
            $('#hdnfrnAddressId').val(data.data.user.addresses[0].addressId);
            $('#ddlState').attr("data-value", data.data.user.addresses[0].stateId);
            $('#ddldistrictid').attr("data-value", data.data.user.addresses[0].districtId);
            $('#ddlcitylocation').attr("data-value", data.data.user.addresses[0].cityLocationId);
            $('#txtAddress').val(data.data.user.addresses[0].fullAddress);
            $('#txtPincode').val(data.data.user.addresses[0].userZip);
        }
        if (data.data.user.feeDetails.length > 0) {
            $("#txtFranchiseeFee").val(data.data.user.feeDetails[0].feeValue);
            $("#txtOtherFeeCost").val(data.data.user.feeDetails[1].feeValue);
            $("#txtFixedMonthlyFee").val(data.data.user.feeDetails[2].feeValue);
            $("#txtMinimumThresholdDueAmount").val(data.data.user.feeDetails[3].feeValue);
            $("#txtPaymentTerm").val(data.data.user.feeDetails[3].paymentTerms);
            $("#txtTotalAmount").val(data.data.user.totalFee);
        }
        $('#txtCompanyName').val(data.data.companyName);
        $('#txtCompanyEmail').val(data.data.companyEmail);
        $('#txtCompanyPhoneno').val(data.data.companyContactNo);

        $('#ddlStateClient').attr("data-value", data.data.stateId);
        $('#ddldistrictidClient').attr("data-value", data.data.districtId);
        $('#ddlcitylocationClient').attr("data-value", data.data.cityLocationId);

        $('#txtcompanyAddress').val(data.data.companyAddress);
        $('#txtCompanyPincode').val(data.data.companyZip);
        $("#hdnfrnUserDetailId").val(data.data.user.userDetail.id);
        $('#txtOwnersAadharCardNo').val(data.data.user.userDetail.userAdharCardNo);
        $('#txtOwnerPANCardNo').val(data.data.user.userDetail.userPAN);
        $('#txtCompanyPANCardNo').val(data.data.comapnyPAN);
        $('#txtCompanyGSTNo').val(data.data.companyGSTNo);
       
        for (var i = 0; i < data.data.user.userDocument.length; i++) {
            if (data.data.user.userDocument[i].documentTypeId == 1) {
                $('#hdnDocId_1').val(data.data.user.userDocument[i].docId);// need to attach property
                $('#selectDocumenttext_0').val(data.data.user.userDocument[i].remark);
                $('#txtAdharDate').val(data.data.user.userDocument[i].documentUploadDate);
            }
            if (data.data.user.userDocument[i].documentTypeId == 2) {
                $('#hdnDocId_2').val(data.data.user.userDocument[i].docId);// need to attach property
                $('#selectDocumenttext_1').val(data.data.user.userDocument[i].remark);
                $('#txtOwnerPANDate').val(data.data.user.userDocument[i].documentUploadDate);
            }
            if (data.data.user.userDocument[i].documentTypeId == 3) {
                $('#hdnDocId_3').val(data.data.user.userDocument[i].docId);// need to attach property
                $('#selectDocumenttext_2').val(data.data.user.userDocument[i].remark);
                $('#txtOwnerPhotoDate').val(data.data.user.userDocument[i].documentUploadDate);
            }
            if (data.data.user.userDocument[i].documentTypeId == 4) {
                $('#hdnDocId_4').val(data.data.user.userDocument[i].docId);// need to attach property
                $('#selectDocumenttext_3').val(data.data.user.userDocument[i].remark);
                $('#txtCompanyPANCardDate').val(data.data.user.userDocument[i].documentUploadDate);
            }
            if (data.data.user.userDocument[i].documentTypeId == 5) {
                $('#hdnDocId_5').val(data.data.user.userDocument[i].docId);// need to attach property
                $('#selectDocumenttext_4').val(data.data.user.userDocument[i].remark);
                $('#txtCompanyGSTNoDate').val(data.data.user.userDocument[i].documentUploadDate);
            }
            if (data.data.user.userDocument[i].documentTypeId == 7) {
                $('#hdnDocId_6').val(data.data.user.userDocument[i].docId);// need to attach property
                $('#selectDocumenttext_5').val(data.data.user.userDocument[i].remark);
                $('#txtSignedLOIDate').val(data.data.user.userDocument[i].documentUploadDate);
            }
            if (data.data.user.userDocument[i].documentTypeId == 6) {
                $('#txtLOIFileName').val(data.data.user.userDocument[i].remark);
                //$('#txtSignedLOIDate').val(data.data.user.userDocument[i].documentUploadDate);
            }
        }
        $("#overlay1").hide();
    });
}

function franchiseeSelfProfileUpdate() {
    debugger;
    $("#overlay1").show();
    var formData = new FormData();
    //if (CheckRequiredFields()) {
    let userFirstName = $('#txtFirstName').val();
    let userMiddleName = $('#txtMiddleName').val();
    let userLastName = $('#txtLastName').val();
    let userEmail = $('#txtEmail').val();
    let userPhone = $('#txtPhone').val();
    let userDetailId = $('#hdnfrnUserDetailId').val();
    let userAlternateEmail = $('#txtAlternateEmail').val();
    let userAlternateNo = $('#txtAlternateNumber').val();
    let userAddressId = $('#hdnfrnAddressId').val();
    let userState = $("#ddlState :selected").val();
    let userCity = $("#ddlcitylocation :selected").val();
    let userPincode = $('#txtPincode').val();
    let userAddress = $('#txtAddress').val();
    let userCompanyName = $('#txtCompanyName').val();
    let userCompanyEmail = $('#txtCompanyEmail').val();
    let userCompanyPhoneNo = $('#txtCompanyPhoneno').val();
    let CompanyAddress = $('#txtcompanyAddress').val();
    let userCompanyPincode = $('#txtCompanyPincode').val();
    let CompanyAddressCity = $("#ddlcitylocation :selected").val();
    let CompanyPANCardNo = $('#txtCompanyPANCardNo').val();
    let CompanyGSTNo = $('#txtCompanyGSTNo').val();
    let OwnerPANCardNo = $('#txtOwnerPANCardNo').val();
    let OwnersAadharCardNo = $('#txtOwnersAadharCardNo').val();

    formData.append("FirstName", userFirstName);
    formData.append("MiddleName", userMiddleName);
    formData.append("LastName", userLastName);
    formData.append("Email", userEmail);
    formData.append("PhoneNumber", userPhone);
    formData.append("UserDetail.Id", userDetailId !== "" ? userDetailId : 0);
    formData.append("UserDetail.AlternateEmail", userAlternateEmail);
    formData.append("UserDetail.AlternateNumber", userAlternateNo);
    formData.append("UserDetail.OwnerPANCardNo", OwnerPANCardNo);
    formData.append("UserDetail.OwnersAadharCardNo", OwnersAadharCardNo);

    formData.append("Address.AddressId", userAddressId !== "" ? userAddressId : 0);
    formData.append("Address.StateId", userState !== "" ? userState : 0);
    formData.append("Address.CityLocationId", userCity !== "" ? userCity : 0);
    formData.append("Address.ZipCode", userPincode !== "" ? userPincode : 0);
    formData.append("Address.FullAdrrss", userAddress);

    formData.append("CompayName", userCompanyName);
    formData.append("CompayEmail", userCompanyEmail);
    formData.append("CompayPhone", userCompanyPhoneNo);
    formData.append("CompanyAddress", CompanyAddress);
    formData.append("CompanyAddressCity", CompanyAddressCity);
    formData.append("CompanyPincode", userCompanyPincode);

    formData.append("CompanyPANCardNo", CompanyPANCardNo);
    formData.append("CompanyGSTNo", CompanyGSTNo);

    $('.docbox').each(function (i) {
        var docFile = document.getElementById("docfile_" + (i + 1) + "").files[0];
        var docid = $('#hdnDocId_' + (i + 1) + '').val();
        if (docid !== "") {
            formData.append("UserDocumentCommands[" + i + "].Id", docid);
        } else {
            formData.append("UserDocumentCommands[" + i + "].Id", 0);
        }
        if (docFile !== undefined) {
            var temp = $('#selectDocument_' + (i + 1) + '').val();
            if (temp !== "" || temp !== undefined) {
                formData.append("UserDocumentCommands[" + i + "].DocumentType", temp);
                formData.append("UserDocumentCommands[" + i + "].DocFile", docFile);
            }
        }
    });
    datasave('Franchisee/franchiseeitselfprofileupdate', formData).then(data => {
        debugger;
        $("#overlay1").hide();
        if (data.data.status === true && data.data.message === "Updated Successfully") {
            alert(data.data.message);
            window.location = "/Account/GetUserDetail";
        }
    });
}

function isNumberKey(evt) {
    var charcode = (evt.which) ? evt.which : evt.keycode
    if (charcode > 31 && (charcode < 48 || charcode > 58)
        && evt.keyCode != 35 && evt.keyCode != 36 && evt.keyCode != 37
        && evt.keyCode != 38 && evt.keyCode != 39 && evt.keyCode != 40
        && evt.keyCode != 46) {
        return false;
    } else {
        return true;
    }
}

function gstFunction() {
    var maxLength = 15;
    var value = $("#txtCompanyGSTNo").val();
    if (value.length < maxLength) {
        $("#errorGSTNumber").html("GST number should not be less than 15 digit");
        $("#errorGSTNumber").show();
        $("#txtCompanyGSTNo").css('border-color', 'red');
        $("#txtCompanyGSTNo").val("");
    } else if (value.length > maxLength) {
        $("#errorGSTNumber").html("GST number must be in 15 digit");
        $("#errorGSTNumber").show();
        $("#txtCompanyGSTNo").css('border-color', 'red');
        $("#txtCompanyGSTNo").val("");
    } else {
        var inputvalues = $("#txtCompanyGSTNo").val();
        var gstinformat = new RegExp(/^([0-9]{2}[a-zA-Z]{4}([a-zA-Z]{1}|[0-9]{1})[0-9]{4}[a-zA-Z]{1}([a-zA-Z]|[0-9]){3}){0,15}$/);
        if (gstinformat.test(inputvalues)) {
            $('#errorGSTNumber').html("");
            $('#errorGSTNumber').hide();
            $("#txtCompanyGSTNo").css('border-color', 'lightgrey');
            return true;
        } else {
            $("#errorGSTNumber").html("Please enter valid GST No(Sample GST No: 05ABDCE1234F1Z2)");
            $("#errorGSTNumber").show();
            $("#txtCompanyGSTNo").css('border-color', 'red');
            $("#txtCompanyGSTNo").val("");

        }
    }
}

function ownerPANCardNoFunction() {
    var maxLength = 10;
    var value = $("#txtOwnerPANCardNo").val();
    if (value.length < maxLength) {
        $('#errorOwnerPANCard').html("Owner PAN card number should not be less than 10 digit");
        $('#errorOwnerPANCard').show();
        $("#txtOwnerPANCardNo").css('border-color', 'red');
        $("#txtOwnerPANCardNo").val("");
    } else if (value.length > maxLength) {
        $('#errorOwnerPANCard').html("Owner PAN card number must be in 10 digit");
        $('#errorOwnerPANCard').show();
        $("#txtOwnerPANCardNo").css('border-color', 'red');
        $("#txtOwnerPANCardNo").val("");
    }
    else {
        var regex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
        var inputvalues = $(txtOwnerPANCardNo).val();
        if (!regex.test(inputvalues)) {
            $('#errorOwnerPANCard').html("Invalid format of PAN card. (Sample PAN No: ABCDE1234F");
            $('#errorOwnerPANCard').show();
            $("#txtOwnerPANCardNo").css('border-color', 'red');
            $("#txtOwnerPANCardNo").val("");
        } else {
            $('#errorOwnerPANCard').html("");
            $('#errorOwnerPANCard').hide();
            $("#txtOwnerPANCardNo").css('border-color', 'lightgrey');
        }
    }
}

function companyPANCardNoFunction() {
    var maxLength = 10;
    var value = $("#txtCompanyPANCardNo").val();
    if (value.length < maxLength) {
        $('#errorCompanyPANCard').html("Company PAN card number should not be less than 10 digit");
        $('#errorCompanyPANCard').show();
        $("#txtCompanyPANCardNo").css('border-color', 'red');
        $("#txtCompanyPANCardNo").val("");
    } else if (value.length > maxLength) {
        $('#errorCompanyPANCard').html("Company PAN card number must be in 10 digit");
        $('#errorCompanyPANCard').show();
        $("#txtCompanyPANCardNo").css('border-color', 'red');
        $("#txtCompanyPANCardNo").val("");
    }
    else {
        //$('#errorCompanyPANCard').html("");
        //$('#errorCompanyPANCard').hide();
        //$("#txtCompanyPANCardNo").css('border-color', 'lightgrey');
        var regex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
        var inputvalues = $(txtOwnerPANCardNo).val();
        if (!regex.test(inputvalues)) {
            $('#errorCompanyPANCard').html("Invalid format of PAN card. (Sample PAN No: ABCDE1234F");
            $('#errorCompanyPANCard').show();
            $("#txtCompanyPANCardNo").css('border-color', 'red');
            $("#txtCompanyPANCardNo").val("");
        } else {
            $('#errorCompanyPANCard').html("");
            $('#errorCompanyPANCard').hide();
            $("#txtCompanyPANCardNo").css('border-color', 'lightgrey');
        }
    }
}

function adharCardNoFunction() {
    var maxLength = 12;
    var value = $("#txtOwnersAadharCardNo").val();
    if (value.length < maxLength) {
        $('#errorOwnerAadharCard').html("Adhar Card should not be less than 12 digit");
        $('#errorOwnerAadharCard').show();
        $("#txtOwnersAadharCardNo").css('border-color', 'red');
        $("#txtOwnersAadharCardNo").val("");
    } else if (value.length > maxLength) {
        $('#errorOwnerAadharCard').html("Adhar Card must be 12 in digit");
        $('#errorOwnerAadharCard').show();
        $("#txtOwnersAadharCardNo").css('border-color', 'red');
        $("#txtOwnersAadharCardNo").val("");
    }
    else {
        $('#errorOwnerAadharCard').html("");
        $('#errorOwnerAadharCard').hide();
        $("#txtOwnersAadharCardNo").css('border-color', 'lightgrey');
    }
}