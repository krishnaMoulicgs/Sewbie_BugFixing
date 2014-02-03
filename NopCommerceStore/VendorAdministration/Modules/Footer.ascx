<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.FooterControl"
    CodeBehind="Footer.ascx.cs" %>

<%--<%@ Register TagPrefix="nopCommerce" TagName="NewsLetterSubscriptionBoxControl" Src="~/Modules/NewsLetterSubscriptionBoxControl.ascx" %>--%>
<div class="footer">
    <div class="footerInfo">
        <div class="leftInfo">
            <ul>
               <li><a href="<%=Page.ResolveUrl("~/contactus.aspx")%>"><%=GetLocaleResourceString("ContactUs.ContactUs")%></a></li>
                <li>|</li>
                <li><a href="<%=Page.ResolveUrl("~/aboutus.aspx")%>"><%=GetLocaleResourceString("Content.AboutUs")%></a></li>
                <li>|</li>
                <li><a href="<%=Page.ResolveUrl("~/shippinginfo.aspx")%>"><%=GetLocaleResourceString("Content.Shipping&Returns")%></a></li>
                <li>|</li>
                <li><a href="<%=Page.ResolveUrl("~/privacyinfo.aspx")%>"><%=GetLocaleResourceString("Content.PrivacyNotice")%></a></li>
                <li>|</li>
                <li><a href="<%=Page.ResolveUrl("~/conditionsinfo.aspx")%>"><%=GetLocaleResourceString("Content.ConditionsOfUse")%></a></li>
                <li>|</li>
                <li><a href="<%=Page.ResolveUrl("~/sitemap.aspx")%>"><%=GetLocaleResourceString("Content.Sitemap")%></a></li>
            </ul>
        </div>
       
    </div>
    <div class="footer-disclaimer">
        <%=String.Format(GetLocaleResourceString("Content.CopyrightNotice"), 
                                    DateTime.Now.Year.ToString(), 
                                    SettingManager.StoreName)%>
    </div>
    <div>
        <%--<nopCommerce:NewsLetterSubscriptionBoxControl ID="ctrlNewsLetterSubscriptionBoxControl" runat="server" />--%>
    </div>
</div>