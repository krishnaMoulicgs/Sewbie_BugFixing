<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/main.master" AutoEventWireup="true"
    CodeBehind="Invoices.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.Administration.Invoices" %>
   <%@ Register TagPrefix="nopCommerce" TagName="DatePicker" Src="Modules/DatePicker.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <div class="section-header">
        <div class="title">
            <img src="Common/ico-catalog.png" title="Invoice List" alt=" <%=GetLocaleResourceString("Admin.Invoices.InvoiceList")%>" /> <%=GetLocaleResourceString("Admin.Invoices.InvoiceList")%>
        </div>
        <div class="options">
           <asp:Button ID="SearchButton" runat="server" Text="<% $NopResources:Admin.Invoices.SearchButton.Text %>"
            CssClass="adminButtonBlue" OnClick="SearchButton_Click" ToolTip="<% $NopResources:Admin.Invoices.SearchButton.Tooltip %>" />
      
            <asp:Button runat="server" Text="<% $NopResources:Admin.Invoices.ExportXLSButton.Text %>"
                CssClass="adminButtonBlue" ID="btnExportXLS" ToolTip="<% $NopResources:Admin.Invoices.ExportXLSButton.Tooltip %>"
                OnClick="btnExportXLS_Click" />
        </div>
    </div>
    <table width="100%">
        <tr>
            <td class="adminTitle">
                <asp:Label runat="server" ID="lblVendor" Text="<% $NopResources:Admin.Invoices.lblVendor.Text %>" />
            </td>
            <td class="adminData">
                <asp:DropDownList ID="ddlVendors" runat="server" CssClass="adminInput">
                </asp:DropDownList>
            </td>
            <td class="adminTitle">
                <asp:Label runat="server" ID="lblInvoiceNumber" Text="<% $NopResources:Admin.Invoices.lblInvoiceNumber.Text %>" />
            </td>
            <td class="adminData">
                <asp:TextBox ID="txtInvoiceNumber" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                <asp:Label runat="server" ID="lblInvoiceStatus" Text="<% $NopResources:Admin.Invoices.lblInvoiceStatus.Text %>"  />
            </td>
            <td class="adminData">
                <asp:DropDownList ID="ddlInvoiceStatus" runat="server" CssClass="adminInput">
                <asp:ListItem Text="All"></asp:ListItem>
                <asp:ListItem Text="Pending"></asp:ListItem>
                <asp:ListItem Text="Paid"></asp:ListItem>
                </asp:DropDownList>
            </td>
             <td class="adminTitle">
                <asp:Label runat="server" ID="lblInvoiceDate" Text="<% $NopResources:Admin.Invoices.lblInvoiceDate.Text %>"  />
            </td>
            <td class="adminData">
                <nopCommerce:DatePicker runat="server" ID="ctrlInvoiceDatePicker" />
            </td>
        </tr>
        <tr>
         <td class="adminTitle">
                <asp:Label runat="server" ID="lblStartDate" Text="<% $NopResources:Admin.Invoices.lblStartDate.Text %>"  />
            </td>
            <td class="adminData">
                <nopCommerce:DatePicker runat="server" ID="ctrlStartDatePicker" />
            </td>
            <td class="adminTitle">
                <asp:Label runat="server" ID="lblEndDate" Text="<% $NopResources:Admin.Invoices.lblEndDate.Text %>"  />
            </td>
            <td class="adminData">
                <nopCommerce:DatePicker runat="server" ID="ctrlEndDatePicker" />
            </td>
        </tr>
       <script type="text/javascript">

           $(window).bind('load', function () {
               var cbHeader = $(".cbHeader input");
               var cbRowItem = $(".cbRowItem input");
               cbHeader.bind("click", function () {
                   cbRowItem.each(function () { this.checked = cbHeader[0].checked; })
               });
               cbRowItem.bind("click", function () { if ($(this).checked == false) cbHeader[0].checked = false; });
           });

           function ChildBlock(img, obj) {
               if (obj.style.display == 'none') {
                   obj.style.display = '';
                   img.src = "<%=CommonHelper.GetStoreLocation()%>images/collapse.png";
               }
               else {
                   obj.style.display = 'none';
                   img.src = "<%=CommonHelper.GetStoreLocation()%>images/expand.png";
               }
           }
           
           function OpenPopDetails(InvoiceID) {
               window.open('InvoiceDetails.aspx?InvoiceID=' + InvoiceID, 'mywindow', 'menubar=1,resizable=1,width=700,height=500');
           }

</script>
    <asp:GridView ID="gvInvoice" DataKeyNames="InvoiceID" runat="server" OnPageIndexChanging="gvInvoice_PageIndexChanging" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="15">
    <Columns>
        <asp:TemplateField HeaderText="<% $NopResources:Admin.Common.Select %>" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
                <asp:CheckBox ID="cbInvoice" runat="server" CssClass="cbRowItem" />
                <asp:HiddenField ID="hfInvoiceID" runat="server" Value='<%# Eval("InvoiceID") %>' />
            </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="<% $NopResources:Admin.Invoices.InvoiceID %>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("InvoiceID").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<% $NopResources:Admin.Invoices.Vendor %>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Vendor.CompanyName").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<% $NopResources:Admin.Invoices.Amount %>" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Amount").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<% $NopResources:Admin.Invoices.InvoiceDate %>" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("CreatedOn").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<% $NopResources:Admin.Invoices.Details %>" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%"
            ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <a href="javascript:void(0)" title="Invoice Charges" onclick="OpenPopDetails('<%# Eval("InvoiceID") %>')">
                    View Details</a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
</asp:GridView>
    </table>
</asp:Content>
