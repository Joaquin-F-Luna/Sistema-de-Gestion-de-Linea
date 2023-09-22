<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="Ver Proyectos.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.ABM_Proyectos.Ver_Proyectos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

        
    </head>

    <body>
        <div class="continer-fluid">
            <div class="alert alert-primary" role="alert">
                <h4 class="title">Listar un proyecto</h4>
            </div>
        </div>

            <form runat="server" class="row row-cols-lg-auto g-4 align-items-center">
              
                    <div class="col-lg-5">
                        <asp:TextBox ID="txtBuscarProyecto" runat="server" inputtype="text" class="form-control fw-bold" placeholder="Ingresa ID Proyecto" aria-label="buscar id"></asp:TextBox>
                    </div>
                    
                    <div class="col-lg-5">
                        <asp:Button ID="buscarProyecto" runat="server" class="form-control btn btn-primary" Text="Buscar" OnClick="buscarProyecto_Click1" />
                    </div>

                    <asp:GridView ID="gdListaUno" runat="server" AutoGenerateColumns="False" DataKeyNames="NumeroTicket" DataSourceID="SqlDataSource2" BorderStyle="None" CssClass="row-cols-auto" BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="NumeroTicket" HeaderText="N. Ticket" SortExpression="NumeroTicket" />
                            <asp:BoundField DataField="N. Linea" HeaderText="N. Linea" SortExpression="N. Linea" />
                            <asp:BoundField DataField="Descripción" HeaderText="Descripción" SortExpression="Descripción" />
                            <asp:BoundField DataField="Inicio" HeaderText="Inicio" SortExpression="Inicio" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" />
                            <asp:BoundField DataField="Finalización" HeaderText="Finalización" SortExpression="Finalización" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" />
                            <asp:BoundField DataField="Creado" HeaderText="Creado" SortExpression="Creado" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" />
                            <asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                            <asp:BoundField DataField="Creador" HeaderText="Creador" SortExpression="Creador" />
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
                

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBSGL %>" SelectCommand="SELECT Proyecto.NumeroTicket, Proyecto.NumeroLinea AS 'N. Linea', Proyecto.Descripcion AS 'Descripción', Proyecto.FechaInicio AS 'Inicio', Proyecto.FechaFinalizacion AS 'Finalización', Proyecto.FechaCreacion AS 'Creado', Proyecto.Localidad, EstadoProyecto.Estado, USUARIOS.Username AS 'Creador' FROM Proyecto INNER JOIN EstadoProyecto ON Proyecto.FK_ID_Estado = EstadoProyecto.Id INNER JOIN USUARIOS ON Proyecto.FK_ID_Usuario = USUARIOS.Id 
WHERE (Proyecto.NumeroTicket = @parametro)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtBuscarProyecto" Name="parametro" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
       
                      <div class="form-control d-grid gap-2">

                          <div class="continer-fluid">
                            <div class="alert alert-primary" role="alert">
                                <h4 class="title">Listar todos los proyectos</h4>
                            </div>
                          </div>
                            <asp:Button ID="listarTodos" runat="server" class="btn btn-primary" Text="Listar Todos" OnClick="buscartodos_Click"/>
                      
                          <asp:GridView ID="gdtodosproyectos" runat="server" BackColor="White" BorderColor="#DEDFDE" 
                              BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False">
                              <AlternatingRowStyle BackColor="White" />
                              <Columns>
                                  <asp:BoundField DataField="N. Ticket" HeaderText="N. Ticket" />
                                  <asp:BoundField DataField="N. Linea" HeaderText="N. Línea" />
                                  <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                  <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                                  <asp:BoundField DataField="F. Inicio" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" HeaderText="F. Inicio" />
                                  <asp:BoundField DataField="F.Termino" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" HeaderText="F. Termino" />
                                  <asp:BoundField DataField="Calle" HeaderText="Calle" />
                                  <asp:BoundField DataField="N. Calle" HeaderText="N. Calle" />
                                  <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                  <asp:BoundField DataField="Nombre de Usuario" HeaderText="Nombre de Usuario" />
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
