$(function () {
    $("#tbluser").DataTable({
        "responsive": true,
        "autoWidth": false,
    });
    $('#tbluser').DataTable().destroy();
    $("#tbluser").css("width", "none");
});
var token = localStorage.getItem('token');
function AddFeePopUp(ctr) {
    $.ajax({
        url: '/Admin/AddFee',
        data: [],
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
        },
        success: function (data) {
            sessionStorage.setItem('userId', ctr);
            $("#bootPoper").empty().append(data);
            $('#modal-default').modal('show');
        },
        error: function (xhr, textStatus, err) {
        }
    })
}

function UpdateFeePopup(ctr) {
    $.ajax({
        url: '/Admin/UpdateFee',
        data: [],
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
        },
        success: function (data) {
            sessionStorage.setItem('userId', ctr);
            $("#bootPoper").empty().append(data);
            $('#modal_Payment').modal('show');
        },
        error: function (xhr, textStatus, err) {
        }
    })
}
$(document).ready(function () {
    var baseURL = baseurl + 'company/getallfranchisee';
    var table = $("#tbluser").DataTable({
        "paging": true,
        "responsive": true,
        "autoWidth": false,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "iDisplayLength": 10,
        "ajax": {
            "url": baseURL,
            "headers": {
                "Authorization": "Bearer " + token
            },
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": null, "searchable": false, "sortable": false }, //for #
            { "data": "fullName", "name": "Franchisee Name", "autoWidth": true },
            {
                "data": "approveStatus", "name": "Status", "autoWidth": true,
                "render": function (data, type, row, meta) {

                    if (data == "Approve") {
                        return '<span id="activeUser" class="badge badge-success">' + data + '</span>'
                    } else {
                        return '<span id="inactiveUser" class="badge badge-danger">' + data + '</span>'
                    }
                },
            },
            {
                "title": "Actions",
                "data": "approveStatus",
                "render": function (data, type, row, meta) {
                    //console.log(data);
                    if (data == "Approve") {
                        return '<a class="btn btn-primary btn-sm" style="background-color: #007B9F;color: white;border-color: #007B9F;" onclick=GetFranchiseView("' + row.id + '");><i class="fa fa-eye" aria-hidden="true"></i> View</a> <a onclick=GetFranchiseUpdate("' + row.id + '"); class="btn btn-primary btn-sm" style="background-color: #007B9F;color: white;border-color: #007B9F;"><i class="fa fa-edit" aria-hidden="true"></i> Edit</a> <a class="btn btn-danger btn-sm" href="javascript:void(0);"  onclick=InactiveData("' + row.id + '"); return false;> <i class="fa fa-times" aria-hidden="true"></i> Disapprove</a> <a class="btn btn-primary btn-sm"  href="javascript:void(0);" id="lnkuserid" onclick="AddFeePopUp(' + row.id + ');"><i class="fas fa-inr"></i>Add Fee</a> <a class="btn btn-warning btn-sm"  href="javascript:void(0);" id="lnkuserid" onclick="UpdateFeePopup(' + row.id + ');"><i class="fas fa-inr"></i>Update Fee Payment</a> <a class="btn btn-secondary btn-sm" href="javascript:void(0);" id="addempid" onclick="AddEmployeePopUp(' + row.id + ');"><i class="fa fa-reply" aria-hidden="true"></i> Add Supervisor</a>';
                    } else {
                        return '<a class="btn btn-primary btn-sm" style="background-color: #007B9F;color: white;border-color: #007B9F;" onclick=GetFranchiseView("' + row.id + '");> <i class="fa fa-eye" aria-hidden="true"></i> View</a> <a onclick=GetFranchiseUpdate("' + row.id + '"); class="btn btn-primary btn-sm" style="background-color: #007B9F;color: white;border-color: #007B9F;"><i class="fa fa-edit" aria-hidden="true"></i> Edit</a> <a class="btn btn-success btn-sm" href="javascript:void(0);"  onclick=ActiveData("' + row.id + '"); return false;> <i class="fa fa-check" aria-hidden="true"></i> Approve </a> <a class="btn btn-primary btn-sm btn-disabled" disabled="disabled"  href="javascript:void(0);" id="lnkuserid" onclick="AddFeePopUp(' + row.id + ');"><i class="fas fa-inr"></i> AddFee</a> <a class="btn btn-warning btn-sm btn-disabled" disabled="disabled"  href="javascript:void(0);" id="lnkuserid" onclick="UpdateFeePopup(' + row.id + ');"><i class="fas fa-inr"></i>Update Fee Payment</a> <a class="btn btn-secondary btn-sm btn-disabled" disabled="disabled" href="javascript:void(0);" id="lnkuserid" onclick="AddFeePopUp(' + row.id + ');"><i class="fa fa-reply" aria-hidden="true"></i> Add Supervisor</a>';
                    }
                },
            }
        ]
    });
    //For #
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function GetFranchiseView(id) {
    $("#overlay1").show();
    var baseURL = baseurl + 'Company/getuserdetailbyid?userId=' + id;
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $("#overlay1").hide();
            sessionStorage.setItem('userData', JSON.stringify(result.data));
            window.location.href = '/Admin/FranchiseeView';
        }
    });
}
function GetFranchiseUpdate(id) {
    $("#overlay1").show();
    var baseURL = baseurl + 'Company/getuserdetailbyid?userId=' + id;
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $("#overlay1").hide();
            sessionStorage.setItem('userData', JSON.stringify(result.data));
            window.location.href = '/Admin/UpdateFranchisee';
        }
    });
}
function InactiveData(id) {
    if (confirm('Are you sure want to disapprove this Franchisee.?')) {
        $("#overlay1").show();
        var baseURL = baseurl + 'company/franchiseeApprove';
        var Details = new Object();
        Details.Id = id;
        Details.IsApproved = false;
        $.ajax({
            url: baseURL,
            headers: {
                "Authorization": "Bearer " + token
            },
            type: 'POST',
            data: (Details),
            dataType: 'json',
            success: function (result) {
                $("#overlay1").hide();
                if (result.message === "Update Successfully") {
                    toastr.options.onHidden = function () { window.location.href = "/Admin/Index"; }
                    toastr.success(result.message);
                } else {
                    toastr.error(result.message);
                }
            }
        });
    } else {
        $("#overlay1").hide();
    }
};
function ActiveData(id) {
    var baseURL = baseurl + 'company/franchiseeApprove';
    var userId = parseInt(id);
    if (confirm('Are you sure want to approve this Franchisee.?')) {
        var Details = new Object();
        Details.Id = userId;
        Details.IsApproved = true;
        $.ajax({
            url: baseURL,
            headers: {
                "Authorization": "Bearer " + token
            },
            type: 'POST',
            data: (Details),
            dataType: 'json',
            success: function (result) {
                $("#overlay1").hide();
                if (result.message === "Update Successfully") {
                    toastr.options.onHidden = function () {  window.location.href = '/Admin/Index'; }
                    toastr.success(result.message);
                } else {
                    toastr.error(result.message);
                }
            }
        });
    } else {
        $("#overlay1").hide();
    }
};

function AddEmployeePopUp(id) {
    $("#hdnEmployeeId").val(id);
    //var baseURL = baseurl + 'Company/getalladminemployee';
    var baseURL = baseurl + 'Company/getalladminsupervisor';
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $("#employeeId").empty();
            var options = '';
            options += '<option value="Select">-Select Employee-</option>';
            for (var i = 0; i < result.data.length; i++) {
                options += '<option value="' + result.data[i].id + '">' + result.data[i].fullName + '</option>';
            }
            $('#employeeId').append(options);
        }
    });
    $("#empmodal_assign").modal("show");
}

$("#btnAddEmployee").click(function () {
    var baseURL = baseurl + 'Company/allocateemployee';
    var userid = $("#hdnEmployeeId").val();
    var empId = $("#employeeId").val();
    if (empId == undefined || empId == "" || empId == null) {
        alert('Please select employee.');
        return false;
    };
    var Details = new Object();
    Details.UserId = userid;
    Details.EmployeeId = empId;
    $.ajax({
        url: baseURL,
        headers: {
            "Authorization": "Bearer " + token
        },
        type: 'POST',
        data: (Details),
        dataType: 'json',
        success: function (result) {
            if (result.message == "Update Successfully") {
                toastr.options.onHidden = function () { window.location.href = "/Admin/Index"; }
                toastr.success(result.message);
            } else {
                toastr.error(result.message);
            }
        }
    });
});