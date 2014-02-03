<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.CategoryPage" CodeBehind="Category.aspx.cs" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="cphHead" runat="server">
    <link type="text/css" href="../css/productshort.css" rel="Stylesheet" />
    <link type="text/css" href="../css/category.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph2" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <div class="top-cont">
        <div class="drop-cont CategoryContent">
            <input type="text" value="" id="TxtPageNo" class="CategoryTextbox" style="display: none" />
            <input type="button" value="Go" onclick="ShowPage(document.getElementById('TxtPageNo').value,0);"
                style="display: none" />
            <div class="divPageSize">
                <span>View:</span>
                <ul id="lnkSelectPageSize" class="divPageSize" style="display: none;">
                    <li onclick="ReArrangePages(25)">25</li>
                    <li onclick="ReArrangePages(50)">50</li>
                    <li onclick="ReArrangePages(75)">75</li>
                </ul>
            </div>
            <label for="textbox" class="labelbox" style="display: none">
                Number Of Items</label>
            <select id="SelectPageSize" name="PageSizeSelect" onchange="ReArrangePages(document.getElementById('SelectPageSize').value);"
                style="display: none">
                <option value="8">8</option>
                <option value="12">12</option>
                <option value="16">16</option>
                <option value="20">20</option>
                <option value="25">25</option>
                <option value="50">50</option>
                <option value="100">100</option>
            </select>
        </div>
        <div id="pageContainerTopPage" class="pageContainer">
        </div>
    </div>
    <input type="hidden" id="hidCategoryID" runat="server" value="0" />
    <input type="hidden" id="hidPageNumber" value="0" />
    <input type="hidden" id="hidMaxRecords" value="0" />
    <input type="hidden" id="hidRequestOut" value="0" />
    <input type="hidden" id="hidPageCount" value="0" />
    <input type="hidden" id="hdPageNo" runat="server" value="0" />
    <div id="divSearchResults" class="CdivSearchResults">
    </div>
    <script id="itemTemplate" type="text/x-jsrender">
        <div class="item-box" onmouseover="ShowProductsshort_button(this)" onmouseout="HideProductsshort_button(this)" style="display:none;" >
            <div class="item-frame" >
                <a id="divImage{{:#index+1}}" href="{{:productURL}}" title="Product details for {{:name}}">
                    <img title="Show details for {{:name}}" src="{{:thumbURL}}" alt="Picture of {{:name}}">
                </a>
                <div id="divItemInfoTop{{:#index+1}}" class="item-info-top">
                    <a id="SellerLink{{:#index+1}}" class="seller-name" title="Seller details for {{:sellerName}}" href="{{:sellerURL}}">{{:sellerName}}</a>    
                    <br>
                    <a id="hlname{{:#index+1}}" class="product-name" title="Product details for {{:name}}" href="{{:productURL}}">{{:shortName}}</a>
                    <br>
                    <span class="product-price" id="lblPrice{{:#index+1}}">{{:strPrice}}</span>
                </div>
                <div id="divItemInfoBottom{{:#index+1}}" class="item-info-bottom" style="display:none;">
                    <a id="lnkQuick{{:#index+1}}" title="Quick Look" href="{{:URL}}">
                       <img src="../images/ico-magnifyingglass.png" alt="Quick Look" style="height:30px;width:30px;" />
                    </a>
                    <a id="lnkAddToCart{{:#index+1}}" title="Add to Cart" href="{{:URL}}">
                       <img src="../images/ico-cart.png" alt="Add to Cart" style="height:30px;width:30px;" />
                    </a>
                    <a id="lnkCheckout{{:#index+1}}" title="One-Step Checkout" href="{{:URL}}">
                       <img src="../images/ico-onestep.gif" alt="One-Step Checkout." style="height:30px;widht:30px;" />
                    </a>
                </div>
            </div>
        </div>
    </script>
    <div class="bottom-cont">
        <div id="pageContainerBottomPage" class="pageContainer">
        </div>
        <div class="drop-cont CategoryContent">
            <input type="text" value="" id="TxtPageNoBottom" class="CategoryTextbox" style="display: none" />
            <input type="button" value="Go" onclick="ShowPage(document.getElementById('TxtPageNoBottom').value,0);"
                style="display: none" />
            <div class="divPageSize">
                View:
                <ul id="lnkSelectPageSizeBottom" class="divPageSize" style="display: none">
                    <li onclick="ReArrangePages(25)">25</li>
                    <li onclick="ReArrangePages(50)">50</li>
                    <li onclick="ReArrangePages(75)">75</li>
                </ul>
            </div>
            <label for="textbox" class="labelbox" style="display: none">
                Number Of Items</label>
            <select id="SelectPageSizeBottom" name="PageSizeSelect" onchange="ReArrangePages(document.getElementById('SelectPageSizeBottom').value);"
                style="display: none">
                <option value="8">8</option>
                <option value="12">12</option>
                <option value="16">16</option>
                <option value="20">20</option>
                <option value="25">25</option>
                <option value="50">50</option>
                <option value="100">100</option>
            </select>
        </div>
    </div>
    <div id="overlay" class="category_overlay" style="display: none; height: 4500px;
        width: 100%; top: 200px; text-align: center; left: 0px;">
    </div>
    <nav id="page-nav" style="display: none;">
      <a href="../pages/2.html">Load More</a>
    </nav>
    <div id="divLoading_Old" style="text-align: center; display: none;">
        <img id="loadingImage" alt="loading image ..." src="../images/ico-loading.gif" />
        <span id="spnLoadingMessage">... loading ... </span>
    </div>
    <div id="divResultEnd" style="text-align: center; display: none;">
        <span id="spnResultEndMessage">Sorry, there are no products to show for this category</span>
    </div>
</asp:Content>
