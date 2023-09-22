using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static Sistema_de_Gestion_de_Linea.ABM_Proyectos.Editar_Proyecto;

namespace Sistema_de_Gestion_de_Linea.Asignaciones
{
    public partial class AsignacionProyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            nroticket.ReadOnly = true;
            nrolinea.ReadOnly = true;
            descripcion.ReadOnly = true;
            DDL_tipoarea.Enabled = false;
            DDL_tipoestado.Enabled = false;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            asignacionProyecto proyecto = ObtenerDatosProyectoPorId(IdProyectoTB.Text);
            if (proyecto != null)
            {
                LlenarCampos(proyecto);
            }
        }

        protected void AgregarAsignacion_Click(object sender, EventArgs e)
        {
            string ConsultaAsignacion = @"INSERT INTO Asignaciones (idProyecto_FK, Area, estado_asignacion, Id_UsuarioAsignador)
                                              VALUES (@idProyecto_FK, @area, @estado_asig, @user)";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand(ConsultaAsignacion, sqlConn);
                sqlCommand.Parameters.AddWithValue("@idProyecto_FK", ObtenerDatosProyectoPorId(IdProyectoTB.Text));
                sqlCommand.Parameters.AddWithValue("@area", DDL_tipoasignar);
                sqlCommand.Parameters.AddWithValue("@estado_asig", "Abierto");
            }
        }

        public asignacionProyecto ObtenerDatosProyectoPorId(string idProyecto)
        {
            string consulta = "SELECT NumeroTicket, NumeroLinea, Descripcion FROM Proyecto WHERE ID_Proyecto = @IdProyecto";
            string consultaAsig = "SELECT TOP 1 estado_asignacion, area " +
                                  "FROM Asignaciones WHERE idProyecto_FK = @IdProyecto " +
                                  " ORDER BY Id_Asignacion DESC;";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand(consulta, sqlConn);
                sqlCommand.Parameters.AddWithValue("@IdProyecto", idProyecto);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    asignacionProyecto proyecto = new asignacionProyecto
                    {
                        NumeroTicket = reader["NumeroTicket"].ToString(),
                        NumeroLinea = reader["NumeroLinea"].ToString(),
                        Descripcion = reader["Descripcion"].ToString()
                    };

                    reader.Close(); // Cierra el primer SqlDataReader

                    SqlCommand sqlCommand2 = new SqlCommand(consultaAsig, sqlConn);
                    sqlCommand2.Parameters.AddWithValue("@IdProyecto", idProyecto);
                    SqlDataReader reader2 = sqlCommand2.ExecuteReader();

                    if (reader2.Read())
                    {
                        proyecto.Area = reader2["area"].ToString();
                        proyecto.Estado = reader2["estado_asignacion"].ToString();

                        if (proyecto.Estado != "Asignaciones")
                        {
                            DDL_tipoestado.SelectedValue = proyecto.Estado;
                        }
                        else
                        {
                            DDL_tipoestado.SelectedValue = "Asignaciones";
                        }

                        reader2.Close(); // Cierra el segundo SqlDataReader
                    }

                    return proyecto;
                }


                return null;
            }
        }
        private void LlenarCampos(asignacionProyecto proyecto)
        {
            nroticket.Text = proyecto.NumeroTicket;
            nrolinea.Text = proyecto.NumeroLinea;
            descripcion.Text = proyecto.Descripcion;
            DDL_tipoarea.Text = proyecto.Area;


        }
        public class asignacionProyecto
        {
            public string NumeroTicket { get; set; }
            public string NumeroLinea { get; set; }
            public string Descripcion { get; set; }
            public string Estado { get; set; }
            public string Area { get; set; }


        }
    }
}