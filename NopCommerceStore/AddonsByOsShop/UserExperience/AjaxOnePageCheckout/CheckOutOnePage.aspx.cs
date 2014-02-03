using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Audit.UsersOnline;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Content.Blog;
using NopSolutions.NopCommerce.BusinessLogic.Content.Forums;
using NopSolutions.NopCommerce.BusinessLogic.Content.NewsManagement;
using NopSolutions.NopCommerce.BusinessLogic.Content.Polls;
using NopSolutions.NopCommerce.BusinessLogic.Content.Topics;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.ExportImport;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Maintenance;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Measures;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.Messages.SMS;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.BusinessLogic.Products.Specs;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Affiliates;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Campaigns;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.QuickBooks;
using NopSolutions.NopCommerce.BusinessLogic.Security;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.BusinessLogic.Warehouses;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.Templates.Payment;


namespace NopSolutions.NopCommerce.Web.OnePagePay
{

    public partial class CheckOutOnePage : System.Web.UI.Page
    {
        protected CheckoutStepChangedEventHandler handler;
        protected ShoppingCart cart = null;
        protected bool paymentControlLoaded = false;
        static int agreeSign;
        static int shippingMethodSign;
        static int billingMethodSign;
        static bool AnonymousCheckoutAllowedSign;
        static int BillingAddressId, ShippingAddressId;
        static int SunTestSign;

        



        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user did not login or shopping cart is empty jump to main page
            if (this.Cart.Count == 0)
                Response.Redirect(CommonHelper.GetStoreLocation());
            
            if (!Page.IsPostBack)
            {
                //Interpretation of whether the shipping method
                bool shoppingCartRequiresShipping = ShippingService.ShoppingCartRequiresShipping(Cart);
                //If anonymous access to hide the existing address display effects
                if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
                {
                    AnonymousCheckoutAllowedSign = true;
                    if (!shoppingCartRequiresShipping)
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(5,0,0);", true);
                    else
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(3,0,0);", true);
                }
                else
                {
                    if (!shoppingCartRequiresShipping)
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(6,0,0);", true);
                   
                }
                shippingMethodSign = 0;
                billingMethodSign = 0;
                SunTestSign = 0;
                AnonymousCheckoutAllowedSign = false;
            }
            LoadPaymentControl();

        }
       
        protected override void OnPreRender(EventArgs e)
        {
            //Agree to the terms
            if (SettingManager.GetSettingValueBoolean("Checkout.TermsOfServiceEnabled"))
            {
                string termsLink = string.Format("{0}conditionsinfopopup.aspx", CommonHelper.GetStoreLocation());
                //this.lTermsOfService.Text = string.Format(GetLocaleResourceString("Checkout.IAcceptTermsOfService"), string.Format("<span class=\"read\" onclick=\"javascript:OpenWindow('{0}', 450, 500, true)\">{1}</span>", termsLink, GetLocaleResourceString("Checkout.AcceptTermsOfService.Read")));
                this.lTermsOfService.Text = GetLocaleResourceString("Checkout.IAcceptTermsOfService");
            }


            if (!Page.IsPostBack)
            {
                bool shoppingCartRequiresShipping = ShippingService.ShoppingCartRequiresShipping(Cart);
                if (!shoppingCartRequiresShipping)
                {
                     dealNoShippingAddressOne.Visible = false;
                    ShowBilling.Checked = false;
                    ShowBilling.Visible = false;
                    dealBilling.Visible = true;
                    dealNoShippingAddressTwo.Visible = false;
                    shippingMethodSign = 1;
                }
                if (shoppingCartRequiresShipping)
                    showUserShipping();//The default is the login information to the address information stored in the database
                showUserBilling();

                if (shoppingCartRequiresShipping) 
                {
                    //Address of consignee
                    var shippingAddress = this.ShippingAddress;
                    SelectshippingAddress(shippingAddress);
                }
                //Billing Address
                var billingAddress = this.BillingAddress;
                SelectbillingAddress(billingAddress);

                
                ShippingAddressBindData();
                
                BillingAddressBindData();
                
                if (shoppingCartRequiresShipping) 
                    ShippingMethodBindData();
                
                BillingMethodBindData();

            }


            //Provides consistency
            int signA = this.ctrlShippingAddress.sendToProvince();
            int signB = this.ctrlBillingAddress.sendToProvince();
            if (signA == 1 && signB == 0)
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showAddressDisabled(1,0);", true);
            else
                if (signA == 0 && signB == 1)
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showAddressDisabled(0,1);", true);
                else
                    if (signA == 1 && signB == 1)
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showAddressDisabled(1,1);", true);

            base.OnPreRender(e);

        }
        //Reset the recipient's address
        protected void btnDefaultShippingAddress_Click(object sender, EventArgs e)
        {
            showUserShipping();
        }
        //Reset payer address
        protected void btnDefaultBillingAddress_Click(object sender, EventArgs e)
        {
            showUserBilling();
        }

        //To confirm payment
        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                agreeSign = 0;
                //Determine whether to agree to the terms
                if (SettingManager.GetSettingValueBoolean("Checkout.TermsOfServiceEnabled"))
                {
                    if (cbTermsOfService.Checked)
                        agreeSign = 1;
                    else
                        agreeSign = 0;
                }

                //Interpretation of the shipping method
                bool shoppingCartRequiresShipping = ShippingService.ShoppingCartRequiresShipping(Cart);

                if (agreeSign == 0 && SettingManager.GetSettingValueBoolean("Checkout.TermsOfServiceEnabled"))
                {
                    if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
                    {
                        if (shoppingCartRequiresShipping == false)
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(1,1,1);", true);
                        else
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(1,1,0);", true);
                    }
                    else
                    {
                        if (shoppingCartRequiresShipping == false)
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(1,0,1);", true);
                        else
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(1,0,0);", true);
                    }
                }
                else
                    if (shippingMethodSign == 1 && billingMethodSign == 1)
                    {
                        //If the selected payer address and the address of the consignee or read their address
                        if (ShowBilling.Checked == true)
                        {
                            //Address of customer
                            var shippingAddress = this.ShippingAddress;
                            SelectshippingAddress(shippingAddress);
                            //Billing Address
                            var billingAddress = this.ShippingAddress;
                            SelectbillingAddress(billingAddress);

                        }
                        else
                        {
                            //Customer Address
                            var shippingAddress = this.ShippingAddress;
                            SelectshippingAddress(shippingAddress);
                            //Billing Address
                            var billingAddress = this.BillingAddress;
                            SelectbillingAddress(billingAddress);
                        }

                        //详细付款方式
                        // LoadPaymentControl();//在重新载入 获取具体的支付值
                        if (this.ValidateForm())
                        {
                            this.PaymentInfo = this.GetPaymentInfo();
                            var args1 = new CheckoutStepEventArgs() { PaymentInfoEntered = true };
                            OnCheckoutStepChanged(args1);

                        }


                        //确认
                        try
                        {
                            var paymentInfo = this.PaymentInfoConfirm;
                            //paymentInfo.BillingAddress = NopContext.Current.User.BillingAddress;
                            paymentInfo.BillingAddress = CustomerService.GetAddressById(BillingAddressId);
                            paymentInfo.ShippingAddress = NopContext.Current.User.ShippingAddress;
                            paymentInfo.CustomerLanguage = NopContext.Current.WorkingLanguage;
                            paymentInfo.CustomerCurrency = NopContext.Current.WorkingCurrency;

                            int orderId = 0;
                            string result = OrderService.PlaceOrder(paymentInfo, NopContext.Current.User, out orderId);
                            this.PaymentInfo = null;
                            var order = OrderService.GetOrderById(orderId);
                            if (!String.IsNullOrEmpty(result))
                            {
                                return;
                            }
                            else
                            {
                                PaymentService.PostProcessPayment(order);
                            }
                            var args2 = new CheckoutStepEventArgs() { OrderConfirmed = true };
                            OnCheckoutStepChanged(args2);
                        }
                        catch (Exception exc)
                        {
                            LogService.InsertLog(LogTypeEnum.OrderError, exc.Message, exc);
                        }


                        //一切无误后 跳转到完成页面
                        Response.Redirect("~/checkoutcompleted.aspx");

                    }
                    else
                    {
                        if (shippingMethodSign == 0 && billingMethodSign == 0)
                        {
                            if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
                            {
                                if (shoppingCartRequiresShipping == false)
                                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(2,1,1);", true);
                                else
                                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(2,1,0);", true);
                            }
                            else
                            {
                                if (shoppingCartRequiresShipping == false)
                                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(2,0,1);", true);
                                else
                                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(2,0,0);", true);
                            }
                        }
                        else
                            if (shippingMethodSign == 0 && billingMethodSign == 1)
                            {
                                if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
                                {
                                    if (shoppingCartRequiresShipping == false)
                                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(3,1,1);", true);
                                    else
                                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(3,1,0);", true);
                                }
                                else
                                {
                                    if (shoppingCartRequiresShipping == false)
                                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(3,0,1);", true);
                                    else
                                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(3,0,0);", true);
                                }

                            }
                            else
                                if (shippingMethodSign == 1 && billingMethodSign == 0)
                                {
                                    if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
                                    {
                                        if (shoppingCartRequiresShipping == false)
                                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(4,1,1);", true);
                                        else
                                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(4,1,0);", true);
                                    }
                                    else
                                    {
                                        if (shoppingCartRequiresShipping == false)
                                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(4,0,1);", true);
                                        else
                                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "alertMessage(4,0,0);", true);
                                    }
                                }
                    }
            }
        }


        //将当前用户信息 显示为 收货人地址栏目里
        public void showUserShipping()
        {
            var shippingAddress = NopContext.Current.User;
            if (shippingAddress != null)
            {
                var billingAddress = new Address();
                billingAddress.AddressId = 0;
                billingAddress.CustomerId = shippingAddress.CustomerId;
                billingAddress.IsBillingAddress = true;
                billingAddress.FirstName = shippingAddress.FirstName;
                billingAddress.LastName = shippingAddress.LastName;
                billingAddress.PhoneNumber = shippingAddress.PhoneNumber;
                billingAddress.Email = shippingAddress.Email;
                billingAddress.FaxNumber = shippingAddress.FaxNumber;
                billingAddress.Company = shippingAddress.Company;
                billingAddress.Address1 = shippingAddress.StreetAddress;
                billingAddress.Address2 = shippingAddress.StreetAddress2;
                billingAddress.City = shippingAddress.City;
                billingAddress.StateProvinceId = shippingAddress.StateProvinceId;
                billingAddress.ZipPostalCode = shippingAddress.ZipPostalCode;
                billingAddress.CountryId = shippingAddress.CountryId;
                billingAddress.CreatedOn = DateTime.Now;

                ctrlShippingAddress.Address = billingAddress;
            }
        }

        //将当前用户信息 显示为 付款人 地址栏目里
        public void showUserBilling()
        {
            var shippingAddress = NopContext.Current.User;
            if (shippingAddress != null)
            {
                var billingAddress = new Address();
                billingAddress.AddressId = 0;
                billingAddress.CustomerId = shippingAddress.CustomerId;
                billingAddress.IsBillingAddress = true;
                billingAddress.FirstName = shippingAddress.FirstName;
                billingAddress.LastName = shippingAddress.LastName;
                billingAddress.PhoneNumber = shippingAddress.PhoneNumber;
                billingAddress.Email = shippingAddress.Email;
                billingAddress.FaxNumber = shippingAddress.FaxNumber;
                billingAddress.Company = shippingAddress.Company;
                billingAddress.Address1 = shippingAddress.StreetAddress;
                billingAddress.Address2 = shippingAddress.StreetAddress2;
                billingAddress.City = shippingAddress.City;
                billingAddress.StateProvinceId = shippingAddress.StateProvinceId;
                billingAddress.ZipPostalCode = shippingAddress.ZipPostalCode;
                billingAddress.CountryId = shippingAddress.CountryId;
                billingAddress.CreatedOn = DateTime.Now;

                ctrlBillingAddress.Address = billingAddress;
            }
        }
        //处理积分返点
        protected void cbUseRewardPoints_CheckedChanged(object sender, EventArgs e)
        {
            int rewardPointsBalance = NopContext.Current.User.RewardPointsBalance;
            decimal rewardPointsAmountBase = OrderService.ConvertRewardPointsToAmount(rewardPointsBalance);
            decimal rewardPointsAmount = CurrencyService.ConvertCurrency(rewardPointsAmountBase, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
            
            //判读是否需要运送方式
            bool shoppingCartRequiresShipping = ShippingService.ShoppingCartRequiresShipping(Cart);
            //如果是匿名访问 隐藏已有地址显示特效
            if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
            {
                AnonymousCheckoutAllowedSign = true;
                //如果是匿名访问 不存在显示和隐藏 那两个的按钮
                if (!shoppingCartRequiresShipping)
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(5,0,0);", true);
                else
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(3,0,0);", true);
            }
            else
            {
                if (!shoppingCartRequiresShipping)
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(6,0,0);", true);

            }

            checkoutDataMessage.Visible = true;


            //reward points
            ApplyRewardPoints();

            //如果 选中返点 并且返点的金额大于商品的 实际金额  就将 支付方式隐藏起来   如果没选中 大家一起显示
            if (cbUseRewardPoints.Checked == false)
            {
                rewardPointShowHide1.Visible = true;
                rewardPointShowHide2.Visible = true;
                if (SunTestSign == 0)
                    billingMethodSign = 0;
            }
            else
            {

                string totalPrice = OrderSummaryControl.getTotalPriceSummary;
                if (totalPrice == "Calculated during checkout")
                {
                    rewardPointShowHide1.Visible = true;
                    rewardPointShowHide2.Visible = true;
                }
                else
                {
                    decimal pointTotalMoney = rewardPointsAmount;
                    decimal allTotalPrice = decimal.Parse(totalPrice);
                    if (pointTotalMoney > allTotalPrice)
                    {
                        rewardPointShowHide1.Visible = false;
                        rewardPointShowHide2.Visible = false;
                        billingMethodSign = 1;

                        //payment methods  如果选择的是返点支付  并且返点的价格大于商品的价格 就默认为 store支付  store ID为25
                        int paymentMethodId = 25;
                        var paymentMethod = PaymentService.GetPaymentMethodById(paymentMethodId);
                        if (paymentMethod != null && paymentMethod.IsActive)
                        {

                            NopContext.Current.User.LastPaymentMethodId = paymentMethodId;
                            this.CustomerService.UpdateCustomer(NopContext.Current.User);
                            //NopContext.Current.User = CustomerService.SetLastPaymentMethodId(NopContext.Current.User.CustomerId, paymentMethodId);
                            var args1 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                            OnCheckoutStepChanged(args1);
                            billingMethodSign = 1;

                        }

                    }
                    else
                    {
                        rewardPointShowHide1.Visible = true;
                        rewardPointShowHide2.Visible = true;
                    }
                }
            }

            if (billingMethodSign == 0)
            {
                rewardPointShowHide2.Visible = false;
            }

            //刷新
            OrderSummaryControl.BindData();

        }

        //处理 是否显示 付款地址
        protected void ShowBilling_CheckedChanged(object sender, EventArgs e)
        {
            //如果是匿名访问 隐藏已有地址显示特效
            if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
            {
                AnonymousCheckoutAllowedSign = true;
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(3,0,0);", true);
            }

            //省份一致性
            int signA = this.ctrlShippingAddress.sendToProvince();
            int signB = this.ctrlBillingAddress.sendToProvince();

            if (ShowBilling.Checked)
            {
                ctrlBillingAddress.ValidationGroup = "";
                dealBilling.Visible = false;
                if (signA == 1 && signB == 0)
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(1,1,0);", true);
                else
                    if (signA == 0 && signB == 1)
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(1,0,1);", true);
                    else
                        if (signA == 1 && signB == 1)
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(1,1,1);", true);
                        else
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(1,0,0);", true);
            }
            else
            {
                ctrlBillingAddress.ValidationGroup = "EnterBillingAddress";
                dealBilling.Visible = true;
                if (AnonymousCheckoutAllowedSign == false)
                {
                    if (signA == 1 && signB == 0)
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(2,1,0);", true);
                    else
                        if (signA == 0 && signB == 1)
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(2,0,1);", true);
                        else
                            if (signA == 1 && signB == 1)
                                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(2,1,1);", true);
                            else
                                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(2,0,0);", true);


                }
                else
                {
                    if (signA == 1 && signB == 0)
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(0,1,0);", true);
                    else
                        if (signA == 0 && signB == 1)
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(0,0,1);", true);
                        else
                            if (signA == 1 && signB == 1)
                                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(0,1,1);", true);

                }
            }

        }

        //触发 运送方式，并刷新购物车显示 
        protected void ShippingMethod_CheckedChanged(object sender, EventArgs e)
        {
            //如果是匿名访问 隐藏已有地址显示特效
            if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
            {
                AnonymousCheckoutAllowedSign = true;
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(3,0,0);", true);
            }

            var shippingOption = this.SelectedShippingOption;
            if (shippingOption != null)
            {
                NopContext.Current.User.LastShippingOption = shippingOption;
                var args1 = new CheckoutStepEventArgs() { ShippingMethodSelected = true };
                OnCheckoutStepChanged(args1);
                shippingMethodSign = 1;

            }


            //刷新
            OrderSummaryControl.BindData();
            //如果先点击的 返点支付 并且已有的返点总额大于 商品总额 隐藏支付方式
            int rewardPointsBalance = NopContext.Current.User.RewardPointsBalance;
            decimal rewardPointsAmountBase = OrderService.ConvertRewardPointsToAmount(rewardPointsBalance);
            decimal rewardPointsAmount = CurrencyService.ConvertCurrency(rewardPointsAmountBase, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
            string totalPrice = OrderSummaryControl.getTotalPriceSummary;
            decimal allTotalPrice = decimal.Parse(totalPrice);
            if (cbUseRewardPoints.Checked == true)
            {
                if (rewardPointsAmount >= allTotalPrice)
                {
                    rewardPointShowHide1.Visible = false;
                    rewardPointShowHide2.Visible = false;
                    billingMethodSign = 1;

                    //payment methods  如果选择的是返点支付  并且返点的价格大于商品的价格 就默认为 store支付  store ID为25
                    int paymentMethodId = 25;
                    var paymentMethod = PaymentService.GetPaymentMethodById(paymentMethodId);
                    if (paymentMethod != null && paymentMethod.IsActive)
                    {
                        NopContext.Current.User.LastPaymentMethodId = paymentMethodId;
                        this.CustomerService.UpdateCustomer(NopContext.Current.User);
                        //NopContext.Current.User = CustomerService.SetLastPaymentMethodId(NopContext.Current.User.CustomerId, paymentMethodId);
                        var args1 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                        OnCheckoutStepChanged(args1);
                        billingMethodSign = 1;
                        
                    }

                }
                else
                {
                    rewardPointShowHide1.Visible = true;
                    rewardPointShowHide2.Visible = true;
                }
            }
        }


        //触发 支付方式， 并显示具体支付信息
        protected void BillingMethod_CheckedChanged(object sender, EventArgs e)
        {
            SunTestSign = 1;
            rewardPointShowHide2.Visible = true;

            //判读是否需要运送方式
            bool shoppingCartRequiresShipping = ShippingService.ShoppingCartRequiresShipping(Cart);
            //如果是匿名访问 隐藏已有地址显示特效
            if ((NopContext.Current.User == null || NopContext.Current.User.IsGuest) && CustomerService.AnonymousCheckoutAllowed)
            {
                AnonymousCheckoutAllowedSign = true;
                //如果是匿名访问 不存在显示和隐藏 那两个的按钮
                if (!shoppingCartRequiresShipping)
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(5,0,0);", true);
                else
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(3,0,0);", true);
            }
            else
            {
                if (!shoppingCartRequiresShipping)
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSucceed", "showHideCheckoutAddress(6,0,0);", true);

            }


            checkoutDataMessage.Visible = true;
            //reward points
            ApplyRewardPoints();
            //payment methods
            int paymentMethodId = this.SelectedPaymentMethodId;
            var paymentMethod = PaymentService.GetPaymentMethodById(paymentMethodId);
           /*
            if (paymentMethod != null && paymentMethod.IsActive)
            {
                NopContext.Current.User = CustomerService.SetLastPaymentMethodId(NopContext.Current.User.CustomerId, paymentMethodId);
                var args1 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                OnCheckoutStepChanged(args1);
                billingMethodSign = 1;

            }
            */ 

            if (paymentMethod != null && paymentMethod.IsActive)
            {
                //save selected payment methods
                NopContext.Current.User.LastPaymentMethodId = paymentMethodId;
                this.CustomerService.UpdateCustomer(NopContext.Current.User);
                var args1 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                OnCheckoutStepChanged(args1);
                billingMethodSign = 1;
            }




            LoadPaymentControl();
            //刷新
            OrderSummaryControl.BindData();
        }


        //显示已有收货人地址
        #region
        //显示已有付款人地址
        protected List<Address> GetAllowedBillingAddresses(Customer customer)
        {
            var addresses = new List<Address>();
            if (customer == null)
                return addresses;

            foreach (var address in customer.BillingAddresses)
            {
                var country = address.Country;
                if (country != null && country.AllowsBilling)
                {
                    addresses.Add(address);
                }
            }

            return addresses;
        }


        public void BillingAddressBindData()
        {
            var shippingAddress = NopContext.Current.User.ShippingAddress;
            //pnlTheSameAsShippingAddress.Visible = CustomerService.CanUseAddressAsBillingAddress(shippingAddress);

            var addresses = GetAllowedBillingAddresses(NopContext.Current.User);

            if (addresses.Count > 0)
            {
                //bind data
                dlBillingAddresses.DataSource = addresses;
                dlBillingAddresses.DataBind();
                //lEnterBillingAddress.Text = GetLocaleResourceString("Checkout.OrEnterNewAddress");
            }
            else
            {
                //pnlSelectBillingAddress.Visible = false;
                //lEnterBillingAddress.Text = GetLocaleResourceString("Checkout.EnterBillingAddress");
            }
        }
        public void ShippingAddressBindData()
        {
            bool shoppingCartRequiresShipping = ShippingService.ShoppingCartRequiresShipping(Cart);
            if (!shoppingCartRequiresShipping)
            {
                SelectshippingAddress(null);
            }
            else
            {
                var addresses = GetAllowedShippingAddresses(NopContext.Current.User);
                if (addresses.Count > 0)
                {
                    dlShippingAddresses.DataSource = addresses;
                    dlShippingAddresses.DataBind();
                    //lEnterShippingAddress.Text = GetLocaleResourceString("Checkout.OrEnterNewAddress");
                }
                else
                {
                    //       pnlSelectShippingAddress.Visible = false;
                    //lEnterShippingAddress.Text = GetLocaleResourceString("Checkout.EnterShippingAddress");
                }
            }
        }

        protected List<Address> GetAllowedShippingAddresses(Customer customer)
        {
            var addresses = new List<Address>();
            if (customer == null)
                return addresses;

            foreach (var address in customer.ShippingAddresses)
            {
                var country = address.Country;
                if (country != null && country.AllowsShipping)
                {
                    addresses.Add(address);
                }
            }

            return addresses;
        }

        #endregion
        //支付方式对应具体信息
        #region

        //显示具体的支付信息
      
        public void LoadPaymentControl()
        {
            PaymentMethod paymentMethod = null;
            if (NopContext.Current.User != null)
            {
                paymentMethod = NopContext.Current.User.LastPaymentMethod;
            }
            if (paymentMethod != null && paymentMethod.IsActive)
            {
                if (paymentControlLoaded)
                {
                    this.PaymentInfoPlaceHolder.Controls.Clear();
                }
                Control child = null;
                child = base.LoadControl(paymentMethod.UserTemplatePath);
                this.PaymentInfoPlaceHolder.Controls.Add(child);
                paymentControlLoaded = true;

            }
            else
            {

            }


        }

        public bool ValidateForm()
        {
            var ctrl = GetPaymentModule();
            if (ctrl != null)
                return ctrl.ValidateForm() && Page.IsValid;
            return Page.IsValid;
        }

        public PaymentInfo GetPaymentInfo()
        {
            PaymentInfo paymentInfo = null;
            var ctrl = GetPaymentModule();
            if (ctrl != null)
            {
                paymentInfo = ctrl.GetPaymentInfo();
                paymentInfo.PaymentMethodId = NopContext.Current.User.LastPaymentMethodId;
            }
            return paymentInfo;
        }

        protected IPaymentMethodModule GetPaymentModule()
        {
            foreach (var ctrl in this.PaymentInfoPlaceHolder.Controls)
                if (ctrl is IPaymentMethodModule)
                    return (IPaymentMethodModule)ctrl;
            return null;
        }

        protected PaymentInfo PaymentInfo
        {
            set
            {
                this.Session["OrderPaymentInfo"] = value;
            }
        }

        protected PaymentInfo PaymentInfoConfirm
        {
            get
            {
                if (this.Session["OrderPaymentInfo"] != null)
                    return (PaymentInfo)(this.Session["OrderPaymentInfo"]);
                return null;
            }
            set
            {
                this.Session["OrderPaymentInfo"] = value;
            }
        }

        #endregion

        //收货地址
        #region
        //处理收货地址
        protected void SelectshippingAddress(Address shippingAddress)
        {
           

            if (shippingAddress == null)
            {
                //set default shipping address
                NopContext.Current.User.ShippingAddressId = 0;
                this.CustomerService.UpdateCustomer(NopContext.Current.User);

                var args1 = new CheckoutStepEventArgs() { ShippingAddressSelected = true };
                OnCheckoutStepChanged(args1);
                if (!this.OnePageCheckout)
                    Response.Redirect("~/checkoutbillingaddress.aspx");
                return;
            }

            if (shippingAddress.AddressId == 0)
            {
                //check if address already exists
                var shippingAddress2 = NopContext.Current.User.ShippingAddresses.FindAddress(shippingAddress.FirstName,
                    shippingAddress.LastName, shippingAddress.PhoneNumber, shippingAddress.Email,
                    shippingAddress.FaxNumber, shippingAddress.Company,
                    shippingAddress.Address1, shippingAddress.Address2,
                    shippingAddress.City, shippingAddress.StateProvinceId, shippingAddress.ZipPostalCode,
                    shippingAddress.CountryId);

                if (shippingAddress2 != null)
                {
                    shippingAddress = shippingAddress2;
                }
                else
                {
                    shippingAddress.CustomerId = NopContext.Current.User.CustomerId;
                    shippingAddress.IsBillingAddress = false;
                    shippingAddress.CreatedOn = DateTime.UtcNow;
                    shippingAddress.UpdatedOn = DateTime.UtcNow;

                    this.CustomerService.InsertAddress(shippingAddress);
                }
            }


            //set default shipping address
            NopContext.Current.User.ShippingAddressId = shippingAddress.AddressId;
            this.CustomerService.UpdateCustomer(NopContext.Current.User);

            var args2 = new CheckoutStepEventArgs() { ShippingAddressSelected = true };
            OnCheckoutStepChanged(args2);
            
            //shippingAddressSign = 1;//如果成功 运送地址标记为 1

        }

        //收货人地址
        protected Address ShippingAddress
        {
            get
            {
                var address = ctrlShippingAddress.Address;
                if (address.AddressId != 0 && NopContext.Current.User != null)
                {
                    var prevAddress = CustomerService.GetAddressById(address.AddressId);
                    if (prevAddress.CustomerId != NopContext.Current.User.CustomerId)
                        return null;
                    address.CustomerId = prevAddress.CustomerId;
                    address.CreatedOn = prevAddress.CreatedOn;
                }
                else
                {
                    address.CreatedOn = DateTime.UtcNow;
                }

                return address;
            }
        }
        #endregion

        //付款地址
        #region
        //获取付款人地址
        protected Address BillingAddress
        {
            get
            {
                var address = ctrlBillingAddress.Address;
                if (address.AddressId != 0 && NopContext.Current.User != null)
                {
                    var prevAddress = CustomerService.GetAddressById(address.AddressId);
                    if (prevAddress.CustomerId != NopContext.Current.User.CustomerId)
                        return null;
                    address.CustomerId = prevAddress.CustomerId;
                    address.CreatedOn = prevAddress.CreatedOn;
                }
                else
                {
                    address.CreatedOn = DateTime.UtcNow;
                }

                return address;
            }
        }
        //处理付款地址
        protected void SelectbillingAddress(Address billingAddress)
        {
            if (billingAddress == null)
            {
                //set default billing address
                NopContext.Current.User.BillingAddressId = 0;
                this.CustomerService.UpdateCustomer(NopContext.Current.User);
                var args1 = new CheckoutStepEventArgs() { BillingAddressSelected = true };
                OnCheckoutStepChanged(args1);
                if (!this.OnePageCheckout)
                    Response.Redirect("~/checkoutshippingmethod.aspx");
                return;
            }

            if (billingAddress.AddressId == 0)
            {
                //check if address already exists
                var billingAddress2 = NopContext.Current.User.BillingAddresses.FindAddress(billingAddress.FirstName,
                     billingAddress.LastName, billingAddress.PhoneNumber, billingAddress.Email,
                     billingAddress.FaxNumber, billingAddress.Company,
                     billingAddress.Address1, billingAddress.Address2,
                     billingAddress.City, billingAddress.StateProvinceId, billingAddress.ZipPostalCode,
                     billingAddress.CountryId);

                if (billingAddress2 != null)
                {
                    billingAddress = billingAddress2;
                }
                else
                {
                    billingAddress.CustomerId = NopContext.Current.User.CustomerId;
                    billingAddress.IsBillingAddress = true;
                    billingAddress.CreatedOn = DateTime.UtcNow;
                    billingAddress.UpdatedOn = DateTime.UtcNow;

                    this.CustomerService.InsertAddress(billingAddress);
                }
            }


            //set default billing address
            NopContext.Current.User.BillingAddressId = billingAddress.AddressId;
            this.CustomerService.UpdateCustomer(NopContext.Current.User);
            var args2 = new CheckoutStepEventArgs() { BillingAddressSelected = true };
            OnCheckoutStepChanged(args2);
            
           
            //记录当前 付款人地址ID
            BillingAddressId = billingAddress.AddressId;
            //billingAddressSign = 1;
        
        }
        #endregion

        //运送方式
        #region
        public void ShippingMethodBindData()
        {
            bool shoppingCartRequiresShipping = ShippingService.ShoppingCartRequiresShipping(Cart);

            //IsShipEnabled 是否有运送方式
            if (!shoppingCartRequiresShipping)
            {
                NopContext.Current.User.LastShippingOption = null;
                var args1 = new CheckoutStepEventArgs() { ShippingMethodSelected = true };
                OnCheckoutStepChanged(args1);

            }
            else
            {
                string error = string.Empty;
                Address address = NopContext.Current.User.ShippingAddress;
                //Address address = NopContext.Current.User.;
                var shippingOptions = ShippingService.GetShippingOptions(Cart, NopContext.Current.User, address, ref error);
                if (!String.IsNullOrEmpty(error))
                {
                    LogService.InsertLog(LogTypeEnum.ShippingError, error, error);
                    phSelectShippingMethod.Visible = false;
                    lShippingMethodsError.Text = Server.HtmlEncode(error);
                }
                else
                {
                    if (shippingOptions.Count > 0)
                    {
                        phSelectShippingMethod.Visible = true;
                        dlShippingOptions.DataSource = shippingOptions;
                        dlShippingOptions.DataBind();
                    }
                    else
                    {
                        phSelectShippingMethod.Visible = false;
                        //lShippingMethodsError.Text = GetLocaleResourceString("Checkout.ShippingIsNotAllowed");
                    }
                }

            }
        }

        protected string FormatShippingOption(ShippingOption shippingOption)
        {
            //calculate discounted and taxed rate
            Discount appliedDiscount = null;
            decimal shippingTotalWithoutDiscount = shippingOption.Rate;
            decimal discountAmount = ShippingService.GetShippingDiscount(NopContext.Current.User,
                shippingTotalWithoutDiscount, out appliedDiscount);
            decimal shippingTotalWithDiscount = shippingTotalWithoutDiscount - discountAmount;
            if (shippingTotalWithDiscount < decimal.Zero)
                shippingTotalWithDiscount = decimal.Zero;
            shippingTotalWithDiscount = Math.Round(shippingTotalWithDiscount, 2);


            decimal rateBase = this.TaxService.GetShippingPrice(shippingTotalWithDiscount, NopContext.Current.User);
            decimal rate = CurrencyService.ConvertCurrency(rateBase, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
            string rateStr = PriceHelper.FormatShippingPrice(rate, true);
            return string.Format("({0})", rateStr);
        }

        protected ShippingOption SelectedShippingOption
        {
            get
            {
                ShippingOption shippingOption = null;
                foreach (DataListItem item in this.dlShippingOptions.Items)
                {
                    var rdShippingOption = (RadioButton)item.FindControl("rdShippingOption");
                    var hfShippingRateComputationMethodId = (HiddenField)item.FindControl("hfShippingRateComputationMethodId");
                    var hfName = (HiddenField)item.FindControl("hfName");

                    if (rdShippingOption.Checked)
                    {
                        int shippingRateComputationMethodId = Convert.ToInt32(hfShippingRateComputationMethodId.Value);
                        string name = hfName.Value;

                        string error = string.Empty;
                        var shippingOptions = ShippingService.GetShippingOptions(Cart, NopContext.Current.User, NopContext.Current.User.ShippingAddress, shippingRateComputationMethodId, ref error);
                        shippingOption = shippingOptions.Find((so) => so.Name == name);
                        break;
                    }
                }
                return shippingOption;
            }
        }
        #endregion

        //付款方式
        #region
        protected string FormatPaymentMethodInfo(PaymentMethod paymentMethod)
        { 
            decimal paymentMethodAdditionalFee = PaymentService.GetAdditionalHandlingFee(paymentMethod.PaymentMethodId);
            decimal rateBase = this.TaxService.GetPaymentMethodAdditionalFee(paymentMethodAdditionalFee, NopContext.Current.User);
            decimal rate = CurrencyService.ConvertCurrency(rateBase, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
            if (rate > decimal.Zero)
            {
                string rateStr = PriceHelper.FormatPaymentMethodAdditionalFee(rate, true);
                return string.Format("({0})", rateStr);
            }
            else
            {
                return string.Empty;
            }
        }

        public void ApplyRewardPoints()
        {
            //reward points
            NopContext.Current.User.UseRewardPointsDuringCheckout = cbUseRewardPoints.Checked;
        }

        protected int SelectedPaymentMethodId
        {
            get
            {
                int selectedPaymentMethodId = 0;
                foreach (DataListItem item in this.dlPaymentMethod.Items)
                {
                    RadioButton rdPaymentMethod = (RadioButton)item.FindControl("rdPaymentMethod");
                    if (rdPaymentMethod.Checked)
                    {
                        selectedPaymentMethodId = Convert.ToInt32(this.dlPaymentMethod.DataKeys[item.ItemIndex].ToString());
                        break;
                    }
                }
                return selectedPaymentMethodId;
            }
        }
        protected string GetLocaleResourceStringHere(string ResourceName, params object[] args)
        {
            Language language = NopContext.Current.WorkingLanguage;
            return string.Format(
                LocalizationManager.GetLocaleResourceString(ResourceName, language.LanguageId),
                args);
        }
        public void BillingMethodBindData()
        {
            //reward points
            if (OrderService.RewardPointsEnabled && !this.Cart.IsRecurring)
            {
                int rewardPointsBalance = NopContext.Current.User.RewardPointsBalance;
                decimal rewardPointsAmountBase = OrderService.ConvertRewardPointsToAmount(rewardPointsBalance);
                decimal rewardPointsAmount = CurrencyService.ConvertCurrency(rewardPointsAmountBase, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                if (rewardPointsAmount > decimal.Zero)
                {
                    string rewardPointsAmountStr = PriceHelper.FormatPrice(rewardPointsAmount, true, false);
                    cbUseRewardPoints.Text = GetLocaleResourceStringHere("Checkout.UseRewardPoints", rewardPointsBalance, rewardPointsAmountStr);
                    pnlRewardPoints.Visible = true;
                }
                else
                {
                    pnlRewardPoints.Visible = false;
                }
            }
            else
            {
                pnlRewardPoints.Visible = false;
            }

            //payment methods
            int? filterByCountryId = null;
            if (NopContext.Current.User.BillingAddress != null && NopContext.Current.User.BillingAddress.Country != null)
            {
                filterByCountryId = NopContext.Current.User.BillingAddress.CountryId;
            }

            bool hasButtonMethods = false;
            var boundPaymentMethods = new List<PaymentMethod>();
            var paymentMethods = PaymentService.GetAllPaymentMethods(filterByCountryId);
            foreach (var pm in paymentMethods)
            {
                switch (pm.PaymentMethodType)
                {
                    case PaymentMethodTypeEnum.Unknown:
                    case PaymentMethodTypeEnum.Standard:
                        {
                            if (!Cart.IsRecurring || PaymentService.SupportRecurringPayments(pm.PaymentMethodId) != RecurringPaymentTypeEnum.NotSupported)
                                boundPaymentMethods.Add(pm);
                        }
                        break;
                    case PaymentMethodTypeEnum.Button:
                        {
                            //PayPal Express is placed here as button
                            if (pm.SystemKeyword == "PayPalExpress")
                            {
                                if (!Cart.IsRecurring || PaymentService.SupportRecurringPayments(pm.PaymentMethodId) != RecurringPaymentTypeEnum.NotSupported)
                                    hasButtonMethods = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            if (boundPaymentMethods.Count == 0)
            {
                if (hasButtonMethods)
                {
                    phSelectPaymentMethod.Visible = false;
                    pnlPaymentMethodsError.Visible = false;

                    //no reward points in this case
                    pnlRewardPoints.Visible = false;
                }
                else
                {
                    phSelectPaymentMethod.Visible = false;
                    pnlPaymentMethodsError.Visible = true;
                    lPaymentMethodsError.Text = GetLocaleResourceString("Checkout.NoPaymentMethods");

                    //no reward points in this case
                    pnlRewardPoints.Visible = false;
                }
            }
            else if (boundPaymentMethods.Count == 1)
            {
                phSelectPaymentMethod.Visible = true;
                pnlPaymentMethodsError.Visible = false;
                dlPaymentMethod.DataSource = boundPaymentMethods;
                dlPaymentMethod.DataBind();
            }
            else
            {
                phSelectPaymentMethod.Visible = true;
                pnlPaymentMethodsError.Visible = false;
                dlPaymentMethod.DataSource = boundPaymentMethods;
                dlPaymentMethod.DataBind();
            }
        }

        #endregion

        //杂项
        #region
        protected virtual void OnCheckoutStepChanged(CheckoutStepEventArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }

        //是否是 单页显示
        public bool OnePageCheckout
        {
            get
            {
                if (ViewState["OnePageCheckout"] != null)
                    return (bool)ViewState["OnePageCheckout"];
                return false;
            }
            set
            {
                ViewState["OnePageCheckout"] = value;
            }
        }

        public ShoppingCart Cart
        {
            get
            {
                if (cart == null)
                {
                    cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);
                }
                return cart;
            }
        }

        #endregion

        #region Services, managers

        public IOnlineUserService OnlineUserService
        {
            get { return IoC.Resolve<IOnlineUserService>(); }
        }
        public ICustomerActivityService CustomerActivityService
        {
            get { return IoC.Resolve<ICustomerActivityService>(); }
        }
        public ILogService LogService
        {
            get { return IoC.Resolve<ILogService>(); }
        }
        public ISearchLogService SearchLogService
        {
            get { return IoC.Resolve<ISearchLogService>(); }
        }
        public ICategoryService CategoryService
        {
            get { return IoC.Resolve<ICategoryService>(); }
        }
        public ISettingManager SettingManager
        {
            get { return IoC.Resolve<ISettingManager>(); }
        }
        public IBlogService BlogService
        {
            get { return IoC.Resolve<IBlogService>(); }
        }
        public IForumService ForumService
        {
            get { return IoC.Resolve<IForumService>(); }
        }
        public INewsService NewsService
        {
            get { return IoC.Resolve<INewsService>(); }
        }
        public IPollService PollService
        {
            get { return IoC.Resolve<IPollService>(); }
        }
        public ITopicService TopicService
        {
            get { return IoC.Resolve<ITopicService>(); }
        }
        public ICustomerService CustomerService
        {
            get { return IoC.Resolve<ICustomerService>(); }
        }
        public ICountryService CountryService
        {
            get { return IoC.Resolve<ICountryService>(); }
        }
        public ICurrencyService CurrencyService
        {
            get { return IoC.Resolve<ICurrencyService>(); }
        }
        public ILanguageService LanguageService
        {
            get { return IoC.Resolve<ILanguageService>(); }
        }
        public IStateProvinceService StateProvinceService
        {
            get { return IoC.Resolve<IStateProvinceService>(); }
        }
        public IExportManager ExportManager
        {
            get { return IoC.Resolve<IExportManager>(); }
        }
        public IImportManager ImportManager
        {
            get { return IoC.Resolve<IImportManager>(); }
        }
        public ILocalizationManager LocalizationManager
        {
            get { return IoC.Resolve<ILocalizationManager>(); }
        }
        public IMaintenanceService MaintenanceService
        {
            get { return IoC.Resolve<IMaintenanceService>(); }
        }
        public IManufacturerService ManufacturerService
        {
            get { return IoC.Resolve<IManufacturerService>(); }
        }
        public IMeasureService MeasureService
        {
            get { return IoC.Resolve<IMeasureService>(); }
        }
        public IDownloadService DownloadService
        {
            get { return IoC.Resolve<IDownloadService>(); }
        }
        public IPictureService PictureService
        {
            get { return IoC.Resolve<IPictureService>(); }
        }
        public ISMSService SMSService
        {
            get { return IoC.Resolve<ISMSService>(); }
        }
        public IMessageService MessageService
        {
            get { return IoC.Resolve<IMessageService>(); }
        }
        public IOrderService OrderService
        {
            get { return IoC.Resolve<IOrderService>(); }
        }
        public IShoppingCartService ShoppingCartService
        {
            get { return IoC.Resolve<IShoppingCartService>(); }
        }
        public IPaymentService PaymentService
        {
            get { return IoC.Resolve<IPaymentService>(); }
        }
        public ICheckoutAttributeService CheckoutAttributeService
        {
            get { return IoC.Resolve<ICheckoutAttributeService>(); }
        }
        public IProductAttributeService ProductAttributeService
        {
            get { return IoC.Resolve<IProductAttributeService>(); }
        }
        public ISpecificationAttributeService SpecificationAttributeService
        {
            get { return IoC.Resolve<ISpecificationAttributeService>(); }
        }
        public IProductService ProductService
        {
            get { return IoC.Resolve<IProductService>(); }
        }
        public IAffiliateService AffiliateService
        {
            get { return IoC.Resolve<IAffiliateService>(); }
        }
        public ICampaignService CampaignService
        {
            get { return IoC.Resolve<ICampaignService>(); }
        }
        public IDiscountService DiscountService
        {
            get { return IoC.Resolve<IDiscountService>(); }
        }
        public IQBService QBService
        {
            get { return IoC.Resolve<IQBService>(); }
        }
        public IACLService ACLService
        {
            get { return IoC.Resolve<IACLService>(); }
        }
        public IBlacklistService BlacklistService
        {
            get { return IoC.Resolve<IBlacklistService>(); }
        }
        public IShippingByTotalService ShippingByTotalService
        {
            get { return IoC.Resolve<IShippingByTotalService>(); }
        }
        public IShippingByWeightAndCountryService ShippingByWeightAndCountryService
        {
            get { return IoC.Resolve<IShippingByWeightAndCountryService>(); }
        }
        public IShippingByWeightService ShippingByWeightService
        {
            get { return IoC.Resolve<IShippingByWeightService>(); }
        }
        public IShippingRateComputationMethod ShippingRateComputationMethod
        {
            get { return IoC.Resolve<IShippingRateComputationMethod>(); }
        }
        public IShippingService ShippingService
        {
            get { return IoC.Resolve<IShippingService>(); }
        }
        public ITaxCategoryService TaxCategoryService
        {
            get { return IoC.Resolve<ITaxCategoryService>(); }
        }
        public ITaxProviderService TaxProviderService
        {
            get { return IoC.Resolve<ITaxProviderService>(); }
        }
        public ITaxRateService TaxRateService
        {
            get { return IoC.Resolve<ITaxRateService>(); }
        }
        public ITaxService TaxService
        {
            get { return IoC.Resolve<ITaxService>(); }
        }
        public ITemplateService TemplateService
        {
            get { return IoC.Resolve<ITemplateService>(); }
        }
        public IWarehouseService WarehouseService
        {
            get { return IoC.Resolve<IWarehouseService>(); }
        }
        #endregion
        protected string GetLocaleResourceString(string resourceName)
        {
            Language language = NopContext.Current.WorkingLanguage;
            return this.LocalizationManager.GetLocaleResourceString(resourceName, language.LanguageId);
        }

        protected string GetLocaleResourceString(string resourceName, params object[] args)
        {
            Language language = NopContext.Current.WorkingLanguage;
            return string.Format(
                this.LocalizationManager.GetLocaleResourceString(resourceName, language.LanguageId),
                args);
        }

    }
}