<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="Bienvenidos.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.Bienvenidos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
<!DOCTYPE html>
    <html>
    <head>
        <title>Listar Proyectos</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
    
        <!-- Bootstrap CSS -->
        <!-- Agrega la referencia al archivo CSS de Bootstrap desde la CDN -->
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">

        <!-- Agrega la referencia al archivo JavaScript de Bootstrap desde la CDN -->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>

        <style>
    .centered-gridview {
        margin: 0 auto; /* Esto centra el elemento horizontalmente */
        max-width: 800px; /* Ajusta el ancho máximo según tus necesidades */

    }
</style>
    </head>
    <body>
     <div>
            <img alt="" class="d-block w-25 mx-auto" src="../MEDIA/LOGO2.jpg" />
        </div>

        <form runat="server" class="row row-cols-lg-auto g-4 align-items-center">
            <div class="form-control d-grid gap-2 centered-gridview">
     <asp:GridView ID="gdtodosproyectos" runat="server" BackColor="White" BorderColor="#DEDFDE" 
              BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Devolución">
            <ItemTemplate>
                <!-- Agrega aquí el botón o icono que te llevará a otro formulario -->
                <asp:Button ID="btnVerDetalle" runat="server" Text="Devolución" OnClick="btnVerDetalle_Click" 
                     PostBackUrl='<%# "Asignaciones/devolucionAsignacion.aspx?Proyecto=" + Eval("Proyecto") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
        <asp:BoundField DataField="Ticket" HeaderText="Ticket" />
        <asp:BoundField DataField="Linea" HeaderText="Línea" />
        <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" />
        <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" />
        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
    </Columns>
    <FooterStyle BackColor="#CCCC99" />
    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle BackColor="#F7F7DE" />
    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FBFBF2" />
    <SortedAscendingHeaderStyle BackColor="#848384" />
    <SortedDescendingCellStyle BackColor="#EAEAD3" />
    <SortedDescendingHeaderStyle BackColor="#575357" />
</asp:GridView>
</div>


            </form>
        </body>
        </html>
</asp:Content>
