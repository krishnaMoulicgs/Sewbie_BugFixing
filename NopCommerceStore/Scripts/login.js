$(document).ready(function () {
    //    $("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_txtGuestEmail").verimail({
    //        messageElement: "#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_txtEmailError"
    //    });

    //assign a method to verify the email address to the item.
    if ($("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_hdnBadEmail").val() != "0") {

        //set the email to the bad email from the server validation.
        $("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_txtEmailError")[0].innerText = "Valid Email Required"
    } else {
        $("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_txtEmailError")[0].innerText = ""
    }



});       //document ready


function verifyEmail() {
//    if (validateEmail($("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_txtGuestEmail").val())) {
//        //email found
//        //enable checkout as guest.
//        $("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_btnCheckoutAsGuest").removeAttr('disabled');
//        $("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_txtEmailError")[0].innerText = "";
//    } else {
//        //email not found
//        //disable checkout as guest.
//        $("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_btnCheckoutAsGuest").attr('disabled', 'disabled');
//        $("#ctl00_ctl00_cph1_cph1_ctrlCustomerLogin_txtEmailError")[0].innerText = "Valid Email Required";
//    }
}

function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test($email)) {
        return false;
    } else {
        return true;
    }
}