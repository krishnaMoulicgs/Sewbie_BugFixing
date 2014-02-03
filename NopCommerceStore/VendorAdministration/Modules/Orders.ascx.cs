using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.ExportImport;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Profile;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.BusinessLogic.Utils;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class OrdersControl : BaseNopVendorAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillDropDowns();
                SetDefaultValues();

                try
                {
                    BindGrid();
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }

                //buttons
                btnPrintPdfPackagingSlips.Visible = this.SettingManager.GetSettingValueBoolean("Features.SupportPDF");
                btnExportXLS.Visible = this.SettingManager.GetSettingValueBoolean("Features.SupportExcel");
            }
        }

        protected void SetDefaultValues()
        {
        }

        protected List<Order> GetOrders()
        {
            DateTime? startDate = ctrlStartDatePicker.SelectedDate;
            DateTime? endDate = ctrlEndDatePicker.SelectedDate;
            if(startDate.HasValue)
            {
                startDate = DateTimeHelper.ConvertToUtcTime(startDate.Value, DateTimeHelper.CurrentTimeZone);
            }
            if(endDate.HasValue)
            {
                endDate = DateTimeHelper.ConvertToUtcTime(endDate.Value, DateTimeHelper.CurrentTimeZone).AddDays(1);
            }

            OrderStatusEnum? orderStatus = null;
            int orderStatusId = int.Parse(ddlOrderStatus.SelectedItem.Value);
            if (orderStatusId > 0)
                orderStatus = (OrderStatusEnum)Enum.ToObject(typeof(OrderStatusEnum), orderStatusId);

            PaymentStatusEnum? paymentStatus = null;
            int paymentStatusId = int.Parse(ddlPaymentStatus.SelectedItem.Value);
            if (paymentStatusId > 0)
                paymentStatus = (PaymentStatusEnum)Enum.ToObject(typeof(PaymentStatusEnum), paymentStatusId);

            ShippingStatusEnum? shippingStatus = null;
            int shippingStatusId = int.Parse(ddlShippingStatus.SelectedItem.Value);
            if (shippingStatusId > 0)
                shippingStatus = (ShippingStatusEnum)Enum.ToObject(typeof(ShippingStatusEnum), shippingStatusId);

            string orderGuid = txtOrderGuid.Text.Trim();

            var orders = this.OrderService.SearchVendorOrders(startDate, endDate,
                txtCustomerEmail.Text,  orderStatus, paymentStatus, shippingStatus, orderGuid, NopCommerce.BusinessLogic.NopContext.Current.User.CustomerId);
            return orders;
        }

        protected void FillDropDowns()
        {
            //order statuses
            this.ddlOrderStatus.Items.Clear();
            ListItem itemOrderStatus = new ListItem(GetLocaleResourceString("VendorAdmin.Common.All"), "0");
            this.ddlOrderStatus.Items.Add(itemOrderStatus);
            OrderStatusEnum[] orderStatuses = (OrderStatusEnum[])Enum.GetValues(typeof(OrderStatusEnum));            
            foreach (OrderStatusEnum orderStatus in orderStatuses)
            {
                ListItem item2 = new ListItem(orderStatus.GetOrderStatusName(), ((int)orderStatus).ToString());
                this.ddlOrderStatus.Items.Add(item2);
            }

            //payment statuses
            this.ddlPaymentStatus.Items.Clear();
            ListItem itemPaymentStatus = new ListItem(GetLocaleResourceString("VendorAdmin.Common.All"), "0");
            this.ddlPaymentStatus.Items.Add(itemPaymentStatus);
            PaymentStatusEnum[] paymentStatuses = (PaymentStatusEnum[])Enum.GetValues(typeof(PaymentStatusEnum));
            foreach (PaymentStatusEnum paymentStatus in paymentStatuses)
            {
                ListItem item2 = new ListItem(paymentStatus.GetPaymentStatusName(), ((int)paymentStatus).ToString());
                this.ddlPaymentStatus.Items.Add(item2);
            }

            //shipping statuses
            this.ddlShippingStatus.Items.Clear();
            ListItem itemShippingStatus = new ListItem(GetLocaleResourceString("VendorAdmin.Common.All"), "0");
            this.ddlShippingStatus.Items.Add(itemShippingStatus);
            ShippingStatusEnum[] shippingStatuses = (ShippingStatusEnum[])Enum.GetValues(typeof(ShippingStatusEnum));
            foreach (ShippingStatusEnum shippingStatus in shippingStatuses)
            {
                ListItem item2 = new ListItem(shippingStatus.GetShippingStatusName(), ((int)shippingStatus).ToString());
                this.ddlShippingStatus.Items.Add(item2);
            }
        }
        
        protected void btnExportXLS_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string fileName = string.Format("orders_{0}_{1}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
                    string filePath = string.Format("{0}files\\ExportImport\\{1}", HttpContext.Current.Request.PhysicalApplicationPath, fileName);
                    var orders = GetOrders();

                    this.ExportManager.ExportOrdersToXls(filePath, orders);
                    CommonHelper.WriteResponseXls(filePath, fileName);
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected void BtnPrintPdfPackagingSlips_OnClick(object sender, EventArgs e)
        {
            try
            {
                string fileName = String.Format("packagingslips_{0}_{1}.pdf", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
                string filePath = String.Format("{0}files\\exportimport\\{1}", HttpContext.Current.Request.PhysicalApplicationPath, fileName);

                PDFHelper.PrintPackagingSlipsToPdf(GetOrders(), filePath);

                CommonHelper.WriteResponsePdf(filePath, fileName);
            }
            catch(Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void BindGrid()
        {
            var orders = GetOrders();
            if (orders.Count > 0)
            {
                this.gvOrders.Visible = true;
                this.lblNoOrdersFound.Visible = false;
                this.gvOrders.DataSource = orders;
                this.gvOrders.DataBind();
            }
            else
            {
                this.gvOrders.Visible = false;
                this.lblNoOrdersFound.Visible = true;
            }
        }

        protected string GetCustomerInfo(int customerId)
        {
            string customerInfo = string.Empty;
            Customer customer = this.CustomerService.GetCustomerById(customerId);
            if (customer != null)
            {
                if (customer.IsGuest)
                {
                    customerInfo = string.Format(GetLocaleResourceString("Admin.Orders.CustomerColumn.Guest"));
                }
                else
                {
                    customerInfo = string.Format(Server.HtmlEncode(customer.Email));
                }
            }
            return customerInfo;
        }

        protected void gvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrders.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    BindGrid();
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }
    }
}