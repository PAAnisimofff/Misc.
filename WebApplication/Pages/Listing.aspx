<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="WebApplication.Pages.Listing" MasterPageFile="~/Pages/Store.Master" %>
<%@ Import Namespace="System.Web.Routing" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>GPU Store</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
                foreach (WebApplication.Models.Item item in GetItems())
                {
                    Response.Write(String.Format(@"
                        <div class='item'>
                            <h3>{0}</h3>
                            {1}
                            <h4>{2:c}</h4>
                        </div>", item.Name, item.Description, item.Price));
                }
            %>
        </div>
    </form>
    <div>
        <%
            for(int i=1; i<=MaxPage; i++)
            {
                Response.Write(String.Format("<a href='/Pages/Listing.aspx?page{0}' {1}>{2}</a>", i, i == CurrentPage ? "class='selected'" : "", i));
            }
        %>
    </div>
</body>
</html>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="WebApplication.Pages.Listing" MasterPageFile="~/Pages/Store.Master" %>
<%@ Import Namespace="System.Web.Routing" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <asp:Repeater ItemType="WebApplication.Models.Item"
            SelectMethod="GetItems" runat="server">
            <ItemTemplate>
                <div class="item">
                    <h3><%# Item.Name %></h3>
                    <%# Item.Description %>
                    <h4><%# Item.Price.ToString("c") %></h4>
                    <button name="add" type="submit" value="<%# Item.ItemId %>">
                        Добавить в корзину
                    </button>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="pager">
        <%
            for (int i = 1; i <= MaxPage; i++)
            {
                string path = RouteTable.Routes.GetVirtualPath(null, null, new RouteValueDictionary() { { "page", i } }).VirtualPath;
                Response.Write(String.Format("<a href='{0}' {1}>{2}</a>", path, i == CurrentPage ? "class='selected'" : "", i));
            }
        %>
    </div>
</asp:Content>