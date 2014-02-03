using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FredCK.FCKeditorV2;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.VendorAdministration.Modules;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class ManufacturerInfoControl : BaseNopVendorAdministrationUserControl
    {
        private void BindData()
        {
            var manufacturer = this.ManufacturerService.GetManufacturerByName(NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.CompanyName);

            if (this.HasLocalizableContent)
            {
                var languages = this.GetLocalizableLanguagesSupported();
                rptrLanguageTabs.DataSource = languages;
                rptrLanguageTabs.DataBind();
                rptrLanguageDivs.DataSource = languages;
                rptrLanguageDivs.DataBind();
            }
            
            if (manufacturer != null)
            {
                this.txtName.Text = manufacturer.Name;
                this.txtDescription.Value = manufacturer.Description;

                var manufacturerPicture = manufacturer.Picture;
                btnRemoveManufacturerImage.Visible = manufacturerPicture != null;
                string pictureUrl = this.PictureService.GetPictureUrl(manufacturerPicture, 100);
                this.iManufacturerPicture.Visible = true;
                this.iManufacturerPicture.ImageUrl = pictureUrl;
            }
            else
            {
                this.btnRemoveManufacturerImage.Visible = false;
                this.iManufacturerPicture.Visible = false;
            }
        }

        private void FillDropDowns()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.FillDropDowns();
                this.BindData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            BindJQuery();
            BindJQueryIdTabs();

            base.OnPreRender(e);
        }
        
        public Manufacturer SaveInfo()
        {
            var manufacturer = this.ManufacturerService.GetManufacturerById(this.ManufacturerId);
            var vendor = this.VendorService.GetVendor(NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.CustomerId);

            if (manufacturer != null)
            {
                Picture manufacturerPicture = manufacturer.Picture;
                HttpPostedFile manufacturerPictureFile = fuManufacturerPicture.PostedFile;
                if ((manufacturerPictureFile != null) && (!String.IsNullOrEmpty(manufacturerPictureFile.FileName)))
                {
                    byte[] manufacturerPictureBinary = manufacturerPictureFile.GetPictureBits();
                    if (manufacturerPicture != null)
                        manufacturerPicture = this.PictureService.UpdatePicture(manufacturerPicture.PictureId, manufacturerPictureBinary, manufacturerPictureFile.ContentType, true);
                    else
                        manufacturerPicture = this.PictureService.InsertPicture(manufacturerPictureBinary, manufacturerPictureFile.ContentType, true);
                }
                int manufacturerPictureId = 0;
                if (manufacturerPicture != null)
                    manufacturerPictureId = manufacturerPicture.PictureId;

                manufacturer.Name = txtName.Text;
                vendor.CompanyName = txtName.Text;
                manufacturer.Description = txtDescription.Value;
                manufacturer.TemplateId = 1;// int.Parse(this.ddlTemplate.SelectedItem.Value);
                manufacturer.PictureId = manufacturerPictureId;
                manufacturer.PriceRanges = String.Empty;// txtPriceRanges.Text;
                manufacturer.Published = true;// cbPublished.Checked;
                manufacturer.DisplayOrder = 0;// txtDisplayOrder.Value;
                manufacturer.UpdatedOn = DateTime.UtcNow;

                manufacturer.SEName = txtName.Text;
                manufacturer.MetaDescription = txtDescription.Value;


                this.ManufacturerService.UpdateManufacturer(manufacturer);
                this.VendorService.UpdateVendor(vendor);
            }
            else
            {
                Picture manufacturerPicture = null;
                HttpPostedFile manufacturerPictureFile = fuManufacturerPicture.PostedFile;
                if ((manufacturerPictureFile != null) && (!String.IsNullOrEmpty(manufacturerPictureFile.FileName)))
                {
                    byte[] manufacturerPictureBinary = manufacturerPictureFile.GetPictureBits();
                    manufacturerPicture = this.PictureService.InsertPicture(manufacturerPictureBinary, manufacturerPictureFile.ContentType, true);
                }
                int manufacturerPictureId = 0;
                if (manufacturerPicture != null)
                    manufacturerPictureId = manufacturerPicture.PictureId;

                DateTime nowDt = DateTime.UtcNow;

                manufacturer = new Manufacturer()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Value,
                    TemplateId = 1,//int.Parse(this.ddlTemplate.SelectedItem.Value),
                    PictureId = manufacturerPictureId,
                    PageSize = 10,
                    PriceRanges = String.Empty,//txtPriceRanges.Text,
                    Published = true, //cbPublished.Checked,
                    DisplayOrder = 0, //txtDisplayOrder.Value,
                    CreatedOn = nowDt,
                    UpdatedOn = nowDt
                };
                this.ManufacturerService.InsertManufacturer(manufacturer);
            }

            SaveLocalizableContent(manufacturer);

            return manufacturer;
        }

        protected void SaveLocalizableContent(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                return;

            if (!this.HasLocalizableContent)
                return;

            foreach (RepeaterItem item in rptrLanguageDivs.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var txtLocalizedName = (TextBox)item.FindControl("txtLocalizedName");
                    var txtLocalizedDescription = (FCKeditor)item.FindControl("txtLocalizedDescription");
                    var lblLanguageId = (Label)item.FindControl("lblLanguageId");

                    int languageId = int.Parse(lblLanguageId.Text);
                    string name = txtLocalizedName.Text;
                    string description = txtLocalizedDescription.Value;

                    bool allFieldsAreEmpty = (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(description));

                    var content = this.ManufacturerService.GetManufacturerLocalizedByManufacturerIdAndLanguageId(manufacturer.ManufacturerId, languageId);
                    if (content == null)
                    {
                        if (!allFieldsAreEmpty && languageId > 0)
                        {
                            //only insert if one of the fields are filled out (avoid too many empty records in db...)
                            content = new ManufacturerLocalized()
                            {
                                ManufacturerId = manufacturer.ManufacturerId,
                                LanguageId = languageId,
                                Name = name,
                                Description = description
                            };

                            this.ManufacturerService.InsertManufacturerLocalized(content);
                        }
                    }
                    else
                    {
                        if (languageId > 0)
                        {
                            content.LanguageId = languageId;
                            content.Name = name;
                            content.Description = description;

                            this.ManufacturerService.UpdateManufacturerLocalized(content);
                        }
                    }
                }
            }
        }

        protected void rptrLanguageDivs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtLocalizedName = (TextBox)e.Item.FindControl("txtLocalizedName");
                var txtLocalizedDescription = (FCKeditor)e.Item.FindControl("txtLocalizedDescription");
                var lblLanguageId = (Label)e.Item.FindControl("lblLanguageId");

                int languageId = int.Parse(lblLanguageId.Text);

                var content = this.ManufacturerService.GetManufacturerLocalizedByManufacturerIdAndLanguageId(this.ManufacturerId, languageId);

                if (content != null)
                {
                    txtLocalizedName.Text = content.Name;
                    txtLocalizedDescription.Value = content.Description;
                }

            }
        }

        protected void btnRemoveManufacturerImage_Click(object sender, EventArgs e)
        {
            try
            {
                Manufacturer manufacturer = this.ManufacturerService.GetManufacturerById(this.ManufacturerId);
                if (manufacturer != null)
                {
                    this.PictureService.DeletePicture(manufacturer.PictureId);

                    manufacturer.PictureId = 0;
                    this.ManufacturerService.UpdateManufacturer(manufacturer);
                    BindData();
                }
            }
            catch (Exception exc)
            {
                ProcessException(exc);
            }
        }

        public int ManufacturerId
        {
            get
            {
                Manufacturer mfu = IoC.Resolve<NopCommerce.BusinessLogic.Manufacturers.IManufacturerService>().GetManufacturerByName(NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.CompanyName);
                int mfuID;
                if (mfu != null)
                {
                    mfuID = mfu.ManufacturerId;
                }
                else
                {
                    mfuID = 0;
                }

                return mfuID;
            }
        }
    }
}