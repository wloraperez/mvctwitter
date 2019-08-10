<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Publicaciones
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Publicaciones de Tweets</h1>
    <% using (Html.BeginForm("Post", "Home"))
       { %>
    <div style="margin: 20px 0 0 0;">
        <h2 style="float: left; width: 10%;">
            Publicar:</h2>
            <p><%: Html.TextArea("Post") %> <input type="submit" value="Post" /></p>
              
        </div>
  
    <% } %>
    <div style="display: inline-block; margin: 10px 0 0 0;">
     <table>
        <tr>
            <th>
                Nombre
            </th>
            <th>
                Tweet
            </th>
            <th>
                Fecha
            </th>
        </tr>
        <% foreach (mvcTwitter.Models.Tweet lista in (List<mvcTwitter.Models.Tweet>)ViewData["tweet"])
           { %>
        <tr>
            <td>
                <%: Html.ActionLink(lista.User.UserName, "follow")%>
            </td>
            <td>
                <%: lista.Post %>
            </td>
            <td>
                <%: String.Format("{0:g}", lista.PostDate) %>
            </td>
        </tr>
        <% }%>
    </table>
    </div>
   
</asp:Content>
