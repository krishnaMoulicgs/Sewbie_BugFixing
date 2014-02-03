<%@ Page Title="" Language="C#" MasterPageFile="~/VendorAdministration/popup.master" AutoEventWireup="true" CodeBehind="ShippingAddressSelect.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.ShippingAddressSelect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">   
 <link type="text/css" rel="stylesheet" href="../App_Themes/SewbieAdmin/styles.css" />  
<script language="javascript" >
    function GetRowValue(val) {
        //window.opener.document.getElementById("hdnShippingAddressId").value = val;
        // opener.document.getElementById("hdnShippingAddressId").value = va;
        // document.getElementById("hdnAddressId").value = val;
        // window.opener.location.reload();
        // window.close();
    }
</script>
<html>
<head><style>
         
      </style>
</head>
      <body>
    <div class="LabelHead"> <literal> Address Details </literal></div> 
<asp:GridView ID="gvShippingAddressDetails"  runat="server" OnPageIndexChanging="gvShippingAddressDetails_PageIndexChanging" 
                AutoGenerateColumns="false" Width="100%" AllowPaging="true" 
        PageSize="15" onrowdatabound="gvShippingAddressDetails_RowDataBound" 
        ondatabinding="gvShippingAddressDetails_DataBinding" 
        ondatabound="gvShippingAddressDetails_DataBound" 
        onrowcommand="gvShippingAddressDetails_RowCommand" 
        CssClass="tablestyle" HeaderStyle-CssClass="headerstyle" RowStyle-CssClass="rowstyle" PagerStyle-CssClass="PagerStyle" AlternatingRowStyle-CssClass="altrowstyle" >
    <Columns>
        <asp:TemplateField> <ItemStyle Width="10%" />
            <AlternatingItemTemplate>
                <asp:Button ID="btnSelect" CssClass="buttonimage" runat="server" Text="Select" />
            </AlternatingItemTemplate>
             <ItemTemplate>
                <asp:Button ID="btnSelect"  CssClass="buttonimage" runat="server" Text="Select" 
                     OnClick ="btnSelect_Click" oncommand="btnSelect_Command" />
            </ItemTemplate>
        </asp:TemplateField>

        


           


         <asp:TemplateField HeaderText="Name" ItemStyle-Width="15%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("FirstName").ToString() +" "+ Eval("LastName").ToString())%> 
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

            <asp:TemplateField HeaderText="City" ItemStyle-Width="15%">
            <ItemTemplate>
            <%#Server.HtmlEncode(Eval("City").ToString())%>
            </ItemTemplate>

            <ItemStyle Width="15%"></ItemStyle>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Country" ItemStyle-Width="15%">
            <ItemTemplate>
            <%#Server.HtmlEncode(Eval("Country.Name").ToString())%>
            </ItemTemplate>

            <ItemStyle Width="15%"></ItemStyle>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="ID"   ItemStyle-Width="10%">
            <ItemTemplate>
                <%#Server.HtmlEncode(Eval("AddressID").ToString())%>
               
                 <asp:HiddenField ID="hdnAddressId" runat="server" Value='<%# Eval("AddressID") %>' />
            </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
        </asp:TemplateField>

    </Columns>
    <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
</asp:GridView>

</body>
</html>

</asp:Content>