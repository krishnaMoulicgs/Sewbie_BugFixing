<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CgetProducts.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AjaxCategory.AjaxPages.getProducts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="OsShop" TagName="ProductList" Src="~/AddonsByOsShop/Modules/CproductList.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <OsShop:ProductList ID="ctrlProductList" runat="server" />
    </form>
</body>
</html>
