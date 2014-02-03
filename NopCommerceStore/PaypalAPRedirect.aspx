<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaypalAPRedirect.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.PaypalAPRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.4.min.js"></script>
    <script type="text/javascript">
        $(
            function () {
                if ($("#hidRedirectURL").val() != "") {
                    window.location.href = $("#hidRedirectURL").val();
                } else {
                    //redirect to failure page.
                    window.location.href = "PaypalAPPaymentRequestFailure.aspx";
                }
            }
        )
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" runat="server" id="hidRedirectURL" />
        </div>
    </form>
</body>
</html>
