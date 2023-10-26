<%@ Page Title="" Language="C#" MasterPageFile="~/PMAESTRA.Master" AutoEventWireup="true" CodeBehind="ListaUsuario.aspx.cs" Inherits="Sistema_de_Gestion_de_Linea.ABM_Usuarios.ModificaciónUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Usuarios Grid</title>
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
            <h2>Usuarios</h2>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
                 <Columns>
                <asp:TemplateField HeaderText="Editar"><ItemTemplate>

                            <asp:Button ID="btnIrAEditar" runat="server" Text="Editar" OnClick="btnIrAEditar_Click" 
                                 PostBackUrl='<%# "ModificarUsuario.aspx?Id=" + Eval("Id") %>' />
                             </ItemTemplate></asp:TemplateField>
                     </Columns>
            </asp:GridView>

            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:DBSGL %>"
                SelectCommand="SELECT * FROM Usuarios">
            </asp:SqlDataSource>
                       
        </div>
    </form>
</body>
</html>
</asp:Content>
