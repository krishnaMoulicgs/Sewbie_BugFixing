using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.Classes
{
    public class NopcommerceLi : WebControl, INamingContainer
    {
        public NopcommerceLi()
        {
            HyperLink = new HyperLink();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("li");
            writer.Write(HtmlTextWriter.TagRightChar);
            HyperLink.RenderControl(writer);
            writer.WriteEndTag("li");
        }

        public HyperLink HyperLink
        {
            get;
            set;
        }
    }
}