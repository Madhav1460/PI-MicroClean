/// <reference path="../theams/plugins/jquery/jquery.min.js" />


function TrainingDocUpload() {
    showProgress();
    let url = 'OnlineTraning/uploadtrainingdoc';
    var formData = new FormData();
    let doctype = $("#purpose option:selected").val();
    let title = $("#txttitle").val();
    let description = $("#txtdescription").val();
    var file = document.getElementById("docfile").files[0];
    var vfile = document.getElementById("videofile").files[0];
    formData.append("DocType", doctype);
    formData.append("Title", title);
    formData.append("Description", description);
    formData.append("DocFile", file);
    formData.append("DocFile", vfile);
    uploadtrainingdoc(url, formData).then(data => {

    });
}

function ChangePassword() {
    var formData = new FormData();
    let oldpassword = $('#txtoldpass').val();
    let newpassword = $('#txtnewpass').val();
    let cnfnewpassword = $('#txtcnfnewpass').val();
    formData.append("OldPassword", oldpassword);
    formData.append("NewPassword", newpassword);
    formData.append("ConfirmNewPassword", cnfnewpassword);
    datasave('Account/changepassword', formData).then(data => {
        if (data.data.status === true && data.data.message === "Updated Successfully") {
            localStorage.removeItem('token');
            localStorage.clear();
            alert(data.data.message);
            window.location = "/Account/Login";
        }
        else {
            alert(data.data.message);
        }
    });

}

function downloadUnSingedLoiDoc() {
    var token = localStorage.getItem('token');
    var baseURLdw = baseurl + 'common/download';
    var baseURL = baseurl + 'Company/getdocumentbyfranchiseeid';
    $.ajax({
        url: baseURL + "?franchiseeid=" + 0,
        headers: {
            "Authorization": "Bearer " + token
        },
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        type: "GET",
        timeout: 0,
        success: function (result) {
            console.log(result);
            if (result.data !== null) {
                downloadfile(baseURLdw, result.data.docPath, result.data.docName)
            } else {
                alert(result.message);
            }
        },
        error: function (err, response) {
            console.log(err, response);
            alert(err, response.responseText);
        }
    });
}