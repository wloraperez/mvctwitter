<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<mvcTwitter.Models.Tweet>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <p>
        <%: Html.ActionLink("Twittear", "Create") %>
    </p>
    <% if (Model != null)
       { %>
    <table>
        <tr>
            <th>
                Tweet
            </th>
            <th>
                Fecha
            </th>
            <th>
                Nombre
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: item.Post%>
            </td>
            <td>
                <%: String.Format("{0:g}", item.PostDate)%>
            </td>
            <td>
                <%: item.User.UserName%>
            </td>
        </tr>
        <% } %>
    </table>
    <% }%>
</asp:Content>
