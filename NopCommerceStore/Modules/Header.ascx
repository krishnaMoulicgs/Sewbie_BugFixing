<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HeaderControl"
    CodeBehind="Header.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="CurrencySelector" Src="~/Modules/CurrencySelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="LanguageSelector" Src="~/Modules/LanguageSelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="TaxDisplayTypeSelector" Src="~/Modules/TaxDisplayTypeSelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="MiniShoppingBox" Src="~/AddonsByOsShop/Modules/MiniShoppingBox.ascx"%>

<div class="header">
    <div class="header-logo">
        <a href="<%=CommonHelper.GetStoreLocation()%>" class="logo">&nbsp; </a>
    </div>
    <div class="clearright"></div>

    <div class="header-links-wrapper">
        <div class="header-links">
            <ul>
                <asp:LoginView ID="topLoginView" runat="server">
                    <AnonymousTemplate>
                        <li><a href="<%=Page.ResolveUrl("~/register.aspx")%>" class="ico ico-register">
                            <%=GetLocaleResourceString("Account.Register")%></a></li>
                        <li><a href="<%=Page.ResolveUrl("~/login.aspx")%>" class="ico ico-login">
                            <%=GetLocaleResourceString("Account.Login")%></a></li>
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        <li>
                            <a href="<%= SEOHelper.GetMyAccountUrl()%>" class="account"><%=GetLocaleResourceString("Account.MyAccount")%></a>
                            <% if (NopContext.Current.IsCurrentCustomerImpersonated)
                               { 
                            %>
                            <span class="impersonate">(<%=string.Format(GetLocaleResourceString("Account.ImpersonatedAs"), this.CustomerService.UsernamesEnabled ? Server.HtmlEncode(NopContext.Current.User.Username) : Server.HtmlEncode(NopContext.Current.User.Email))%>
                                -
                                <asp:LinkButton runat="server" ID="lFinishImpersonate" Text="<% $NopResources:Account.ImpersonatedAs.Finish %>"
                                    ToolTip="<% $NopResources:Account.ImpersonatedAs.Finish.Tooltip %>" OnClick="lFinishImpersonate_Click"
                                    CssClass="finish-impersonation"></asp:LinkButton>)</span>
                            <%} %>
                        </li>
                        <li><a href="<%=Page.ResolveUrl("~/logout.aspx")%>" class="ico ico-logout">
                            <%=GetLocaleResourceString("Account.Logout")%></a> &nbsp;</li>
                        <% if (this.ForumService.AllowPrivateMessages)
                           { %>
                        <li><a href="<%=Page.ResolveUrl("~/privatemessages.aspx")%>" class="ico ico-inbox">
                            <%=GetLocaleResourceString("PrivateMessages.Inbox")%></a>
                            <asp:Literal runat="server" ID="lUnreadPrivateMessages" />
                        </li>
                        <%} %>
                    </LoggedInTemplate>
                </asp:LoginView>
               <%-- <li><a href="<%= SEOHelper.GetShoppingCartUrl()%>" class="ico-cart">
                    <%=GetLocaleResourceString("Account.ShoppingCart")%>
                </a><a href="<%= SEOHelper.GetShoppingCartUrl()%>">(<%=this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart).TotalProducts%>)</a>
                </li>--%>                
                <% if (this.SettingManager.GetSettingValueBoolean("Common.EnableWishlist"))
                   { %>
                <li><a href="<%= SEOHelper.GetWishlistUrl()%>" class="ico ico-wishlist">
                    <%=GetLocaleResourceString("Wishlist.Wishlist")%></a> <a href="<%= SEOHelper.GetWishlistUrl()%>">
                        (<%=this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.Wishlist).TotalProducts%>)</a></li>
                <%} %>
                <% if (NopContext.Current.User != null && NopContext.Current.User.IsAdmin)
                   { %>
                <li><a href="<%=Page.ResolveUrl("~/administration/")%>" class="ico ico-admin">
                    <%=GetLocaleResourceString("Account.Administration")%></a> </li>
                <%} %>
                <% if (NopContext.Current.User != null && NopContext.Current.User.IsVendor)
                   { %>
                <li><a href="<%=Page.ResolveUrl("~/vendoradministration/Default.aspx")%>" class="ico ico-admin">
                    <%=GetLocaleResourceString("Account.VendorAdministration")%></a> </li>
                <%} %>
                <% if (NopContext.Current.User != null && !NopContext.Current.User.IsVendor)
                   { %>
                <li><a href="<%=Page.ResolveUrl("~/SellerSignUp.aspx")%>" class="ico ico-admin">
                    <%=GetLocaleResourceString("Account.VendorRegistration")%></a> </li>
                <%} %>
            </ul>
        </div>
    </div>
    <div class="header-selectors-wrapper">
        <div class="header-taxDisplayTypeSelector">
            <nopCommerce:TaxDisplayTypeSelector runat="server" ID="ctrlTaxDisplayTypeSelector">
            </nopCommerce:TaxDisplayTypeSelector>
        </div>
        <div class="header-languageselector">
            <nopCommerce:LanguageSelector runat="server" ID="ctrlLanguageSelector"></nopCommerce:LanguageSelector>
        </div>
    </div>

    <div class="MiniShoppingBox">
        <nopCommerce:MiniShoppingBox ID="ctrlMiniShoppingBox" runat="server" />
    </div>
</div>
