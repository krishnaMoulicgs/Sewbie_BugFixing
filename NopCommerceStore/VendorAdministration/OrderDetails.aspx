<%@ Page Language="C#" MasterPageFile="~/VendorAdministration/main.master" AutoEventWireup="true"
     Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.VendorAdministration_OrderDetails"
    CodeBehind="OrderDetails.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="OrderDetails" Src="~/VendorAdministration/Modules/OrderDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:OrderDetails runat="server" ID="ctrlOrderDetails" />
</asp:Content>
