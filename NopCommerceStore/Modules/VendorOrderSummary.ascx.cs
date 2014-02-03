using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.Modules
{
    public partial class VendorOrderSummaryControl : BaseNopFrontendUserControl
    {
        // This is the vendor summary.  This is only going to be used on the inside of 
        // the shopping cart so the user can confirm their purchase FROM A SINGLE VENDOR.
        public int VendorId;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Page.Request.Path != "/PaypalExpressReturn.aspx"){
                this.BindData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            //terms of service
            if (this.SettingManager.GetSettingValueBoolean("Checkout.TermsOfServiceEnabled"))
            {
                string termsLink = string.Format("{0}conditionsinfopopup.aspx", CommonHelper.GetStoreLocation());
                this.lTermsOfService.Text = string.Format(GetLocaleResourceString("Checkout.IAcceptTermsOfService"), string.Format("<span class=\"read\" onclick=\"javascript:OpenWindow('{0}', 450, 500, true)\">{1}</span>", termsLink, GetLocaleResourceString("Checkout.AcceptTermsOfService.Read")));
            }

            //Google Checkout button
            if (this.IsShoppingCart)
            {
                //btnGoogleCheckoutButton.BindData();
            }

            base.OnPreRender(e);
        }

        public void BindData()
        {
            //VendorId may be passed from the paypal express return screen.  If this is the case
            // then the request string should be ignored in case it's a hack.
            if (VendorId == 0) { 
                VendorId = Request.QueryString["vendorId"] == null ? 0 : int.Parse(Request.QueryString["vendorId"]);
            }
            var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart, VendorId);


            var poo = (from p in cart
                       select new { VendorID = p.VendorID, VendorName = p.Vendor.CompanyName })
                        .Distinct();


            if (cart.Count > 0)
            {
                pnlEmptyCart.Visible = false;
                pnlCart.Visible = true;

                //shopping cart
                this.rptShopHeader.DataSource = poo;
                this.rptShopHeader.DataBind();

                //minimum order subtotal
                bool minOrderSubtotalAmountOK = this.OrderService.ValidateMinOrderSubtotalAmount(cart, NopContext.Current.User);
                if (minOrderSubtotalAmountOK)
                {
                }
                else
                {
                    decimal minOrderSubtotalAmount = this.CurrencyService.ConvertCurrency(this.OrderService.MinOrderSubtotalAmount, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                }

                //cross-sells
                var crossSells = this.ProductService.GetCrosssellProductsByShoppingCart(cart);
                if (crossSells.Count > 0)
                {
                    dlCrossSells.DataSource = crossSells;
                    dlCrossSells.DataBind();
                    dlCrossSells.Visible = true;
                }
                else
                {
                    dlCrossSells.Visible = false;
                }

                ValidateShoppingCart();

                foreach (RepeaterItem rptHeadItem in rptShopHeader.Items)
                {
                    ValidateCartItems(rptHeadItem);
                }
                
            }
            else
            {
                pnlEmptyCart.Visible = true;
                pnlCart.Visible = false;
            }

            
        }

        /// <summary>
        /// Validates shopping cart
        /// </summary>
        /// <returns>Indicates whether there're some warnings/errors</returns>
        protected bool ValidateShoppingCart()
        {
            bool hasErrors = false;

            //shopping cart
            var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);

            var warnings = this.ShoppingCartService.GetShoppingCartWarnings(cart, string.Empty, false);
            if (warnings.Count > 0)
            {
                hasErrors = true;
                pnlCommonWarnings.Visible = true;
                lblCommonWarning.Visible = true;

                var scWarningsSb = new StringBuilder();
                for (int i = 0; i < warnings.Count; i++)
                {
                    scWarningsSb.Append(Server.HtmlEncode(warnings[i]));
                    if (i != warnings.Count - 1)
                    {
                        scWarningsSb.Append("<br />");
                    }
                }

                lblCommonWarning.Text = scWarningsSb.ToString();
            }
            else
            {
                pnlCommonWarnings.Visible = false;
                lblCommonWarning.Visible = false;
            }

            return hasErrors;
        }

        /// <summary>
        /// Validates shopping cart items
        /// </summary>
        /// <returns>Indicates whether there're some warnings/errors</returns>
        protected bool ValidateCartItems(RepeaterItem rptHeadItem)
        {
            //TODO: this is working off the old shopping cart repeater
            //      it needs to be changed to use the child.  We
            //      may not have access!!!!
            bool hasErrors = false;


            Repeater rptDetail = (Repeater)rptHeadItem.FindControl("rptShoppingCart");
            //individual items
            foreach (RepeaterItem item in rptDetail.Items)
            {
                var txtQuantity = item.FindControl("txtQuantity") as TextBox;
                var lblShoppingCartItemId = item.FindControl("lblShoppingCartItemId") as Label;
                var cbRemoveFromCart = item.FindControl("cbRemoveFromCart") as CheckBox;
                var pnlWarnings = item.FindControl("pnlWarnings") as Panel;
                var lblWarning = item.FindControl("lblWarning") as Label;

                int shoppingCartItemId = 0;
                int quantity = 0;

                if (txtQuantity != null && lblShoppingCartItemId != null && cbRemoveFromCart != null)
                {
                    int.TryParse(lblShoppingCartItemId.Text, out shoppingCartItemId);

                    if (!cbRemoveFromCart.Checked)
                    {
                        int.TryParse(txtQuantity.Text, out quantity);
                        var sci = this.ShoppingCartService.GetShoppingCartItemById(shoppingCartItemId);

                        var warnings = this.ShoppingCartService.GetShoppingCartItemWarnings(
                            sci.ShoppingCartType,
                            sci.ProductVariantId,
                            sci.AttributesXml,
                            sci.CustomerEnteredPrice,
                            quantity);

                        if (warnings.Count > 0)
                        {
                            hasErrors = true;
                            if (pnlWarnings != null && lblWarning != null)
                            {
                                pnlWarnings.Visible = true;
                                lblWarning.Visible = true;

                                var addToCartWarningsSb = new StringBuilder();
                                for (int i = 0; i < warnings.Count; i++)
                                {
                                    addToCartWarningsSb.Append(Server.HtmlEncode(warnings[i]));
                                    if (i != warnings.Count - 1)
                                    {
                                        addToCartWarningsSb.Append("<br />");
                                    }
                                }

                                lblWarning.Text = addToCartWarningsSb.ToString();
                            }
                        }
                    }
                }
            }
            return hasErrors;
        }

        protected void UpdateShoppingCart(RepeaterItem rptHeadItem)
        {

            //TODO: this is working off the old shopping cart repeater
            //      it needs to be changed to use the child.  We
            //      may not have access!!!!
            if (!IsShoppingCart)
                return;

            ApplyCheckoutAttributes();
            bool hasErrors = ValidateCartItems(rptHeadItem);

            Repeater rptShoppingCart = (Repeater)rptHeadItem.FindControl("rptShoppingCart");

            if (!hasErrors)
            {
                foreach (RepeaterItem item in rptShoppingCart.Items)
                {
                    var txtQuantity = item.FindControl("txtQuantity") as TextBox;
                    var lblShoppingCartItemId = item.FindControl("lblShoppingCartItemId") as Label;
                    var cbRemoveFromCart = item.FindControl("cbRemoveFromCart") as CheckBox;

                    int shoppingCartItemId = 0;
                    int quantity = 0;

                    if (txtQuantity != null && lblShoppingCartItemId != null && cbRemoveFromCart != null)
                    {
                        int.TryParse(lblShoppingCartItemId.Text, out shoppingCartItemId);
                        if (cbRemoveFromCart.Checked)
                        {
                            this.ShoppingCartService.DeleteShoppingCartItem(shoppingCartItemId, true);
                        }
                        else
                        {
                            int.TryParse(txtQuantity.Text, out quantity);
                            List<string> addToCartWarning = this.ShoppingCartService.UpdateCart(shoppingCartItemId, quantity, true);
                        }
                    }
                }

                Response.Redirect(SEOHelper.GetShoppingCartUrl());
            }
        }

        protected void ContinueShopping()
        {
            string returnUrl = NopContext.Current.LastContinueShoppingPage;
            if (!String.IsNullOrEmpty(returnUrl))
                Response.Redirect(returnUrl);
            else
                Response.Redirect(CommonHelper.GetStoreLocation());
        }

        protected void Checkout()
        {
            ApplyCheckoutAttributes();
            if (NopContext.Current.User == null || NopContext.Current.User.IsGuest)
            {
                string loginURL = SEOHelper.GetLoginPageUrl(true, this.CustomerService.AnonymousCheckoutAllowed);
                Response.Redirect(loginURL);
            }
            else
            {
                Response.Redirect("~/checkout.aspx");
            }
        }

        protected void ApplyDiscountCouponCode()
        {
            string couponCode = this.txtDiscountCouponCode.Text.Trim();
            if (String.IsNullOrEmpty(couponCode))
                return;

            var discounts = this.DiscountService.GetAllDiscounts(null);
            var discount = discounts.FindByCouponCode(couponCode);
            bool isDiscountValid = discount != null;
            if (isDiscountValid)
            {
                pnlDiscountWarnings.Visible = false;
                lblDiscountWarning.Visible = false;

                this.CustomerService.ApplyDiscountCouponCode(couponCode);
                this.BindData();
            }
            else
            {
                pnlDiscountWarnings.Visible = true;
                lblDiscountWarning.Visible = true;
                lblDiscountWarning.Text = GetLocaleResourceString("ShoppingCart.DiscountCouponCode.WrongDiscount");
            }
        }

        protected void ApplyGiftCardCouponCode()
        {
            string couponCode = this.txtGiftCardCouponCode.Text.Trim();
            if (String.IsNullOrEmpty(couponCode))
                return;

            var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);
            if (!cart.IsRecurring)
            {
                bool isGiftCardValid = GiftCardHelper.IsGiftCardValid(couponCode);
                if (isGiftCardValid)
                {
                    pnlGiftCardWarnings.Visible = false;
                    lblGiftCardWarning.Visible = false;

                    string couponCodesXML = string.Empty;
                    if (NopContext.Current.User != null)
                        couponCodesXML = NopContext.Current.User.GiftCardCouponCodes;
                    couponCodesXML = GiftCardHelper.AddCouponCode(couponCodesXML, couponCode);
                    this.CustomerService.ApplyGiftCardCouponCode(couponCodesXML);
                    this.BindData();
                }
                else
                {
                    pnlGiftCardWarnings.Visible = true;
                    lblGiftCardWarning.Visible = true;
                    lblGiftCardWarning.Text = GetLocaleResourceString("ShoppingCart.GiftCards.WrongGiftCard");
                }
            }
            else
            {
                pnlGiftCardWarnings.Visible = true;
                lblGiftCardWarning.Visible = true;
                lblGiftCardWarning.Text = GetLocaleResourceString("ShoppingCart.GiftCards.DontWorkWithAutoshipProducts");
            }
        }

        protected void ApplyCheckoutAttributes()
        {
            if (ctrlCheckoutAttributes.HasAttributes)
            {
                string checkoutAttributes = ctrlCheckoutAttributes.SelectedAttributes;
                this.CustomerService.ApplyCheckoutAttributes(checkoutAttributes);
            }
        }

        public string GetProductVariantName(ShoppingCartItem shoppingCartItem)
        {
            var productVariant = shoppingCartItem.ProductVariant;
            if (productVariant != null)
                return productVariant.LocalizedFullProductName;
            return "Not available";
        }

        public string GetProductVariantImageUrl(ShoppingCartItem shoppingCartItem)
        {
            string pictureUrl = String.Empty;
            var productVariant = shoppingCartItem.ProductVariant;
            if (productVariant != null)
            {
                var productVariantPicture = productVariant.Picture;
                pictureUrl = this.PictureService.GetPictureUrl(productVariantPicture, this.SettingManager.GetSettingValueInteger("Media.ShoppingCart.ThumbnailImageSize", 80), false);
                if (String.IsNullOrEmpty(pictureUrl))
                {
                    var product = productVariant.Product;
                    var picture = product.DefaultPicture;
                    if (picture != null)
                    {
                        pictureUrl = this.PictureService.GetPictureUrl(picture, this.SettingManager.GetSettingValueInteger("Media.ShoppingCart.ThumbnailImageSize", 80));
                    }
                    else
                    {
                        pictureUrl = this.PictureService.GetDefaultPictureUrl(this.SettingManager.GetSettingValueInteger("Media.ShoppingCart.ThumbnailImageSize", 80));
                    }
                }
            }
            return pictureUrl;
        }

        public string GetProductUrl(ShoppingCartItem shoppingCartItem)
        {
            var productVariant = shoppingCartItem.ProductVariant;
            if (productVariant != null)
                return SEOHelper.GetProductUrl(productVariant.ProductId);
            return string.Empty;
        }

        public string GetAttributeDescription(ShoppingCartItem shoppingCartItem)
        {
            string result = ProductAttributeHelper.FormatAttributes(shoppingCartItem.ProductVariant, shoppingCartItem.AttributesXml);
            if (!String.IsNullOrEmpty(result))
                result = "<br />" + result;
            return result;
        }

        public string GetRecurringDescription(ShoppingCartItem shoppingCartItem)
        {
            string result = string.Empty;
            if (shoppingCartItem.ProductVariant.IsRecurring)
            {
                result = string.Format(GetLocaleResourceString("ShoppingCart.RecurringPeriod"), shoppingCartItem.ProductVariant.CycleLength, ((RecurringProductCyclePeriodEnum)shoppingCartItem.ProductVariant.CyclePeriod).ToString());
                if (!String.IsNullOrEmpty(result))
                    result = "<br />" + result;
            }
            return result;
        }

        public string GetShoppingCartItemUnitPriceString(ShoppingCartItem shoppingCartItem)
        {
            var sb = new StringBuilder();
            if (shoppingCartItem.ProductVariant.CallForPrice)
            {
                sb.Append("<span class=\"productPrice\">");
                sb.Append(GetLocaleResourceString("Products.CallForPrice"));
                sb.Append("</span>");
            }
            else
            {
                decimal taxRate = decimal.Zero;
                decimal shoppingCartUnitPriceWithDiscountBase = this.TaxService.GetPrice(shoppingCartItem.ProductVariant, PriceHelper.GetUnitPrice(shoppingCartItem, true), out taxRate);
                decimal shoppingCartUnitPriceWithDiscount = this.CurrencyService.ConvertCurrency(shoppingCartUnitPriceWithDiscountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                string unitPriceString = PriceHelper.FormatPrice(shoppingCartUnitPriceWithDiscount);
                sb.Append("<span class=\"productPrice\">");
                sb.Append(unitPriceString);
                sb.Append("</span>");
            }
            return sb.ToString();
        }

        public string GetShoppingCartItemSubTotalString(ShoppingCartItem shoppingCartItem)
        {
            var sb = new StringBuilder();
            if (shoppingCartItem.ProductVariant.CallForPrice)
            {
                sb.Append("<span class=\"productPrice\">");
                sb.Append(GetLocaleResourceString("Products.CallForPrice"));
                sb.Append("</span>");
            }
            else
            {
                //sub total
                decimal taxRate = decimal.Zero;
                decimal shoppingCartItemSubTotalWithDiscountBase = this.TaxService.GetPrice(shoppingCartItem.ProductVariant, PriceHelper.GetSubTotal(shoppingCartItem, true), out taxRate);
                decimal shoppingCartItemSubTotalWithDiscount = this.CurrencyService.ConvertCurrency(shoppingCartItemSubTotalWithDiscountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                string subTotalString = PriceHelper.FormatPrice(shoppingCartItemSubTotalWithDiscount);

                sb.Append("<span class=\"productPrice\">");
                sb.Append(subTotalString);
                sb.Append("</span>");

                //display an applied discount amount
                decimal shoppingCartItemSubTotalWithoutDiscountBase = this.TaxService.GetPrice(shoppingCartItem.ProductVariant, PriceHelper.GetSubTotal(shoppingCartItem, false), out taxRate);
                decimal shoppingCartItemDiscountBase = shoppingCartItemSubTotalWithoutDiscountBase - shoppingCartItemSubTotalWithDiscountBase;
                if (shoppingCartItemDiscountBase > decimal.Zero)
                {
                    decimal shoppingCartItemDiscount = this.CurrencyService.ConvertCurrency(shoppingCartItemDiscountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                    string discountString = PriceHelper.FormatPrice(shoppingCartItemDiscount);

                    sb.Append("<br />");
                    sb.Append(GetLocaleResourceString("ShoppingCart.ItemYouSave"));
                    sb.Append("&nbsp;&nbsp;");
                    sb.Append(discountString);
                }
            }
            return sb.ToString();
        }

        public string GetCheckoutAttributeDescription()
        {
            string result = string.Empty;
            if (NopContext.Current.User != null)
            {
                string checkoutAttributes = NopContext.Current.User.CheckoutAttributes;
                result = CheckoutAttributeHelper.FormatAttributes(checkoutAttributes);
            }

            return result;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //UpdateShoppingCart();
        }

        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            ContinueShopping();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            //Checkout();
        }
        public string getTotalPriceSummary
        {
            get
            {
                return String.Empty;// ctrlOrderTotals.getTotalPriceOrder;
            }
        }
        protected void btnApplyDiscountCouponCode_Click(object sender, EventArgs e)
        {
            ApplyDiscountCouponCode();
        }

        protected void btnApplyGiftCardCouponCode_Click(object sender, EventArgs e)
        {
            ApplyGiftCardCouponCode();
        }

        [DefaultValue(false)]
        public bool IsShoppingCart
        {
            get
            {
                object obj2 = this.ViewState["IsShoppingCart"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["IsShoppingCart"] = value;
            }
        }

        protected void rptShopHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptShoppingCart;
            RepeaterItem item = e.Item;
            OrderTotalsControl ctrlOrderTotals = (OrderTotalsControl)item.FindControl("ctrlOrderTotals");

            var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);

            var poo = (from p in cart
                       where DataBinder.Eval(e.Item.DataItem, "VendorID").ToString() == p.VendorID.ToString()
                       select p);

            if ((item.ItemType == ListItemType.Item) ||
                (item.ItemType == ListItemType.AlternatingItem))
            {
                rptShoppingCart = (Repeater)item.FindControl("rptShoppingCart");

                rptShoppingCart.DataSource = poo;
                rptShoppingCart.DataBind();
            }

            ctrlOrderTotals.vendorId = (int)DataBinder.Eval(e.Item.DataItem, "VendorID");
            ctrlOrderTotals.BindData(this.IsShoppingCart);
        }

        protected void rptShopHeader_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //buton click response.
            Response.Write(e.CommandName);

            switch (e.CommandName)
            {

                case "UpdateCart":
                    UpdateShoppingCart(e.Item);
                    break;
                case "CheckoutCart":
                    Checkout();
                    break;
            }
        }
    }
}