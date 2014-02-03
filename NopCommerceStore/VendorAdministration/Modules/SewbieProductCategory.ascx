<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.SewbieProductCategoryControl"
    CodeBehind="SewbieProductCategory.ascx.cs" %>
    <%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
    <%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
    <%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
    
    <script type="text/javascript">
        var ddlMensCategory_id = '<%= ddlMensCategory.ClientID %>';
        var ddlWomensCategory_id = '<%= ddlWomensCategory.ClientID %>';
        var ddlKidsCategory_id = '<%= ddlKidsCategory.ClientID %>';
    </script>
    
    <script type="text/javascript" src="../../Scripts/ProductCategory.js"> </script>

    <table>
        <tr>
            <td>
                Mens
            </td>
            <td>
                Womens
            </td>
            <td>
                Kids
            </td>
        </tr>
    
        <tr>
            <td><%--Men's--%>
                <div id="divMensCategory" style="display:inline; width:39%;">
                    <select multiple="true" size="15" id="ddlMensCategory" runat="server">
    
                    </select>
                </div>
            </td>
            <td>
                <%--Women's--%>
                <div id="divWomensCategory" style="display:inline; width:39%;">
                    <select multiple="true" size="15" id="ddlWomensCategory" runat="server">
    
                    </select>
                </div>
            </td>
            <td>
                <%--Kid's--%>
                <div id="divKidsCategory" style="display:inline; width:39%;">
                    <select multiple="true" size="15" id="ddlKidsCategory" runat="server">
    
                    </select>
                </div>
            </td>
            <td>
                <asp:CustomValidator ID="reqCategory" runat="server" ClientValidationFunction="RequireCategory_Check" 
                            ErrorMessage="Please select at least one category.">
                </asp:CustomValidator>
            </td>
        </tr>
    </table>
    
    

