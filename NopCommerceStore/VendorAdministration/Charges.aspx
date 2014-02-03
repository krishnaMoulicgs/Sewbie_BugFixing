<%@ Page Title="" Language="C#" MasterPageFile="~/VendorAdministration/main.master" AutoEventWireup="true" CodeBehind="Charges.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Charges" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
 <div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" title="Manage Charges" alt=" <%=GetLocaleResourceString("Admin.Charges.ManageCharges")%>" /> <%=GetLocaleResourceString("Admin.Charges.ManageCharges")%>
         
    </div>
     <div class="options">
     <asp:Button ID="SearchButton" runat="server" Text="<% $NopResources:Admin.Charges.SearchButton.Text %>"
            CssClass="adminButtonBlue" OnClick="SearchButton_Click" ToolTip="<% $NopResources:Admin.Charges.SearchButton.Tooltip %>" />
</div></div>
    <table width="100%">
    <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblChargeTypes" Text="<% $NopResources:Admin.Charges.ChargeType %>"/>
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlChargeTypes" runat="server" CssClass="adminInput">
            </asp:DropDownList>
        </td>
    </tr>
    <asp:GridView ID="gvCharges" runat="server" OnPageIndexChanging="gvCharges_PageIndexChanging" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="15">
    <Columns>
        <asp:TemplateField HeaderText="<% $NopResources:Admin.Common.Select %>" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
                <asp:CheckBox ID="cbCharge" runat="server" CssClass="cbRowItem" />
                <asp:HiddenField ID="hfChargeID" runat="server" Value='<%# Eval("ChargeID") %>' />
            </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="<% $NopResources:Admin.Charges.ChargeID%>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("ChargeID").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Vendor" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Vendor.CompanyName").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Charge Type" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("ChargeType.Name").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="<% $NopResources:Admin.Charges.Amount%>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Amount").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<% $NopResources:Admin.Charges.ChargeDate%>" ItemStyle-Width="30%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("CreatedOn").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<% $NopResources:Admin.Charges.EffectiveDate%>" ItemStyle-Width="30%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("EffectiveDate", "{0:MM/dd/yyyy}").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<% $NopResources:Admin.Charges.IsInvoiceCharge%>" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <nopCommerce:ImageCheckBox runat="server" ID="cbIsInvoiceCharge" Checked='<%# Eval("IsInvoiceCharge") %>'>
                    </nopCommerce:ImageCheckBox>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
    <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
</asp:GridView>


    </table>
    </asp:Content>

