using Dislicores_Proceso_5_Terceros_a_ClientesPOS.Modelos;
using Dislicores_Proceso_5_Terceros_a_ClientesPOS.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dislicores_Proceso_5_Terceros_a_ClientesPOS
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            try
            {
                ds = OperacionesSQL.sp_Proceso_5_Terceros_a_ClientesPOS();

                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow registro in ds.Tables[0].Rows)
                    {

                        EstructuraClientesPOS objEnvio = new EstructuraClientesPOS();

                        objEnvio.F_CIA = registro["F_CIA"].ToString();
                        objEnvio.F9740_ID = registro["F9740_ID"].ToString();
                        objEnvio.F9740_NIT = registro["F9740_NIT"].ToString();
                        objEnvio.F9740_ID_TIPO_IDENT = registro["F9740_ID_TIPO_IDENT"].ToString();
                        objEnvio.F9740_IND_TIPO_TERCERO = registro["F9740_IND_TIPO_TERCERO"].ToString();
                        objEnvio.F9740_RAZON_SOCIAL = registro["F9740_RAZON_SOCIAL"].ToString();
                        objEnvio.F9740_APELLIDO_1 = registro["F9740_APELLIDO_1"].ToString();
                        objEnvio.F9740_APELLIDO_2 = registro["F9740_APELLIDO_2"].ToString();
                        objEnvio.F9740_NOMBRE = registro["F9740_NOMBRE"].ToString();
                        objEnvio.F9740_FECHA_INGRESO = DateTime.Parse(registro["F9740_FECHA_INGRESO"].ToString()).ToString("yyyyMMdd");
                        objEnvio.F9740_FECHA_NACIMIENTO = DateTime.Parse(registro["F9740_FECHA_NACIMIENTO"].ToString()).ToString("yyyyMMdd");
                        objEnvio.F9740_CONTACTO = registro["F9740_CONTACTO"].ToString();
                        objEnvio.F9740_DIRECCION1 = registro["F9740_DIRECCION1"].ToString();
                        objEnvio.F9740_ID_DEPTO = registro["F9740_ID_DEPTO"].ToString();
                        objEnvio.F9740_ID_CIUDAD = registro["F9740_ID_CIUDAD"].ToString();
                        objEnvio.F9740_ID_BARRIO = registro["F9740_ID_BARRIO"].ToString();
                        objEnvio.F9740_TELEFONO = registro["F9740_TELEFONO"].ToString();
                        objEnvio.F9740_EMAIL = registro["F9740_EMAIL"].ToString();
                        objEnvio.F9740_CELULAR = registro["F9740_CELULAR"].ToString();
                        objEnvio.F9740_NOTAS = registro["F9740_NOTAS"].ToString();

                        ModuloConsumoWSUNOEE.cargarConector("Importacion a Siesa", objEnvio);

                    }

                }

            }
            catch (Exception ex)
            {
                OperacionesSQL.sp_Proceso_5_Log_Guardar(true, ex.Message);
            }

            this.Close();

        }


    }
}
