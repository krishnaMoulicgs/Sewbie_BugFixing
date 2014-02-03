<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ManufacturersControl"
    CodeBehind="Manufacturers.ascx.cs" %>
<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt="<%=GetLocaleResourceString("VendorAdmin.Manufacturers.Title")%>" />
        <%=GetLocaleResourceString("Admin.Manufacturers.Title")%>
    </div>
    <div class="options">
        <input type="button" onclick="location.href='ManufacturerAdd.aspx'" value="<%=GetLocaleResourceString("VendorAdmin.Manufacturers.AddButton.Text")%>"
            id="btnAddNew" class="adminButtonBlue" title="<%=GetLocaleResourceString("VendorAdmin.Manufacturers.AddButton.Tooltip")%>" />
        <asp:Button runat="server" Text="<% $NopResources:VendorAdmin.Manufacturers.ExportXMLButton.Text %>"
            CssClass="adminButtonBlue" ID="btnExportXML" OnClick="btnExportXML_Click" ValidationGroup="ExportXML"
            ToolTip="<% $NopResources:VendorAdmin.Manufacturers.ExportXMLButton.Tooltip %>" />
    </div>
</div>
<table class="adminContent">
    <asp:GridView ID="gvManufacturers" runat="server" AutoGenerateColumns="False" Width="100%"
    OnPageIndexChanging="gvManufacturers_PageIndexChanging" AllowPaging="true" PageSize="15">
        <Columns>
            <asp:TemplateField HeaderText="<% $NopResources:VendorAdmin.Manufacturers.Name %>" ItemStyle-Width="65%">
                <ItemTemplate>
                    <%#Server.HtmlEncode(Eval("Name").ToString())%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<% $NopResources:VendorAdmin.Manufacturers.Published %>"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <nopCommerce:ImageCheckBox runat="server" ID="cbPublished" Checked='<%# Eval("Published") %>'>
                    </nopCommerce:ImageCheckBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<% $NopResources:VendorAdmin.Manufacturers.Edit %>" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="ManufacturerDetails.aspx?ManufacturerID=<%#Eval("ManufacturerId")%>" title="<%#GetLocaleResourceString("VendorAdmin.Manufacturers.Edit.Tooltip")%>">
                        <%#GetLocaleResourceString("VendorAdmin.Manufacturers.Edit")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
    </asp:GridView>
</table>
