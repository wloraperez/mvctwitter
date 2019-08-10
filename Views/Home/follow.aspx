<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<mvcTwitter.Models.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    follow
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Los Usuarios que Sigo</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                UserName
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: Html.ActionLink("Dejar de Seguir", "Delete", new { id=item.id })%>
            </td>
            <td>
                <%:Html.ActionLink(item.UserName,"PostPage",new{ name = item.UserName}) %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
</asp:Content>
