using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.Web.AddonsByOsShop.Classes;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules
{
    public partial class CproductSpec : BaseNopUserControl
    {
        private DataTable _dataSource = null;
        private string _title = "";
        private string _titleId = "";
        private string _titleIndex = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBind();
            }
        }
        public void DataBind()
        {
            if (dataSource != null)
            {
                foreach (DataRow dr in dataSource.Rows)
                {
                    var link = new NopcommerceLi();
                    link.HyperLink.Text = dr["Name"].ToString();
                    link.HyperLink.Attributes.Add("href", "#" + dr["SpecificationAttributeOptionID"].ToString());
                    link.HyperLink.Attributes.Add("class", "unSelected");
                    link.HyperLink.Attributes.Add("onclick", "return AjaxClient.OnAttrClick(this)");
                    this.phSpec.Controls.Add(link);
                }
            }
        }
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public string titleId
        {
            get
            {
                return _titleId;
            }
            set
            {
                _titleId = value;
            }
        }
        public string titleIndex
        {
            get
            {
                return _titleIndex;
            }
            set
            {
                _titleIndex = value;
            }
        }
        private string _pannelTitle;
        public string pannelTitle
        {
            get
            {
                return _pannelTitle;
            }
            set
            {
                _pannelTitle = value;
            }
        }
        public DataTable dataSource
        {
            set
            {
                _dataSource = value;
            }
            get
            {
                return _dataSource;
            }
        }
    }
}