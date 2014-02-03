<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HeaderMenuControl"
    CodeBehind="HeaderMenu.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="SearchBox" Src="~/AddonsByOsShop/UserExperience/AjaxSearchAutoComplete/SearchBox.ascx" %>
<%@ Register Src="~/AddonsByOsShop/UserExperience/AjaxMenuPro/HeadMenuPro.ascx" TagName="ProductMenu" TagPrefix="nopCommerce" %>
<script type="text/javascript" src="../Scripts/navsite.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/nav.js"></script>

<div class="menu-block">
    <div class="rightcorner">
        <div id="div_center">
            <ul id="nav">
            <div class="site-nav">
                <div id="floor_nav">
                    <div class="menu">
                        <ul class="floors">
                <li>
                    <asp:HyperLink  runat="server" id="Menu_RecentlyAddedProducts" NavigateUrl="~/Category.aspx?categoryid=0"><%= GetLocaleResourceString("OsShop.whatnew") %></asp:HyperLink >
                </li>
                <%if (MenuSelect == 1)
                      {%>
                       <nopCommerce:ProductMenu runat="server" ID="ctrlProMenuShow" />
                <%=ctrlProMenuShow.Menu_Value%>
                    <%} else if (MenuSelect == 2)
                      {%>
                        <%=Menu_Category%>
                    <%} %>
                <li>
                    <%if ( ManufacturerService.GetAllManufacturers().Count != 0)
                      {%>
                        <%=AllBrands%>
                    <%} %>
                </li>
                <% if ( BlogService.BlogEnabled)
                    { %>
                    <li>
                        <asp:HyperLink  runat="server" id="Menu_Blog" NavigateUrl="http://sewbie.wordpress.com"><%=GetLocaleResourceString("Blog.Blog")%></asp:HyperLink >
                    </li>
                    <%} %> 
                <% if ( ForumService.ForumsEnabled)
                   { %>
                    <li>
                        <asp:HyperLink  runat="server" id="Menu_Forum" NavigateUrl="~/Boards"><%=GetLocaleResourceString("Forum.Forum")%></asp:HyperLink >
                    </li>
                <%} %>
                <nopCommerce:SearchBox ID="ctrlSearchBox" runat="server"/>
                </ul></div></div></div>
            </ul>
        </div>
    </div>
</div>