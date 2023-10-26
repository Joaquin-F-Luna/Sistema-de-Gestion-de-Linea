<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="ModificarUsuario.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.ABM_Usuarios.ModificarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!DOCTYPE html>
<html lang="es">
        
<head>
 
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    
    <!-- Bootstrap CSS -->
    <!-- Agrega la referencia al archivo CSS de Bootstrap desde la CDN -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
   <link rel="stylesheet" href="../lib/css/bootstrap-grid.css">
    <!-- Agrega la referencia al archivo JavaScript de Bootstrap desde la CDN -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>

   
   </head>

    <body>
  
    <div class="continer-fluid">
        <div class="alert alert-primary" role="alert">
            <h4 class="title">Alta de Usuarios</h4>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>

    <form  runat="server">
        <div class="row">
              <div class="mb-3 col-lg-4">
                <label for="text" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" inputtype="text" class="form-control" placeholder="Nombre" aria-label="Nombre"></asp:TextBox>
              </div>
              <div class="mb-3 col-lg-4">
                <label for="text" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" inputtype="text" class="form-control" placeholder="Apellido" aria-label="Apellido"></asp:TextBox>
              </div>
              <div class="mb-3 col-lg-8">
                <label for="text" class="form-label">E-mail</label>
                <asp:TextBox ID="txtEmail" runat="server" inputtype="text" class="form-control" placeholder="ejemplo@email.com" aria-label="email"></asp:TextBox>
              </div>
              <div class="mb-4 col-lg-8">
                <label for="text" class="form-label">Nombre de usuario</label>
                <asp:TextBox ID="txtUser" runat="server" inputtype="text" class="form-control" placeholder="Usuario" aria-label="usuario"></asp:TextBox>
              </div>
              <div class="mb-3 col-lg-4">
                <label for="txtPass" class="form-label">Contraseña</label> <asp:CheckBox ID="cbxPass" runat="server" />
                <asp:TextBox ID="txtPass" runat="server" inputtype="password" class="form-control" placeholder="Contraseña" aria-label="txtPass"></asp:TextBox>
              </div>
              <div class="mb-4 col-lg-8">
              <label for="DropDownList" class="form-label">Rol asignado:  </label>
               <asp:DropDownList ID="dropRol" runat="server" BackColor="ButtonShadow" Font-Size="Large" DataSourceID="SqlDataSource1"  DataTextField="Descripcion" DataValueField="Idrol"
                  CssClass="border-black" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" ForeColor="#333333" >
                          
                  </asp:DropDownList>
                      <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                SelectCommand="SELECT * FROM ROLES">
            </asp:SqlDataSource>
                  <br>
                  <label for="rbtnActivo" class="form-label">Activo:  </label>
                  <asp:CheckBox ID="cbxEstadoUser" runat="server" />
              </div>
              <div class="mb-2 col-lg-8 <%--floating-label--%>"> 
              <asp:Label ID="lblnoti" runat="server" Text=" "></asp:Label>
              </div>
              <div class="mb-2 col-lg-5">
                <asp:Button ID="btActualizarUsuario" runat="server" class="form-control btn btn-primary" Text="Actualizar Usuario" OnClick="btActualizarUsuario_Click" />
              </div>
         </div>


        
    </form>
   </div>
</body>

    </html>
</asp:Content>
