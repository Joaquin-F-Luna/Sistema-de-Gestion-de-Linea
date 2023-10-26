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
using System.Net;
using System.Net.Mail;

namespace Sistema_de_Gestion_de_Linea.ABM_Usuarios
{
    public partial class AltadeUsuario : System.Web.UI.Page
    {
        public static string encriptarPass(string pass)
        {
            SHA1 metodo = new SHA1CryptoServiceProvider();
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(pass);
            byte[] hash = metodo.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {

            string claveRandom = Guid.NewGuid().ToString("N").Substring(0, 10);

            string SQL_Select = @"SELECT COUNT(*)
                      FROM Usuarios
                      WHERE Username = @usuario
                      AND Email = @mail";


            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                sqlConn.Open();
                SqlTransaction transaction = sqlConn.BeginTransaction(); // Inicia la transacción

                try
                {
                   

                    SqlCommand sqlcomando = new SqlCommand(SQL_Select, sqlConn);

                    sqlcomando.Parameters.AddWithValue("usuario", txtUser.Text);
                    sqlcomando.Parameters.AddWithValue("mail", txtEmail.Text);

                    int result = Convert.ToInt32(sqlcomando.ExecuteScalar());

                 
                    if (result == 0)
                    {
                        string Consulta = @"INSERT INTO USUARIOS (Nombre,Apellido,Email,Contraseña,Username,Activo,idRol_FK)
                                    VALUES (@nombreusuario,@apellido,@email,@contraseña,@username,@activo,@rol)";
                        try
                        {

                            string passEncriptada = encriptarPass(claveRandom);

                            SqlCommand sqlcomando2 = new SqlCommand(Consulta, sqlConn);
                            sqlcomando2.Parameters.AddWithValue("nombreusuario", txtNombre.Text);
                            sqlcomando2.Parameters.AddWithValue("apellido", txtApellido.Text);
                            sqlcomando2.Parameters.AddWithValue("email", txtEmail.Text);
                            sqlcomando2.Parameters.AddWithValue("username", txtUser.Text);
                            sqlcomando2.Parameters.AddWithValue("rol", dropRol.SelectedValue);
                            sqlcomando2.Parameters.AddWithValue("activo", "True");
                            sqlcomando2.Parameters.AddWithValue("contraseña", passEncriptada);
                            sqlcomando2.ExecuteNonQuery();
                            transaction.Commit();



                            lblnoti.ForeColor = Color.Green;
                            lblnoti.Text = "Se dio de alta con exito!";

                            //Enviar email
                            string correoRemitente = "megared.sgl@gmail.com";
                            string correoDestinatario = txtEmail.Text;

                            // Configuración del cliente SMTP
                            SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com");
                            clienteSmtp.Port = 587;
                            clienteSmtp.EnableSsl = true;
                            clienteSmtp.Credentials = new NetworkCredential(correoRemitente, "uovm eulg fzjv jdch");

                            // Crear el mensaje
                            MailMessage mensaje = new MailMessage(correoRemitente, correoDestinatario);
                            mensaje.Subject = "Bienvenido al Sistema de Gestión de Lineas";
                            mensaje.Body = "¡Hola! Bienvenido al sistema SGL de MegaRed\n" +
                                 "Tu usuario es: " + txtUser.Text + "\n" +
                                 "Tu clave es: " + claveRandom + "\n" +
                                 "Recuerda cambiar tu contraseña una vez que hayas ingresado.\n" +
                                 "Saludos!";
                            try
                            {
                                // Enviar el correo electrónico
                                clienteSmtp.Send(mensaje);
                                Response.Write("Correo enviado exitosamente.");
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Error al enviar el correo: " + ex.Message);
                            }


                        }
                        catch (Exception ex)
                        {
                           
                            lblnoti.ForeColor = Color.Red;
                            lblnoti.Text = ex.Message;
                        }

                    }
                    else
                    {

                        lblnoti.ForeColor = Color.Red;
                        lblnoti.Text = "Ya existe un usuario con ese username o email, intente otra vez.";

                        sqlConn.Close();

                    }
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
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtUser.Text = "";
        }
         
    }
}