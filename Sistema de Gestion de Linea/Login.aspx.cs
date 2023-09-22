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

            //Consulta sql server para reiniciciar tabla a 0 de nuevo (id a cero) --> DBCC CHECKIDENT('NombreTabla', RESEED, 0);

            Session["usuario"] = " ";
            Session["Email"] = " ";
            Session["Nombre"] = " ";
            Session["Apellido"] = " ";
            Session["Activo"] = " ";
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
                cmd.Parameters.AddWithValue("@contraseña", txtPass.Text);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)
                {
                    lblNoti.Text = "Usuario o Contraseña incorrecta";
                    lblNoti.ForeColor = Color.DarkRed;
                }
                else
                {

                    Session["usuario"] = TxtUsuario.Text;


                    string ConsultaValida = @"SELECT Email, Nombre, Apellido, Activo
                      FROM USUARIOS
                      WHERE Username = @usuario";

                    using (SqlConnection com = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
                    {

                        SqlCommand sqlcomando = new SqlCommand(ConsultaValida, connSGL);
                        sqlcomando.Parameters.AddWithValue("@usuario", Session["usuario"]);

                        Session["Email"] = Convert.ToString(sqlcomando.ExecuteScalar());
                        Session["Nombre"] = Convert.ToString(sqlcomando.ExecuteScalar());                      
                        Session["Apellido"] = Convert.ToString(sqlcomando.ExecuteScalar());                       
                        Session["Activo"] = Convert.ToString(sqlcomando.ExecuteScalar());
                        connSGL.Close();

                    }

                    Response.Redirect("Bienvenidos.aspx");

                }



            }
        }
        

    }
}