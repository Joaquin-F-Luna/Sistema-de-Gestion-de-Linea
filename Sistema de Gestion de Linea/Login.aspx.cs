using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Gestion_de_Linea
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

          
            Session["usuario"] = " ";
            Session["Email"] = " ";
            Session["Nombre"] = " ";
            Session["Apellido"] = " ";
            Session["Activo"] = " ";
        }
        public static string encriptarPass(string pass)
        {
            SHA1 metodo = new SHA1CryptoServiceProvider();
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(pass);
            byte[] hash = metodo.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sqlvalidauser = @"SELECT COUNT(*)
                        FROM USUARIOS
                        WHERE Username = @nomusuario
                        AND Contraseña = @contraseña";


            using (SqlConnection connSGL = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                connSGL.Open();

                SqlCommand cmd = new SqlCommand(sqlvalidauser, connSGL);
                cmd.Parameters.AddWithValue("@nomusuario", TxtUsuario.Text);
                //cmd.Parameters.AddWithValue("@contraseña", txtPass.Text);
                cmd.Parameters.AddWithValue("@contraseña", encriptarPass(txtPass.Text));

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)
                {
                    lblNoti.Text = "Usuario o Contraseña incorrecta";
                    lblNoti.ForeColor = Color.DarkRed;
                }
                else
                {

                    Session["usuario"] = TxtUsuario.Text;


                    string ConsultaValida = @"SELECT U.Email, U.Nombre, U.Apellido, U.Activo, R.Descripcion
                      FROM USUARIOS U
                      INNER JOIN ROLES R
                                ON U.idRol_FK = R.Idrol
                      WHERE Username = @usuario";

                    SqlCommand sqlcomando = new SqlCommand(ConsultaValida, connSGL);
                        sqlcomando.Parameters.AddWithValue("@usuario", Session["usuario"]);

                        SqlDataReader reader = sqlcomando.ExecuteReader();

                    if (reader.Read())
                    {
                        Session["Email"] = Convert.ToString(reader["Email"]);
                        Session["Nombre"] = Convert.ToString(reader["Nombre"]);
                        Session["Apellido"] = Convert.ToString(reader["Apellido"]);
                        Session["Activo"] = Convert.ToString(reader["Activo"]);
                        Session["Rol"] = Convert.ToString(reader["Descripcion"]);
                    }
                    connSGL.Close();

                    Response.Redirect("~/Bienvenidos.aspx");

                }

            }
        }

    }
}