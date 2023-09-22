using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;
using Sistema_de_Gestion_de_Linea;

using static Sistema_de_Gestion_de_Linea.ABM_Proyectos.Editar_Proyecto;

namespace Sistema_de_Gestion_de_Linea.ABM_Proyectos
{
    public partial class Editar_Proyecto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Label1.Text = Session["usuario"].ToString();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            //Obtener los datos del priyecto deseado

            Proyecto proyecto = ObtenerDatosProyectoPorId(IdProyectoTB.Text);
            if (proyecto != null)
            {
                LlenarCampos(proyecto);
            }
        }
        private void LlenarCampos(Proyecto proyecto)
        {
            nroticket.Text = proyecto.NumeroTicket;
            nrolinea.Text = proyecto.NumeroLinea;
            descripcion.Text = proyecto.Descripcion;
            fechainicio.Text = proyecto.FechaInicio.ToString("dd/MM/yyyy");
            fechafin.Text = proyecto.FechaFinalizacion.ToString("dd/MM/yyyy");
            calle.Text = proyecto.Calle;
            nrocalle.Text = proyecto.NumeroCalle;
            localidad.Text = proyecto.Localidad;
            DDL_TipoTrab.Text = proyecto.IdTipoDeTrabajo;
            DDL_tipoPriori.Text = proyecto.IdTipoPrioridad;
        }
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

        public class ProyectoJson
        {
            public string NumeroTicket { get; set; }
            public string NumeroLinea { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFinalizacion { get; set; }
            public string Calle { get; set; }
            public string NumeroCalle { get; set; }
            public string Localidad { get; set; }

            public string TipoDeTrabajo { get; set; }

            public string TipoDePrioridad { get; set; }
        }

        protected void btn_editar(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaIn = DateTime.ParseExact(fechainicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                DateTime fechaFin = DateTime.ParseExact(fechafin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                Proyecto proyecto = ObtenerDatosProyectoPorId(IdProyectoTB.Text);

                ProyectoJson proyectoActual = new ProyectoJson
                {
                    NumeroTicket = proyecto.NumeroTicket,
                    NumeroLinea = proyecto.NumeroLinea,
                    Descripcion = proyecto.Descripcion,
                    FechaInicio = proyecto.FechaInicio,
                    FechaFinalizacion = proyecto.FechaFinalizacion,
                    Calle = proyecto.Calle,
                    NumeroCalle = proyecto.NumeroCalle,
                    Localidad = proyecto.Localidad,
                    TipoDeTrabajo = proyecto.IdTipoDeTrabajo,
                    TipoDePrioridad = proyecto.IdTipoPrioridad

                };

                string json = JsonConvert.SerializeObject(proyectoActual);


                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
                {
                    sqlConn.Open();
                    string consulta = @"UPDATE Proyecto 
                                        SET FK_ID_Usuario = @FK_ID_Usuario, 
                                            NumeroTicket = @NumeroTicket, 
                                            NumeroLinea = @NumeroLinea, 
                                            Descripcion = @Descripcion, 
                                            FechaInicio = @fechaInicio, 
                                            FechaFinalizacion = @fechaFin, 
                                            Calle = @Calle, 
                                            NumeroCalle = @NumeroCalle, 
                                            Localidad = @Localidad,
                                            idTipoTrabajo_FK = @idTipoTrabajo_FK,
                                            idPrioridad_FK = @idPrioridad
                                        WHERE Id_Proyecto = @Id_Proyecto";

                    int trabajo = Convert.ToInt32(DDL_TipoTrab.SelectedValue);
                    int prioridad = Convert.ToInt32(DDL_tipoPriori.SelectedValue);

                    SqlCommand sqlcomando = new SqlCommand(consulta, sqlConn);
                    sqlcomando.Parameters.AddWithValue("@FK_ID_Usuario", idUsuarioLoggeado(Session["Usuario"].ToString()));
                    sqlcomando.Parameters.AddWithValue("@NumeroTicket", nroticket.Text);
                    sqlcomando.Parameters.AddWithValue("@NumeroLinea", nrolinea.Text);
                    sqlcomando.Parameters.AddWithValue("@Descripcion", descripcion.Text);
                    sqlcomando.Parameters.AddWithValue("@fechaInicio", fechaIn);
                    sqlcomando.Parameters.AddWithValue("@fechaFin", fechaFin);
                    sqlcomando.Parameters.AddWithValue("@Calle", calle.Text);
                    sqlcomando.Parameters.AddWithValue("@NumeroCalle", nrocalle.Text);
                    sqlcomando.Parameters.AddWithValue("@Localidad", localidad.Text);
                    sqlcomando.Parameters.AddWithValue("@idTipoTrabajo_FK", trabajo);
                    sqlcomando.Parameters.AddWithValue("@idPrioridad", prioridad);
                    sqlcomando.Parameters.AddWithValue("@Id_Proyecto", IdProyectoTB.Text);
                   
                    sqlcomando.ExecuteNonQuery();

                    string consultaMovimiento = "INSERT INTO historicoProyectos (idProyecto_FK, idUsuario_FK, insercionFecha, Data) " +
                        "VALUES (@Fk_Id_Proyecto, @Id_Usuario_Movimiento, @InserciónFecha, @Data)";

                    SqlCommand sqlCommandMovimiento = new SqlCommand(consultaMovimiento, sqlConn);
                    sqlCommandMovimiento.Parameters.AddWithValue("@Fk_Id_Proyecto", IdProyectoTB.Text); // Reemplaza esto con el método que obtenga el ID del proyecto
                    sqlCommandMovimiento.Parameters.AddWithValue("@Id_Usuario_Movimiento", idUsuarioLoggeado(Session["Usuario"].ToString()));
                    sqlCommandMovimiento.Parameters.AddWithValue("@InserciónFecha", DateTime.Now);
                    sqlCommandMovimiento.Parameters.AddWithValue("@Data", json);

                    sqlCommandMovimiento.ExecuteNonQuery();

                    notificacion.ForeColor = Color.Green;
                    notificacion.Text = "Se actualizó con éxito!";
                }
            }
            catch (Exception ex)
            {
                notificacion.ForeColor = Color.Red;
                notificacion.Text = "Error: " + ex.Message;
               
                Label2.ForeColor = Color.Red;
                Label2.Text = "id tipo trabajo:   " + DDL_TipoTrab.SelectedValue;
            }

            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            nrolinea.Text = "";
            nroticket.Text = "";
            nrocalle.Text = "";
            descripcion.Text = "";
            calle.Text = "";
            localidad.Text = "";
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


        public static int idUsuarioLoggeado(String usuarioIniciado)
        {
            string ConsultaValida = @"SELECT Id FROM USUARIOS WHERE Username = @user";

            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlcomando = new SqlCommand(ConsultaValida, sqlConn);
                sqlcomando.Parameters.AddWithValue("user", usuarioIniciado);

                int idUsuario = Convert.ToInt32(sqlcomando.ExecuteScalar());
                sqlConn.Close();

                return idUsuario;
            }
        }

    }
}