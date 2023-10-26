<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="Editar Proyecto.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.ABM_Proyectos.Editar_Proyecto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Edit proyecto</title>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>

    <!-- Necesario para agregar los calendarios -->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>

    
        <script>
            $.datepicker.regional['es'] = {
                closeText: 'Cerrar',
                prevText: '< Ant',
                nextText: 'Sig >',
                currentText: 'Hoy',
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };

            $.datepicker.setDefaults($.datepicker.regional['es']);
            $(function () {
                $("#<%= fechainicio.ClientID %>").datepicker();
                $("#<%= fechafin.ClientID %>").datepicker();
            });
        </script>

     <style>
        body {
            margin: 20px; /* Puedes ajustar el margen según tus necesidades */
        }
    </style>

    <!-- ---------------------------------------------------------------------------- -->
    <style>
        .notificacion-float {
            position: fixed;
            bottom: 50%;
            left: 50%;
            transform: translate(-50%, 50%);
            padding: 10px;
            background-color: #ffffff;
            border: 1px solid #cccccc;
            border-radius: 5px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            z-index: 1000;
            display: none;
        }
    </style>

    <script>
        function mostrarNotificacion(mensaje, color) {
            var notificacion = document.getElementById('notificacion');
            notificacion.innerText = mensaje;
            notificacion.style.color = color;  // Establece el color del texto

            notificacion.style.display = 'block';

            setTimeout(function () {
                notificacion.style.display = 'none';
            }, 5000);
        }
    </script>
    <title>Edit proyecto</title>
</head>

<body>
    <div class="container mx-auto">
        <div class="alert alert-primary" role="alert">
            <h4 class="title">Editar un Proyecto</h4>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>

        <form runat="server" >
            
               <div class="row">
            <div class="form-group col-md-5">
                    <label for="Id Pryecto" class="fw-bold"> Buscar Proyecto</label>
                    <asp:TextBox runat="server" type="number"  min="1" class="form-control" step="any"
                        name="IdProyectoTB" id="IdProyectoTB" maxlength="6" 
                        value="" placeholder="Id Proyecto" required></asp:TextBox>
                </div>

                    <div class="form-group col-md-5">
                        <br>
                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Buscar Proyecto" OnClick="Button1_Click" />
                        </div>
                    </div>
            <div class="row">
                <!-- Nro Ticket -->
                <div class="form-group col-md-5">
                    <label for="nroticket" class="fw-bold">Número de Ticket</label>
                    <asp:TextBox runat="server" type="number"  min="1" class="form-control" step="any"
                        name="nroticket" id="nroticket" maxlength="6" 
                        value="" placeholder="Ing. nro de ticket" ></asp:TextBox>
                </div>

                <!-- Nro linea -->
                <div class="form-group col-md-5">
                    <label for="nrolinea" class="fw-bold">Número de linea</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control"
                        step="any" name="nrolinea" id="nrolinea" maxlength="6" 
                        value="" placeholder="Ing. nro de linea" ></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <!-- descripcion -->
                <div class="form-group col-md-12">
                    <label for="descripcion" class="fw-bold">Descripcion del Proyecto</label>
                          <asp:TextBox runat="server" class="form-control" name="descripcion" TextMode="MultiLine"
                        ID="descripcion" MaxLength="1000" Style="height: 100px; width: 600px" 
                        Rows="3" placeholder="Descripcion proyecto ..."></asp:TextBox>
                </div> 
                 </div>
            
            <br>
          <br>
            <div class="row">
                <!-- FechaInicio -->
                <div class="form-group col-md-3">
                    <label for="fechainicio" class="fw-bold">Fecha Inicio Proyecto</label>
                    <%--<input type="text" class="form-control" name="fechainicio" id="fechainicio" maxlength="10"  value="" placeholder="" required>--%>
                     <asp:TextBox ID="fechainicio" runat="server" CssClass="form-control" MaxLength="10" placeholder="dd/mm/yyyy" ></asp:TextBox>
               <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="fechainicio" Format="dd/MM/yyyy" PopupPosition="TopRight" />--%>
                </div>
                <!-- FechaFin -->
                <div class="form-group col-md-3">
                    <label for="fechafin" class="fw-bold">Fecha Fin Proyecto</label>
                    <%--<input type="text" class="form-control" name="fechafin" id="fechafin" maxlength="10"  value="" placeholder="" required>--%>
                    <asp:TextBox ID="fechafin" runat="server" CssClass="form-control" MaxLength="10" placeholder="dd/mm/yyyy" ></asp:TextBox>
                <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="fechafin" Format="dd/MM/yyyy" PopupPosition="TopRight" />--%>
                </div>
            </div>

            <div class="row">
                <!-- Calle -->
                <div class="form-group col-md-5">
                    <label for="calle" class="fw-bold">Calle</label>
                    <asp:TextBox runat="server" type="text" class="form-control" name="calle" 
                        id="calle" maxlength="50"  value="" placeholder="Nombre de calle" ></asp:TextBox>
                </div>

                <!-- Nro calle -->
                <div class="form-group col-md-2">
                    <label for="nrocalle" class="fw-bold">Número</label>
                    <asp:TextBox runat="server" type="number" min="1" class="form-control" step="any" 
                        name="nrocalle" id="nrocalle" maxlength="6"  value="" 
                        placeholder="Ing. nro de calle" ></asp:TextBox>
                </div>

                <!-- localidad -->
                <div class="form-group col-md-5">
                    <label for="localidad" class="fw-bold">Localidad</label>
                    <asp:TextBox runat="server" type="text" class="form-control"
                        name="localidad" id="localidad" maxlength="50"  value="" 
                        placeholder="Localidad..." ></asp:TextBox>
                </div>
            </div>

            <div class="row">

               <!-- tipo trabajo -->


                 <div class="form-group col-md-4 fw-bold">
              <label for="tipotrabajo" class="form-label">Tipo de Trabajo  </label>
              <asp:DropDownList ID="DDL_TipoTrab" runat="server" BackColor="ButtonShadow" Font-Size="Large" 
                  CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" ForeColor="#333333" >
                  <asp:ListItem Value="1" Text="Ampliacion"></asp:ListItem>
                  <asp:ListItem Value="2" Text="Caja Nueva (COB)"></asp:ListItem>
                  <asp:ListItem Value="3" Text="Caja IaaS"></asp:ListItem>
            
                  </asp:DropDownList>
              </div>

                <!-- tipo prioridad -->
                           <div class="form-group col-md-4 fw-bold">
              <label for="prioridad" class="form-label">Tipo de prioridad  </label>
              <asp:DropDownList ID="DDL_tipoPriori" runat="server" BackColor="ButtonShadow" Font-Size="Large" 
                  CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" ForeColor="#333333" >
                  <asp:ListItem Value="1" Text="Baja"></asp:ListItem>
                  <asp:ListItem Value="2" Text="Media"></asp:ListItem>
                  <asp:ListItem Value="3" Text="Alta"></asp:ListItem>
            
                  </asp:DropDownList>
                               </div>
  
                 <div class="form-group col-md-4 fw-bold">
                 <label for="ddlEstado" class="form-label">Estado:  </label>
              <asp:DropDownList ID="ddl_estado" runat="server" BackColor="ButtonShadow" Font-Size="Large" DataSourceID="SqlDataSource1"  DataTextField="Estado" DataValueField="Id"
                   CssClass="form-select" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" ForeColor="#333333" >
                          
                  </asp:DropDownList>
                      <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                SelectCommand="SELECT * FROM EstadoProyecto">
            </asp:SqlDataSource>
                     </div>

           </div>
            <br>
 


            <br>
                  <div class="row">

                 </div>


                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Editar el Proyecto" OnClick="btn_editar"/>

            <div id="notificacion" class="notificacion-float">
    <asp:Label runat="server" ID="notificacion" CssClass="control-label-sm-2"></asp:Label>
</div>

                <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="" CssClass="control-label-sm-2"></asp:Label>
                    </div>
        </form>
        
    </div>
   


</body>  

</html>

</asp:Content>
