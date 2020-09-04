function checkRequiredForUpdatePayment() {
    debugger;
    var isValid = true;
    $('.required1').each(function () {
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

$("#franchiseeFeeAmtReceived").on('input', function () {
    debugger;
    var fFeeBal = parseFloat($("#franchiseeFeePreviousBalAmount").val());
    var fFeeVal = parseFloat($("#franchiseeFeeAmtReceived").val());
    var oFeeVal = parseFloat($("#OtherFeeAmtReceived").val());

    if (fFeeBal >= fFeeVal) {
        $("#spanFranchiseeFeePayment").css("display", "none");
        $('#franchiseeFeeAmtReceived').css('border-color', 'lightgrey');
    } else {
        $("#spanFranchiseeFeePayment").css("display", "block").css("color", "red");
        $('#franchiseeFeeAmtReceived').css('border-color', 'Red');
        var total = $("#totalAmtReceived").val() - fFeeVal;
        if (total <= 0) {
            parseFloat($("#totalAmtReceived").val(0.00));
            $("#franchiseeFeeAmtReceived").val("");
            $("#btnUpdatePayment").attr("disabled", true);
        } else {
            parseFloat($("#totalAmtReceived").val(total));
            $("#franchiseeFeeAmtReceived").val("");
        }
    }
});

$("#OtherFeeAmtReceived").on('input', function () {
    debugger;
    var oFeeBal = parseFloat($("#OtherFeePreviousBalAmount").val());
    var oFeeVal = parseFloat($("#OtherFeeAmtReceived").val());
    if (oFeeBal >= oFeeVal) {
        $("#spanOtherFeePayment").css("display", "none");
        $('#OtherFeeAmtReceived').css('border-color', 'lightgrey');
    } else {
        $("#spanOtherFeePayment").css("display", "block").css("color", "red");
        $('#OtherFeeAmtReceived').css('border-color', 'Red');
        var total = $("#totalAmtReceived").val() - oFeeVal;
        if (total <= 0) {
            parseFloat($("#totalAmtReceived").val(0.00));
            $("#OtherFeeAmtReceived").val("");
            $("#btnUpdatePayment").attr("disabled", true);
        } else {
            parseFloat($("#totalAmtReceived").val(total));
            $("#OtherFeeAmtReceived").val("");
        }
    }
});

$('.updatesum').on('input', function () {
    var total = 0;
    $('.updatesum').each(function () {
        total += parseFloat($(this).val()) || 0;
    });
    if (total > 0) {
        $('.totalupdatesumtotal').val(parseFloat(total).toFixed(2));
        $('#btnUpdatePayment').prop("disabled", false);
    } else {
        $('.summitiontotal').val(parseFloat(total));
        $('#btnUpdatePayment').prop("disabled", true);
    }
});

$('#franchiseeFeeAmtReceived').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#franchiseeFeeAmtReceived').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#franchiseeFeeAmtReceived').css('border-color', 'lightgrey');
    }
});
$('#OtherFeeAmtReceived').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#OtherFeeAmtReceived').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#OtherFeeAmtReceived').css('border-color', 'lightgrey');
    }
});
$('#PaymentTypeMode').keyup('change', function () {
    var $this = $(this);
    if ($this.val().trim() == "") {
        $('#PaymentTypeMode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PaymentTypeMode').css('border-color', 'lightgrey');
    }
});



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