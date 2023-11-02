using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing.Printing;
using System.Xml.Linq;
using ClosedXML.Excel;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace Sistema_de_Gestion_de_Linea.ABM_Proyectos
{
    public partial class Ver_Proyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
                Buscar();
            
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
        protected void todoslosproyectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            todoslosproyectos.PageIndex = e.NewPageIndex;
            todoslosproyectos.DataBind();
            Buscar();
        }

        public void Buscar()
        {

            string sqlBuscar = @"SELECT Proyecto.ID_Proyecto AS 'Proyecto', Proyecto.NumeroTicket AS 'Ticket', 
                     Proyecto.NumeroLinea AS 'Linea', Proyecto.Descripcion, Proyecto.Localidad, 
                     Proyecto.Calle, Proyecto.NumeroCalle AS 'Altura', Proyecto.FechaInicio AS 'Inicio', 
                     Proyecto.FechaFinalizacion AS 'Final', EstadoProyecto.Estado, 
                     USUARIOS.Username AS 'Usuario', Prioridad.Descripción AS 'Prioridad', 
                     TipoDeTrabajo.Descripción AS 'Tipo' 
                     FROM Proyecto 
                     INNER JOIN EstadoProyecto ON Proyecto.FK_ID_Estado = EstadoProyecto.Id 
                     INNER JOIN USUARIOS ON Proyecto.FK_ID_Usuario = USUARIOS.Id 
                     INNER JOIN TipoDeTrabajo ON Proyecto.idTipoTrabajo_FK = TipoDeTrabajo.idTipoTrabajo 
                     INNER JOIN Prioridad ON Proyecto.idPrioridad_FK = Prioridad.idPrioridad
                     WHERE 1 = 1";

            if (cblEstado.Items.Cast<ListItem>().Any(li => li.Selected))
            {
                sqlBuscar += " AND EstadoProyecto.Estado IN ('" + string.Join("','", cblEstado.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value)) + "')";
            }

            if (cblPrioridad.SelectedValue != "")
            {
                sqlBuscar += " AND Prioridad.Descripción IN ('" + string.Join("','", cblPrioridad.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Text)) + "')";
            }

            if (cblTipo.SelectedValue != "")
            {
                sqlBuscar += " AND TipoDeTrabajo.Descripción IN ('" + string.Join("','", cblTipo.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Text)) + "')";
            }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGL"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(sqlBuscar, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                todoslosproyectos.DataSource = dt;
                todoslosproyectos.DataBind();
                conn.Close();
            }
        }
        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        public void ExportToExcel(GridView gridView, string nombreArchivo)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();

                foreach (TableCell cell in gridView.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }

                foreach (GridViewRow row in gridView.Rows)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dr[i] = row.Cells[i].Text;
                    }
                    dt.Rows.Add(dr);
                }

                wb.Worksheets.Add(dt, "Proyectos");

                string filePath = Server.MapPath("~/ArchivosExcel/") + nombreArchivo + ".xlsx";
                wb.SaveAs(filePath);

                // Proporcionar un enlace para descargar el archivo
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.TransmitFile(filePath);
                Response.End();
            }
        }



        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string nombreArchivo = "Proyectos_" + DateTime.Now.ToString("yyyyMMdd");
            ExportToExcel(todoslosproyectos, nombreArchivo);
        }
    }


}