<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="asignaciones_vencidas.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.Asignaciones.asignaciones_vencidas" %>
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

        




    </head>

    <body>
        <div class="continer-fluid mt-1">
        <div class="continer-fluid">
            <div class="alert alert-primary" role="alert">
                <h4 class="title">Area Analisis</h4>
            </div>
        </div>

            <form runat="server" class="row row-cols-lg-auto g-4 align-items-center">
              
                    
                <div class="form-control d-grid gap-2 centered-gridview">
                    <asp:GridView ID="gdListaUno" runat="server" AutoGenerateColumns="False"
                        DataSourceID="SqlDataSource2" BorderStyle="None"
                        CssClass="row-cols-auto" BackColor="White" BorderColor="#DEDFDE"
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                          
                            <asp:BoundField DataField="IdProyecto_FK" HeaderText="IdProyecto_FK" SortExpression="IdProyecto_FK" />
                            <asp:BoundField DataField="area" HeaderText="area" SortExpression="area" />
                            <asp:BoundField DataField="UsuarioAsignado" HeaderText="UsuarioAsignado" SortExpression="UsuarioAsignado" />
                            <asp:BoundField DataField="fechaMovimiento" HeaderText="fechaMovimiento" SortExpression="fechaMovimiento" />
                            <asp:BoundField DataField="DiasDesvio" HeaderText="DiasDesvio" ReadOnly="True" SortExpression="DiasDesvio" />
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
                

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                         SelectCommand="SELECT 
    asignaciones.IdProyecto_FK, 
    asignaciones.area, 
    USUARIOS.username as UsuarioAsignado, 
    movimientos_asignaciones.fechaMovimiento, 
    DATEDIFF(day, movimientos_asignaciones.fechaMovimiento +2, GETDATE()) as DiasDesvio 
FROM 
    asignaciones 
INNER JOIN 
    movimientos_asignaciones ON asignaciones.Id_Asignacion = movimientos_asignaciones.IdAsignaciones_FK 
INNER JOIN 
    USUARIOS ON USUARIOS.Id = movimientos_asignaciones.IdUserAsignado_FK 
WHERE 
    asignaciones.IdProyecto_FK = 1 
    AND DATEDIFF(day, movimientos_asignaciones.fechaMovimiento, GETDATE()) >= 2 
    AND movimientos_asignaciones.estadoMovimiento = 'PENDIENTE' 
    AND movimientos_asignaciones.area_mov = 'ANALISIS';">
                    </asp:SqlDataSource>
                </div>
                      <div class="form-control d-grid gap-2">

                          <div class="continer-fluid">
                            <div class="alert alert-primary" role="alert">
                                <h4 class="title">Area Cuadrilla</h4>
                            </div>
                          </div>
                       

                          <div class="form-control d-grid gap-2 centered-gridview">
                              <asp:GridView ID="todoslosproyectos" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                  BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                  AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
                                  <AlternatingRowStyle BackColor="White" />
             
                     <Columns>

                          

                         <asp:BoundField DataField="IdProyecto_FK" HeaderText="IdProyecto_FK" SortExpression="IdProyecto_FK" />
                         <asp:BoundField DataField="area" HeaderText="area" SortExpression="area" />
                         <asp:BoundField DataField="UsuarioAsignado" HeaderText="UsuarioAsignado" SortExpression="UsuarioAsignado" />
                         <asp:BoundField DataField="fechaMovimiento" HeaderText="fechaMovimiento" SortExpression="fechaMovimiento" />
                         <asp:BoundField DataField="DiasDesvio" HeaderText="DiasDesvio" ReadOnly="True" SortExpression="DiasDesvio"></asp:BoundField>
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
                              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                                   SelectCommand="SELECT 
    asignaciones.IdProyecto_FK, 
    asignaciones.area, 
    USUARIOS.username as UsuarioAsignado, 
    movimientos_asignaciones.fechaMovimiento, 
    DATEDIFF(day, movimientos_asignaciones.fechaMovimiento +10, GETDATE()) as DiasDesvio 
FROM 
    asignaciones 
INNER JOIN 
    movimientos_asignaciones ON asignaciones.Id_Asignacion = movimientos_asignaciones.IdAsignaciones_FK 
INNER JOIN 
    USUARIOS ON USUARIOS.Id = movimientos_asignaciones.IdUserAsignado_FK 
WHERE 

    DATEDIFF(day, movimientos_asignaciones.fechaMovimiento, GETDATE()) >= 10 
    AND movimientos_asignaciones.estadoMovimiento = 'PENDIENTE' 
    AND movimientos_asignaciones.area_mov = 'CUADRILLA';">
           
                              </asp:SqlDataSource>


                          </div>
                      </div>
                 <div class="form-control d-grid gap-2">

                          <div class="continer-fluid">
                            <div class="alert alert-primary" role="alert">
                                <h4 class="title">Area Certificacion</h4>
                            </div>
                          </div>

                              <div class="form-control d-grid gap-2 centered-gridview">
                              <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                  BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                  AutoGenerateColumns="False" DataSourceID="SqlDataSource3" AllowPaging="True" AllowSorting="True">
                                  <AlternatingRowStyle BackColor="White" />
             
                     <Columns>

                          

                         <asp:BoundField DataField="IdProyecto_FK" HeaderText="IdProyecto_FK" SortExpression="IdProyecto_FK" />
                         <asp:BoundField DataField="area" HeaderText="area" SortExpression="area" />
                         <asp:BoundField DataField="UsuarioAsignado" HeaderText="UsuarioAsignado" SortExpression="UsuarioAsignado" />
                         <asp:BoundField DataField="fechaMovimiento" HeaderText="fechaMovimiento" SortExpression="fechaMovimiento" />
                         <asp:BoundField DataField="DiasDesvio" HeaderText="DiasDesvio" ReadOnly="True" SortExpression="DiasDesvio"></asp:BoundField>
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
                              <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                                   SelectCommand=" SELECT 
    asignaciones.IdProyecto_FK, 
    asignaciones.area, 
    USUARIOS.username as UsuarioAsignado, 
    movimientos_asignaciones.fechaMovimiento, 
    DATEDIFF(day, movimientos_asignaciones.fechaMovimiento +7, GETDATE()) as DiasDesvio 
FROM 
    asignaciones 
INNER JOIN 
    movimientos_asignaciones ON asignaciones.Id_Asignacion = movimientos_asignaciones.IdAsignaciones_FK 
INNER JOIN 
    USUARIOS ON USUARIOS.Id = movimientos_asignaciones.IdUserAsignado_FK
WHERE 
DATEDIFF(day, movimientos_asignaciones.fechaMovimiento, GETDATE()) >= 7 
    AND movimientos_asignaciones.estadoMovimiento = 'PENDIENTE' 
    AND movimientos_asignaciones.area_mov = 'CERTIFICACION';">
           
                              </asp:SqlDataSource>

                              


                          </div>
                      </div>
            </form>
            </div>
    </body>
    </html>
</asp:Content>
