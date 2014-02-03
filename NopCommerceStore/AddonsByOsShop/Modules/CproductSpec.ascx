<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CproductSpec.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules.CproductSpec" %>

<asp:panel ID="panel" runat="server">
<div class="block block-category-navigation">
    <div class="title">
        <%=title%>
    </div>
    <div class="clear"></div>
    <div class="listbox"  id="<%=pannelTitle %>_<%=titleIndex%>">
        <ul id="refineSpeBy<%=titleId%>">
            <asp:PlaceHolder runat="server" ID="phSpec" />
        </ul>
    </div>
</div>
</asp:panel>