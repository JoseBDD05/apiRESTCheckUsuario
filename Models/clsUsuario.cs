using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// -----------------------------
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;


namespace apiRESTCheckUsuario.Models
{
    public class clsUsuario
    {
        // Definición de atributos
        public string cve { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string ruta { get; set; }
        public string tipo { get; set; }


        // Metodos y atributos de funcionalidad y seguridad
        string cadConn = ConfigurationManager.ConnectionStrings["bdControl_Acceso"].ConnectionString;
        // Constructores
        public clsUsuario()
        {
            //Codigo pendiente .....
        }
        public clsUsuario(string usuario, string contrasena)
        {
            this.usuario = usuario;
            this.contrasena = contrasena;
        }
        public clsUsuario(string nombre, string apellidoPaterno,
                          string apellidoMaterno, string usuario,
                          string contrasena, string ruta, string tipo)
        {
            this.nombre = nombre;
            this.apellidoPaterno = apellidoPaterno;
            this.apellidoMaterno = apellidoMaterno;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.ruta = ruta;
            this.tipo = tipo;
        }
        //  Metodo para la ejecucion del spInsUsuario
        public DataSet spInsUsuario(){

            string cadSql = "CALL spInsUsuario('" + this.nombre +
                                               "', '" + this.apellidoPaterno +
                                               "', '" + this.apellidoMaterno +
                                               "', '" + this.usuario +
                                               "', '" + this.contrasena +
                                               "', '" + this.ruta +
                                               "', " + this.tipo + ")";
            // Configuracion de los objetos de conexion a datos
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecucion del Adaptador de Datos (retorna un DataSet)
            da.Fill(ds, "spInsUsuario");
            // Retorno de los datos recibidos
            return ds;
        }
        // Proceso de validación de usuarios (spValidarAcceso)
        public DataSet spValidarAcceso()
        {
            // Crear el comando SQL
            string cadSQL = "";
            cadSQL = "call spValidarAcceso('" + this.usuario + "','"
                                              + this.contrasena + "');";
            // Configuración de objetos de conexión
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cnn);
            DataSet ds = new DataSet();
            // Ejecución y salida
            da.Fill(ds, "spValidarAcceso");
            return ds;
        }

        // Proceso de validación de usuarios (vwRptUsuario)

        // clsUsuario.cs
        public DataSet vwRptUsuario(string filtro)
        {
            string cadSQL = "SELECT * FROM vwRptUsuario";
            if (!string.IsNullOrEmpty(filtro))
            {
                cadSQL += " WHERE nombre LIKE '%" + filtro + "%'";
            }
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "vwRptUsuario");
            return ds;
        }

        public DataSet vwTipoUsuario()
        {
            string cadSQL = "SELECT * FROM control_acceso.vwtipousuario";
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "vwtipousuario");
            return ds;
        }


    }

}