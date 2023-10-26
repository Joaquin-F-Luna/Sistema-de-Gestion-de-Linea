using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Sistema_de_Gestion_de_Linea.Asignaciones.AsignacionProyectos;

namespace Sistema_de_Gestion_de_Linea.Asignaciones
{
    public partial class devolucionAsignacion : System.Web.UI.Page
    {
        public class asignacionProyecto
        {
            public string NumeroTicket { get; set; }
            public string NumeroLinea { get; set; }
            public string Descripcion { get; set; }
            public string Estado { get; set; }
            public string Area { get; set; }
            public string IdAsignacion { get; set; }
            public string nameUsuarioAsignador { get; set; }


        }
        private void LlenarCampos(asignacionProyecto proyectoA)
        {
            nroticket.Text = proyectoA.NumeroTicket;
            nrolinea.Text = proyectoA.NumeroLinea;
            descripcion.Text = proyectoA.Descripcion;
            DDL_tipoarea.Text = proyectoA.Area;
            //userAsignador.Text = proyectoA.UsuarioAsignador;

            userAsignador.Text = proyectoA.nameUsuarioAsignador;
        }

        public asignacionProyecto ObtenerDatosProyectoPorId(string idProyecto)
        {
            string consulta = "SELECT NumeroTicket, NumeroLinea, Descripcion FROM Proyecto WHERE ID_Proyecto = @IdProyecto";
            string consultaAsig = "SELECT TOP 1 Asignaciones.Id_Asignacion,Asignaciones.estado_asignacion, Asignaciones.area, Asignaciones.Id_UsuarioAsignador, U.Username " +
                      "FROM Asignaciones " +
                      "INNER JOIN USUARIOS U ON Asignaciones.Id_UsuarioAsignador = U.Id " +
                      "WHERE Asignaciones.idProyecto_FK = @IdProyecto " +
                      "ORDER BY Id_Asignacion DESC;";

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
                        proyecto.IdAsignacion = reader2["Id_Asignacion"].ToString();
                        proyecto.nameUsuarioAsignador = reader2["Username"].ToString();

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Proyecto"] != null)
                {
                    string proyecto = Request.QueryString["Proyecto"];
                    IdProyectoTB.Text = proyecto; // Asigna el valor a un Label

                   
                }

                asignacionProyecto proyectoA = ObtenerDatosProyectoPorId(IdProyectoTB.Text);
                if (proyectoA != null)
                {
                    LlenarCampos(proyectoA);
                }

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

                using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
                {
                    connSGL.Open();
                    SqlCommand cmd = new SqlCommand(sqlBuscar, connSGL);
                    cmd.Parameters.AddWithValue("idProyecto", IdProyectoTB.Text);

                    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    gdtodosproyectos.DataSource = dt1;
                    gdtodosproyectos.DataBind();
                    connSGL.Close();
                }

            }

            nroticket.ReadOnly = true;
            nrolinea.ReadOnly = true;
            descripcion.ReadOnly = true;
            DDL_tipoarea.Enabled = false;
            DDL_tipoestado.Enabled = false;
            IdProyectoTB.Enabled = false;

        }

        protected void FinalizarDevolucion_Click(object sender, EventArgs e)
        {
            string obtenerIDAsignacion = @"SELECT Id_Asignacion FROM Asignaciones WHERE idProyecto_FK = @idProyecto_FK";

            string ConsultaMovimiento = @"INSERT INTO movimientos_asignaciones (IdAsignaciones_FK, IdUserAsignado_FK, estadoMovimiento, 
                                            FechaMovimiento, observacion, area_mov)
                             VALUES (@IdAsignaciones_FK, @IdUserAsignado_FK, @estadoMovimiento, @FechaMovimiento, @observacion, @area_mov)";

            string ConsultaUpdate = @"UPDATE Asignaciones SET area = @areaDevolucion WHERE Id_Asignacion = @id";

            string ConsultaUpdateMov = @"UPDATE movimientos_asignaciones SET estadoMovimiento = @cerrarEstado 
                             WHERE IdAsignaciones_FK = @idAsig_fk 
                             AND FechaMovimiento = (SELECT MAX(FechaMovimiento) 
                                                  FROM movimientos_asignaciones 
                                                  WHERE IdAsignaciones_FK = @idAsig_fk)";


            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                int idUsuarioLog = idUsuario(Session["usuario"].ToString());
                int idDelProyecto = int.Parse(IdProyectoTB.Text);
                sqlConn.Open();
                SqlTransaction transaction = sqlConn.BeginTransaction(); // Inicia la transacción

                SqlCommand sqlcomandoIdAsig = new SqlCommand(obtenerIDAsignacion, sqlConn, transaction);
                sqlcomandoIdAsig.Parameters.AddWithValue("@idProyecto_FK", idDelProyecto);
                int idAsign = Convert.ToInt32(sqlcomandoIdAsig.ExecuteScalar());
                try
                {
                    // Primero el UPDATE

                    if (DDL_tipoarea.Text != "Certificacion" && DDL_tipoestadoasignacion.Text != "Okey")  // Reemplaza "condicion" con tu condición real
                    {
                        // Si la condición se cumple, ejecuta el código siguiente
                        SqlCommand sqlUpdate = new SqlCommand(ConsultaUpdate, sqlConn, transaction);
                        sqlUpdate.Parameters.AddWithValue("@areaDevolucion", "Asignaciones");
                        sqlUpdate.Parameters.AddWithValue("@id", idAsign);
                        sqlUpdate.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand sqlUpdate = new SqlCommand(ConsultaUpdate, sqlConn, transaction);
                        sqlUpdate.Parameters.AddWithValue("@areaDevolucion", "ODN");
                        sqlUpdate.Parameters.AddWithValue("@id", idAsign);
                        sqlUpdate.ExecuteNonQuery();
                    }

                    SqlCommand sqlUpdateMov = new SqlCommand(ConsultaUpdateMov, sqlConn, transaction);
                    sqlUpdateMov.Parameters.AddWithValue("@cerrarEstado", "Hecho"); // Reemplaza con el nuevo estado
                    sqlUpdateMov.Parameters.AddWithValue("@idAsig_fk", idAsign); // Reemplaza con el ID adecuado
                    sqlUpdateMov.ExecuteNonQuery();

                    // Luego el INSERT
                    SqlCommand insertMovimiento = new SqlCommand(ConsultaMovimiento, sqlConn, transaction);
                    insertMovimiento.Parameters.AddWithValue("@IdAsignaciones_FK", idAsign);
                    insertMovimiento.Parameters.AddWithValue("@IdUserAsignado_FK", idUsuarioLog);
                    insertMovimiento.Parameters.AddWithValue("@estadoMovimiento", DDL_tipoestadoasignacion.Text);
                    insertMovimiento.Parameters.AddWithValue("@FechaMovimiento", DateTime.Now);
                    insertMovimiento.Parameters.AddWithValue("@observacion", observacion.Text);
                    insertMovimiento.Parameters.AddWithValue("@area_mov", DDL_tipoarea.Text);
                    insertMovimiento.ExecuteNonQuery();

                    transaction.Commit(); // Confirma la transacción si todo fue exitoso
                    notificacion.ForeColor = Color.Green;
                    notificacion.Text = "Se realizó la devolución con éxito!";


                    Server.Transfer("~/Bienvenidos.aspx");


                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // En caso de error, deshace los cambios
                    notificacion.ForeColor = Color.Red;
                    notificacion.Text = "Error: " + ex.Message;
                }
                finally
                {
                    sqlConn.Close(); // Cierra la conexión
                }

            }
           observacion.Text = "";

        }

    }
}