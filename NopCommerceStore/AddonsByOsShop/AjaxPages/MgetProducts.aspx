<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MgetProducts.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.AjaxPages.MgetProducts" %>
<%@ Register TagPrefix="OsShop" TagName="ProductList" Src="~/AddonsByOsShop/Modules/MproductList.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="category-page">
            <div id="productBox">
                <OsShop:ProductList ID="ctrlProductList" runat="server" />
            </div>   
        </div>
    </form>
</body>
</html>
