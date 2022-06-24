using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class ClienteController : Controller
    {
        private static Usuario SesionUsuario;
        // GET: Cliente
        public ActionResult Crear()
        {
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        public ActionResult AsignarCliente()
        {
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        [HttpPost]
        public JsonResult ObtenerCliente()
        {
            List<Cliente> oListaCliente = CD_Cliente.Instancia.ObtenerClientes();
            return Json(new { data = oListaCliente }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerCliente1()
        {
            List<Cliente> oListaCliente = CD_Cliente.Instancia.ObtenerClientes();
            return Json(new { data = oListaCliente }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerUsuariosCliente()
        {
            Usuario rptUsuario = CD_Usuario.Instancia.ObtenerDetalleUsuario(SesionUsuario.IdUsuario);
            return Json(rptUsuario, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerProductoPorTiendaCliente(int IdTienda)
        {

            List<ProductoTienda> oListaProductoTienda = CD_ProductoTienda.Instancia.ObtenerProductoTienda();
            oListaProductoTienda = oListaProductoTienda.Where(x => x.oTienda.IdTienda == IdTienda && x.Stock > 0).ToList();

            return Json(new { data = oListaProductoTienda }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Cliente objeto)
        {
            bool respuesta = false;

            if (objeto.IdCliente == 0)
            {
                respuesta = CD_Cliente.Instancia.RegistrarCliente(objeto);
            }
            else
            {
                respuesta = CD_Cliente.Instancia.ModificarCliente(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Cliente.Instancia.EliminarCliente(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerAsignaciones()
        {
            List<ProductoTienda> lista = CD_ProductoTienda.Instancia.ObtenerProductoTienda();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ObtenerAsignacionesCliente(int idCliente)
        {
            List<ListaPreciosCliente> lista = CD_ListaPrecios.Instancia.ObtenerProductoCliente(idCliente);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarProductoCliente(ListaPreciosCliente objeto)
        {
            bool respuesta = CD_Cliente.Instancia.RegistrarProductoCliente(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarProductoCliente(int id = 0)
        {
            bool respuesta = CD_Cliente.Instancia.EliminarProductoCliente(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}