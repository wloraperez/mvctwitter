<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<mvcTwitter.Models.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Usuario
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">
// <![CDATA[

    function SeguirUsuario(u) {
        var miElemento = document.getElementById("lb3");
        //alert(miElemento.innerHTML);
        alert(u);
    }

// ]]>
    </script>
    <h4><a href="/Home/follow">Los que sigo</a></h4>
    <h1 align="center"> Listados de usuarios Sugeridos</h1>
    <h2 align="center" style="color: #FF0000" > <%=ViewData["MensajeUser"]%></h2>



    <%foreach (mvcTwitter.Models.User usuario in Model)
      { %>
      <table align="center" frame="box" 
        style="table-layout: auto; height: 25px;">
      <tr>
      <td style="width: 101px">
      <img src="http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/64/Places-user-identity-icon.png" 
      alt="" style="width: 71px; height: 41px" />
      </td>
          <td align="left" style="width: 460px; font-size: medium;" >

                 <label id="lb3" style="text-align: justify"> <%=usuario.UserName %>  </label>
            </td>
          <td style="width: 6px">
              <%-- <button id="Button1" name="btnseguir"  onclick="SeguirUsuario('<%=usuario %>')"> Segui+ </button>--%> 
    <form action ="/home/seguirUser">
          <%: Html.Hidden("followed", usuario.id) %>
               <input  type="submit" value="Seguir+"/>
          </form>      
           </td>
      </tr>
     </table>

   
      
    <%} %>


    


</asp:Content>
