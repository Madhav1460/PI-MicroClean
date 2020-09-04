function CheckRequiredFieldsFrnchise() {
    var isValid = true;
    $('.requiredfranchise').each(function () {
        if ($.trim($(this).val()) === '') {
            isValid = false;
            $(this).addClass('red_border');
        }
        else {
            $(this).removeClass('red_border');
        }
    });
    return isValid;
}

function CheckRequiredFieldsCompany() {
    var isValid = true;
    $('.requiredcompany').each(function () {
        if ($.trim($(this).val()) === '') {
            isValid = false;
            $(this).addClass('red_border');
        }
        else {
            $(this).removeClass('red_border');
        }
    });
    return isValid;
}

function CheckRequiredFieldsCompanyKyc() {
    var isValid = true;
    $('.requiredkyc').each(function () {
        if ($.trim($(this).val()) === '') {
            isValid = false;
            $(this).addClass('red_border');
        }
        else {
            $(this).removeClass('red_border');
        }
    });
    return isValid;
}

function validateEmail(email) {
    if (email.value !== '') {
        var reg = /^[\w-\._\+%]+@@(?:[\w-]+\.)+[\w]{2,6}$/;
        if (reg.test(email.value) === false) {
            jQuery('#error_email').html("Invalid email address");
            jQuery('#error_email').show();
            document.getElementById("txtemail").value = '';
            return false;
        } else {
            jQuery('#error_email').html("");
            jQuery('#error_email').hide();
        }
        return true;
    }
}
// Validate Alternate Email
function validateAlternameEmail(email) {
    if (email !== undefined && email !== '') {
        var reg = /^[\w-\._\+%]+@@(?:[\w-]+\.)+[\w]{2,6}$/;
        if (reg.test(email.value) === false) {
            $('#Altererror_email').html("Invalid email address");
            $('#Altererror_email').show();
            document.getElementById("txtAlternateEmail").value = '';
            return false;
        } else {
            $('#Altererror_email').html("");
            $('#Altererror_email').hide();
        }
        return true;
    }
}
// Validate Company Email
function validateCompanyEmail(email) {
    if (email !== undefined && email !== '') {
        var reg = /^[\w-\._\+%]+@@(?:[\w-]+\.)+[\w]{2,6}$/;
        if (reg.test(email.value) === false) {
            $('#error_companyemail').html("Invalid email address");
            $('#error_companyemail').show();
            document.getElementById("txtCompanyEmail").value = '';
            return false;
        } else {
            $('#error_companyemail').html("");
            $('#error_companyemail').hide();
        }
        return true;
    }
}

function isNumberKey(evt) {
    var charcode = (evt.which) ? evt.which : evt.keycode
    if (charcode > 31 && (charcode < 48 || charcode > 58)
        && evt.keyCode !== 35 && evt.keyCode !== 36 && evt.keyCode !== 37
        && evt.keyCode !== 38 && evt.keyCode !== 39 && evt.keyCode !== 40
        && evt.keyCode !== 46) {
        return false;
    } else {
        return true;
    }
}

$('#txtfrnFirstName').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtfrnFirstName').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtfrnFirstName').css('border-color', 'lightgrey');
    }
});
$('#txtfrnLastName').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtfrnLastName').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtfrnLastName').css('border-color', 'lightgrey');
    }
});
$('#txtfrnEmail').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtfrnEmail').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtfrnEmail').css('border-color', 'lightgrey');
    }
});
$('#txtfrnPhoneno').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtfrnPhoneno').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtfrnPhoneno').css('border-color', 'lightgrey');
    }
});

$('#txtfrnPincode').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtDob').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtDob').css('border-color', 'lightgrey');
    }
});