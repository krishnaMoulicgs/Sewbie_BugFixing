using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
namespace NopSolutions.NopCommerce.Web.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ShippingService : IShippingService
    {

        NopSolutions.NopCommerce.BusinessLogic.Shipping.ShippingService shippingService = IoC.Resolve<NopSolutions.NopCommerce.BusinessLogic.Shipping.ShippingService>();
        NopSolutions.NopCommerce.BusinessLogic.Orders.IShoppingCartService shoppingCartService = IoC.Resolve<IShoppingCartService>();
          
        public ShippingOption GetShippingOptions(int vendorId)
        {
            ShoppingCart shoppingCart = shoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart, vendorId);
            string strError = string.Empty;
            
            List<ShippingOption> foo = IoC.Resolve<NopSolutions.NopCommerce.BusinessLogic.Shipping.ShippingService>().GetShippingOptions(
                shoppingCart, 
                NopCommerce.BusinessLogic.NopContext.Current.User,
                NopCommerce.BusinessLogic.NopContext.Current.User.ShippingAddress, 
                ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                throw new Exception(strError);
            }
            //TODO:Assuming one is always returned.
            return foo[0];
        }
    }
}
