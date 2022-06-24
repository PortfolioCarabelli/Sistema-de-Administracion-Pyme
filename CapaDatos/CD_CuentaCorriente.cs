using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CapaDatos
{
    public class CD_CuentaCorriente
    {
      
            public static CD_CuentaCorriente _instancia = null;

            private CD_CuentaCorriente()
            {

            }

            public static CD_CuentaCorriente Instancia
            {
                get
                {
                    if (_instancia == null)
                    {
                        _instancia = new CD_CuentaCorriente();
                    }
                    return _instancia;
                }
            }
        public List<CuentaCorriente> ObtenerCuentaCorriente()
        {
            List<CuentaCorriente> rptListaCuentaCorriente = new List<CuentaCorriente>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerCuentaCorriente", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaCuentaCorriente.Add(new CuentaCorriente()
                        {
                            IdCuentaCorriente = Convert.ToInt32(dr["IdCuentaCorriente"].ToString()),
                            Monto = Convert.ToDecimal((dr["Monto"].ToString())),
                        });
                    }
                    dr.Close();

                    return rptListaCuentaCorriente;

                }
                catch (Exception ex)
                {
                    rptListaCuentaCorriente = null;
                    return rptListaCuentaCorriente;
                }
            }
        }





    }
}
