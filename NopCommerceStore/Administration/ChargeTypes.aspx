<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/main.master" AutoEventWireup="true" CodeBehind="ChargeTypes.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.Administration.ChargeTypes" %>
<%@ Register TagPrefix="nopCommerce" TagName="ConfirmationBox" Src="Modules/ConfirmationBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
   

 <div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" title="Manage Charge Types" alt=" <%=GetLocaleResourceString("Admin.ChargeTypes.ManageChargeTypes")%>" />
         <%=GetLocaleResourceString("Admin.ChargeTypes.ManageChargeTypes")%>
    </div>
     <div class="options">
        <input type="button" onclick="location.href='ChargeTypeAdd.aspx'" value="<%=GetLocaleResourceString("Admin.ChargeTypes.AddButton.Text")%>"
            id="btnAddNew" class="adminButtonBlue" title="<%=GetLocaleResourceString("Admin.ChargeTypes.AddButton.Tooltip")%>" />
       <asp:Button runat="server" Text="<% $NopResources:Admin.ChargeTypes.ExportXLSButton.Text %>"
            CssClass="adminButtonBlue" ID="btnExportXLS" ToolTip="<% $NopResources:Admin.ChargeTypes.ExportXLSButton.Tooltip %>"  OnClick="btnExportXLS_Click"/>   
            <asp:Button runat="server" Text="<% $NopResources:Admin.ChargeTypes.DeleteButton.Text %>"
            CssClass="adminButtonBlue" ID="btnDelete"  OnClick="btnDelete_Click" />
            </div>
</div>
<nopCommerce:ConfirmationBox runat="server" ID="cbDelete" TargetControlID="btnDelete"
    YesText="<% $NopResources:Admin.Common.Yes %>" NoText="<% $NopResources:Admin.Common.No %>"
    ConfirmText="<% $NopResources:Admin.Common.AreYouSure %>" />
<table width="100%">
    <asp:GridView ID="gvCharges" runat="server" OnPageIndexChanging="gvCharges_PageIndexChanging" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="15">
    <Columns>
        <asp:TemplateField HeaderText="<% $NopResources:Admin.Common.Select %>" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
                <asp:CheckBox ID="cbCharge" runat="server" CssClass="cbRowItem" />
                <asp:HiddenField ID="hfChargeTypeID" runat="server" Value='<%# Eval("ChargeTypeID") %>' />
            </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="<% $NopResources:Admin.ChargeTypes.Name %>" ItemStyle-Width="35%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Name").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<% $NopResources:Admin.ChargeTypes.Description%>" ItemStyle-Width="35%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Description").ToString())%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<% $NopResources:Admin.ChargeTypes.IsActive %>" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <nopCommerce:ImageCheckBox runat="server" ID="cbStatus" Checked='<%# Eval("IsActive") %>'>
                    </nopCommerce:ImageCheckBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<% $NopResources:Admin.ChargeTypes.Edit %>" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="ChargeTypeDetails.aspx?ChargeTypeID=<%#Eval("ChargeTypeID")%>" title="Edit">
                       Edit
                    </a>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>       
    </Columns>
    <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
</asp:GridView>

    </table>

</asp:Content>
