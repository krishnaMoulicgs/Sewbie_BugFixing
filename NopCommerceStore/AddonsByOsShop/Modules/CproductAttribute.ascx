<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CproductAttribute.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules.CproductAttribute" %>

<asp:panel ID="panel" runat="server">
<div class="block block-category-navigation">
    <div class="title">
        <%=title%>
    </div>
    <div class="clear"></div>
    <div class="listbox"  id="<%=pannelTitle %>_<%=titleIndex%>">
        <ul id="refineBy<%=titleId%>">
            <asp:PlaceHolder runat="server" ID="placeHold" />
        </ul>
    </div>
</div>
</asp:panel>