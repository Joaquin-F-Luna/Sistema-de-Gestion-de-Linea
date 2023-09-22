using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Sistema_de_Gestion_de_Linea.ABM_Proyectos.Editar_Proyecto;

namespace Sistema_de_Gestion_de_Linea.ABM_Proyectos
{
    public partial class historidoDeProyectos : System.Web.UI.Page
    {
        //public static int idDelProyecto(String idEnString)
        //{
        //    string ConsultaValida = @"SELECT ID_Proyecto FROM Proyecto WHERE ID_Proyecto = @id";

        //    using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
        //    {
        //        sqlConn.Open();
        //        SqlCommand sqlcomando = new SqlCommand(ConsultaValida, sqlConn);
        //        sqlcomando.Parameters.AddWithValue("id", idEnString);

        //        int idProyecto = Convert.ToInt32(sqlcomando.ExecuteScalar());
        //        sqlConn.Close();

        //        return idProyecto;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            txtnroticket.ReadOnly = true;
            nrolinea.ReadOnly = true;
            descripcion.ReadOnly = true;
            calle.ReadOnly = true;
            nrocalle.ReadOnly = true;
            localidad.ReadOnly = true;
            DDL_TipoTrab.Enabled = false;
            DDL_tipoPriori.Enabled = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string consulta = "SELECT Data FROM historicoProyectos WHERE idProyecto_FK = @IdProyecto";
            
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand(consulta, sqlConn);
                sqlCommand.Parameters.AddWithValue("@IdProyecto", IdProyectoTB.Text);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    string json = reader["Data"].ToString();
                    ProyectoJson proyectoRecuperado = JsonConvert.DeserializeObject<ProyectoJson>(json);

                    // Ahora tienes el objeto ProyectoJson recuperado y puedes usar sus propiedades
                    // por ejemplo:
                    txtnroticket.Text = proyectoRecuperado.NumeroTicket;
                    nrolinea.Text = proyectoRecuperado.NumeroLinea;
                    descripcion.Text = proyectoRecuperado.Descripcion;
                    calle.Text = proyectoRecuperado.Calle;
                    nrocalle.Text = proyectoRecuperado.NumeroCalle;
                    localidad.Text = proyectoRecuperado.Localidad;
                    // y así sucesivamente

                    // Luego puedes usar estos valores en tu formulario como lo necesites
                }


            }
        }
    }
}