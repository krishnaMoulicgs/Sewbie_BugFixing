<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ManufacturerAddControl"
    CodeBehind="ManufacturerAdd.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerInfo" Src="ManufacturerInfo.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerSEO" Src="ManufacturerSEO.ascx" %>
<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt="<%=GetLocaleResourceString("VendorAdmin.ManufacturerAdd.Title")%>" />
        <%=GetLocaleResourceString("VendorAdmin.ManufacturerAdd.Title")%><a href="Manufacturers.aspx"
            title="<%=GetLocaleResourceString("VendorAdmin.ManufacturerAdd.BackToManufacturers")%>">
            (<%=GetLocaleResourceString("VendorAdmin.ManufacturerAdd.BackToManufacturers")%>)</a>
    </div>
    <div class="options">
        <asp:Button ID="SaveButton" runat="server" Text="<% $NopResources:VendorAdmin.ManufacturerAdd.SaveButton.Text %>"
            CssClass="adminButtonBlue" OnClick="SaveButton_Click" ToolTip="<% $NopResources:VendorAdmin.ManufacturerAdd.SaveButton.Tooltip %>" />

        <asp:Button ID="SaveAndStayButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ManufacturerAdd.SaveAndStayButton.Text %>"
            OnClick="SaveAndStayButton_Click" />
    </div>
</div>
<ajaxToolkit:TabContainer runat="server" ID="ManufacturerTabs" ActiveTabIndex="0">
    <ajaxToolkit:TabPanel runat="server" ID="pnlManufacturerInfo" HeaderText="<% $NopResources:VendorAdmin.ManufacturerAdd.ManufacturerInfo %>">
        <ContentTemplate>
            <nopCommerce:ManufacturerInfo ID="ctrlManufacturerInfo" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel runat="server" ID="pnlManufacturerSEO" HeaderText="<% $NopResources:VendorAdmin.ManufacturerAdd.SEO %>">
        <ContentTemplate>
            <nopCommerce:ManufacturerSEO ID="ctrlManufacturerSEO" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>