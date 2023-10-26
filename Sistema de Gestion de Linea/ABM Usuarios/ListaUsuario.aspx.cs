using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Gestion_de_Linea.ABM_Usuarios
{
    public partial class ModificaciónUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIrAEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string idUsuario = GridView1.DataKeys[row.RowIndex].Value.ToString();
            Response.Redirect("ModificarUsuario.aspx?Id=" + idUsuario);
        }
    }
}