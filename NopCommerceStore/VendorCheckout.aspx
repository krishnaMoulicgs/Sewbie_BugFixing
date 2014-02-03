<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.VendorCheckoutPage" CodeBehind="VendorCheckout.aspx.cs"
    EnableEventValidation="false"  %>

<%@ Register TagPrefix="nopCommerce" TagName="CheckoutOnePage" Src="~/Modules/VendorCheckout.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
<%--    <script type="text/javascript">
        $(document).ready(function () {

            //if there is a value there, redirect to the 
            if ($('#ctl00_ctl00_cph1_cph1_ctrlCheckoutOnePage_ctrlCheckoutConfirm_hidPaypalURL').val() != "") { // or .len == 0
                document.getElementById('PPDGFrame').src = $('#ctl00_ctl00_cph1_cph1_ctrlCheckoutOnePage_ctrlCheckoutConfirm_hidPaypalURL').val();
            }

        });
    </script>--%>

    <nopCommerce:CheckoutOnePage ID="ctrlCheckoutOnePage" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph2" runat="server">
    <script type="text/javascript" charset="utf-8" src="scripts/VendorCheckout.js"></script>
</asp:Content>
