using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace NopSolutions.NopCommerce.Web.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SearchService : ISearchService
    {
        public SearchDTO SearchByCategory(string category, string page)
        {
            SearchDTO dto = new SearchDTO();
            int recs = 0;
            int pageNumber = Convert.ToInt32(page);// != String.Empty || page != null ? Convert.ToInt32(page) : 0;
            //pageNumber = pageNumber > 0 ? pageNumber - 1: 0; //0 based index.
           
            int PageNo = 0, NoOfItemsPerPage = 0;            
            if(HttpContext.Current.Request.QueryString["PageNo"]!=null)
             PageNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["PageNo"].Replace("'", ""));
            if (HttpContext.Current.Request.QueryString["NoOfItemsPerPage"] != null)
             NoOfItemsPerPage = Convert.ToInt32(HttpContext.Current.Request.QueryString["NoOfItemsPerPage"].Replace("'", ""));

            List<Product> products ;
            if (PageNo > 0)
            {
                products = IoC.Resolve<IProductService>().GetAllProducts(Convert.ToInt32(category), 0, 0, 0, null, null, null, String.Empty, false, NoOfItemsPerPage, PageNo-1, new List<int>(), ProductSortingEnum.CreatedOn, out recs);
                dto.maxRecords = recs;
                dto.currentPage = PageNo-1;
            }
            else
            {
               
                products = IoC.Resolve<IProductService>().GetAllProducts(Convert.ToInt32(category), 0, 0, 0, null, null, null, String.Empty, false, 20, pageNumber, new List<int>(), ProductSortingEnum.CreatedOn, out recs);
                dto.maxRecords = recs;
                dto.currentPage = pageNumber;
            }                
            
            List<SearchProducts> lstSPs = new List<SearchProducts>();
            
            foreach (Product p in products)
            {
                SearchProducts dtoSP = new SearchProducts();
                dtoSP.productid = p.ProductId;
                dtoSP.price = p.ProductVariants.First().Price;
                dtoSP.strPrice = p.ProductVariants.First().Price.ToString("C2");
                dtoSP.name = p.Name;
                dtoSP.shortName = p.Name.Length > 30 ? p.Name.Substring(0, 30) + "..." : p.Name;
                dtoSP.sellerName = p.ProductVariants[0].Vendor.CompanyName;
                dtoSP.sellerURL = SEOHelper.GetManufacturerUrl(IoC.Resolve<IManufacturerService>().GetManufacturerByName(p.ProductVariants[0].Vendor.CompanyName));
                dtoSP.productURL = SEOHelper.GetProductUrl(p.ProductId);
                dtoSP.thumbURL = IoC.Resolve<IPictureService>().GetWidePictureUrl(p.DefaultPicture, 238);               
                lstSPs.Add(dtoSP);
            }

            dto.products = lstSPs;

            return dto;
        }

        public SearchDTO SearchByCategoryPaging(string category, string page,string pagesize)
        {
            SearchDTO dto = new SearchDTO();
            int recs = 0;
            int pageNumber = Convert.ToInt32(page);// != String.Empty || page != null ? Convert.ToInt32(page) : 0;
            //pageNumber = pageNumber > 0 ? pageNumber - 1: 0; //0 based index.
            string tval = "";

            int PageNo = 0, NoOfItemsPerPage = 0;
            if (HttpContext.Current.Request.QueryString["PageNo"] != null)
                PageNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["PageNo"].Replace("'", ""));
            if (HttpContext.Current.Request.QueryString["NoOfItemsPerPage"] != null)
                NoOfItemsPerPage = Convert.ToInt32(HttpContext.Current.Request.QueryString["NoOfItemsPerPage"].Replace("'", ""));

            List<Product> products;
            if (PageNo > 0)
            {
                products = IoC.Resolve<IProductService>().GetAllProducts(Convert.ToInt32(category), 0, 0, 0, null, null, null, String.Empty, false, NoOfItemsPerPage, PageNo, new List<int>(), ProductSortingEnum.CreatedOn, out recs);
                dto.maxRecords = recs;
                dto.currentPage = PageNo;
            }
            else
            {
                products = IoC.Resolve<IProductService>().GetAllProducts(Convert.ToInt32(category), 0, 0, 0, null, null, null, String.Empty, false, Convert.ToInt32(pagesize), pageNumber, new List<int>(), ProductSortingEnum.CreatedOn, out recs);
                dto.maxRecords = recs;
                dto.currentPage = pageNumber;
            }  


            List<SearchProducts> lstSPs = new List<SearchProducts>();

            foreach (Product p in products)
            {
                SearchProducts dtoSP = new SearchProducts();
                dtoSP.productid = p.ProductId;
                dtoSP.price = p.ProductVariants.First().Price;
                dtoSP.strPrice = p.ProductVariants.First().Price.ToString("C2");
                dtoSP.name = p.Name;
                dtoSP.shortName = p.Name.Length > 30 ? p.Name.Substring(0, 30) + "..." : p.Name;
                dtoSP.sellerName = p.ProductVariants[0].Vendor.CompanyName;
                dtoSP.sellerURL = SEOHelper.GetManufacturerUrl(IoC.Resolve<IManufacturerService>().GetManufacturerByName(p.ProductVariants[0].Vendor.CompanyName));
                dtoSP.productURL = SEOHelper.GetProductUrl(p.ProductId);
                dtoSP.thumbURL = IoC.Resolve<IPictureService>().GetWidePictureUrl(p.DefaultPicture, 238);
                tval = tval + p.Name + "  ";
                lstSPs.Add(dtoSP);
            }

            dto.products = lstSPs;

            return dto;
        }

        public SearchDTO GetRecentlyAddedItems(string category, string page)
        {
            SearchDTO dto = new SearchDTO();
            int recs = 0;
            int pageNumber = Convert.ToInt32(page);

            List<Product> products = IoC.Resolve<IProductService>().GetRecentlyAddedProducts(30);

            dto.maxRecords = recs;
            dto.currentPage = pageNumber;

            List<SearchProducts> lstSPs = new List<SearchProducts>();

            foreach (Product p in products)
            {
                SearchProducts dtoSP = new SearchProducts();
                dtoSP.productid = p.ProductId;
                dtoSP.price = p.ProductVariants.First().Price;
                dtoSP.strPrice = p.ProductVariants.First().Price.ToString("C2");
                dtoSP.name = p.Name;
                dtoSP.shortName = p.Name.Length > 30 ? p.Name.Substring(0, 30) + "..." : p.Name;
                dtoSP.sellerName = p.ProductVariants[0].Vendor.CompanyName;
                dtoSP.sellerURL = SEOHelper.GetManufacturerUrl(IoC.Resolve<IManufacturerService>().GetManufacturerByName(p.ProductVariants[0].Vendor.CompanyName));
                dtoSP.productURL = SEOHelper.GetProductUrl(p.ProductId);
                dtoSP.thumbURL = IoC.Resolve<IPictureService>().GetWidePictureUrl(p.DefaultPicture, 238);
                lstSPs.Add(dtoSP);
            }

            dto.products = lstSPs;

            return dto;
        }
    }


}
