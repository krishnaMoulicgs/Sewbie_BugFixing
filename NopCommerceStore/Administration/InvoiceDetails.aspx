<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/popup.master" AutoEventWireup="true" CodeBehind="InvoiceDetails.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.Administration.InvoiceDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
  <div class="section-header">
        <div class="title">
            <img src="Common/ico-catalog.png" title="Invoice List" alt=" <%=GetLocaleResourceString("Admin.InvoiceDetails.Details")%>" /> <%=GetLocaleResourceString("Admin.Invoices.InvoiceList")%>
        </div></div>
<asp:GridView ID="gvInvoice" runat="server" OnPageIndexChanging="gvInvoice_PageIndexChanging" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="15">
    <Columns>
            <asp:TemplateField HeaderText="<% $NopResources:Admin.InvoiceDetails.InvoiceID %>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("InvoiceID").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<% $NopResources:Admin.InvoiceDetails.ChargeType %>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Charge.ChargeType.Name").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<% $NopResources:Admin.InvoiceDetails.Amount %>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Amount").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
</asp:GridView>
</asp:Content>
