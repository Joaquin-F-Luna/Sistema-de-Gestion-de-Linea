<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="devolucionAsignacion.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.Asignaciones.devolucionAsignacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!DOCTYPE html>    
    <html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- Bootstrap CSS -->
    <!-- Agrega la referencia al archivo CSS de Bootstrap desde la CDN -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">

    <!-- Agrega la referencia al archivo JavaScript de Bootstrap desde la CDN -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>

    <title>Asignaciones del proyecto</title>

    <style>
   .miFieldset {
      border-color: #0026ff; /* Cambia #FF5733 al color que prefieras */
   }

   <style>
    .centered-gridview {
        margin: 0 auto; /* Esto centra el elemento horizontalmente */
        max-width: auto; /* Ajusta el ancho máximo según tus necesidades */
        margin-left: 20px; /* Agrega un margen a la izquierda */
    }
</style>

</style>
</style>

</head>

<body>
    <div class="container-fluid">
        <div class="alert alert-primary" role="alert">
            <h4 class="title">Devolución</h4>
        </div>
        <form runat="server">
 

            
               <div class="row">
            <div class="form-group col-md-5">
                    <label for="Id Pryecto" class="fw-bold"> Id del Proyecto</label>
                    <asp:TextBox runat="server" type="number"  min="1" class="form-control" step="any"
                        name="IdProyectoTB" id="IdProyectoTB" maxlength="6" 
                        value="" placeholder="Id Proyecto" required></asp:TextBox>
                </div>              
                    </div>
 
    <div class="border bg-light p-3">
        
        <div class="form-row">
            <div class="row">
                <!-- Nro Ticket -->
                <div class="form-group col-md-5">
                    <label for="nroticket" class="fw-bold">Número de Ticket</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nroticket"
                            ID="nroticket" MaxLength="6" value="" placeholder=""></asp:TextBox>
                </div>
                <div class="form-group col-md-5">
                    <label for="nrolinea" class="fw-bold">Número de Linea</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nrolinea"
                            ID="nrolinea" MaxLength="6" value="" placeholder=""></asp:TextBox>
                </div>

                <!-- descripcion -->
                <div class="form-group col-md-12">
                    <label for="descripcion" class="fw-bold">Descripcion del Proyecto</label>
                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" name="descripcion"
                        id="descripcion" maxlength="250"
                        Style="height: 100px; width: 600px" rows="3" placeholder=""></asp:TextBox>
                </div>
            </div>
        </div>
          <br><br>
     

        <div class="row">
            <div class="form-group col-md-4 fw-bold">
                <label for="estado" class="form-label">Estado:</label>
                <asp:DropDownList ID="DDL_tipoestado" runat="server" BackColor="ButtonShadow" Font-Size="Large"
                    CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" ForeColor="#333333">
                    <asp:ListItem>Abierto</asp:ListItem>
                    <asp:ListItem>Cerrado</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-4 fw-bold">
                <label for="area" class="form-label">Area actual:</label>
                <asp:DropDownList ID="DDL_tipoarea" runat="server" BackColor="ButtonShadow" Font-Size="Large"
                    CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" ForeColor="#333333">
                    <asp:ListItem>Asignaciones</asp:ListItem>
                    <asp:ListItem>Analisis</asp:ListItem>
                    <asp:ListItem>Cuadrilla</asp:ListItem>
                    <asp:ListItem>Certificacion</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-4 fw-bold">
            <label for="userAsignador" class="fw-bold"> Usuario que asigna</label>
                <br>
                <br>
                    <asp:Label ID="userAsignador" runat="server" Text=""></asp:Label>
                 </div>
        </div>
    </div>
<br><br>
       <%--    Acá van los datos a completar --%>

        
    <div class="border bg-light p-3">
        <div class="row">

            <!-- observacion -->
            <div class="form-group col-md-12">
                <label for="observacion" class="fw-bold">Observacion </label>
                <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" name="observacion" id="observacion" 
                    maxlength="750"
                    style="height: 80px; width:600px " rows="3" placeholder="Datos a tener en cuenta..." required></asp:TextBox>
            </div>
            <br><br>
              <br><br>
            <br><br>
    </div>

         <div class="row">
            <div class="form-group col-md-5">
                     <label for="estadoasignacion" class="fw-bold">Estado de asignacion:</label>
                  <br>       
                <asp:DropDownList ID="DDL_tipoestadoasignacion" runat="server" BackColor="ButtonShadow" Font-Size="Large"
                    CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" ForeColor="#333333">
                    <asp:ListItem>Rechazado</asp:ListItem>
                    <asp:ListItem>Okey</asp:ListItem>
                    <asp:ListItem>NoOkey</asp:ListItem>
                </asp:DropDownList>
                </div>

             
                      <div class="form-group col-md-5">
                        <br>
                       <asp:Button ID="btnDevolver"  runat="server" class="form-control btn btn-primary" Text="Finalizar"
                            OnClick="FinalizarDevolucion_Click" />
                        </div>
                    </div>

                      <asp:Label ID="notificacion" runat="server" Text=""></asp:Label>
   
            <br><br>
                    
                      <asp:GridView ID="gdtodosproyectos" runat="server" BackColor="White" BorderColor="#DEDFDE" 
                              BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False">
                              <AlternatingRowStyle BackColor="White" />
                              <Columns>
                                  <asp:BoundField DataField="Ticket" HeaderText="Ticket" />
                                  <asp:BoundField DataField="Linea" HeaderText="Línea" />
                                  <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                                  <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                  <asp:BoundField DataField="Fecha" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" HeaderText="Fecha" />
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

             </form>
         </div>
    </body>
</html>
</asp:Content>
