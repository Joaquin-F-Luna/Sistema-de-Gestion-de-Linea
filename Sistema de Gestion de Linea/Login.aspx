<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.Login" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Ingreso</title>
    <!-- Bootstrap CSS -->
    <!-- Agrega la referencia al archivo CSS de Bootstrap desde la CDN -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">

    <!-- Agrega la referencia al archivo JavaScript de Bootstrap desde la CDN -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>
</head>

<body>
    <div class="alert alert-primary" role="alert">

        <h4 class="title">Accede con tu cuenta</h4>
    </div>
    <div class="container col-lg-5 col-md-8 col-sm-8 col-xs-8">
        <br>
        <div>
            <img alt="" class="d-block w-25 mx-auto" src="../MEDIA/LOGO2.jpg" />
        </div>
        <br>
        <form class="border border-primary bg-light rounded-lg p-3" action="" method="post" runat="server">
            <h4 class="title">Ingresa tu email y contraseña</h4>
            <br>
            <div>
                <div form runat="server" class="form-group col-md-12">
                    <label for="TxtUsuario" class="fw-bold">Usuario:</label>
                    <asp:TextBox runat="server" type="text" class="form-control" name="TxtUsuario" id="TxtUsuario" maxlength="50" value=""
                        placeholder="Usuario..." required></asp:TextBox>
                </div>

                <div class="form-group col-md-12">
                    <label for="txtPass" class="fw-bold">Contraseña:</label>
                    <asp:TextBox runat="server" type="password" class="form-control" name="txtPass" id="txtPass" maxlength="50" value=""
                        placeholder="Contraseña..." required></asp:TextBox>
                </div>
                <div class="form-group">
                        <asp:Label ID="lblNoti" runat="server" Text="" CssClass="control-label-sm-2"></asp:Label>
                    </div>
                <br>
                    <div class="form-group">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Acceder" CssClass="btn btn-primary" />
                    </div>
                </form>
            </div>
        </div>
    </body>
</html>