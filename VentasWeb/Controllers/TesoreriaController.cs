using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class TesoreriaController : Controller
    {
        // GET: Categoria
        public ActionResult Consultar()
        {
            return View();
        }
        public JsonResult ObtenerDatos(string fechainicio, string fechafin)
        {
            List<Tesoreria> oListaUsuario = CD_Tesoreria.ObtenerDatosChart(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin));
            if (oListaUsuario == null)
                oListaUsuario = new List<Tesoreria>();
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ObtenerVentas(string codigo, string fechainicio, string fechafin, string numerodocumento, string nombres)
        {
            List<Venta> lista = CD_Venta.Instancia.ObtenerListaVenta(codigo, Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), numerodocumento, nombres);


            if (lista == null)
                lista = new List<Venta>();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerCompras(string fechainicio, string fechafin, int idproveedor, int idtienda)
        {
            List<Compra> lista = CD_Compra.Instancia.ObtenerListaCompra(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idproveedor, idtienda);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();

            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");

                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    JSONString.Append("{");

                    for (int j = 0; j <= table.Columns.Count - 1; j++)
                    {
                        if (j < table.Columns.Count - 1)
                            JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        else if (j == table.Columns.Count - 1)
                            JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "[" + table.Rows[i][j].ToString() + "]");
                    }

                    if (i == table.Rows.Count - 1)
                        JSONString.Append("}");
                    else
                        JSONString.Append("},");
                }

                JSONString.Append("]");
            }

            return JSONString.ToString();
        }
    }

}





