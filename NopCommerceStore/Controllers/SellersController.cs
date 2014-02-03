using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NopSolutions.NopCommerce.Web.Controllers
{
    public class SellersController : Controller
    {
        //
        // GET: /Sellers/

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Sellers/Apply
        public ActionResult Apply()
        {
            return View();
        }
    }
}
