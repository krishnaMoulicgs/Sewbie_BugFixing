<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getMiniShoppingInfo.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.OsShop.AjaxTemplateBySwn.AjaxPage.getMiniShoppingInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
     <!--读取购物车的数据-->
     <asp:ListView ID="lvCart" runat="server" OnItemDataBound="lvCart_ItemDataBound" EnableViewState="false">
            <LayoutTemplate>
                <div class="items">
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <ItemSeparatorTemplate>
            </ItemSeparatorTemplate>
    </asp:ListView>
    
    </form>
</body>
</html>
