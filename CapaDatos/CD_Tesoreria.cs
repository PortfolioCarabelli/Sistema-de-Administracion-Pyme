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
    public class CD_Tesoreria
    {
        public static CD_Tesoreria _instancia = null;

        private CD_Tesoreria()
        {

        }

        public static CD_Tesoreria Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Tesoreria();
                }
                return _instancia;
            }
        }
        public static List<Tesoreria> ObtenerDatosChart(DateTime FechaInicio, DateTime FechaFin)
        {
            List<Tesoreria> rptListaTesoreria = new List<Tesoreria>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerDatosTesoreria", oConexion);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaTesoreria.Add(new Tesoreria()
                        {
                            
                            IdTesoreria = 1,
                            Ventas = Convert.ToDecimal(dr["Ventas"].ToString()),
                            Compras = Convert.ToDecimal(dr["Compras"].ToString())
                        }) ;
                    }
                    dr.Close();

                    return rptListaTesoreria;

                }
                catch (Exception ex)
                {
                    rptListaTesoreria = null;
                    return rptListaTesoreria;
                }
            }
        }

    }
}
