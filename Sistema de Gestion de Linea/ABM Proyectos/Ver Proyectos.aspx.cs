using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Sistema_de_Gestion_de_Linea.ABM_Proyectos
{
    public partial class Ver_Proyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    DropDownList ddlEstado = (DropDownList)FindControl("ddlEstado");
            //  ddlEstado.Items.Insert(0, new ListItem("Pendiente", "Pendiente"));
            //}
        }

        protected void buscarProyecto_Click1(object sender, EventArgs e)
        {

        }


        protected void btnIrAEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string proyecto = btn.CommandArgument;

            Response.Redirect("Editar Proyecto.aspx?proyecto=" + proyecto);
        }

        protected void btnIrAEditar2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string proyecto = btn.CommandArgument;

            // Redirige al usuario al otro formulario, pasando el valor de "proyecto" si es necesario
            Response.Redirect("Editar Proyecto.aspx?proyecto=" + proyecto);
        }
    }
}