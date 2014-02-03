<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.CurrencySelectorControl"
    CodeBehind="CurrencySelector.ascx.cs" %>

      <script type="text/javascript">


          function getCurrencySelector(obj) {
              var chkSpan = obj.getElementsByTagName("option");
              var dropDownValue = obj.options[obj.selectedIndex].value;

              jQuery.ajax({
                  url: "/AddonsByOsShop/AjaxPages/dealCurrencyBug.aspx?" + new Date().getTime(), //加入时间 url欺骗 重新运行ajax
                  type: "Get",
                  data: 'currencyValue=' + dropDownValue,
                  success: function (data, statue) {
                      dataCurrencyMessage = data + "";
                      window.location.reload();
                  }
              });

          };

  </script>


<asp:DropDownList runat="server" ID="ddlCurrencies" onchange="getCurrencySelector(this);" OnSelectedIndexChanged="ddlCurrencies_OnSelectedIndexChanged"
    CssClass="currencylist" EnableViewState="false">
</asp:DropDownList>
