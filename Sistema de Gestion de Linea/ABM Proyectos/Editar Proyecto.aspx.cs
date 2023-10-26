using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
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
            if (!IsPostBack)
            {
                if (Request.QueryString["Proyecto"] != null)
                {
                    string proyecto = Request.QueryString["Proyecto"];
                    IdProyectoTB.Text = proyecto; // Asigna el valor a un Label


                }

                Proyecto proyectoA = ObtenerDatosProyectoPorId(IdProyectoTB.Text);
                if (proyectoA != null)
                {
                    LlenarCampos(proyectoA);
                }
            }
            
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
            ddl_estado.Text = proyecto.IdEstado;
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

            public string IdEstado { get; set; }
            public string IdTipoDeTrabajo { get; set; }
            public string IdTipoPrioridad { get; set; }

        }

        public class ProyectoJson
        {
            public string NumeroTicket { get; set; }
            public string NumeroLinea { get; set; }
            public string Descripcion { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFinalizacion { get; set; }
            public string Calle { get; set; }
            public string NumeroCalle { get; set; }
            public string Localidad { get; set; }

            public string Estado { get; set; }
            public string TipoDeTrabajo { get; set; }

            public string TipoDePrioridad { get; set; }

            public string Actualizacion { get; set; }
            public string UsuarioEditor { get; set; }

        }

        protected void btn_editar(object sender, EventArgs e)
        {
            DateTime fechaIn = DateTime.ParseExact(fechainicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
            DateTime fechaFin = DateTime.ParseExact(fechafin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;

          if (fechaIn > fechaFin)
          {
          
                string color = "red";
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('La fecha de inicio debe ser anterior.', '{color}');", true);

          }
          else 
          { 
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
                {
                    sqlConn.Open();
                    int trabajo = Convert.ToInt32(DDL_TipoTrab.SelectedValue);
                    int prioridad = Convert.ToInt32(DDL_tipoPriori.SelectedValue);
                    int estado = Convert.ToInt32(ddl_estado.SelectedValue);

                    SqlTransaction transaction = sqlConn.BeginTransaction(); // Inicia la transacción

                   

                 
                    Proyecto proyecto = ObtenerDatosProyectoPorId(IdProyectoTB.Text);
                    string fechaInicioJson = proyecto.FechaInicio.ToString("dd/MM/yyyy");
                    string fechaFinJson = proyecto.FechaFinalizacion.ToString("dd/MM/yyyy");

                    ProyectoJson proyectoActual = new ProyectoJson
                    {
                       
                        NumeroTicket = proyecto.NumeroTicket,
                        NumeroLinea = proyecto.NumeroLinea,
                        Descripcion = proyecto.Descripcion,
                        FechaInicio = fechaInicioJson,
                        FechaFinalizacion = fechaFinJson,
                        Calle = proyecto.Calle,
                        NumeroCalle = proyecto.NumeroCalle,
                        Localidad = proyecto.Localidad,
                        Estado = proyecto.IdEstado,
                        TipoDeTrabajo = proyecto.IdTipoDeTrabajo,
                        TipoDePrioridad = proyecto.IdTipoPrioridad,
                        Actualizacion = DateTime.Now.ToString("dd/MM/HH:mm:ss"),
                        UsuarioEditor = Session["usuario"].ToString()
                    };
                     string json = JsonConvert.SerializeObject(proyectoActual);

                    try
                    {
                        string consultaMovimiento = "INSERT INTO historicoProyectos (idProyecto_FK, idUsuario_FK, insercionFecha, Data) " +
                             "VALUES (@Fk_Id_Proyecto, @Id_Usuario_Movimiento, @InserciónFecha, @Data)";

                        SqlCommand sqlCommandMovimiento = new SqlCommand(consultaMovimiento, sqlConn, transaction);
                        sqlCommandMovimiento.Parameters.AddWithValue("@Fk_Id_Proyecto", IdProyectoTB.Text); // Reemplaza esto con el método que obtenga el ID del proyecto
                        sqlCommandMovimiento.Parameters.AddWithValue("@Id_Usuario_Movimiento", idUsuarioLoggeado(Session["Usuario"].ToString()));
                        sqlCommandMovimiento.Parameters.AddWithValue("@InserciónFecha", DateTime.Now);
                        sqlCommandMovimiento.Parameters.AddWithValue("@Data", json);

                        sqlCommandMovimiento.ExecuteNonQuery();


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
                                            FK_ID_Estado = @estado,
                                            idTipoTrabajo_FK = @idTipoTrabajo_FK,
                                            idPrioridad_FK = @idPrioridad
                                        WHERE Id_Proyecto = @Id_Proyecto";



                        SqlCommand sqlcomando = new SqlCommand(consulta, sqlConn, transaction);
                        sqlcomando.Parameters.AddWithValue("@FK_ID_Usuario", idUsuarioLoggeado(Session["Usuario"].ToString()));
                        sqlcomando.Parameters.AddWithValue("@NumeroTicket", nroticket.Text);
                        sqlcomando.Parameters.AddWithValue("@NumeroLinea", nrolinea.Text);
                        sqlcomando.Parameters.AddWithValue("@Descripcion", descripcion.Text);
                        sqlcomando.Parameters.AddWithValue("@fechaInicio", fechaIn);
                        sqlcomando.Parameters.AddWithValue("@fechaFin", fechaFin);
                        sqlcomando.Parameters.AddWithValue("@Calle", calle.Text);
                        sqlcomando.Parameters.AddWithValue("@NumeroCalle", nrocalle.Text);
                        sqlcomando.Parameters.AddWithValue("@Localidad", localidad.Text);
                        sqlcomando.Parameters.AddWithValue("@estado", estado);
                        sqlcomando.Parameters.AddWithValue("@idTipoTrabajo_FK", trabajo);
                        sqlcomando.Parameters.AddWithValue("@idPrioridad", prioridad);
                        sqlcomando.Parameters.AddWithValue("@Id_Proyecto", IdProyectoTB.Text);

                        sqlcomando.ExecuteNonQuery();

                        
                        transaction.Commit();
                            string color = "green";
                            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('Se actualizó con éxito!', '{color}');", true);

                            Response.AppendHeader("Refresh", "5;url=Editar Proyecto.aspx");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                            string color = "red";
                            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('Error: {ex.Message}', '{color}');", true);

                        }
                        finally 
                    {
                        sqlConn.Close(); 
                    }
                   

                 
                }
            }
            catch (Exception ex)
            {
                    string color = "red";
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('Error: {ex.Message}', '{color}');", true);

                    Label2.ForeColor = Color.Red;
                Label2.Text = "id tipo trabajo:   " + DDL_TipoTrab.SelectedValue;
            }
         

            LimpiarCampos();
          }
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