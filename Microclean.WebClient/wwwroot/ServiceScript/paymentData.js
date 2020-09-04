
function commercialUpdate() {
    debugger;
    let userid = sessionStorage.userId;

    let franchiseefeesId = $('#hidFranchiseeFeeId').val();
    let otherfeesId = $('#hidOtherFeeid').val();

    let franchiseeFeeTypeId = $('#hidFranchiseeFeeType').val();
    let OtherFeeTypeId = $('#hidOtherFeeType').val();

    let franchiseefeeidval = $('#hidFranchiseeFee').val();
    let otherfeeidval = $('#hidOtherFee').val();

    let paymenttermsval = $('#txtDescription').val();
    let duedateval = $('#paymentDate').val();
    let totalamountval = $('#totalAmtReceived').val();
    let paymentTypeId = $("#PaymentTypeMode :selected").val();
    let franchiseeFeePaidAmout = $('#franchiseeFeeAmtReceived').val();
    let otherFeePaidAmout = $('#OtherFeeAmtReceived').val();
    let paymentRef = $('#paymentRef').val();
    
    if (paymentTypeId == 0) {
        $('#PaymentTypeMode').css('border-color', 'Red');
        return false;
    }
    if (checkRequiredForUpdatePayment()) {
        var formData = new FormData();
        formData.append("UserId", userid);

        formData.append("FranchiseeFeesId", franchiseefeesId != "" ? franchiseefeesId : 0);
        formData.append("OtherFeesId", otherfeesId != "" ? otherfeesId : 0);

        formData.append("FranchiseeFeeId", franchiseeFeeTypeId);
        formData.append("OtherFeeId", OtherFeeTypeId);

        formData.append("OtherFeeId", otherfeeidval);
        formData.append("PaymentTerms", paymenttermsval);
        formData.append("PaymentDueDate", duedateval);
        formData.append("TotalAmmount", parseFloat(totalamountval));
        formData.append("PaymentTypeId", paymentTypeId);
        formData.append("FranchiseeFeePaidAmout", franchiseeFeePaidAmout != "" ? parseFloat(franchiseeFeePaidAmout) : 0);
        formData.append("OtherFeePaidAmout", otherFeePaidAmout != "" ? parseFloat(otherFeePaidAmout) : 0);
        formData.append("PaymentRef", paymentRef);

        datasave('Company/addfranchiseefee', formData).then(data => {
            debugger;
            if (data.data !== null) {
                alert(data.data.message);
                $('#modal_Payment').modal('hide');
            }
        });
    }
}

