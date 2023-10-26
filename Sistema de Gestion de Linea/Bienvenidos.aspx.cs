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
    public partial class Bienvenidos : System.Web.UI.Page
    {
        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string proyecto = btn.CommandArgument;

            // Redirige al usuario al otro formulario, pasando el valor de "proyecto" si es necesario
            Response.Redirect("Asignaciones/devolucionAsignacion.aspx?proyecto=" + proyecto);
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
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    string usuarioLogueado = Session["usuario"].ToString();

                    string sqlBuscar = @"SELECT P.ID_Proyecto AS 'Proyecto', P.NumeroTicket AS 'Ticket', P.NumeroLinea AS 'Linea',
                                MA.observacion AS 'Observaciones',
                                MA.estadoMovimiento AS 'Estado', MA.FechaMovimiento AS 'Fecha',UAsignado.Username AS 'Usuario'
                                FROM Proyecto P
                                INNER JOIN Asignaciones A
                                                   ON P.ID_Proyecto = A.idProyecto_FK
                                LEFT JOIN 
                                      movimientos_asignaciones MA ON A.Id_Asignacion = MA.IdAsignaciones_FK
                                LEFT JOIN 
                                     Usuarios UAsignado ON MA.IdUserAsignado_FK = UAsignado.Id
                                 WHERE UAsignado.username = @nombreUsuario AND MA.estadoMovimiento = 'Pendiente'";

                    using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
                    {
                        connSGL.Open();
                        SqlCommand cmd = new SqlCommand(sqlBuscar, connSGL);
                        cmd.Parameters.AddWithValue("nombreUsuario", usuarioLogueado); 

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
    }
}