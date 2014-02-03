<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="CheckOutOnePage.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.OnePagePay.CheckOutOnePage" %>

<%@ Register TagPrefix="nopCommerce" TagName="AddressEditPay" Src="/AddonsByOsShop/Modules/AddressEditPay.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="PaypalExpressButton" Src="~/Modules/PaypalExpressButton.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="OrderSummary" Src="~/Modules/OrderSummary.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="AddressDisplay" Src="~/Modules/AddressDisplay.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">  

<link type='text/css' href='/AddonsByOsShop/Styles/osx.css' rel='stylesheet' media='screen' /> 
           
<script type='text/javascript' src='/AddonsByOsShop/CustomScripts/jquery.simplemodal.js'></script>
<script type='text/javascript' src="/AddonsByOsShop/CustomScripts/osx.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/jquery.ui.draggable.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/jquery.alerts.js"></script>


<script type="text/javascript">
    var signAddress2 = 0;
    var NoShippingMethod = 0;
    $(document).ready(function () {
        if (signAddress2 == 0)
            $("#address2").hide();
        //$("#DefaultBillingAddressShow").hide();
    });
    function createMessage(Id) {
        var queryString = { checkoutAddressId: Id };
        return queryString;
    }

    function getShippingAddressByAjax(ShippingAddressID) {
        var addressId = ShippingAddressID;
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/getExistAddress.aspx?" + new Date().getTime(),
            type: "Get",
            data: createMessage(addressId),
            success: function (data, statue) {

                var addressData = data;
                addressData = addressData + "";

                var strItem = new Array();
                strItem = addressData.split("<!>");

                var a = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtFirstName_txtValue");
                a.value = strItem[0];
                var b = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtLastName_txtValue");
                b.value = strItem[1];
                var c = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtPhoneNumber_txtValue");
                c.value = strItem[2];
                var d = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtEmail_txtValue");
                d.value = strItem[3];
                var e = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtFaxNumber");
                e.value = strItem[4];
                var f = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtCompany");
                f.value = strItem[5];
                var g = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtAddress1_txtValue");
                g.value = strItem[6];
                var h = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtAddress2");
                h.value = strItem[7];
                var i = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtCity_txtValue");
                i.value = strItem[8];
                var j = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_ddlCountry");
                j.value = strItem[9];

                var k = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtStateProvince");
                k.value = strItem[10];
                //$("#ctl00_ctl00_cph1_cph1_ctrlShippingAddress_ddlStateProvince").empty();
                $("#ctl00_ctl00_cph1_cph1_ctrlShippingAddress_ddlStateProvince").append("<option value='1' selected='true'>" + strItem[10] + "</option>");

                var l = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlShippingAddress_txtZipPostalCode_txtValue");
                l.value = strItem[11];

                showAddressDisabled(1,0);


            }
        });


    }
    function getBillingAddressByAjax(BillingAddressID) {
        var addressId = BillingAddressID;
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/getExistAddress.aspx?" + new Date().getTime(),
            type: "Get",
            data: createMessage(addressId),
            success: function (data, statue) {

                var addressData = data;
                addressData = addressData + "";

                var strItem = new Array();
                strItem = addressData.split("<!>");

                var a = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtFirstName_txtValue");
                a.value = strItem[0];
                var b = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtLastName_txtValue");
                b.value = strItem[1];
                var c = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtPhoneNumber_txtValue");
                c.value = strItem[2];
                var d = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtEmail_txtValue");
                d.value = strItem[3];
                var e = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtFaxNumber");
                e.value = strItem[4];
                var f = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtCompany");
                f.value = strItem[5];
                var g = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtAddress1_txtValue");
                g.value = strItem[6];
                var h = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtAddress2");
                h.value = strItem[7];
                var i = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtCity_txtValue");
                i.value = strItem[8];
                var j = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_ddlCountry");
                j.value = strItem[9];

                var k = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtStateProvince");
                k.value = strItem[10];
                $("#ctl00_ctl00_cph1_cph1_ctrlBillingAddress_ddlStateProvince").empty();
                $("#ctl00_ctl00_cph1_cph1_ctrlBillingAddress_ddlStateProvince").html("<option value='1' selected='selected'>" + strItem[10] + "</option>");

                var l = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlBillingAddress_txtZipPostalCode_txtValue");
                l.value = strItem[11];

                showAddressDisabled(0,1);

            }
        });

    }
    function shippingAddressShow() {

        $("#shippingAddressShow").show();
        $("#billingAddressShow").hide();
    }
    function billingAddressShow() {
        $("#billingAddressShow").show();
        $("#shippingAddressShow").hide();
    }

    function showHideCheckoutAddress(sign, signA, signB) {
        if (sign == 1) {
            $("#address2").hide();
            //$("#DefaultBillingAddressShow").hide();
        }
        if (sign == 2) {
            $("#address2").show();
            //$("#DefaultBillingAddressShow").show();
        }
         if (sign == 3) {
             $("#address1").hide();
             $(".OnePagePay_address .select-address-title").css("margin-bottom","0px");
             $("#DefaultShippingAddressShow").hide();
             $("#DefaultBillingAddressShow").hide();
        }

        if (parseInt(signA) == 1) {
            $("#ShippingAddressDisabled input").attr("disabled", "false");
            $("#ShippingAddressDisabled select").attr("disabled", "false");
        }
        if (parseInt(signB) == 1) {
            $("#BillingAddressDisabled input").attr("disabled", "false");
            $("#BillingAddressDisabled select").attr("disabled", "false");
        }

        if (sign == 5) {
            $("#address1").hide();
            $(".OnePagePay_address .select-address-title").css("margin-bottom", "0px");
            $("#DefaultShippingAddressShow").hide();
            $("#DefaultBillingAddressShow").hide();
            $(".OnePagePay_address").css("padding-top", "0px");
        }
        if (sign == 6) {
            signAddress2 = 1;
            NoShippingMethod = 1;
           $("#address1").hide();
           $("#address2").show();
           $("#address2").css("top", "44px");
           $("#address2").css("top", "39px\0");
           $(".OnePagePay_address").css("padding-top", "0px");
           //$("#address2").attr("top","0px");

        }

       
    }



    function showAddressDisabled(signA1, signB1) {
        if (parseInt(signA1) == 1) {
            $("#ShippingAddressDisabled input").attr("disabled", "false");
            $("#ShippingAddressDisabled select").attr("disabled", "false");

        }
        if (parseInt(signB1) == 1) {
            $("#BillingAddressDisabled input").attr("disabled", "false");
            $("#BillingAddressDisabled select").attr("disabled", "false");
        }
    }



    function alertMessage(signA, signB, signC) {
        //signC 判断是否有运送方式
        if (parseInt(signB) == 1) {
            $("#address1").hide();
            $(".OnePagePay_address .select-address-title").css("margin-bottom", "0px");
            $("#DefaultShippingAddressShow").hide();
            $("#DefaultBillingAddressShow").hide();
        }

        if (parseInt(signA) == 1) {
            jAlert('<%=GetLocaleResourceString("OsShop.ServiceTerms")%>');
        }
        if (parseInt(signA) == 2) {
            jAlert('<%=GetLocaleResourceString("OsShop.AddressBothTips")%>');
        }
        if (parseInt(signA) == 3) {
            jAlert('<%=GetLocaleResourceString("OsShop.AddressShippingTips")%>');
        }
        if (parseInt(signA) == 4) {
            jAlert('<%=GetLocaleResourceString("OsShop.AddressBillingTips")%>');
        }

        if (parseInt(signC) == 1) {
            showHideCheckoutAddress(6, 0, 0);
        }
    }



</script>
  
 <div id='container'>
	
	<div id='content'>

        <div id='osx-modal'>
            <input id="address1" type='button' name='osx' value="<%=GetLocaleResourceString("OsShop.ShowShippingAddresses")%>" class='osx demo' onclick="shippingAddressShow();"/> 
            <input id="address2" type='button' name='osx' value="<%=GetLocaleResourceString("OsShop.ShowBillingAddresses")%>" class='osx demo' onclick="billingAddressShow();"  /> 
        </div>
	
		<!-- modal content -->
		<div id="osx-modal-content">
			<div id="osx-modal-title"><%=GetLocaleResourceString("OsShop.SelectCurrentAddresses")%></div>
			<div class="close"><a href="#" class="simplemodal-close">x</a></div>
			<div id="osx-modal-data">
            <!--显示已有 收货人地址-->
		    <div id="shippingAddressShow">
            <asp:DataList ID="dlShippingAddresses" runat="server" RepeatColumns="0" RepeatDirection="Horizontal"
                RepeatLayout="Flow" ItemStyle-CssClass="item-box">
                <ItemTemplate>
                    <div class="address-item">
                        <div class="select-button">
                     
                            <input type="button" id="shipping" value="<%#GetLocaleResourceString("Checkout.ShipToThisAddress")%>" onclick="getShippingAddressByAjax(<%# Eval("AddressId") %>);" />
                        </div>
                        <div class="address-box">
                            <nopCommerce:AddressDisplay ID="adAddress1" runat="server" Address='<%# Container.DataItem %>'
                                ShowDeleteButton="false" ShowEditButton="false"></nopCommerce:AddressDisplay>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
           </div>
          <!--显示已有 付款人地址-->
          <div id="billingAddressShow">
          <asp:DataList ID="dlBillingAddresses" runat="server" RepeatColumns="0" RepeatDirection="Horizontal"
                RepeatLayout="Flow" ItemStyle-CssClass="item-box">
                <ItemTemplate>
                    <div class="address-item">
                        <div class="select-button">

                       <input type="button" id="billing" value="<%#GetLocaleResourceString("Checkout.BillingToThisAddress")%>" onclick="getBillingAddressByAjax(<%# Eval("AddressId") %>);" />
                        </div>
                        <div class="address-box">
                            <nopCommerce:AddressDisplay ID="adAddress2" runat="server" Address='<%# Container.DataItem %>'
                                ShowDeleteButton="false" ShowEditButton="false"></nopCommerce:AddressDisplay>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            </div>

                <p><button class="simplemodal-close"><%=GetLocaleResourceString("OsShop.Close")%></button> <span>(<%=GetLocaleResourceString("OsShop.ESC")%>)</span></p>
			</div>
		</div>
	</div>
	
</div>
          





<asp:ScriptManager ID="ScriptManager1" runat="server" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <!--收货人地址-->   
<div class="OnePagePay_address">
<asp:PlaceHolder ID="dealNoShippingAddressOne" runat="server">
<div class="select-address-title">
<%=GetLocaleResourceString("Checkout.SelectShippingAddress")%>
</div>
<div class="clear">
</div>
 <div id="DefaultShippingAddressShow" class="OnePagePay_defaultAddress">
<asp:Button runat="server" ID="DefaultShippingAddress" Text="<% $NopResources:OsShop.DefalutAddress %>"  CssClass="defaultAddress" OnClick="btnDefaultShippingAddress_Click" />
</div>
<div class="enter-address-body" id="ShippingAddressDisabled">
<nopCommerce:AddressEditPay ID="ctrlShippingAddress" runat="server" IsNew="true" IsBillingAddress="false"
ValidationGroup="EnterBillingAddress" />
</div>
</asp:PlaceHolder>
<br/>


<!--单选框 处理显示付款地址-->
<asp:CheckBox ID="ShowBilling" runat="server" oncheckedchanged="ShowBilling_CheckedChanged" AutoPostBack="true" Checked="true" Text="<% $NopResources:Checkout.BillingAddressTheSameAsShippingAddress %>" />
<!--付款人地址-->
<asp:PlaceHolder ID="dealBilling" runat="server" Visible="false">
<div class="select-address-title">
<%=GetLocaleResourceString("Checkout.SelectBillingAddress")%>
</div>
<div class="clear">   
</div>
<div id="DefaultBillingAddressShow" class="OnePagePayBillingAddress_defaultAddress">
<asp:Button runat="server" ID="DefaultBillingAddress" Text="<% $NopResources:OsShop.DefalutAddress %>" CssClass="defaultAddress" OnClick="btnDefaultBillingAddress_Click" />
</div>
<div class="enter-address-body" id="BillingAddressDisabled">
<nopCommerce:AddressEditPay ID="ctrlBillingAddress" runat="server" IsNew="true" IsBillingAddress="true" />
</div>
</asp:PlaceHolder>
</div>
<!--运送方式-->

<div class="Onepagepay_checkoutdata">
<asp:PlaceHolder ID="dealNoShippingAddressTwo" runat="server">
    <div class="checkout-data">             
        <div class="select-address-title">
        <%=GetLocaleResourceString("Checkout.SelectShippingMethod")%>
        </div>
    <div class="shipping-options">
        <asp:Panel runat="server" ID="phSelectShippingMethod">
            <asp:DataList runat="server" ID="dlShippingOptions">
                <ItemTemplate>
                    <div class="shipping-option-item">
                        <div class="option-name">
                            <nopCommerce:GlobalRadioButton runat="server" ID="rdShippingOption" Checked="false"
                                GroupName="shippingOptionGroup" AutoPostBack="true" OnCheckedChanged="ShippingMethod_CheckedChanged"/>
                            <%#Server.HtmlEncode(Eval("Name").ToString()) %>
                            <%#Server.HtmlEncode(FormatShippingOption(((ShippingOption)Container.DataItem)))%>
                            <asp:HiddenField ID="hfShippingRateComputationMethodId" runat="server" Value='<%# Eval("ShippingRateComputationMethodId") %>' />
                            <asp:HiddenField ID="hfName" runat="server" Value='<%# Eval("Name") %>' />
                        </div>
                        <div class="option-description">
                            <%#Eval("Description") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <div class="clear">
            </div>
        </asp:Panel>
        <div class="clear">
        </div>
        <div class="error-block">
            <div class="message-error">
                <asp:Literal runat="server" ID="lShippingMethodsError" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </div>
</div>
</asp:PlaceHolder>

<!--支付方式-->
<div class="checkout-data">
        <div class="select-address-title">
        <%=GetLocaleResourceString("Checkout.SelectPaymentMethod")%>
        </div>
                    
    <asp:Panel runat="server" ID="pnlRewardPoints" CssClass="userewardpoints">
        <asp:CheckBox runat="server" ID="cbUseRewardPoints" AutoPostBack="true" OnCheckedChanged="cbUseRewardPoints_CheckedChanged" Text="Use my reward points" />
    </asp:Panel>
     <asp:PlaceHolder runat="server" ID="rewardPointShowHide1">
    <div class="clear">
    </div>
    <nopCommerce:PaypalExpressButton runat="server" ID="btnPaypalExpressButton"></nopCommerce:PaypalExpressButton>
    <br />
    <div class="payment-methods">
        <asp:PlaceHolder runat="server" ID="phSelectPaymentMethod">
            <asp:DataList runat="server" ID="dlPaymentMethod" DataKeyField="PaymentMethodId">
                <ItemTemplate>
                    <div class="payment-method-item" style="text-align: left;">
                        <nopCommerce:GlobalRadioButton runat="server" ID="rdPaymentMethod" Checked="false"
                            GroupName="paymentMethodGroup"  AutoPostBack="true" OnCheckedChanged="BillingMethod_CheckedChanged"/>
                        <%#Server.HtmlEncode(Eval("VisibleName").ToString()) %>
                        <%#Server.HtmlEncode(FormatPaymentMethodInfo(((PaymentMethod)Container.DataItem)))%>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <div class="clear">
            </div>
        </asp:PlaceHolder>
        <div class="clear">
        </div>
        <asp:Panel runat="server" ID="pnlPaymentMethodsError" Visible="false" CssClass="error-block">
            <div class="message-error">
                <asp:Label runat="server" ID="lPaymentMethodsError"></asp:Label>
            </div>
        </asp:Panel>
    </div>
    </asp:PlaceHolder>
</div>
<!--支付方式对应的信息-->
<asp:PlaceHolder runat="server" ID="rewardPointShowHide2">
<div class="checkout-data" runat="server" visible="false" id="checkoutDataMessage" >
    <div class="payment-info">
        <div class="body">
        <asp:PlaceHolder runat="server" ID="PaymentInfoPlaceHolder" ViewStateMode="Disabled"></asp:PlaceHolder>
        </div>
        <div class="clear">
        </div>
    </div>
</div>
</asp:PlaceHolder>
</div>


<script type="text/javascript" language="javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    var sign = 1;
    function BeginRequestHandler(sender, args) {
        if (NoShippingMethod == 1) {
            $(".OnePagePay_address").css("padding-top", "0px");
        }
        if (sign == 1) {
            document.getElementById("ShowDetialFood").style.display = "none";
            document.getElementById("ShowLoading").style.display = "block";
        }
    }
    function EndRequestHandler(sender, args) {
        if (NoShippingMethod == 1) {
            $(".OnePagePay_address").css("padding-top", "0px");
        }
        if (sign == 1) {
            document.getElementById("ShowDetialFood").style.display = "block";
            document.getElementById("ShowLoading").style.display = "none";
        }
        sign = 1;
    }

    function NotShowLoading() {
        sign = 0;
        if (NoShippingMethod == 1) {
            $(".OnePagePay_address").css("padding-top", "0px");
        }
    }
</script>


<!--显示 购物车详情-->
<div class="OnePagePay_summary">    
<div class="order-summary-title">
<%=GetLocaleResourceString("Checkout.OrderSummary")%>
</div>
<div class="clear">
</div>


<div class="order-summary-body">
<nopCommerce:OrderSummary ID="OrderSummaryControl" runat="server" IsShoppingCart="false">
</nopCommerce:OrderSummary>
</div>
<div id="ShowLoading" style="display:none;">
  
</div>

    <%if (SettingManager.GetSettingValueBoolean("Checkout.TermsOfServiceEnabled"))
    { %>

<script language="javascript" type="text/javascript">
    function accepttermsofservice(msg) {
        if (!document.getElementById('<%=cbTermsOfService.ClientID%>').checked) {
            alert(msg);
            return false;
        }
        else
            return true;
    }
</script>
           
<div class="terms-of-service">
    <asp:CheckBox runat="server" ID="cbTermsOfService" /> <asp:Literal runat="server" ID="lTermsOfService" />
</div>
<%} %>

<!--结账下一步-->

<div class="button">
<asp:Button runat="server" ID="btnNextStep" Text="Place Order"
OnClick="btnNextStep_Click" OnClientClick="NotShowLoading();" CssClass="newaddressnextstepbutton" ValidationGroup="EnterBillingAddress" />
</div>  
       
</div>
</ContentTemplate>
<%--
<Triggers>
    <asp:PostBackTrigger ControlID="btnNextStep" />
</Triggers>
--%>
</asp:UpdatePanel>
      


</asp:Content>

