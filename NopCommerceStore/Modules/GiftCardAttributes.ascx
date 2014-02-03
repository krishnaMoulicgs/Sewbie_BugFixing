<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GiftCardAttributes.ascx.cs"
    Inherits="NopSolutions.NopCommerce.Web.Modules.GiftCardAttributesControl" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>

<script type="text/javascript">
    $(document).ready(function () {
        GiftCardData[0] += '<%=ProductVariantId%>' + "," + '<%=RecipientName%>' + "|";
        GiftCardData[1] += '<%=ProductVariantId%>' + "," + '<%=RecipientEmail%>' + "|";
        GiftCardData[2] += '<%=ProductVariantId%>' + "," + '<%=SenderName%>' + "|";
        GiftCardData[3] += '<%=ProductVariantId%>' + "," + '<%=SenderEmail%>' + "|";
        GiftCardData[4] += '<%=ProductVariantId%>' + "," + '<%=GiftCardMessage%>' + "|";
    });
    function getCardMethod(obj) {
        if (obj.getAttribute("DataType") == "RecipientName") {
            GiftCardData[0] = dealArrayValue(obj.getAttribute("pvID"), obj.value.toString().trim(), GiftCardData[0].toString().toString());
        }
        if (obj.getAttribute("DataType") == "RecipientEmail") {
            GiftCardData[1] = dealArrayValue(obj.getAttribute("pvID"), obj.value.toString().trim(), GiftCardData[1].toString().toString());
        }
        if (obj.getAttribute("DataType") == "SenderName") {
            GiftCardData[2] = dealArrayValue(obj.getAttribute("pvID"), obj.value.toString().trim(), GiftCardData[2].toString().toString());
        }
        if (obj.getAttribute("DataType") == "SenderEmail") {
            GiftCardData[3] = dealArrayValue(obj.getAttribute("pvID"), obj.value.toString().trim(), GiftCardData[3].toString().toString());
        }
        if (obj.getAttribute("DataType") == "GiftCardMessage") {
            GiftCardData[4] = dealArrayValue(obj.getAttribute("pvID"), obj.value.toString().trim(), GiftCardData[4].toString().toString());
        }

    }

    function dealArrayValue(pvId, strContent, arrayValue) {

        var stransString = arrayValue.toString();
        var strItem = new Array();
        var str = new Array();
        var sign = 1;
        strItem = stransString.split("|");
        for (var i = 0; i < strItem.length - 1; i++) {
            str = strItem[i].toString().split(",");
            if (str[0] == pvId.toString()) {//如果属性ID有 则去掉以前的 再把新的放到后面
                subStr = str[0] + "," + str[1] + "|";
                stransString = stransString.replace(subStr, "");
                stransString = stransString + pvId + "," + strContent + "|";
                sign = 0;
                break;
            }
        }
        if (sign == 1)//循环了一般 如果没有，则添加到后面
            stransString = stransString + pvId + "," + strContent + "|";
        return stransString;

    }
</script>


<div class="giftCard">
    <dl>
        <dt>
            <asp:Label runat="server" ID="lblRecipientName" Text="<% $NopResources:Products.GiftCard.RecipientName %>"
                AssociatedControlID="txtRecipientName"></asp:Label></dt>
        <dd>
            <asp:TextBox runat="server" ID="txtRecipientName"></asp:TextBox></dd>
        <asp:PlaceHolder runat="server" ID="phRecipientEmail">
            <dt>
                <asp:Label runat="server" ID="lblRecipientEmail" Text="<% $NopResources:Products.GiftCard.RecipientEmail %>"
                    AssociatedControlID="txtRecipientEmail"></asp:Label></dt>
            <dd>
                <asp:TextBox runat="server" ID="txtRecipientEmail"></asp:TextBox></dd>
        </asp:PlaceHolder>
        <dt>
            <asp:Label runat="server" ID="lblSenderName" Text="<% $NopResources:Products.GiftCard.SenderName %>"
                AssociatedControlID="txtSenderName"></asp:Label></dt>
        <dd>
            <asp:TextBox runat="server" ID="txtSenderName"></asp:TextBox></dd>
        <asp:PlaceHolder runat="server" ID="phSenderEmail">
            <dt>
                <asp:Label runat="server" ID="lblSenderEmail" Text="<% $NopResources:Products.GiftCard.SenderEmail %>"
                    AssociatedControlID="txtSenderEmail"></asp:Label></dt>
            <dd>
                <asp:TextBox runat="server" ID="txtSenderEmail"></asp:TextBox></dd>
        </asp:PlaceHolder>
        <dt>
            <asp:Label runat="server" ID="lblGiftCardMessage" Text="<% $NopResources:Products.GiftCard.Message %>"
                AssociatedControlID="txtGiftCardMessage"></asp:Label></dt>
        <dd>
            <asp:TextBox runat="server" ID="txtGiftCardMessage" TextMode="MultiLine" Height="100px"
                Width="300px"></asp:TextBox></dd>
    </dl>
</div>
