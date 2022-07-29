using Dislicores_Proceso_5_Terceros_a_ClientesPOS.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dislicores_Proceso_5_Terceros_a_ClientesPOS.Modulos
{
    public static class ModuloConsumoWSUNOEE
    {

        public static void cargarConector(string proceso, EstructuraClientesPOS objEnvio)
        {

            string plano = "";
            string strXML = "";
            short resultSiesa = 123;
            DataSet ds = new DataSet();
            string strMensaje = "";
            string identificador = ""; 

            try
            {

                identificador = objEnvio.F9740_ID;

                //Formmateo de datos
                objEnvio.F9740_ID = objEnvio.F9740_ID.Trim().PadRight(20, char.Parse(" "));
                objEnvio.F9740_NIT = objEnvio.F9740_NIT.Trim().PadRight(15, char.Parse(" "));
                objEnvio.F9740_ID_TIPO_IDENT = objEnvio.F9740_ID_TIPO_IDENT.Trim().PadRight(1);
                objEnvio.F9740_IND_TIPO_TERCERO = objEnvio.F9740_IND_TIPO_TERCERO.Trim().PadRight(1);
                objEnvio.F9740_RAZON_SOCIAL = objEnvio.F9740_RAZON_SOCIAL.Trim().PadRight(50, char.Parse(" "));
                objEnvio.F9740_APELLIDO_1 = objEnvio.F9740_APELLIDO_1.Trim().PadRight(15, char.Parse(" "));
                objEnvio.F9740_APELLIDO_2 = objEnvio.F9740_APELLIDO_2.Trim().PadRight(15, char.Parse(" "));
                objEnvio.F9740_NOMBRE = objEnvio.F9740_NOMBRE.Trim().PadRight(20, char.Parse(" "));
                objEnvio.F9740_FECHA_INGRESO = objEnvio.F9740_FECHA_INGRESO.Trim().PadRight(8);
                objEnvio.F9740_FECHA_NACIMIENTO = objEnvio.F9740_FECHA_NACIMIENTO.Trim().PadRight(8);
                objEnvio.F9740_CONTACTO = objEnvio.F9740_CONTACTO.Trim().PadRight(40, char.Parse(" "));
                objEnvio.F9740_DIRECCION1 = objEnvio.F9740_DIRECCION1.Trim().PadRight(50, char.Parse(" "));
                objEnvio.F9740_ID_DEPTO = objEnvio.F9740_ID_DEPTO.Trim().PadRight(2, char.Parse(" "));
                objEnvio.F9740_ID_CIUDAD = objEnvio.F9740_ID_CIUDAD.Trim().PadRight(3, char.Parse(" "));
                objEnvio.F9740_ID_BARRIO = objEnvio.F9740_ID_BARRIO.Trim().PadRight(40, char.Parse(" "));
                objEnvio.F9740_TELEFONO = objEnvio.F9740_TELEFONO.Trim().PadRight(20, char.Parse(" "));
                objEnvio.F9740_EMAIL = objEnvio.F9740_EMAIL.Trim().PadRight(50, char.Parse(" "));
                objEnvio.F9740_CELULAR = objEnvio.F9740_CELULAR.Trim().PadRight(20, char.Parse(" "));
                objEnvio.F9740_NOTAS = objEnvio.F9740_NOTAS.Trim().PadRight(255, char.Parse(" "));

                plano += "<Datos>";
                plano += "<Linea>000000100000001002</Linea>";
                plano += $"<Linea>0000002974000010021{objEnvio.F9740_ID}{objEnvio.F9740_NIT}0";
                plano += $"{objEnvio.F9740_ID_TIPO_IDENT}{objEnvio.F9740_IND_TIPO_TERCERO}";
                plano += $"{objEnvio.F9740_RAZON_SOCIAL}";
                plano += $"{objEnvio.F9740_APELLIDO_1}";
                plano += $"{objEnvio.F9740_APELLIDO_2}";
                plano += $"{objEnvio.F9740_NOMBRE}";
                plano += $"{objEnvio.F9740_FECHA_INGRESO}";
                plano += $"{objEnvio.F9740_FECHA_NACIMIENTO}";

                string temp = " ";
                temp = temp.Trim().PadRight(255, char.Parse(" "));
                plano += $"0{temp}";
                plano += $"{objEnvio.F9740_CONTACTO}";
                plano += $"{objEnvio.F9740_DIRECCION1}";

                temp = temp.Trim().PadRight(40, char.Parse(" "));
                plano += $"{temp}{temp}169";
                plano += $"{objEnvio.F9740_ID_DEPTO}";
                plano += $"{objEnvio.F9740_ID_CIUDAD}";
                plano += $"{objEnvio.F9740_ID_BARRIO}";
                plano += $"{objEnvio.F9740_TELEFONO}";

                temp = temp.Trim().PadRight(20, char.Parse(" "));
                plano += $"{temp}";
                temp = temp.Trim().PadRight(10, char.Parse(" "));
                plano += $"{temp}";

                plano += $"{objEnvio.F9740_EMAIL}";

                temp = temp.Trim().PadRight(8, char.Parse(" "));
                plano += $"{temp}";

                plano += $"{objEnvio.F9740_CELULAR}";
                plano += $"{objEnvio.F9740_NOTAS}";

                temp = temp.Trim().PadRight(35, char.Parse(" "));
                plano += $"{temp}01";

                plano += $"</Linea>"; //Criterio Cliente POS
                plano += "<Linea>000000399990001002</Linea>";
                plano += "</Datos>";

                strXML += "<Importar>" + Environment.NewLine;
                strXML += $"<NombreConexion>{Properties.Settings.Default.ConexionUNOEE}</NombreConexion>" + Environment.NewLine;
                strXML += $"<IdCia>{Properties.Settings.Default.CiaUNOEE}</IdCia>" + Environment.NewLine;
                strXML += $"<Usuario>{Properties.Settings.Default.UsuarioUNOEE}</Usuario>" + Environment.NewLine;
                strXML += $"<Clave>{Properties.Settings.Default.ClaveUNOEE}</Clave>" + Environment.NewLine;
                strXML += plano;
                strXML += "</Importar>" + Environment.NewLine;


                WSUNOEE.WSUNOEE objUNOEE = new WSUNOEE.WSUNOEE();

                objUNOEE.Timeout = 18000000;
                ds = objUNOEE.ImportarXML(strXML, ref resultSiesa);

                switch (resultSiesa)
                {
                    case 0:
                        strMensaje = identificador + "-" + proceso + " : Importacion Exitosa ";
                        break;
                    case 1:
                        strMensaje = identificador + "-" + proceso + " : Error : 1 - Error de datos al cargar la informacion a siesa a Siesa " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 2:
                        strMensaje = identificador + "-" + proceso + " : Error : 2 - El impodatos no envio algun parametro " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 3:
                        strMensaje = identificador + "-" + proceso + " : Error :  3 - El usuario o la contraseña que ingreso no son validos " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 4:
                        strMensaje = identificador + "-" + proceso + " : Error : 4 - La version del impodatos no se corresponde con la version del ERP o el impodatos esta en una maquina que no tiene cliente Siesa " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 5:
                        strMensaje = identificador + "-" + proceso + " : Error :  5 - La base de datos no existe o están ingresándole un parámetro erróneo a la hora de especificar la conexión. " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 6:
                        strMensaje = identificador + "-" + proceso + " : Error : 6 - El archivo que se está especificando en la ruta de los parámetros del .BAT no existe " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 7:
                        strMensaje = identificador + "-" + proceso + "  Error :  7 - El archivo que se está especificando en la ruta de los parámetros del .BAT no es valido " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 8:
                        strMensaje = identificador + "-" + proceso + " : Error : 8 - Hay un problema con la tabla en la base de datos donde se ingresaran los archivos " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 9:
                        strMensaje = identificador + "-" + proceso + " : Error :  9 - La compañía que se ingresó en los parámetros del .BAT no es valida " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 10:
                        strMensaje = identificador + "-" + proceso + " : Error : 10 - Error desconocido " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 99:
                        strMensaje = identificador + "-" + proceso + "Error : 99 - Error de tipo diferente a los anteriores, normalmente de permisos a nivel del ERP " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                }

                if(resultSiesa == 0)
                {
                    OperacionesSQL.sp_Proceso_5_Log_Guardar(false, strMensaje, identificador, resultSiesa.ToString(), strXML);
                }
                else
                {
                    OperacionesSQL.sp_Proceso_5_Log_Guardar(true, strMensaje, identificador, resultSiesa.ToString(), strXML);
                }               
               
            }
            catch (Exception ex)
            {
                string detalle = "Error Paso 2 guardando datos en SQL : " + ex.Message;
                OperacionesSQL.sp_Proceso_5_Log_Guardar(true, detalle, identificador, resultSiesa.ToString(), strXML);                
            }

        }


    }
}
