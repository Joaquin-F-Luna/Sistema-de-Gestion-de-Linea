<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="historicoDeProyectos.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.ABM_Proyectos.historidoDeProyectos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>
<html lang="es">

<head>

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>

    <!-- Calendario -->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>

    <title>Historico proyecto</title>
</head>

<body>

    <div class="container-fluid">
        <div class="alert alert-primary" role="alert">
            <h4 class="title">Historico de un Proyecto</h4>
        </div>

        <form runat="server">
            <div class="row">
                <div class="form-group col-md-5">
                    <label for="IdProyectoTB" class="fw-bold"> Buscar Proyecto</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any"
                        name="IdProyectoTB" ID="IdProyectoTB" MaxLength="6" value="" placeholder="Id Proyecto" required>
                    </asp:TextBox>
                </div>

                <div class="form-group col-md-5">
                    <br />
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Buscar Proyecto"
                        OnClick="Button1_Click" />
                </div>
            </div>

            <!-- Nro Ticket y Nro Linea -->
            <div class="row">
                <div class="form-group col-md-3">
                    <label for="nroticket" class="fw-bold">Número de Ticket</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nroticket"
                        id="txtnroticket" maxlength="6" value="" placeholder="Ing. nro de ticket"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <label for="nrolinea" class="fw-bold">Número de linea</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nrolinea"
                        id="nrolinea" maxlength="6" value="" placeholder="Ing. nro de linea"></asp:TextBox>
                </div>


                <!-- Segundo -->
                <div class="form-group col-md-3">
                    <label for="nroticket" class="fw-bold">Número de Ticket</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nroticket"
                        id="TextBox1" maxlength="6" value="" placeholder="Ing. nro de ticket">
                    </asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <label for="nrolinea" class="fw-bold">Número de linea</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nrolinea"
                        id="TextBox2" maxlength="6" value="" placeholder="Ing. nro de linea">
                    </asp:TextBox>
                </div>

            </div>

            <!-- Descripcion -->
            <div class="row">
                <div class="form-group col-md-6">
                    <label for="descripcion" class="fw-bold">Descripcion del Proyecto</label>
                         <asp:TextBox runat="server" class="form-control" name="descripcion" TextMode="MultiLine"
                        ID="descripcion" MaxLength="1000" Style="height: 100px; width: 600px" 
                        Rows="3" placeholder="Descripcion proyecto ..." required></asp:TextBox>

                <!-- Segundo -->

                <div class="form-group col-md-6">
                    <label for="descripcion" class="fw-bold">Descripcion del Proyecto</label>
                          <asp:TextBox runat="server" class="form-control" name="descripcion" TextMode="MultiLine"
                        ID="TextBox3" MaxLength="1000" Style="height: 100px; width: 600px" 
                        Rows="3" placeholder="Descripcion proyecto ..." required></asp:TextBox>
                    </asp:TextBox>
                </div>

            </div>
            <br>
            <!-- Fechas de Inicio y Fin -->
            <div class="row">

                <div class="form-group col-md-3">
                    <label for="fechainicio" class="fw-bold">Fecha Inicio</label>
                    <asp:TextBox ID="fechainicio" runat="server" CssClass="form-control" MaxLength="10"
                        placeholder="dd/mm/yyyy"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <label for="fechafin" class="fw-bold">Fecha Fin</label>
                    <asp:TextBox ID="fechafin" runat="server" CssClass="form-control" MaxLength="10"
                        placeholder="dd/mm/yyyy"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <label for="fechainicio" class="fw-bold">Fecha Inicio</label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" MaxLength="10"
                        placeholder="dd/mm/yyyy"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <label for="fechafin" class="fw-bold">Fecha Fin</label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" MaxLength="10"
                        placeholder="dd/mm/yyyy"></asp:TextBox>
                </div>

            </div>

            <!-- Calle, Nro Calle y Localidad -->
            <div class="row">
                <div class="form-group col-md-4">
                    <label for="calle" class="fw-bold">Calle</label>
                    <asp:TextBox runat="server" type="text" class="form-control" name="calle" id="calle" maxlength="50"
                        value="" placeholder="Nombre de calle"></asp:TextBox>
                </div>

                <div class="form-group col-md-2">
                    <label for="nrocalle" class="fw-bold">Número</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nrocalle"
                        id="nrocalle" maxlength="6" value="" placeholder="Ing. nro de calle"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <label for="calle" class="fw-bold">Calle</label>
                    <asp:TextBox runat="server" type="text" class="form-control" name="calle" id="TextBox6"
                        maxlength="50" value="" placeholder="Nombre de calle"></asp:TextBox>
                </div>

                <div class="form-group col-md-2">
                    <label for="nrocalle" class="fw-bold">Número</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" name="nrocalle"
                        id="TextBox7" maxlength="6" value="" placeholder="Ing. nro de calle">
                    </asp:TextBox>
                </div>

            </div>
            <div class="row">
                <div class="form-group col-md-6">
                    <label for="localidad" class="fw-bold">Localidad</label>
                    <asp:TextBox runat="server" type="text" class="form-control" name="localidad" id="localidad"
                        maxlength="50" value="" placeholder="Localidad..."></asp:TextBox>
                </div>
                <div class="form-group col-md-6">

                    <label for="localidad" class="fw-bold">Localidad</label>
                    <asp:TextBox runat="server" type="text" class="form-control" name="localidad" id="TextBox8"
                        maxlength="50" value="" placeholder="Localidad..."></asp:TextBox>
                </div>
            </div>
            <!-- Tipo de Trabajo y Tipo de Prioridad -->
            <div class="row">
                <div class="form-group col-md-3 fw-bold">
                    <label for="tipotrabajo" class="form-label">Tipo de Trabajo</label>
                    <asp:DropDownList ID="DDL_TipoTrab" runat="server" BackColor="ButtonShadow" Font-Size="Large"
                        CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" ForeColor="#333333">
                        <asp:ListItem Value="1" Text="Ampliacion"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Caja Nueva (COB)"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Caja IaaS"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group col-md-3 fw-bold">
                    <label for="prioridad" class="form-label">Tipo de prioridad</label>
                    <asp:DropDownList ID="DDL_tipoPriori" runat="server" BackColor="ButtonShadow" Font-Size="Large"
                        CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" ForeColor="#333333">
                        <asp:ListItem Value="1" Text="Baja"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Media"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Alta"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group col-md-3 fw-bold">
                    <label for="tipotrabajo" class="form-label">Tipo de Trabajo</label>
                    <asp:DropDownList ID="DropDownList1" runat="server" BackColor="ButtonShadow"
                        Font-Size="Large" CssClass="form-select" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" ForeColor="#333333">
                        <asp:ListItem Value="1" Text="Ampliacion"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Caja Nueva (COB)"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Caja IaaS"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group col-md-3 fw-bold">
                    <label for="prioridad" class="form-label">Tipo de prioridad</label>
                    <asp:DropDownList ID="DropDownList2" runat="server" BackColor="ButtonShadow"
                        Font-Size="Large" CssClass="form-select" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" ForeColor="#333333">
                        <asp:ListItem Value="1" Text="Baja"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Media"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Alta"></asp:ListItem>
                    </asp:DropDownList>
                </div>



            </div>
        </form>
    </div>

</body>

</html>
</asp:Content>
