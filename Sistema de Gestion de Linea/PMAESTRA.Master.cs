using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Gestion_de_Linea
{
    public partial class PMAESTRA : System.Web.UI.MasterPage
    {
        protected bool crearUsuarios = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbUserIniciado.Text = "Bienvenido: " + Session["usuario"].ToString() + "  " + "Tu id es: " + idUsuarioLoggeado(Session["usuario"].ToString());

            if (Session["rol"].ToString() != "Administrador") 
            {
                crearUsuarios = false;
            }

            if (!IsPostBack)
            {
                ActualizarNotificaciones();
            }
        }
        protected void ActualizarNotificaciones()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                conn.Open();

                // Consulta SQL para obtener el número de notificaciones
                string query = "SELECT COUNT(*) AS TotalPendientes, " +
                    "SUM(CASE WHEN DATEDIFF(DAY, MA.FechaMovimiento, GETDATE()) <= 2 THEN 1 ELSE 0 END) AS PendientesMenos2Dias," +
                    "SUM(CASE WHEN DATEDIFF(DAY, MA.FechaMovimiento, GETDATE()) > 2 AND DATEDIFF(DAY, MA.FechaMovimiento, GETDATE()) < 5 THEN 1 " +
                    "ELSE 0 END) AS PendientesEntre2y5Dias, SUM(CASE WHEN DATEDIFF(DAY, MA.FechaMovimiento, GETDATE()) >= 5 THEN 1 " +
                    "ELSE 0 END) AS PendientesMas5Dias FROM Proyecto P INNER JOIN Asignaciones A ON P.ID_Proyecto = A.idProyecto_FK " +
                    "LEFT JOIN movimientos_asignaciones MA ON A.Id_Asignacion = MA.IdAsignaciones_FK LEFT JOIN Usuarios UAsignado ON MA.IdUserAsignado_FK = UAsignado.Id " +
                    "WHERE UAsignado.username = @nombreUsuario AND MA.estadoMovimiento = 'Pendiente';";

                using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
                {
                    connSGL.Open();
                    SqlCommand cmd = new SqlCommand(query, connSGL);
                    cmd.Parameters.AddWithValue("nombreUsuario", Session["usuario"].ToString());

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int totalPendientes = reader["TotalPendientes"] != DBNull.Value ? Convert.ToInt32(reader["TotalPendientes"]) : 0;
                        int pendientesMenos2Dias = reader["PendientesMenos2Dias"] != DBNull.Value ? Convert.ToInt32(reader["PendientesMenos2Dias"]) : 0;
                        int pendientesEntre2y5Dias = reader["PendientesEntre2y5Dias"] != DBNull.Value ? Convert.ToInt32(reader["PendientesEntre2y5Dias"]) : 0;
                        int pendientesMas5Dias = reader["PendientesMas5Dias"] != DBNull.Value ? Convert.ToInt32(reader["PendientesMas5Dias"]) : 0;

                        // Asignar los valores a los Literales
                        NotificacionesLiteral.Text = totalPendientes.ToString();
                        Literal1.Text = pendientesMenos2Dias.ToString();
                        Literal2.Text = pendientesEntre2y5Dias.ToString();
                        Literal3.Text = pendientesMas5Dias.ToString();
                        // ... (continuar para los demás literales)
                    }

                    connSGL.Close();
                }

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