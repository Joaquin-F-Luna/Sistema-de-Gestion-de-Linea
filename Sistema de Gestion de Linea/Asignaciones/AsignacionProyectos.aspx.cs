using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        public static int idUsuario(String usuarioIniciado)
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            int idDelProyecto = int.Parse(IdProyectoTB.Text);
          

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

                                 WHERE P.ID_Proyecto = @idproyecto";

            string verificacionPendiente = @"SELECT  estado_asignacion, area
                                FROM Asignaciones 
                                WHERE idProyecto_FK = @idProyecto_FK ";

            string consultaVerificacion = @"SELECT COUNT(*) 
                                FROM Asignaciones 
                                WHERE idProyecto_FK = @idProyecto_FK ";

            using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                connSGL.Open();

                SqlCommand sqlVerificacion = new SqlCommand(consultaVerificacion, connSGL);
                sqlVerificacion.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                int cantidadRegistros = (int)sqlVerificacion.ExecuteScalar();

                SqlCommand sqlverif = new SqlCommand(verificacionPendiente, connSGL);
                sqlverif.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                SqlDataReader reader = sqlverif.ExecuteReader();

                string estadoAsignacion = null;
                string areaAsignacion = null;

                if (reader.Read())
                {
                    estadoAsignacion = reader["estado_asignacion"].ToString();
                    areaAsignacion = reader["area"].ToString();
                }
                

                reader.Close();

                if (cantidadRegistros > 0)
                {
                  if  (areaAsignacion != "Asignaciones"  || areaAsignacion!="ODN")
                    {
                        DDL_tipoasignar.Enabled = false;
                        usuario_asignar.ReadOnly = true;
                        observacion.ReadOnly = true;
                        btnAsignar.Enabled = false;
                    }
                    else
                    {
                        DDL_tipoasignar.Enabled = true;
                        usuario_asignar.ReadOnly = false;
                        observacion.ReadOnly = false;
                        btnAsignar.Enabled = true;
                    }

                }
                else
                {
                    DDL_tipoasignar.Enabled = true;
                    usuario_asignar.ReadOnly = false;
                    observacion.ReadOnly = false;
                    btnAsignar.Enabled = true;
                }

                SqlCommand cmd = new SqlCommand(sqlBuscar, connSGL);
                cmd.Parameters.AddWithValue("idproyecto", idDelProyecto);

                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                gdtodosproyectos.DataSource = dt1;
                gdtodosproyectos.DataBind();
                connSGL.Close();
            }

            asignacionProyecto proyecto = ObtenerDatosProyectoPorId(IdProyectoTB.Text);
            if (proyecto != null)
            {
                LlenarCampos(proyecto);
            }
        }

        protected void AgregarAsignacion_Click(object sender, EventArgs e)
        {
            //Verificar si el proyecto ya tiene creado un registro para la tabla Asignaciones
            string consultaVerificacion = @"SELECT COUNT(*) 
                                FROM Asignaciones 
                                WHERE idProyecto_FK = @idProyecto_FK 
                                  AND Area = 'Asignaciones'";

            //En caso de que ya exista un registro en area "Asignaciones", haremos un  UPDATE con el nuevo area
            string ConsultaUpdate = @"UPDATE Asignaciones SET estado_asignacion = @nuevoEstado, area = @nuevoArea WHERE idProyecto_FK = @idProyecto_FK";

            //Si no hay registros en asignaciones para el proyecto, se utilizan las siguientes consultas

            //Insert en la tabla de Asignaciones (en caso de que no haya un registro para ese proyecto)
            string ConsultaAsignacion = @"INSERT INTO Asignaciones (idProyecto_FK, Area, estado_asignacion, Id_UsuarioAsignador)
                                              VALUES (@idProyecto_FK, @area, @estado_asig, @user)";
            //Obtener el id_asignacion luego del insert, para utilizarlo en la tabla movimientos_asignaciones
            string obtenerIDAsignacion = @"SELECT Id_Asignacion FROM Asignaciones WHERE idProyecto_FK = @idProyecto_FK";

            //Realizar Insert en la tabla movimiento (el estado será pendiente)
            string ConsultaMovimiento = @"INSERT INTO movimientos_asignaciones (IdAsignaciones_FK, IdUserAsignado_FK, estadoMovimiento, FechaMovimiento, observacion, area_mov)
                                 VALUES (@IdAsignaciones_FK, @IdUserAsignado_FK, @estadoMovimiento, @FechaMovimiento, @observacion, @area_mov)";
            

            int idDelProyecto = int.Parse(IdProyectoTB.Text);
           
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlTransaction transaction = sqlConn.BeginTransaction();

                int idUsuarioLog = idUsuario(Session["Usuario"].ToString());

                string asignado = usuario_asignar.Text;

                if (string.IsNullOrEmpty(asignado))
                {

                    string color = "red";
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('Error: Ingrese un nombre de usuario para asignar', '{color}');", true);
                    return;
                }
                if (UsuarioExiste(asignado))
                {
                    try
                    {
                        SqlCommand sqlVerificacion = new SqlCommand(consultaVerificacion, sqlConn, transaction);
                        sqlVerificacion.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                        int cantidadRegistros = (int)sqlVerificacion.ExecuteScalar();

                        if (cantidadRegistros > 0)
                        {

                            SqlCommand sqlUpdate = new SqlCommand(ConsultaUpdate, sqlConn, transaction);
                            sqlUpdate.Parameters.AddWithValue("@nuevoEstado", "Abierto");
                            sqlUpdate.Parameters.AddWithValue("@nuevoArea", DDL_tipoasignar.Text);
                            sqlUpdate.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                            sqlUpdate.ExecuteNonQuery();

                            SqlCommand sqlcomandoIdAsig = new SqlCommand(obtenerIDAsignacion, sqlConn, transaction);
                            sqlcomandoIdAsig.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                            int idAsign = Convert.ToInt32(sqlcomandoIdAsig.ExecuteScalar());

                            SqlCommand insertMovimiento = new SqlCommand(ConsultaMovimiento, sqlConn, transaction);
                            insertMovimiento.Parameters.AddWithValue("@IdAsignaciones_FK", idAsign);
                            insertMovimiento.Parameters.AddWithValue("@IdUserAsignado_FK", idUsuario(usuario_asignar.Text));
                            insertMovimiento.Parameters.AddWithValue("@estadoMovimiento", "Pendiente");
                            insertMovimiento.Parameters.AddWithValue("@FechaMovimiento", DateTime.Now);
                            insertMovimiento.Parameters.AddWithValue("@observacion", observacion.Text);
                            insertMovimiento.Parameters.AddWithValue("@area_mov", DDL_tipoasignar.Text);
                            insertMovimiento.ExecuteNonQuery();


                        }
                        else
                        {
                            SqlCommand sqlAsignacion = new SqlCommand(ConsultaAsignacion, sqlConn, transaction);
                            sqlAsignacion.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                            sqlAsignacion.Parameters.AddWithValue("@area", DDL_tipoasignar.Text);
                            sqlAsignacion.Parameters.AddWithValue("@estado_asig", "Abierto");
                            sqlAsignacion.Parameters.AddWithValue("@user", idUsuarioLog);
                            sqlAsignacion.ExecuteNonQuery();

                            SqlCommand sqlcomandoIdAsig = new SqlCommand(obtenerIDAsignacion, sqlConn, transaction);
                            sqlcomandoIdAsig.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                            int idAsign = Convert.ToInt32(sqlcomandoIdAsig.ExecuteScalar());

                            SqlCommand insertMovimiento = new SqlCommand(ConsultaMovimiento, sqlConn, transaction);
                            insertMovimiento.Parameters.AddWithValue("@IdAsignaciones_FK", idAsign);
                            insertMovimiento.Parameters.AddWithValue("@IdUserAsignado_FK", idUsuario(usuario_asignar.Text));
                            insertMovimiento.Parameters.AddWithValue("@estadoMovimiento", "Pendiente");
                            insertMovimiento.Parameters.AddWithValue("@FechaMovimiento", DateTime.Now);
                            insertMovimiento.Parameters.AddWithValue("@observacion", observacion.Text);
                            insertMovimiento.Parameters.AddWithValue("@area_mov", DDL_tipoasignar.Text);
                            insertMovimiento.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        string color = "green";
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('Se asignó con éxito', '{color}');", true);
                        Response.AppendHeader("Refresh", "5;url=AsignacionProyectos.aspx");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        notificacion.ForeColor = Color.Red;
                        notificacion.Text = "Error: " + ex.Message;
                    }
                    finally
                    {
                        sqlConn.Close();
                    }
                    observacion.Text = "";
                }
                else
                {
                    string color = "red";
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacion", $"mostrarNotificacion('Error: El usuario ingresado no es válido.', '{color}');", true);
                }

                

                
            }
        }

        private bool UsuarioExiste(string usuario)
        {
            string consultaUsuario = "SELECT COUNT(*) FROM USUARIOS WHERE Username = @usuario";

            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlCommand sqlcomando = new SqlCommand(consultaUsuario, sqlConn);
                sqlcomando.Parameters.AddWithValue("usuario", usuario);

                int count = Convert.ToInt32(sqlcomando.ExecuteScalar());
                sqlConn.Close();

                return count > 0;
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