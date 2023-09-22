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
    public partial class editarTuUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string encriptarPass(string pass)
        {
            SHA1 metodo = new SHA1CryptoServiceProvider();
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(pass);
            byte[] hash = metodo.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
        protected void btActualizar_Click(object sender, EventArgs e)
        {
            string usuarioIniciado = Session["usuario"].ToString();

            string SQL_UPDATE = "UPDATE Usuarios SET Contraseña = @nuevapass WHERE Username = @nombreusuario";
            string SQL_Select = @"SELECT COUNT(*)
                      FROM Usuarios
                      WHERE Username = @usuario
                      AND Contraseña = @password";


            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                try
                {
                    sqlConn.Open();

                    SqlCommand sqlcomando = new SqlCommand(SQL_Select, sqlConn);

                    sqlcomando.Parameters.AddWithValue("usuario", usuarioIniciado);
                    sqlcomando.Parameters.AddWithValue("password", encriptarPass(pass.Text));

                    int result = Convert.ToInt32(sqlcomando.ExecuteScalar());

                    if (result == 0)
                    {
                        lblnoti.ForeColor = Color.Red;
                        lblnoti.Text = "La contraseña actual indicada no es la correcta, intente otra vez.";

                        sqlConn.Close();
                    }
                    else
                    {
                        if (newPass.Text != checkNewPass.Text)
                        {

                            lblnoti.ForeColor = Color.Red;
                            lblnoti.Text = "La contraseña nueva no coincide, intente otra vez.";
                            sqlConn.Close();
                        }
                        else
                        {
                            SqlCommand SQLUP = new SqlCommand(SQL_UPDATE, sqlConn);

                            SQLUP.Parameters.AddWithValue("nombreusuario", usuarioIniciado);
                            SQLUP.Parameters.AddWithValue("nuevapass", encriptarPass(newPass.Text));

                            lblnoti.ForeColor = Color.Green;
                            lblnoti.Text = "La contraseña fue modificada con éxito";

                            pass.Text = "";
                            newPass.Text = "";
                            checkNewPass.Text = "";

                            SQLUP.ExecuteNonQuery();

                            sqlConn.Close();
                        }
                    }

                }
                catch (Exception ex)
                {
                    lblnoti.ForeColor = Color.Red;
                    lblnoti.Text = ex.Message;
                }

            }
        }
    }
}