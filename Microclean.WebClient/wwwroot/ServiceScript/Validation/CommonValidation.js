var isValid = true;
function CheckRequiredFields() {
    var isValid = true;
    $('.required').each(function () {
        if ($.trim($(this).val()) == '') {
            isValid = false;
            $(this).addClass('red_border');
        }
        else {
            $(this).removeClass('red_border');
        }
    });
    return isValid;
}

function isNumberKey(evt) {//onkeypress="return isNumberKey(event);"
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

function toggleDisabled(target) {
    debugger;
    var confirm = $("input:checkbox:checked");
    if (!confirm.length > 0) {
        $("#confirm").addClass('disabled');
        //$("#btnsubmit").addClass('disabled');
        $('#btnsubmit').attr('disabled', true);
    } else {
        $("#confirm").removeClass('disabled');
        //$("#btnsubmit").removeClass('disabled');
        $('#btnsubmit').attr('disabled', false);
    }
}
var $notnull = $('.notnull');
$(document).on('keyup change', '.notnull', function (e) {
    console.log(e.currentTarget)
    toggleDisabled(e.currentTarget);

});
$notnull.each(function (i, el) {
    toggleDisabled(el);
});
function reload() {
    $notnull.each(function (i, el) {
        toggleDisabled(el);
    });
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    //return pattern.test(emailAddress);
    var emailPattern = pattern.test(emailAddress);
    return emailPattern;
}

function EnableDigitValidation() {
    $('.onlyInt').keypress(function (event) {
        var $this = $(this);
        if ((event.which !== 46 || $this.val().indexOf('.') !== -1) &&
            ((event.which < 48 || event.which > 57) &&
                (event.which !== 0 && event.which !== 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which === 46) && (text.indexOf('.') === -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }
        if ((text.indexOf('.') !== -1) &&
            (text.substring(text.indexOf('.')).length > 2) &&
            (event.which !== 0 && event.which !== 8) &&
            ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });
    $('.onlyInt').bind("paste", function (e) {
        var text = e.originalEvent.clipboardData.getData('Text');
        if ($.isNumeric(text)) {
            if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
                e.preventDefault();
                $(this).val(text.substring(0, text.indexOf('.') + 3));
            }
        }
        else {
            e.preventDefault();
        }
    });
}

$('.summition').on('input', function () {
    var total = 0;
    $('.summition').each(function () {
        total += parseFloat($(this).val()) || 0;
    });
    if (total > 0) {
        $('.summitiontotal').val(parseFloat(total));
    } else {
        $('.summitiontotal').val(parseFloat(total));
    }
});

$('#txtFirstName').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() == "") {
        $('#txtFirstName').css('border-color', 'Red');
        $('#error_firstname').html("First name is required");
        $('#error_firstname').show();
        isValid = false;
        $('.btnvalid').attr('disabled', true);
    } else {
        $('#txtFirstName').css('border-color', 'lightgrey');
        $('#error_firstname').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

$('#txtLastName').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() == "") {
        $('#txtLastName').css('border-color', 'Red');
        $('#error_lastname').html("Last name is required");
        $('#error_lastname').show();
        $('.btnvalid').attr('disabled', true);
        isValid = false;
    } else {
        $('#txtLastName').css('border-color', 'lightgrey');
        $('#error_lastname').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

$('.pwdmath').keyup(function () {
    debugger;
    var password = $("#txtPassword").val();
    var confirmPassword = $("#txtreinterpassword").val();
    if (password == "") {
        $('#txtPassword').css('border-color', 'Red');
        $('#error_password').html("Password are required");
        $('#error_password').show();
        $('.btnvalid').attr('disabled', true);
        isValid = false;
        $('#txtreinterpassword').css('border-color', 'lightgrey');
        $('#error_confirm_password').html("");
        $('#error_confirm_password').hide();
        $("#txtreinterpassword").val("");
        return false;
    }
    if (password != confirmPassword) {
        $('#txtPassword').css('border-color', 'lightgrey');
        $('#error_password').html("");        
        $('#error_confirm_password').html("Password and Confirm Password do not match.");
        $('#error_confirm_password').show();
        $('#txtreinterpassword').css('border-color', 'Red');
        $('.btnvalid').attr('disabled', true);
        isValid = false;
        return false;
    } else {
        $('#txtreinterpassword').css('border-color', 'lightgrey');
        $('#error_confirm_password').html("");
        $('#error_confirm_password').hide();
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
    return true;
});

$(".phonedigit").keyup(function () {
    debugger;
    var phoneValue = $("#txtPhone").val();
    if (phoneValue.trim() == "") {
        $('#errorPhoneMsg').html("");
        $('#txtPhone').css('border-color', 'Red');
        $('#errorPhoneMsg').html("Phone no is required");
        $('.btnvalid').attr('disabled', true);
        isValid = false;
    } else {
        $('#errorPhoneMsg').html("");
        $('#txtPhone').css('border-color', 'lightgrey');
        $('.btnvalid').attr('disabled', false);
        isValid = true;
        if (phoneValue.length != 10) {
            $('#txtPhone').css('border-color', 'red');
            $('#errorPhoneMsg').html("");
            $('#errorPhoneMsg').html("Required 10 digits, match requested format!");
            $('#errorPhoneMsg').show();
            $('.btnvalid').attr('disabled', true);
            isValid = false;
        } else {
            $('#txtPhone').css('border-color', 'lightgrey');
            $('#errorPhoneMsg').html("");
            $('#errorPhoneMsg').hide();
            $('.btnvalid').attr('disabled', false);
            isValid = true;
        }
    } 
});

$(".alternate-phonedigit").keyup(function () {
    debugger;
    var phoneValue = $("#txtAlternateNumber").val();
    if (phoneValue == "") {
        $('#txtAlternateNumber').css('border-color', 'lightgrey');
        $('#errorAlternatePhoneMsg').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
        return;
    }
    if (phoneValue.length != 10) {
        $('#txtAlternateNumber').css('border-color', 'red');
        $('#errorAlternatePhoneMsg').html("Required 10 digits, match requested format!");
        $('#errorAlternatePhoneMsg').show();
        $('.btnvalid').attr('disabled', true);
        isValid = false;
    }
    else {        
        $('#txtAlternateNumber').css('border-color', 'lightgrey');
        $('#errorAlternatePhoneMsg').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

$(".onlyChar").keyup(function (e) {
    var regex = /^[a-zA-Z]+$/;
    if (regex.test(this.value) !== true)
        this.value = this.value.replace(/[^a-zA-Z]+/, '');
});

$(".onlyDigit").keyup(function (e) {
    if (/\D/g.test(this.value)) {
        this.value = this.value.replace(/\D/g, '');
    }
});

$(".pinlength").keyup(function (e) {
    debugger;
    var pinVal = $("#txtPincode").val();
    var pattern = new RegExp("^[0-9]{6}$");
    if (pinVal.trim() == "") {
        $('#error_pincode').html("");
        $('#txtPincode').css('border-color', 'Red');
        $('#error_pincode').html("Pincode is required");
        $('#error_pincode').show();
        isValid = false;
    } else {
        $('#error_pincode').html("");
        $('#txtPincode').css('border-color', 'lightgrey');        
        if (pattern.test(pinVal) == false) {
            $('#txtPincode').css('border-color', 'Red');
            $('#error_pincode').html("Required 6 digits, match requested format!");
            $('#error_pincode').show();
            isValid = false;
        } else {
            $('#txtPincode').css('border-color', 'lightgrey');
            $('#error_pincode').html("");
            isValid = true;
        }
    }
});

$(".fullAddress").keyup(function (e) {
    debugger;
    var addressVal = $("#txtAddress").val();    
    if (addressVal.trim() == "") {
        $('#error_fullAddress').html("");
        $('#txtAddress').css('border-color', 'Red');
        $('#error_fullAddress').html("Address is required");
        $('#error_fullAddress').show();
        //$('.btnvalid').attr('disabled', true);
        isValid = false;
    } else {
        $('#error_fullAddress').html("");
        $('#txtAddress').css('border-color', 'lightgrey'); 
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

$('#txtDob').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() == "") {
        $('#txtDob').css('border-color', 'Red');
        $('#error_dob').html("DOB name is required");
        $('#error_dob').show();      
    } else {
        $('#error_dob').css('border-color', 'lightgrey');
        $('#error_lastname').html("");
    }
});

$(".validateEmail").keyup(function () {
    debugger;
    var email = $("#txtEmail").val();
    if (email != 0) {
        if (isValidEmailAddress(email)) {
            $('#txtEmail').css('border-color', 'lightgrey');
            $('#error_email').html("");
            $('#error_email').hide();
            $('.btnvalid').attr('disabled', false);
            isValid = true;
            ExistingEmailCheck();
        } else {
            $('#txtEmail').css('border-color', 'Red');
            $('#error_email').html("Invalid email address");
            $('#error_email').show();    
            $('.btnvalid').attr('disabled', true);
            isValid = false;
        }
    } else {
        $('#txtEmail').css('border-color', 'Red');
        $('#error_email').html("Email address is required");
        $('#error_email').show();
        $('.btnvalid').attr('disabled', true);
        isValid = false;
    }
});

$(".validateAlternateEmail").keyup(function () {
    debugger;
    var email = $("#txtAlternateEmail").val();
    if (email != 0) {
        if (isValidEmailAddress(email)) {
            $('#txtAlternateEmail').css('border-color', 'lightgrey');
            $('#error_alteremail').html("");
            $('#error_alteremail').hide();
            $('.btnvalid').attr('disabled', false);
            //ExistingEmailCheck();
            isValid = true;
        } else {
            $('#txtAlternateEmail').css('border-color', 'Red');
            $('#error_alteremail').html("Invalid email address");
            $('#error_alteremail').show();
            $('.btnvalid').attr('disabled', true);
            isValid = false;
        }
    } else {
        $('#txtAlternateEmail').css('border-color', 'lightgrey');
        $('#error_alteremail').html("");
        $('#error_alteremail').show();
        $('.btnvalid').attr('disabled', false);
        isValid = true;
        return;
    } 
});

$('.companyNameRequire').keyup(function () {
    debugger;//txtCompanyName
    var $this = $(this);
    if ($this.val().trim() == "") {
        $('#txtCompanyName').css('border-color', 'Red');
        $('#error_companyname').html("Company name is required"); //<span class="error" id="error_companyname" style="display: none;"></span>
        $('#error_companyname').show();
        $('.btnvalid').attr('disabled', true);
        isValid = false;

    } else {
        $('#txtCompanyName').css('border-color', 'lightgrey');
        $('#error_companyname').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

$('.companyEmailRequire').keyup(function () {
    var $this = $(this); //txtCompanyEmail
    if ($this.val().trim() == "") {
        $('#txtCompanyEmail').css('border-color', 'Red');
        $('#error_companyemailreq').html("Company email is required");//<span class="error" id="error_companyemailreq" style="display: none;"></span>
        $('#error_companyemailreq').show();
        $('.btnvalid').attr('disabled', true);
        isValid = false;

    } else {
        $('#txtCompanyEmail').css('border-color', 'lightgrey');
        $('#error_companyemailreq').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

$(".validateCompanyEmail").keyup(function () {
    debugger;//txtCompanyEmail
    var email = $("#txtCompanyEmail").val();
    if (email != 0) {
        if (isValidEmailAddress(email)) {
            $('#txtCompanyEmail').css('border-color', 'lightgrey');
            $('#error_companyemail').html("");//<span class="error" id="error_companyemail" style="display: none;"></span>
            $('#error_companyemail').hide();
            $('.btnvalid').attr('disabled', false);
            isValid = true;
            //ExistingEmailCheck();
        } else {
            $('#txtCompanyEmail').css('border-color', 'Red');
            $('#error_companyemail').html("Invalid email address");
            $('#error_companyemail').show();
            isValid = false;
            $('.btnvalid').attr('disabled', true);
        }
    } else {
        $('#txtCompanyEmail').css('border-color', 'lightgrey');
        $('#error_companyemail').html("");
        $('#error_companyemail').show();
        $('.btnvalid').attr('disabled', false);
        isValid = true;
        return;
    } 
});

$('.companyphonerequire').keyup(function () {
    //txtCompanyPhoneno
    var $this = $(this);
    if ($this.val().trim() == "") {
        $('#txtCompanyPhoneno').css('border-color', 'Red'); 
        $('#error_companyemailreq').html("Company phone no is required");//<span class="error" id="error_companyphonereq" style="display: none;"></span>
        $('#error_companyphonereq').show();
        $('.btnvalid').attr('disabled', true);
        isValid = false;
    } else {
        $('#error_companyphonereq').css('border-color', 'lightgrey');
        $('#error_companyemailreq').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

$(".company-phonedigit").keyup(function () {
    //debugger; //txtCompanyPhoneno
    var phoneValue = $("#txtCompanyPhoneno").val();
    if (phoneValue.length != 10) {
        if (phoneValue == "") {
            $('#txtCompanyPhoneno').css('border-color', 'lightgrey');
            $('#error_CompanyPhoneMsg').html("");
            $('#error_CompanyPhoneMsg').hide();
            $('.btnvalid').attr('disabled', false);
            isValid = true;
            //return true;
        } else {
            $('#txtCompanyPhoneno').css('border-color', 'red');
            $('#error_CompanyPhoneMsg').html("Required 10 digits, match requested format!");
            $('#error_CompanyPhoneMsg').show();//<span class="error" id="error_CompanyPhoneMsg" style="display: none;"></span>
            $('.btnvalid').attr('disabled', true);
            isValid = false;
        }
       
    } else {
        $('#txtCompanyPhoneno').css('border-color', 'lightgrey');
        $('#error_CompanyPhoneMsg').html("");
        $('#error_CompanyPhoneMsg').hide();
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }

});

$(".compy-pinlength").keyup(function (e) {
    debugger;
    var pinVal = $("#txtCompanyPincode").val();
    var pattern = new RegExp("^[0-9]{6}$");
    if (pattern.test(pinVal) == false) {
        if (pinVal == "") {
            $('#error_companypincode').html(""); //<span class="error" id="error_companypincode" style="display: none;"></span>
            $('#txtCompanyPincode').css('border-color', 'lightgrey');
            $('.btnvalid').attr('disabled', false);
            isValid = true;
        } else {
            $('#txtCompanyPincode').css('border-color', 'Red');
            $('#error_companypincode').html("Required 6 digits, match requested format!");
            $('#error_companypincode').show();
            $('.btnvalid').attr('disabled', true);
            isValid = false;
        }
    } else {
        $('#txtCompanyPincode').css('border-color', 'lightgrey');
        $('#error_companypincode').html("");
        $('.btnvalid').attr('disabled', false);
        isValid = true;
    }
});

