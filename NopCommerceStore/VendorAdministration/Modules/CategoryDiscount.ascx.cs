//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

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
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.VendorAdministration.Modules;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
 

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class CategoryDiscountControl : BaseNopVendorAdministrationUserControl
    {
        private void BindData()
        {
            List<int> _discountIds = new List<int>();

            var category = this.CategoryService.GetCategoryById(this.CategoryId);
            if (category != null)
            {
                var discounts = category.Discounts;                
                foreach (Discount dis in discounts)
                    _discountIds.Add(dis.DiscountId);
            }

            DiscountMappingControl.SelectedDiscountIds = _discountIds;
            DiscountMappingControl.BindData(DiscountTypeEnum.AssignedToCategories);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
        }

        public void SaveInfo()
        {
            SaveInfo(this.CategoryId);
        }

        public void SaveInfo(int catId)
        {
            var category = this.CategoryService.GetCategoryById(catId);

            if (category != null)
            {
                List<int> selectedDiscountIds = this.DiscountMappingControl.SelectedDiscountIds;
                var existingDiscounts = this.DiscountService.GetDiscountsByCategoryId(category.CategoryId);

                var allDiscounts = this.DiscountService.GetAllDiscounts(DiscountTypeEnum.AssignedToCategories);
                foreach (Discount discount in allDiscounts)
                {
                    if (selectedDiscountIds.Contains(discount.DiscountId))
                    {
                        if (existingDiscounts.Find(d => d.DiscountId == discount.DiscountId) == null)
                        {
                            this.DiscountService.AddDiscountToCategory(category.CategoryId, discount.DiscountId);
                        }
                    }
                    else
                    {
                        if (existingDiscounts.Find(d => d.DiscountId == discount.DiscountId) != null)
                        {
                            this.DiscountService.RemoveDiscountFromCategory(category.CategoryId, discount.DiscountId);
                        }
                    }
                }
            }
        }

        public int CategoryId
        {
            get
            {
                return CommonHelper.QueryStringInt("CategoryId");
            }
        }
    }
}