$('.summition').on('input', function (e) {
    var total = 0;
    $('.summition').each(function () {
        total += parseFloat($(this).val()) || 0;
    });
    $('.summitiontotal').val(total);
});


$(function () {
    $("#txtThresholdAmount").change(function () {
        if ($('#txtFranchiseeFee').val() !== undefined || $('#txtFranchiseeFee').val() !== "") {
            var fiftypercentoffee = (parseFloat($('#txtFranchiseeFee').val() * 50) / 100);
            var totalFee = parseFloat($('#txtFranchiseeFee').val()) + (parseFloat($('#txtOtherFee').val()));
            if (fiftypercentoffee > parseFloat($("#txtThresholdAmount").val())) {
                alert('Threshold Amount not less then 50% of Franchisee Fee. ' + fiftypercentoffee + '');
                $("#txtThresholdAmount").val('')
                return false;
            } else {
                if (totalFee < parseFloat($("#txtThresholdAmount").val())) {
                    alert('Threshold Amount should not be more than from Total Due Amount.');
                    $("#txtThresholdAmount").val('')
                    return false;
                }
            }
        }
    });
});
$('.notmorthanvlidation').on('input', function (e) {
    if (this.id === "txtFranchiseeFeepaymentdue") {
        if (parseFloat($(this).val()) > parseFloat($('#txtFranchiseeFee').val())
            && e.keyCode !== 46 // keycode for delete
            && e.keyCode !== 8 // keycode for backspace
        ) {
            e.preventDefault();
            $(this).val('');
        }
    }
    if (this.id === "txtOtherFeepaymentdue") {
        if (parseFloat($(this).val()) > parseFloat($('#txtOtherFee').val())
            && e.keyCode !== 46 // keycode for delete
            && e.keyCode !== 8 // keycode for backspace
        ) {
            e.preventDefault();
            $(this).val('');
        }
    }
    if (this.id === "txtFixedMonthlyFeepaymentdue") {
        if (parseFloat($(this).val()) > parseFloat($('#txtFixedMonthlyFee').val())
            && e.keyCode !== 46 // keycode for delete
            && e.keyCode !== 8 // keycode for backspace
        ) {
            e.preventDefault();
            $(this).val('');
        }
    }
    if (this.id === "txtThresholdAmountpaymentdue") {
        if (parseFloat($(this).val()) > parseFloat($('#txtThresholdAmount').val())
            && e.keyCode !== 46 // keycode for delete
            && e.keyCode !== 8 // keycode for backspace
        ) {
            e.preventDefault();
            $(this).val('');
        }
    }
});

function CheckRequiredFields() {
    var isValid = true;
    $('.required').each(function () {
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
$('#txtFranchiseeFee').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtFranchiseeFee').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtFranchiseeFee').css('border-color', 'lightgrey');
    }
});
$('#txtOtherFee').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtOtherFee').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtOtherFee').css('border-color', 'lightgrey');
    }
});
$('#txtFixedMonthlyFee').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtFixedMonthlyFee').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtFixedMonthlyFee').css('border-color', 'lightgrey');
    }
});
$('#txtThresholdAmount').keyup(function () {
    var $this = $(this);
    if ($this.val().trim() === "") {
        $('#txtThresholdAmount').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#txtThresholdAmount').css('border-color', 'lightgrey');
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