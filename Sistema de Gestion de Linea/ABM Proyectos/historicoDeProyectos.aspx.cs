using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public class Proyecto
        {
            public string NumeroTicket { get; set; }
            public string NumeroLinea { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFinalizacion { get; set; }
            public string Calle { get; set; }
            public string NumeroCalle { get; set; }
            public string Localidad { get; set; }
            public string IdTipoDeTrabajo { get; set; }
            public string IdTipoPrioridad { get; set; }

        }
        public Proyecto ObtenerDatosProyectoPorId(string idProyecto)
        {
            string consulta = "SELECT NumeroTicket, NumeroLinea, Descripcion, FechaInicio, FechaFinalizacion, " +
                "Calle, NumeroCalle, Localidad, idTipoTrabajo_FK, idPrioridad_FK " +
                "FROM Proyecto WHERE ID_Proyecto = @IdProyecto";

            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand(consulta, sqlConn);
                sqlCommand.Parameters.AddWithValue("@IdProyecto", idProyecto);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    Proyecto proyecto = new Proyecto
                    {
                        NumeroTicket = reader["NumeroTicket"].ToString(),
                        NumeroLinea = reader["NumeroLinea"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                        FechaFinalizacion = Convert.ToDateTime(reader["FechaFinalizacion"]),
                        Calle = reader["Calle"].ToString(),
                        NumeroCalle = reader["NumeroCalle"].ToString(),
                        Localidad = reader["Localidad"].ToString(),
                        IdTipoDeTrabajo = reader["idTipoTrabajo_FK"].ToString(),
                        IdTipoPrioridad = reader["idPrioridad_FK"].ToString()
                    };

                    return proyecto;
                }

                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            nroticket.ReadOnly = true;
            nrolinea.ReadOnly = true;
            descripcion.ReadOnly = true;
            calle.ReadOnly = true;
            nrocalle.ReadOnly = true;
            localidad.ReadOnly = true;
            DDL_TipoTrab.Enabled = false;
            DDL_tipoPriori.Enabled = false;
            fechainicio.Enabled = false;
            fechafin.Enabled = false;

            nroticketActual.ReadOnly = true;
            nrolineaActual.ReadOnly = true;
            descripcionActual.ReadOnly = true;
            calleActual.ReadOnly = true;
            nrocalleActual.ReadOnly = true;
            localidadActual.ReadOnly = true;
            tipotrabajoActual.Enabled = false;
            prioridadActual.Enabled = false;
            fechainicioActual.Enabled = false;
            fechafinActual.Enabled = false;
        }
        private void LlenarCampos(Proyecto proyecto)
        {
            nroticketActual.Text = proyecto.NumeroTicket;
            nrolineaActual.Text = proyecto.NumeroLinea;
            descripcionActual.Text = proyecto.Descripcion;
            fechainicioActual.Text = proyecto.FechaInicio.ToString("dd/MM/yyyy");
            fechafinActual.Text = proyecto.FechaFinalizacion.ToString("dd/MM/yyyy");
            calleActual.Text = proyecto.Calle;
            nrocalleActual.Text = proyecto.NumeroCalle;
            localidadActual.Text = proyecto.Localidad;
            tipotrabajoActual.Text = proyecto.IdTipoDeTrabajo;
            prioridadActual.Text = proyecto.IdTipoPrioridad;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {




            Proyecto proyecto = ObtenerDatosProyectoPorId(IdProyectoTB.Text);
            if (proyecto != null)
            {
                LlenarCampos(proyecto);
            }

            string consultaHistorico = "SELECT TOP 1 U.Username, HP.insercionFecha, Data " +
                "FROM historicoProyectos HP " +
                "LEFT JOIN USUARIOS U " +
                "ON HP.idUsuario_FK = U.Id " +
                "WHERE idProyecto_FK = @IdProyecto " +
                "ORDER BY HP.idMovProyecto DESC ";


            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {

                sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand(consultaHistorico, sqlConn);
                sqlCommand.Parameters.AddWithValue("@IdProyecto", IdProyectoTB.Text);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    lbUsuario.Text = "Último editor: " + reader["Username"].ToString();
                    lbActualizacion.Text = "Fecha Actualización: " + reader["insercionFecha"].ToString();

                    string json = reader["Data"].ToString();
                    ProyectoJson proyectoRecuperado = JsonConvert.DeserializeObject<ProyectoJson>(json);

                   
                    nroticket.Text = proyectoRecuperado.NumeroTicket;
                    nrolinea.Text = proyectoRecuperado.NumeroLinea;
                    descripcion.Text = proyectoRecuperado.Descripcion;
                    calle.Text = proyectoRecuperado.Calle;
                    nrocalle.Text = proyectoRecuperado.NumeroCalle;
                    localidad.Text = proyectoRecuperado.Localidad;
                    fechainicio.Text = proyectoRecuperado.FechaInicio;
                    fechafin.Text = proyectoRecuperado.FechaFinalizacion;
                    DDL_tipoPriori.Text = proyectoRecuperado.TipoDePrioridad.ToString();
                    DDL_TipoTrab.Text = proyectoRecuperado.TipoDeTrabajo.ToString();



                }
                sqlConn.Close();

                // y así sucesivamente

                // Luego puedes usar estos valores en tu formulario como lo necesites
            }

         

            string sqlBuscar = @"SELECT Data 
                     FROM historicoProyectos HP
                     WHERE HP.idProyecto_FK = @idproyecto";

            using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                connSGL.Open();
                SqlCommand cmd = new SqlCommand(sqlBuscar, connSGL);
                cmd.Parameters.AddWithValue("@idproyecto", IdProyectoTB.Text);

                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                List<ProyectoJson> listaProyectos = new List<ProyectoJson>();

                foreach (DataRow row in dt1.Rows)
                {
                    string json = row["Data"].ToString();
                    ProyectoJson proyectoRecuperado = JsonConvert.DeserializeObject<ProyectoJson>(json);
                    listaProyectos.Add(proyectoRecuperado);
                }

                // Combinar las listas
                List<ProyectoJson> listaFinal = listaProyectos;

                gdtodosproyectos.DataSource = listaFinal;
                gdtodosproyectos.DataBind();

                connSGL.Close();
            }


        }

    }
}
