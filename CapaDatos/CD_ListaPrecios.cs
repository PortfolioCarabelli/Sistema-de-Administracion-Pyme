using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ListaPrecios
    {
        public static CD_ListaPrecios _instancia = null;

        private CD_ListaPrecios()
        {

        }

        public static CD_ListaPrecios Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ListaPrecios();
                }
                return _instancia;
            }
        }

        public List<ListaPreciosCliente> ObtenerProductoCliente(int id)
        {
            List<ListaPreciosCliente> rptListaPreciosCliente = new List<ListaPreciosCliente>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerProductoCliente", oConexion);
                cmd.Parameters.AddWithValue("IdCliente", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPreciosCliente.Add(new ListaPreciosCliente()
                        {
                            IdListaPrecios = Convert.ToInt32(dr["IdListaPrecio"].ToString()),
                            oProducto = new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                                Codigo = dr["CodigoProducto"].ToString(),
                                Nombre = dr["NombreProducto"].ToString(),

                            },
                            oCliente = new Cliente()
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"].ToString()),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Nombre = dr["Nombre"].ToString(),

                            },
                            
                            PrecioCliente = Convert.ToDecimal(dr["PrecioCliente"].ToString()),
                           
                           

                        }); 
                    }
                    dr.Close();

                    return rptListaPreciosCliente;

                }
                catch (Exception ex)
                {
                    rptListaPreciosCliente = null;
                    return rptListaPreciosCliente;
                }
            }
        }

        public List<ProductoCliente> ObtenerProductoClienteLP(int IdCliente)
        {
            List<ProductoCliente> rptListaProductoCliente = new List<ProductoCliente>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerProductoClienteListaPrecio", oConexion);
                cmd.Parameters.AddWithValue("IdCliente", IdCliente);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaProductoCliente.Add(new ProductoCliente()
                        {
                            IdProductoTienda = Convert.ToInt32(dr["IdProductoTienda"].ToString()),
                            oProducto = new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                                Codigo = dr["CodigoProducto"].ToString(),
                                Nombre = dr["NombreProducto"].ToString(),
                                Descripcion = dr["DescripcionProducto"].ToString(),
                            },
                            oTienda = new Tienda()
                            {
                                IdTienda = Convert.ToInt32(dr["IdTienda"].ToString()),
                                RUC = dr["RUC"].ToString(),
                                Nombre = dr["NombreTienda"].ToString(),
                                Direccion = dr["DireccionTienda"].ToString(),
                            },
                            oListaPrecio = new ListaPreciosCliente()
                            {
                                PrecioCliente = Convert.ToDecimal(dr["PrecioCliente"].ToString(), new CultureInfo("es-PE")),
                            },
                            
                            PrecioUnidadCompra = Convert.ToDecimal(dr["PrecioUnidadCompra"].ToString(), new CultureInfo("es-PE")),
                            PrecioUnidadVenta = Convert.ToDecimal(dr["PrecioUnidadVenta"].ToString(), new CultureInfo("es-PE")),
                            Stock = Convert.ToInt32(dr["Stock"].ToString()),
                            Iniciado = Convert.ToBoolean(dr["Iniciado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaProductoCliente;

                }
                catch (Exception ex)
                {
                    rptListaProductoCliente= null;
                    return rptListaProductoCliente;
                }
            }
        }

    }
}
