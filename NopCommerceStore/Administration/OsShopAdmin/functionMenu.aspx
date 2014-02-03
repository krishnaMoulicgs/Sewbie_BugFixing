<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="functionMenu.aspx.cs" MasterPageFile="~/Administration/main.master"  Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AdminManager.functionMenu" %>

<%@ Register Src="~/AddonsByOsShop/UserExperience/AjaxMenuPro/HeadMenuPro.ascx" TagName="ProductMenu" TagPrefix="nopCommerce" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="~/Administration/Modules/ToolTipLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
   <nopCommerce:ProductMenu runat="server" ID="ctrlProMenu" />
    <div class="section-header" >
    <div class="title">
    <img src="/App_Themes/administration/images/osshop1.png" />  <%=GetLocaleResourceString("OsShop.OsShop")%>
    </div>
   <div class="options">
   <asp:Button ID="Button3" class="adminButtonBlue" OnClick="ChangeMenu_Click" runat="server" Text="<% $NopResources:Account.Save %>" />
   </div>
   </div>
      <ajaxToolkit:TabContainer runat="server" ID="ProductsTabs" ActiveTabIndex="0" CssClass="grey">
             <ajaxToolkit:TabPanel runat="server" ID="pnlProductDescription" HeaderText="<% $NopResources:OsShop.SynchronizeMenuItem %>">
                <ContentTemplate>
         			<div class="fulldescription">
                        <div>
                        <asp:Button ID="Button4" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:OsShop.Synchronize %>" onclick="CreateMenu_Click" />
                        </div>
            		</div>
             	</ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductReviews" HeaderText="<% $NopResources:OsShop.MenuOptions %>">
                <ContentTemplate>
                <table class="adminContent">
                <tr>
                 <td class="adminTitle">
                 <nopCommerce:ToolTipLabel runat="server" ID="lblMenuVar" Text="<% $NopResources:OsShop.MenuProfessional %>" ToolTip="" ToolTipImage="~/Administration/Common/ico-help.gif" />
                 </td>
                 <td class="adminData">
                 <input runat="server" type="radio" id="rblMenuVariant" name="MenuMethod" value="1"/>
                </td>
                </tr>


                <tr>              
                <td class="adminTitle">
                <nopCommerce:ToolTipLabel runat="server" ID="ToolTipLabel1" Text="<% $NopResources:OsShop.MenuElite %>" ToolTip="" ToolTipImage="~/Administration/Common/ico-help.gif" />
                </td>
                <td class="adminData">
               <input runat="server" type="radio" id="rblMenuOne" name="MenuMethod" value="2"/>
                </td>
                </tr>
                </table>    

                </ContentTemplate>
            </ajaxToolkit:TabPanel>
           
        </ajaxToolkit:TabContainer>


    </asp:Content>
