using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dislicores_Proceso_5_Terceros_a_ClientesPOS.Modulos
{
    public static class OperacionesSQL
    {

        public static DataSet sp_Proceso_5_Terceros_a_ClientesPOS()
        {
            SqlConnection conexionSQL = new SqlConnection(Properties.Settings.Default.strConexion);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter();
            DataSet ds = new DataSet();

            comandoSQL.CommandTimeout = 0;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandType = CommandType.StoredProcedure;
            comandoSQL.CommandText = "sp_Proceso_5_Terceros_a_ClientesPOS";

            try
            {

                //comandoSQL.Parameters.AddWithValue("@isError", datos.isError);
                //comandoSQL.Parameters.AddWithValue("@tipo", datos.type);
                //comandoSQL.Parameters.AddWithValue("@message", datos.message);

                adaptadorSQL.SelectCommand = comandoSQL;
                adaptadorSQL.Fill(ds);

            }
            catch (Exception ex)
            {
                //objLog.bitError = true;
                //objLog.detalle = "Error Paso 2 guardando datos en SQL : " + ex.Message;
                //SQL_DTO.sp_Proceso_4_Log_Guardar(objLog);
            }
            finally
            {
                comandoSQL.Parameters.Clear();
                comandoSQL.Connection.Close();
            }

            return ds;

        }

        public static void sp_Proceso_5_Log_Guardar(bool bitError,string detalle, string idTercero= "", string resultadoUNOEE = "",string datosEnviados = "")
        {
            SqlConnection conexionSQL = new SqlConnection(Properties.Settings.Default.strConexion);
            SqlCommand comandoSQL = new SqlCommand();

            comandoSQL.CommandTimeout = 0;
            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandType = CommandType.StoredProcedure;
            comandoSQL.CommandText = "sp_Proceso_5_Log_Guardar";

            try
            {

                comandoSQL.Parameters.AddWithValue("@idTercero", idTercero);
                comandoSQL.Parameters.AddWithValue("@bitError", bitError);
                comandoSQL.Parameters.AddWithValue("@resultadoUNOEE", resultadoUNOEE);            
                comandoSQL.Parameters.AddWithValue("@detalle", detalle);
                comandoSQL.Parameters.AddWithValue("@datosEnviados", datosEnviados);

                comandoSQL.Connection.Open();
                comandoSQL.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                comandoSQL.Connection.Close();
            }

        }

    }
}
