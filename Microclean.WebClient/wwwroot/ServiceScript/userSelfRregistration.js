$(document).ready(function () {
    //debugger;        
    GetAllState();
    GetAllStateClient();
});

function franchiseeItSelfRegistration() {
    $("#overlay1").show();
    if (CheckRequiredFields()) {
        debugger;
        var formData = new FormData();
        let userfirstnameval = $('#txtFirstName').val();
        let usermiddlenameval = $('#txtMiddleName').val();
        let userlastnameval = $('#txtLastName').val();
        let useremailval = $('#txtEmail').val();
        let userphoneval = $('#txtPhone').val();
        let usercompanynameval = $('#txtCompanyName').val();
        let userpasswordval = $('#txtPassword').val();
        let userreinterpasswordval = $('#txtreinterpassword').val();
        //let userstateval = $("#ddlState :selected").val();
        //let usercityval = $("#ddlCity :selected").val();
        //let userpincodeval = $('#txtPincode').val();

        let userCityLocation = $("#ddlcitylocation :selected").val();
        let userPincode = $("#txtPincode").val();
        let userAddress = $('#txtAddress').val();
        formData.append("FirstName", userfirstnameval);
        formData.append("MiddleName", usermiddlenameval);
        formData.append("LastName", userlastnameval);
        formData.append("Email", useremailval);
        formData.append("PhoneNumber", userphoneval);
        formData.append("CompanyName", usercompanynameval);
        formData.append("PasswordHash", userpasswordval);
        formData.append("ConfirmPassword", userreinterpasswordval);
        formData.append("Address.CityLocationId", userCityLocation);
        formData.append("Address.ZipCode", userPincode);
        formData.append("Address.FullAdrrss", userAddress);
        //formData.append("StateId", userstateval);
        //formData.append("CityId", usercityval);        
        //formData.append("Pincode", userpincodeval);
        formData.append("UserTypeId", 2);
        datasave('Franchisee/franchiseeItSelfRegistration', formData).then(data => {
            debugger;
            $("#overlay1").hide();
            if (data.data !== null) {
                if (data.data.status == false) {
                    alert(data.data.message);
                    //return false;
                } else {
                    alert(data.data.message);
                }
                window.location = "/Account/Login";
            }
        });
    }
}


