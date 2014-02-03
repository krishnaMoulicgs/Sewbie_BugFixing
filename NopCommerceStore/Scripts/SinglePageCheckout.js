$(document).ready(function () {
    //initialize page here.
    $('#divBillingAddress').hide();
    $('#divShippingAddress').hide();


    $("#shippingAddressDiv").dialog({
        autoOpen: false,
        height: 500,
        width: 550,
        modal: true,
        title: "Shipping",
        dialogClass: "no-close",
        open: function (event, ui) {
            //load values for the existing addresses.
            loadAddresses(this);
        },
        buttons: {
            "Save Address": function () {
                if (validateAddress(this)) {
                    saveAddress(this, 'shipping');
                    $(this).dialog("close");
                } else {
                    return;
                }
            }
        },
        close: function () {
            //check to see if billing information needs to be entered as well.
            // if it's blank wae'll automatically pop the billing address info as well.
            if ($('#ctl00_ctl00_cph1_chp2_hdnBillingAddressFlag').val() == "0") {
                $('#billingAddressDiv').dialog('open');
            }
        }
    });
    $("#billingAddressDiv").dialog({
        autoOpen: false,
        height: 500,
        width: 550,
        modal: true,
        title: "Billing",
        dialogClass: "no-close",
        open: function (event, ui) {
            //load values for the existing addresses.
            loadAddresses(this);
        },
        buttons: {
            "Save Address": function () {
                if (validateAddress(this)) {
                    saveAddress(this, 'billing');
                    $(this).dialog("close");
                } else {
                    return;
                }
            }
        }
    });

    $("#address-dialog-succcess").dialog({
        autoOpen: false,
        resizable: false,
        height: 140,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });

    $("#address-dialog-error").dialog({
        autoOpen: false,
        resizable: false,
        height: 140,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });
    
    //if the addresses are blank, or this is no user, the first thing to do is to prompt the user for their shipping address.
    //additional logic will follow the "close" event to determine if the user also needs to enter their billing address.
    if ($("#ctl00_ctl00_cph1_chp2_hdnShippingAddressFlag").val() == "0") {
        //doesn't exist
        $("#shippingAddressDiv").dialog("open");
    }

});

function loadAddresses(objControl) {
    var requestObject = new Object();
    requestObject.request = new Object();
    var request = requestObject.request;

    //needs the customer id
    request.CustomerId = parseInt($('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val() == '' ? 0 : $('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val());
    request.Billing = objControl.id.toLowerCase().indexOf('billing') >= 0 ? 'billing' : '';

    var dataToSend = JSON.stringify(requestObject);

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "../Services/AddressService.svc/GetAddresses",
        data: dataToSend,
        success: addressLoad_onSuccess,
        error: addressLoad_onError
    });
}

function addressLoad_onSuccess(result, message, data) {
    //render the values returned fro the results.
    if (result.d.addresses.length > 0) {
        if (result.d.billing > 0) {
            $('#divBillingSelect').html($('#itemTemplate').render(result.d.addresses));
        } else {
            $('#divShippingSelect').html($('#itemTemplate').render(result.d.addresses));
        }
    }
}

function selectAddress(objControl) {
    var requestObject = new Object();
    requestObject.request = new Object();
    var request = requestObject.request;

    //needs the customer id
    request.CustomerId = parseInt($('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val() == '' ? 0 : $('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val());
    //isbilling? Based on the input that sent it.
    request.Billing = objControl.parentNode.id.toLowerCase().indexOf('billing') >= 0 ? 'billing' : '';
    request.address = new Object();
    request.address.addressId = $(objControl).find('.address-item-id').val();


    
    var dataToSend = JSON.stringify(requestObject);


    //retrieve the addressid
    //pass a request object to the service with the addressid
    //assign address over to order
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "../Services/AddressService.svc/SaveAddress",
        data: dataToSend,
        success: addressSave_onSuccess,
        error: addressSave_onError
    });
    //close dialog.
}

function addressLoad_onError(result, message, data) {
    // either clear values or do nothing.
}

function saveAddress(objControl, type) {

    //create the address response object;
    var requestObject = new Object();
    requestObject.request = new Object();
    var request = requestObject.request;
    
    //needs the customer id
    request.CustomerId = parseInt($('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val() == '' ? 0 : $('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val());
    //isbilling? Based on the input that sent it.
    request.Billing = type;
    request.address = new Object();
    request.address.address1 = $(objControl).find('.address-one').val();
    request.address.address2 = $(objControl).find('.address-two').val();
    request.address.city = $(objControl).find('.address-city').val();
    request.address.stateId = parseInt($(objControl).find('.address-state').val());
    request.address.zip = $(objControl).find('.address-zip').val();
    request.address.Country = $(objControl).find('.address-country').val();
    request.address.firstName = $(objControl).find('.address-first-name').val();
    request.address.lastName = $(objControl).find('.address-last-name').val();
    //additional fields we may capture later (phone/fax/company)

    var dataToSend = JSON.stringify(requestObject);
    
    switch (type) {
        case 'shipping':
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "../Services/AddressService.svc/SaveAddress",
                data: dataToSend,
                success: addressSave_onSuccess,
                error: addressSave_onError
            });
            break;
        //end shipping case;
        case 'billing':
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "../Services/AddressService.svc/SaveAddress",
                data: dataToSend,
                success: addressSave_onSuccess,
                error: addressSave_onError
            });
            break;
        //end billiing case; 
    }
}

function validateAddress(objControl) {
    var result = true;
    //clear all address labels.
    $('.address-error').remove();
    //required fields

    if ($(objControl).find('.address-first-name').val() == "") { $(objControl).find('.address-first-name-label').after('<span class="address-error">This field is required</span>'); result = false; };
    if ($(objControl).find('.address-last-name').val() == "") { $(objControl).find('.address-last-name-label').after('<span class="address-error">This field is required</span>'); result = false; };
    if ($(objControl).find('.address-one').val() == "") {$(objControl).find('.address-one-label').after('<span class="address-error">This field is required</span>');result = false; };
    if ($(objControl).find('.address-city').val() == "") { $(objControl).find('.address-city-label').after('<span class="address-error">This field is required</span>'); result = false; };
    if ($(objControl).find('.address-zip').val() == "") { $(objControl).find('.address-zip-label').after('<span class="address-error">This field is required</span>'); result = false; };
    
    return result;
}

function addressSave_onSuccess(result, message, foo) {
    //is billing or shipping?
    if (result.d.billing > 0) {
        $("#billingAddressDiv").dialog("close");
        $("#address-dialog-succcess").dialog("open");
        $('#ctl00_ctl00_cph1_cph1_pnlSelectBillingAddress .address-box').html($('#currentAddressTemplate').render(result.d.addresses));

        //plus update the billing id field.
        $('#ctl00_ctl00_cph1_cph1_hdnBillingAddressId').val(result.d.addresses[0].addressId);

        //clear fields
        clearFields($("#billingAddressDiv"));
    } else {
        $("#shippingAddressDiv").dialog("close");
        $("#address-dialog-succcess").dialog("open");
        $('#ctl00_ctl00_cph1_cph1_pnlSelectShippingAddress .address-box').html($('#currentAddressTemplate').render(result.d.addresses));

        //plus update the shipping id field.
        $('#ctl00_ctl00_cph1_cph1_hdnShippingAddressId').val(result.d.addresses[0].addressId);



        getShippingOptions();
//        //successful save, update the shipping options.
//        $.ajax({
//            type: "POST",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            url: "../Services/ShippingService.svc/VerifyPaypalEmail",
//            data: '{}',
//            success: shippingUpdate_onSuccess,
//            error: shippingUpdate_onError
//        });
        //clear & store fields
        storeFields($("#shippingAddressDiv"));
        clearFields($("#shippingAddressDiv"));
    }
    
    
    //display new address, overwrite exisiting with new fields - complete blow out and rebuild.
}

function getShippingOptions() {

    var vendorId = queryObj()["vendorid"];
    var datatosend = JSON.stringify(vendorId);
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "Services/ShippingService.svc/GetShippingOptions",
        data: '{}',
        success: shippingOptions_onSuccess,
        error: shippingOptions_onFailure
    });
}


function shippingOptions_onSuccess(result, message, foo) {

}

function shippingOptions_onFailure(result, message, foo) {

}

function queryObj() {
    var result = {}, keyValuePairs = location.search.slice(1).split('&');
    
    keyValuePairs.forEach(function (keyValuePair) {
        keyValuePair = keyValuePair.split('=');
        result[keyValuePair[0]] = keyValuePair[1] || '';
    });

    return result;
}

function storeFields(objControl) {
    $(objControl).find('.address-first-name-hidden').val($(objControl).find('.address-first-name').val());
    $(objControl).find('.address-last-name-hidden').val($(objControl).find('.address-last-name').val());
    $(objControl).find('.address-one-hidden').val($(objControl).find('.address-one').val());
    $(objControl).find('.address-city-hidden').val($(objControl).find('.address-city').val());
    $(objControl).find('.address-zip-hidden').val($(objControl).find('.address-zip').val());
}

function clearFields(objControl) {
    $(objControl).find('.address-first-name').val('');
    $(objControl).find('.address-last-name').val('');
    $(objControl).find('.address-one').val('');
    $(objControl).find('.address-city').val('');
    $(objControl).find('.address-zip').val('');
}

function shippingUpdate_onSuccess(result, message, foo) {
    $("#").html($('#shipping-option-template').render(result.d.shippingOptions));
}

function shippingUpdate_onError(result, message, foo) {

}


function addressSave_onError(result, message, foo) {
    $("#address-dialog-error").dialog("open");
}

function shippingAddressChange() {
    $("#shippingAddressDiv").dialog("open");
}
function billingAddressChange() {
    $("#billingAddressDiv").dialog("open");
}

function showPaypalAUP() {
    window.open("https://cms.paypal.com/us/cgi-bin/marketingweb?cmd=_render-content&content_ID=ua/AcceptableUse_full&locale.x=en_US");
}

function UseShippingAsBilling() {
    

    //create the address response object;
    var requestObject = new Object();
    requestObject.request = new Object();
    var request = requestObject.request;

    //needs the customer id
    request.CustomerId = parseInt($('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val() == '' ? 0 : $('#ctl00_ctl00_cph1_chp2_hdnCustomerId').val());
    //isbilling? Based on the input that sent it.
    request.Billing = 'billing';
    request.UseShippingAsBilling = "1";
    request.address = new Object();

    request.address.addressId = $('#ctl00_ctl00_cph1_cph1_hdnShippingAddressId').val();


    var dataToSend = JSON.stringify(requestObject);

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "../Services/AddressService.svc/SaveAddress",
        data: dataToSend,
        success: addressSave_onSuccess,
        error: addressSave_onError
    });


}