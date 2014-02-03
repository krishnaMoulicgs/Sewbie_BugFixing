function btnSubscribe_Click() {
    var emailAddress;

    $.ajax({
    type: "POST",
    url: "Services/NewsLetterSubscription/ExtendedNewletterSubscriptionService.asmx/subscribeEmail",
    contentType: "application/json; charset=utf-8",
    data: "{'pEmail':'" + emailAddress + "'}",
    dataType: "json",
    success: AjaxSucceeded,
    error: AjaxFailed
    });
}


function AjaxSucceeded(result) {
    alert("succeed!");
}

function AjaxFailed(result) {
    alert("Fail!");
}