using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CapaDatos
{
    public class CD_Cobro
    {
        public static CD_Cobro _instancia = null;

        private CD_Cobro()
        {

        }

        public static CD_Cobro Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Cobro();
                }
                return _instancia;
            }
        }

        public int RegistrarCobro(string Detalle)
        {
            int respuesta = 0;
         
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarCobro", oConexion);
                    cmd.Parameters.Add("Detalle", SqlDbType.Xml).Value = Detalle;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                  
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);


                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }
        public List<Cobro> ObtenerListaCobro( DateTime FechaInicio, DateTime FechaFin, string NumeroDocumento, string Nombre)
        {
            List<Cobro> rptListaVenta = new List<Cobro>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerListaCobro", oConexion);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.Parameters.AddWithValue("@NumeroDocumento", NumeroDocumento);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaVenta.Add(new Cobro()
                        {
                            IdCobro = Convert.ToInt32(dr["IdCobro"].ToString()),
                                                     
                            FechaCobro = Convert.ToDateTime(dr["FechaCobro"].ToString()).ToString("dd/MM/yyyy"),
                            VFechaCobro = Convert.ToDateTime(dr["FechaCobro"].ToString()),
                            oCliente = new Cliente() { NumeroDocumento = dr["NumeroDocumento"].ToString(), Nombre = dr["Nombre"].ToString() },
                            TotalCosto = float.Parse(dr["MontoCobrado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaVenta;

                }
                catch (Exception ex)
                {
                    rptListaVenta = null;
                    return rptListaVenta;
                }
            }
        }
    }
}
