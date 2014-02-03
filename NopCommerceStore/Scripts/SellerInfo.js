var controlExtension = "ctl00_cph1_ctrlManufacturerDetails_ctrlSellerInfo_";
var parentControlExtension = "ctl00_cph1_ctrlManufacturerDetails_";

function btnVerifyPaypalEmail_ClientClick() {
    //set variable values.
    var emailAddress = $('#' + txtPaypalEmailAddress_id).val();
    var accountFirstName = $('#' + txtFirstName_id).val();
    var accountLastName = $('#' + txtLastName_id).val();

    //ensure items are not blank.
    if (emailAddress == "" || accountFirstName == "" || accountLastName == "" || !IsValidEmail(emailAddress)) {
        hideAllMessages();
        $('#lblInvalid').show();
        return;
    }

    hideAllMessages();
    $('#lblVerifying').show();


    //disable the button for the call.
    $('#' + controlExtension + 'btnVerifyPaypalEmail').attr("disabled", "disabled");

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "../Services/PaypalAdaptiveAccount/PaypalAdaptiveAccountService.svc/VerifyPaypalEmail",
        data: '{"email":"' + emailAddress +  '",' +
              '"firstName": "' + accountFirstName + '",' +
              '"lastName": "' + accountLastName + '"}',
        
        success:btnVerifyPaypalEmail_onSuccess,
        error:btnVerifyPaypalEmail_onError
    });
}

function btnVerifyPaypalEmail_onSuccess(val) {
    //re-enable button on success.
    $('#' + controlExtension + 'btnVerifyPaypalEmail').removeAttr("disabled");
    if (val.d == "true") {
        //account is valid.
        hideAllMessages();
        $('#' + hidPaypalVerified_id).val('true');
        $('#' + lblSuccess_id).show();
//        $('#' + parentControlExtension + 'SaveButton').removeAttr("disabled");
//        $('#' + parentControlExtension + 'SaveAndStayButton').removeAttr("disabled");
    } else {
        hideAllMessages();
        $('#' + hidPaypalVerified_id).val('false');
        $('#lblFailure').show();
    }
}

function btnVerifyPaypalEmail_onError(val) {
    //re-enable button on failure as well.
    $('#' + controlExtension + 'btnVerifyPaypalEmail').removeAttr("disabled");
    hideAllMessages();
    $('#lblError').css('display', 'block');
}

function hideAllMessages() {
    $('#lblError').hide();
    $('#lblFailure').hide();
    $('#' + lblSuccess_id).hide();
    $('#' + lblUnverified_id).hide();
    $('#lblInvalid').hide();
    $('#lblVerifying').hide();
}

function IsValidEmail(email)
{
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return filter.test(email);
}

function VerifyFields_onChange() {
    hideAllMessages();

//    $('#' + parentControlExtension + 'SaveButton').attr("disabled", "disabled");
//    $('#' + parentControlExtension + 'SaveAndStayButton').attr("disabled", "disabled");
    $('#' + hidPaypalVerified_id).val('false');


    $('#' + lblUnverified_id).show();
}
