using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Gestion_de_Linea.ABM_Usuarios
{
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);

            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    
                    CargarDatosUsuario(id);
                }
            }
        }
        private void CargarDatosUsuario(int id)
        {
          
            using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                string query = "SELECT * FROM USUARIOS WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connSGL))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connSGL.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtApellido.Text = reader["Apellido"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtUser.Text = reader["Username"].ToString();
                            txtPass.Text = reader["Contraseña"].ToString();

                            int idRol = Convert.ToInt32(reader["idRol_FK"]);
                            ListItem item = dropRol.Items.FindByValue(idRol.ToString());
                            if (item != null)
                            {
                                item.Selected = true;
                            }

                            bool activo = Convert.ToBoolean(reader["Activo"]);
                            cbxEstadoUser.Checked = activo;

                        }
                    }
                }
            }
        }
        public static string encriptarPass(string pass)
        {
            SHA1 metodo = new SHA1CryptoServiceProvider();
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(pass);
            byte[] hash = metodo.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
        protected void btActualizarUsuario_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlTransaction transaction = sqlConn.BeginTransaction();
                try
                {
                    string Consulta = @"UPDATE USUARIOS
                    SET 
                        Nombre = @nombreusuario,
                        Apellido = @apellido,
                        Email = @email,
                        Contraseña = 
                            CASE 
                                WHEN @cambiarPass = 1 THEN @contraseña
                                ELSE Contraseña 
                            END,
                        Username = @username,
                        Activo = @activo,
                        idRol_FK = @rol
                    WHERE Id = @idUsuario";

                    string passEncriptada = encriptarPass(txtPass.Text);
                    int id = Convert.ToInt32(Request.QueryString["Id"]);

                    string estadoUser = cbxEstadoUser.Checked ? "true" : "false";
                    bool cambiarPass = cbxPass.Checked;

                    SqlCommand sqlcomando2 = new SqlCommand(Consulta, sqlConn, transaction);
                    sqlcomando2.Parameters.AddWithValue("nombreusuario", txtNombre.Text);
                    sqlcomando2.Parameters.AddWithValue("apellido", txtApellido.Text);
                    sqlcomando2.Parameters.AddWithValue("email", txtEmail.Text);
                    sqlcomando2.Parameters.AddWithValue("username", txtUser.Text);
                    sqlcomando2.Parameters.AddWithValue("activo", estadoUser);
                    sqlcomando2.Parameters.AddWithValue("rol", dropRol.SelectedValue);
                    sqlcomando2.Parameters.AddWithValue("contraseña", cambiarPass ? passEncriptada : "");
                    sqlcomando2.Parameters.AddWithValue("@idUsuario", id);

                 
                    sqlcomando2.Parameters.AddWithValue("@cambiarPass", cambiarPass);

                    sqlcomando2.ExecuteNonQuery();
                    transaction.Commit();




                    lblnoti.ForeColor = Color.Green;
                    lblnoti.Text = "Se dio de alta con exito!";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblnoti.ForeColor = Color.Red;
                    lblnoti.Text = ex.Message;
                }
                finally
                {
                    sqlConn.Close();
                }
            }

        }            
    }
}