<%@ Page Title="" Language="C#" MasterPageFile="~/VendorAdministration/popup.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="BillingAddressSelect.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.BillingAddressSelect" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">


    <script language="javascript" >
        function GetRowValue(val) {            
        }
</script>

<asp:GridView ID="gvShippingAddressDetails" runat="server" OnPageIndexChanging="gvShippingAddressDetails_PageIndexChanging" 
                AutoGenerateColumns="false" Width="100%" AllowPaging="true" 
        PageSize="15" onrowdatabound="gvShippingAddressDetails_RowDataBound" 
        ondatabinding="gvShippingAddressDetails_DataBinding" 
        ondatabound="gvShippingAddressDetails_DataBound" 
        onrowcommand="gvShippingAddressDetails_RowCommand">
    <Columns>
        <asp:TemplateField>
            <AlternatingItemTemplate>
                <asp:Button ID="btnSelect" runat="server" Text="Select" />
            </AlternatingItemTemplate>
             <ItemTemplate>
                <asp:Button ID="btnSelect"  runat="server" Text="Select" 
                     OnClick ="btnSelect_Click" oncommand="btnSelect_Command" />
            </ItemTemplate>
        </asp:TemplateField>

            <asp:TemplateField HeaderText="AddressID" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("AddressID").ToString())%>
                 <asp:HiddenField ID="hdnAddressId" runat="server" Value='<%# Eval("AddressID") %>' />
            </ItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="First Name" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("FirstName").ToString())%>
            </ItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Last Name" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("LastName").ToString())%>
            </ItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Address 1" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Address1").ToString())%>
            </ItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Address 2" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("Address2").ToString())%>
            </ItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
        </asp:TemplateField>


    </Columns>
    <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
</asp:GridView>

</asp:Content>