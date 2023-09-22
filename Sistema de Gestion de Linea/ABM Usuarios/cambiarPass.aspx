<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="cambiarPass.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.ABM_Usuarios.editarTuUsuario" %>
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
            <h4 class="title">Editar tu Usuario</h4>
        </div>

    <form  runat="server">
        <div class="row">

              <div class="mb-3 col-lg-4">
                <label for="pass">Contraseña actual</label>
                <asp:TextBox ID="pass" runat="server" inputtype="text" class="form-control" 
                    placeholder="Escribe tu Contraseña Actual" required></asp:TextBox>
              </div>
         
        </div>
        <div class="row">
            <div class="mb-3 col-lg-4">
                <label for="newPass" class="form-label">Nueva contraseña</label>
                <asp:TextBox ID="newPass" runat="server" inputtype="text" class="form-control" 
                    placeholder="Escribe tu contraseña nueva" required></asp:TextBox>
              </div>
         <div class="mb-3 col-lg-4">
                <label for="checkNewPass" class="form-label">Confirmar nueva contraseña</label>
                <asp:TextBox ID="checkNewPass" runat="server" inputtype="text" class="form-control"
                    placeholder="Confirma tu contraseña nueva" required></asp:TextBox>
              </div>
        </div>
              

           

        
                
              <div class="mb-2 col-lg-8 <%--floating-label--%>"> 
              <asp:Label ID="lblnoti" runat="server" Text=" "></asp:Label>
              </div>
              <div class="mb-2 col-lg-5">
                <asp:Button ID="btActualizar" runat="server" class="form-control btn btn-primary" Text="Actualizar" OnClick="btActualizar_Click" />
              </div>



        
    </form>
   </div>
</body>

    </html>
</asp:Content>
