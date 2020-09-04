function kyCDocumentList() {
    var doctyp = ['Owner Adhar Card', 'Owner PAN Card', 'Owner Photo', 'Company PAN Card', 'Company GST Doc']
    var doclist = "";
    for (var i = 0; i < 5; i++) {
        doclist += ` <div class="col-sm-2" style="display:grid">
                                                <label for="lblEmail"> `+ doctyp[i] + ` </label>&nbsp;
                                                <input type="hidden" name="pencard" id="hdndocpath_`+ i + `" value="" />
                                                <input type="hidden" name="docname" id="kycdocnameid_`+ i + `" value="" />
                                                <input type="button" id="downloadkycdoc_`+ i + `" onclick="downlodpencard(` + i + `);" value="Download" style=" background-color: #07628b; color: white;" class="btn" /><br />
                                                <input type="text" class="form-control required form-inline" id="kycdocnameids_`+ i + `" name="UploadedFileName" value="" placeholder="Fiile Name" readonly>
                                            </div>`;
    }
    $('#dvdoclist').html(doclist);
}
var token = localStorage.getItem('token');
var baseURLdw = baseurl + 'common/download';
function downlodpencard(ctr) {
    var FranchiseId = $("#franchiseeid option:selected").val();
    $('#error_ddl').html("");
    $('#franchiseeid').css('border-color', '#d2d6de');
    if (FranchiseId == "Select") {
        $('#error_ddl').html("Please select the Franchisee");
        $('#error_ddl').show();
        $('#franchiseeid').css('border-color', 'Red');
        return false;
    }
    let docurl = $('#hdndocpath_' + ctr + '').val();
    let docName = $('#kycdocnameids_' + ctr + '').val();
    if (docurl != "") {
        downloadfile(baseURLdw, docurl, docName);
    }
};
function GetFrenId(ctr) {
    $('#error_ddl').html("");
    $('#franchiseeid').css('border-color', '#d2d6de');
    var url = 'company/getkycdocument'
    GetKycDocumentProfile(url, $(ctr).children("option:selected").val()).then(data => {
        debugger;
        if (data.data.length != 0) {
            for (var i = 0; i < data.data.length; i++) {
                if (data.data[i].documentType == 1) {
                    $('#hdndocpath_0').val(data.data[i].docPath);
                    $('#kycdocnameids_0').val(data.data[i].docName);
                    //var docuName = $('#kycdocnameid_' + i + '').val(data.data[i].docName);
                //} else {
                //    $('#hdndocpath_1').val("")
                //    $('#kycdocnameids_1').val("")
                }
                if (data.data[i].documentType == 2) {
                    $('#hdndocpath_1').val(data.data[i].docPath)
                    $('#kycdocnameids_1').val(data.data[i].docName)
                }
                if (data.data[i].documentType == 3) {
                    $('#hdndocpath_2').val(data.data[i].docPath)
                    $('#kycdocnameids_2').val(data.data[i].docName)
                }
                if (data.data[i].documentType == 4) {
                    $('#hdndocpath_3').val(data.data[i].docPath)
                    $('#kycdocnameids_3').val(data.data[i].docName)
                }
                if (data.data[i].documentType == 5) {
                    $('#hdndocpath_4').val(data.data[i].docPath)
                    $('#kycdocnameids_4').val(data.data[i].docName)
                }
                if (data.data[i].documentType == 6) {
                    //$('#hdnadpasportid').val(data.data[i].docPath)
                    $('#LOIFilename').val(data.data[i].docName)
                }
                if (data.data[i].documentType == 7) {
                    //$('#hdnadpasportid').val(data.data[i].docPath)
                    $('#SignedLOIFilename').val(data.data[i].docName)
                }
            }
        } else {
            for (var i = 0; i < 5; i++) {                
                $('#hdndocpath_' + i + '').val("");
                $('#kycdocnameids_' + i + '').val("");               
                $('#LOIFilename').val("");
                $('#SignedLOIFilename').val("");
            }
        }
    });
};

function getfranchiseeList() {
    var baseURL = baseurl + 'company/getallfranchisee';
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: "GET",
        datatype: "json",
        success: function (data) {
            $("#franchiseeid").empty();
            var options = '';
            options += '<option value="Select">-Select Franchisee-</option>';
            if (data.data != null) {
                for (var i = 0; i < data.data.length; i++) {
                    if (data.data[i].isApproved === true) {
                        options += '<option value="' + data.data[i].id + '">' + data.data[i].fullName + '</option>';
                    }
                }
                $('#franchiseeid').append(options);
            } else {
                $('#franchiseeid').append(options);
            }
            
        },
        error: function (response) {
            console.log(response);
        }
    });
}

$('#btnUpload').click(function () {
    var baseURL = baseurl + 'Company/uploadloidocumentforfranchisee';
    var FranchiseId = $("#franchiseeid option:selected").val();
    $('#error_ddl').html("");
    $('#franchiseeid').css('border-color', '#d2d6de');
    if (FranchiseId == "Select") {
        $('#error_ddl').html("Please select the Franchisee");
        $('#error_ddl').show();
        $('#franchiseeid').css('border-color', 'Red');
        return false;
    }
    if ($("#LOIFileId").val() == '') {
        alert('Please select a file.');
        return false;
    }
    var form = new FormData();
    var file = $('#LOIFileId')[0];
    form.append('LoiDocFile', file.files[0]);
    form.append("FranchiseeId", FranchiseId);

    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'POST',
        timeout: 0,
        mimeType: "multipart/form-data",
        contentType: false,
        processData: false,
        data: form,
        success: function (result) {
            var json = $.parseJSON(result);
            $('#error_ddl').html("");
            $('#error_ddl').hide();
            $('#LOIFilename').val(json.data.fileName);
            if (json.status == true) {
                toastr.success(json.message);
            }
            else {
                toastr.error(result.Message);
            }
        },
        error: function (err) {
            $('#error_ddl').html("");
            $('#error_ddl').hide();
            toastr.error("File is not uploaded");
        }
    });
});

$("#downloadSignedLOI").click(function () {
    var imagebasepath = imagebaseurl;
    var baseURL = baseurl + 'Company/getdocumentbyfranchiseeid';
    var FranchiseId = $("#franchiseeid option:selected").val();
    $('#error_ddl').html("");
    $('#franchiseeid').css('border-color', '#d2d6de');
    if (FranchiseId == "Select") {
        $('#error_ddl').html("Please select the Franchisee");
        $('#error_ddl').show();
        $('#franchiseeid').css('border-color', 'Red');
        return false;
    }
    if ($("#SignedLOIFilename").val() == "No file available") {
        alert("There is no file available for download");
        return false;
    }
    $.ajax({
        url: baseURL + "?franchiseeid=" + FranchiseId,
        headers: {
            "Authorization": "Bearer " + token
        },
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        type: "GET",
        timeout: 0,
        success: function (result) {
            if (result.data != null && result.data.docPath != "") {
                downloadfile(baseURLdw, result.data.docPath, result.data.docName)
            } else {
                alert("No file avalible for requesting franchisee");
            }
        },
        error: function (err, response) {
            console.log(err, response);
            alert(err, response.responseText);
        }
    });
});

