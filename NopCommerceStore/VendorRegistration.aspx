<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SimpleColumn.master" AutoEventWireup="true" CodeBehind="VendorRegistration.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.VendorRegistration" %>
<%@ Register TagPrefix="nopCommerce" TagName="VendorRegister" Src="~/Modules/VendorRegister.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:VendorRegister ID="ctrlVendorRegister" runat="server" />
</asp:Content>
