<%@ Page Title="" Language="C#" MasterPageFile="~/VendorAdministration/main.master" AutoEventWireup="true" CodeBehind="SellerInfo.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.SellerInfo" %>

<%@ Register TagPrefix="nopCommerce" TagName="AddressEdit" Src="~/Modules/AddressEdit.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">

Entering your store address will save you time entering in your products as this will be the default
shipping address that is used.  You can change it manually anytime or update this page as well.

    <div class="address-edit-page">
        <div class="page-title">
            <h1><asp:Literal runat="server" ID="lHeaderTitle"></asp:Literal></h1>
        </div>
        <div class="clear">
        </div>
        <div class="body">
            <nopCommerce:AddressEdit ID="AddressEditControl" runat="server"></nopCommerce:AddressEdit>
            <table width="100%" cellspacing="0" cellpadding="2" border="0">
                <tbody>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="saveaddressbutton" />
                        </td>
                        <td align="left">
                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="<% $NopResources:Address.Delete %>"
                                CausesValidation="false" CssClass="deleteaddressbutton" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>
