<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="modificarProyecto.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.ABM_Proyectos.modificarProyecto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>GridView de Proyectos</title>
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            border: 1px solid black;
            padding: 8px;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Proyectos</h2>
            
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="ID_Proyecto" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowPaging="True" AllowSorting="True">
                <Columns>
                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                    <asp:BoundField DataField="ID_Proyecto" HeaderText="ID_Proyecto" ReadOnly="True" InsertVisible="False" SortExpression="ID_Proyecto"></asp:BoundField>
                    <asp:BoundField DataField="FK_ID_Usuario" HeaderText="FK_ID_Usuario" SortExpression="FK_ID_Usuario"></asp:BoundField>
                    <asp:BoundField DataField="NumeroTicket" HeaderText="NumeroTicket" SortExpression="NumeroTicket"></asp:BoundField>
                    <asp:BoundField DataField="NumeroLinea" HeaderText="NumeroLinea" SortExpression="NumeroLinea"></asp:BoundField>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion"></asp:BoundField>
                    <asp:BoundField DataField="FechaInicio" HeaderText="FechaInicio" SortExpression="FechaInicio"></asp:BoundField>
                    <asp:BoundField DataField="FechaFinalizacion" HeaderText="FechaFinalizacion" SortExpression="FechaFinalizacion"></asp:BoundField>
                    <asp:BoundField DataField="Calle" HeaderText="Calle" SortExpression="Calle"></asp:BoundField>
                    <asp:BoundField DataField="NumeroCalle" HeaderText="NumeroCalle" SortExpression="NumeroCalle"></asp:BoundField>
                    <asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad"></asp:BoundField>
                    <asp:BoundField DataField="FK_ID_Estado" HeaderText="FK_ID_Estado" SortExpression="FK_ID_Estado"></asp:BoundField>
                    <asp:BoundField DataField="FechaCreacion" HeaderText="FechaCreacion" SortExpression="FechaCreacion"></asp:BoundField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre"></asp:BoundField>
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido"></asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado"></asp:BoundField>
                </Columns>

            </asp:GridView>
            <asp:SqlDataSource runat="server" ID="SqlDataSource1"
                ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                SelectCommand="SELECT Proyecto.ID_Proyecto, Proyecto.FK_ID_Usuario, Proyecto.NumeroTicket, Proyecto.NumeroLinea,
                Proyecto.Descripcion, Proyecto.FechaInicio, Proyecto.FechaFinalizacion, Proyecto.Calle, Proyecto.NumeroCalle, 
                Proyecto.Localidad, Proyecto.FK_ID_Estado, Proyecto.FechaCreacion, USUARIOS.Nombre, USUARIOS.Apellido, EstadoProyecto.Estado FROM Proyecto
                INNER JOIN EstadoProyecto ON Proyecto.FK_ID_Estado = EstadoProyecto.Id INNER JOIN USUARIOS ON Proyecto.FK_ID_Usuario = USUARIOS.Id" 
                UpdateCommand="UPDATE Proyecto SET Descripcion= @Descripcion WHERE ID_Proyecto = @ID_Proyecto">
                <UpdateParameters>
                    <asp:Parameter Name="Descripcion"></asp:Parameter>
                    <asp:Parameter Name="ID_Proyecto"></asp:Parameter>
                </UpdateParameters>

            </asp:SqlDataSource>
                       
        </div>
    </form>
</body>
</html>

</asp:Content>
