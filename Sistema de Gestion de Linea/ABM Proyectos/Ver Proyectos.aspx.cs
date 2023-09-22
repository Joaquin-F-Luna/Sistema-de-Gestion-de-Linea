using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Sistema_de_Gestion_de_Linea.ABM_Proyectos
{
    public partial class Ver_Proyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buscarProyecto_Click(object sender, EventArgs e)
        {

        }

        protected void buscarProyecto_Click1(object sender, EventArgs e)
        {

        }

        protected void buscartodos_Click(object sender, EventArgs e)
        {
            botonBuscar();
            
        }

        private void botonBuscar()
        {
            string sqlBuscar = @"SELECT Proyecto.NumeroTicket AS 'N. Ticket', Proyecto.NumeroLinea AS 'N. Linea', Proyecto.Descripcion, 
                                Proyecto.Localidad, Proyecto.FechaInicio AS 'F. Inicio', Proyecto.FechaFinalizacion AS 'F.Termino', 
                                Proyecto.Calle, Proyecto.NumeroCalle AS 'N. Calle', EstadoProyecto.Estado, USUARIOS.Username AS 'Nombre de Usuario'
                                FROM Proyecto
                                INNER JOIN EstadoProyecto ON Proyecto.FK_ID_Estado = EstadoProyecto.Id 
                                INNER JOIN USUARIOS ON Proyecto.FK_ID_Usuario = USUARIOS.Id";

            /*"SELECT Proyecto.ID_Proyecto, Proyecto.NumeroTicket, Proyecto.NumeroLinea, Proyecto.Descripcion, 
                                Proyecto.Localidad, Proyecto.FechaInicio,Proyecto.FechaFinalizacion, Proyecto.Calle, Proyecto.NumeroCalle,
                                EstadoProyecto.Estado, USUARIOS.Id, USUARIOS.Username FROM Proyecto
                                INNER JOIN EstadoProyecto ON Proyecto.FK_ID_Estado = EstadoProyecto.Id 
                                INNER JOIN USUARIOS ON Proyecto.FK_ID_Usuario = USUARIOS.Id";*/

            /*if (DropDownList1.SelectedValue.ToString() == "username")
            {
                sqlBuscar += "AND username LIKE @entrada";
            }
            if (DropDownList1.SelectedValue.ToString() == "Id")
            {
                sqlBuscar += "AND Id LIKE @entrada";
            }*/

            using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                connSGL.Open();
                SqlCommand cmd = new SqlCommand(sqlBuscar, connSGL);
                /*cmd.Parameters.AddWithValue("entrada",txtBuscar.Text);*/

                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                gdtodosproyectos.DataSource = dt1;
                gdtodosproyectos.DataBind();
                connSGL.Close();
            }

        }
    }
}