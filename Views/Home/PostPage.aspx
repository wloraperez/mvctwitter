<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tweets de Amigos

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <h1>
        Tweets de Amigo</h1>
    <div style="padding: 10px 0 0 0;">
        <%: Html.ActionLink("Volver atras", "follow") %>
    </div>
   <div style="display: inline-block; margin: 10px 0 0 0;">
       <% if (ViewData["tweet"] != null)
          { %>
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
                <%: lista.Post%>
            </td>
            <td>
                <%: String.Format("{0:g}", lista.PostDate)%>
            </td>
        </tr>
        <% }%>
    </table>
    <%} %>
    </div>

   
</asp:Content>
