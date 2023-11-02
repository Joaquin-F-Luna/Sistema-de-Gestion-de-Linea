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
           
    </head>

    <body>
        <div class="continer-fluid mt-1">
        <div class="continer-fluid">
            <div class="alert alert-primary" role="alert">
                <h4 class="title">Buscar un proyecto</h4>
            </div>
        </div>

            <form runat="server" class="row row-cols-lg-auto g-4 align-items-center">
              
                    <div class="col-lg-5">
                        <asp:TextBox ID="txtBuscarProyecto" runat="server" inputtype="text" class="form-control fw-bold" placeholder="Ingresa ID Proyecto" aria-label="buscar id"></asp:TextBox>
                    </div>
                    <br><br>
                <br>
                <br>
                    <div class="col-lg-5">
                        <asp:Button ID="buscarProyecto" runat="server" class="form-control btn btn-primary" Text="Buscar" OnClick="buscarProyecto_Click1" />
                    </div>
<%----------------------------------------------GRIDVIEW -------------------------------------------------------------%>
                <div class="form-control d-grid gap-2 centered-gridview">
                    <asp:GridView ID="gdListaUno" runat="server" AutoGenerateColumns="False" DataKeyNames="NumeroTicket"
                        DataSourceID="SqlDataSource2" BorderStyle="None"
                        CssClass="row-cols-auto" BackColor="White" BorderColor="#DEDFDE" 
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Editar"><ItemTemplate>

                            <asp:Button ID="btnIrAEditar" runat="server" Text="Editar" OnClick="btnIrAEditar_Click" 
                                 PostBackUrl='<%# "Editar Proyecto.aspx?Proyecto=" + Eval("Proyecto") %>' />
                             </ItemTemplate></asp:TemplateField>
                            <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" SortExpression="Proyecto" />
                            <asp:BoundField DataField="NumeroTicket" HeaderText="Ticket" SortExpression="NumeroTicket" />
                            <asp:BoundField DataField="Linea" HeaderText="Linea" SortExpression="N. Linea" />
                            <asp:BoundField DataField="Descripción" HeaderText="Descripción" SortExpression="Descripción" />
                            <asp:BoundField DataField="Inicio" HeaderText="Inicio" SortExpression="Inicio" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" />
                            <asp:BoundField DataField="Final" HeaderText="Final" SortExpression="Final" DataFormatString="&quot;{0:dd/MM/yyyy}&quot;" />
                            
                            <asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                            <asp:BoundField DataField="Creación" HeaderText="Creación" SortExpression="Creación" DataFormatString="&quot;{0:dd/MM/yyyy HH:mm:ss }&quot;" />
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
                        SelectCommand="SELECT Proyecto.ID_Proyecto AS 'Proyecto', Proyecto.NumeroTicket, Proyecto.NumeroLinea AS 'Linea', 
                        Proyecto.Descripcion AS 'Descripción', Proyecto.FechaInicio AS 'Inicio', 
                        Proyecto.FechaFinalizacion AS 'Final', Proyecto.FechaCreacion AS 'Creación', 
                        Proyecto.Localidad, EstadoProyecto.Estado, USUARIOS.Username AS 'Usuario' 
                        FROM Proyecto INNER JOIN EstadoProyecto ON Proyecto.FK_ID_Estado = EstadoProyecto.Id 
                        INNER JOIN USUARIOS ON Proyecto.FK_ID_Usuario = USUARIOS.Id 
                                        WHERE (Proyecto.NumeroTicket = @parametro)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtBuscarProyecto" Name="parametro" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
<%----------------------------------------------GRIDVIEW FIN-------------------------------------------------------------%>
                      <div class="form-control d-grid gap-2">

                          <div class="continer-fluid">
                            <div class="alert alert-primary" role="alert">
                                <h4 class="title">Todos los proyectos</h4>
                            </div>

                              <div class="form-group col-md-3">
                                  <asp:Button ID="btnExcel" CssClass="form-control btn btn-primary" runat="server" Text="Exportar" OnClick="btnExcel_Click" />
                                  </div>
                          </div>
<%----------------------------------------------FILTROS -------------------------------------------------------------%>
                            <div class="form-control d-grid gap-2">
                            <div class="row">
                            <div class="form-group col-md-3">
                            <asp:CheckBoxList runat="server" DataSourceID="SqlDataSource3" ID="cblEstado" DataTextField="Estado"></asp:CheckBoxList>
                            <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                            SelectCommand="SELECT * FROM [EstadoProyecto]">
                            </asp:SqlDataSource>
                            </div>

                            <div class="form-check col-md-3">
                            <asp:CheckBoxList runat="server" DataSourceID="SqlDataSource4" OnCheckedChanged="cblPrioridad_CheckedChanged"
                            ID="cblPrioridad" DataTextField="Descripción"></asp:CheckBoxList>
                            <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                            SelectCommand="SELECT * FROM [Prioridad]">
                            </asp:SqlDataSource>
                            </div>

                            <div class="form-check col-md-4">
                            <asp:CheckBoxList runat="server" DataSourceID="SqlDataSource5" ID="cblTipo" OnCheckedChanged="cblTipo_CheckedChanged" DataTextField="Descripción"></asp:CheckBoxList>
                            <asp:SqlDataSource runat="server" ID="SqlDataSource5" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                            SelectCommand="SELECT * FROM [TipoDeTrabajo]">
                            </asp:SqlDataSource>
                            </div>

                            
                            </div><br />
                            </div>
                            
<%----------------------------------------------FILTROS FIN -------------------------------------------------------------%>
                <div class="form-group col-md-3">
                            
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar Filtros" OnClick="btnAplicarFiltros_Click" class="btn btn-primary" style="width: 100%;" />

                            
                            </div>
<%----------------------------------------------GRIDVIEW -------------------------------------------------------------%>
                          <div class="form-control d-grid gap-2 centered-gridview">
                              <asp:GridView ID="todoslosproyectos" runat="server" OnPageIndexChanging="todoslosproyectos_PageIndexChanging" 
                                 
                                  BackColor="White" BorderColor="#DEDFDE"
                                  BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                  AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" DataKeyNames="Proyecto">
                              <AlternatingRowStyle BackColor="White" />
             
                     <Columns>

                             <asp:TemplateField HeaderText="Editar"><ItemTemplate>

                            <asp:Button ID="btnIrAEditar2" runat="server" Text="Editar" OnClick="btnIrAEditar2_Click" 
                                 PostBackUrl='<%# "Editar Proyecto.aspx?Proyecto=" + Eval("Proyecto") %>' />
                             </ItemTemplate></asp:TemplateField>

                         <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" ReadOnly="True" InsertVisible="False" SortExpression="Proyecto" />
                         <asp:BoundField DataField="Ticket" HeaderText="Ticket" SortExpression="Ticket" />
                         <asp:BoundField DataField="Linea" HeaderText="Linea" SortExpression="Linea" />
                         <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                         <asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad" />
                         <asp:BoundField DataField="Calle" HeaderText="Calle" SortExpression="Calle" />
                         <asp:BoundField DataField="Altura" HeaderText="Altura" SortExpression="Altura" />
                         <asp:BoundField DataField="Inicio" HeaderText="Inicio" SortExpression="Inicio" />
                         <asp:BoundField DataField="Final" HeaderText="Final" SortExpression="Final" />
                         <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                         <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                         <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad"></asp:BoundField>
                         <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo"></asp:BoundField>
                     </Columns>
                                  <EmptyDataTemplate>
                                    <div class="text-center">
                                        Sin resultados
                                    </div>
                                </EmptyDataTemplate>

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
                      </div>
<%----------------------------------------------GRIDVIEW FIN-------------------------------------------------------------%> 
                </form>
            </div>
    </body>
    </html>
</asp:Content>
