using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Globalization;
using static Sistema_de_Gestion_de_Linea.ABM_Proyectos.Editar_Proyecto;
using System.Web.UI;

namespace Sistema_de_Gestion_de_Linea.ABM_Proyectos
{
    public partial class AltadeProyecto : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
          
           
        }

        protected void AgregarNuevoProyecto_Click(object sender, EventArgs e)
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

            
                string Consulta = @"INSERT INTO Proyecto (FK_ID_Usuario, NumeroTicket, NumeroLinea,
                                                       Descripcion, FechaInicio, FechaFinalizacion, Calle, NumeroCalle, 
                                                         Localidad, FK_ID_Estado, FechaCreacion, idTipoTrabajo_FK, idPrioridad_FK)

                                            VALUES (@FK_ID_Usuario, @NumeroTicket, @NumeroLinea,
                                                     @Descripcion, @fechaInicio,@fechaFin, @Calle, @NumeroCalle,
                                                        @Localidad, @FK_ID_Estado, @FechaCreacion,@idTipoTrabajo_FK, @idPrioridad_FK)";
            

             using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
             {
                sqlConn.Open();
                SqlTransaction transaction = sqlConn.BeginTransaction(); // Inicia la transacción
                try
                {
                    int idUsuarioLog = idUsuarioLoggeado(Session["Usuario"].ToString());

                   
                    SqlCommand sqlcomando = new SqlCommand(Consulta, sqlConn, transaction);
                    sqlcomando.Parameters.AddWithValue("@FK_ID_Usuario", idUsuarioLog);
                    sqlcomando.Parameters.AddWithValue("@NumeroTicket", nroticket.Text);
                    sqlcomando.Parameters.AddWithValue("@NumeroLinea", nrolinea.Text);
                    sqlcomando.Parameters.AddWithValue("@Descripcion", descripcion.Text);
                    sqlcomando.Parameters.AddWithValue("@fechaInicio", fechaIn);
                    sqlcomando.Parameters.AddWithValue("@fechaFin", fechaFin);
                    sqlcomando.Parameters.AddWithValue("@Calle", calle.Text);
                    sqlcomando.Parameters.AddWithValue("@NumeroCalle", nrocalle.Text);
                    sqlcomando.Parameters.AddWithValue("@Localidad", localidad.Text);
                    sqlcomando.Parameters.AddWithValue("@FK_ID_Estado", 1);
                    sqlcomando.Parameters.AddWithValue("@idTipoTrabajo_FK", idDeTipoTrabajo(DDL_TipoTrab.Text));
                    sqlcomando.Parameters.AddWithValue("@idPrioridad_FK", idDePrioridad(DDL_tipoPriori.Text));
                    sqlcomando.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
                    sqlcomando.ExecuteNonQuery();

                    transaction.Commit();


                        string color = "green";
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('Se dio de alta con éxito!', '{color}');", true);
                        Response.AppendHeader("Refresh", "5;url=AltadeProyecto.aspx");
                        //Label1.Text = Convert.ToString(idProyecto);
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
            nrolinea.Text = "";
            nroticket.Text = "";
            nrocalle.Text = "";
            descripcion.Text = "";
            calle.Text = "";
            localidad.Text = "";
        }
            protected void CancelarNuevoProyecto_Click(object sender, EventArgs e)
        {
            
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

        public static int idDePrioridad(String prioridadElegida)
        {
            string ConsultaValida = @"SELECT idPrioridad FROM Prioridad WHERE Descripción = @Descrip";

            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlcomando = new SqlCommand(ConsultaValida, sqlConn);
                sqlcomando.Parameters.AddWithValue("Descrip", prioridadElegida);

                int idPrioridad = Convert.ToInt32(sqlcomando.ExecuteScalar());
                sqlConn.Close();

                return idPrioridad;
            }
        }

        public static int idDeTipoTrabajo(String TrabajoELegido)
        {
            string ConsultaValida = @"SELECT idTipoTrabajo FROM TipoDeTrabajo WHERE Descripción = @Descrip";

            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlcomando = new SqlCommand(ConsultaValida, sqlConn);
                sqlcomando.Parameters.AddWithValue("Descrip", TrabajoELegido);

                int idTipoTrabajo = Convert.ToInt32(sqlcomando.ExecuteScalar());
                sqlConn.Close();

                return idTipoTrabajo;
            }
        }

        protected void descripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
        


   