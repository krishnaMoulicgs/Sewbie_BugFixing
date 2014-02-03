<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsLetterSubscriptionBoxControl.ascx.cs"
    Inherits="NopSolutions.NopCommerce.Web.Modules.NewsLetterSubscriptionBoxControl" %>
<%@ Register TagPrefix="nopCommerce" TagName="EmailTextBox" Src="EmailTextBox.ascx" %>
<div class="block block-newsletter">
    <div class="title">
        <%=GetLocaleResourceString("NewsLetterSubscriptionBox.Info")%>
    </div>
    <div class="clear">
    </div>



    <div class="listbox">
        <div runat="server" id="pnlSubscribe">
            <div style="vertical-align: middle; width: 150px; display:inline;">
                <nopCommerce:EmailTextBox runat="server" ID="txtEmail" ValidationGroup="NewsLetterSubscriptionValidation" />
            </div>
            <div class="buttons" style="display:inline; width:50px;">
                <asp:Button runat="server" ID="btnSubscribe" Text="<% $NopResources:NewsLetterSubscriptionBox.BtnSubscribe.Text %>"
                    ValidationGroup="NewsLetterSubscriptionValidation" OnClick="BtnSubscribe_OnClick" CssClass="newsletterbox-subscribebutton"  />
            </div>

              <%--<asp:Label ID="Label1" runat="server" Text="<% $NopResources:NewsLetterSubscriptionBox.EmailInput %>" />:--%>
             


             <div class="options">
                <asp:RadioButton runat="server" ID="rbSubscribe" Text="<% $NopResources:NewsLetterSubscriptionBox.Subscribe %>"
                    Checked="true" ValidationGroup="NewsLetterSubscriptionValidation" GroupName="NewsLetterSubscription" />
                <asp:RadioButton runat="server" ID="rbUnsubscribe" Text="<% $NopResources:NewsLetterSubscriptionBox.Unsubscribe %>"
                    ValidationGroup="NewsLetterSubscriptionValidation" GroupName="NewsLetterSubscription" />
            </div>


          
        </div>



        <div runat="server" id="pnlResult" visible="false">
            <asp:Label runat="server" ID="lblResult" />
        </div>
    </div>
</div>
