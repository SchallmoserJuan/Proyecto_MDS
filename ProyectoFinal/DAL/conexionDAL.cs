﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // Trabajar con SQL
using MySql.Data.MySqlClient;

namespace ProyectoFinal.DAL
{
    internal class conexionDAL
    {
        private string CadenaConexion = "server=localhost;port=3306;database=dbsistema;user=root;password=admin;";
        MySqlConnection Conexion;

        public MySqlConnection EstablecerConexion()
        {
             this.Conexion = new MySqlConnection(this.CadenaConexion);
             return this.Conexion;
        }


        //Creacion de metodo
        /* Metodo (INSERT, DELETE, UPDATE) */
        public bool ejecutarComandoSinRetornoDatos(string strComando){
            //Test para probar la conexion
            try {
                
                MySqlCommand Comando = new MySqlCommand();

                Comando.CommandText = strComando;
                Comando.Connection = this.EstablecerConexion();
                Conexion.Open();
                Comando.ExecuteNonQuery(); //
                Conexion.Close();

                return true;

            } catch
            {
                return false;
            }
        }

        //Sobrecarga
        public bool ejecutarComandoSinRetornoDatos(MySqlCommand MYSQLComando)
        {
            //Test para probar la conexion
            try
            {

                MySqlCommand Comando = MYSQLComando;
                Comando.Connection = this.EstablecerConexion();
                Conexion.Open();
                Comando.ExecuteNonQuery(); //
                Conexion.Close();

                return true;

            }
            catch
            {
                return false;
            }
        }

        /* select ( retorno datos ) */
        public DataSet EjecutarSentencia(MySqlCommand mysqlComando)
        {
            DataSet DS = new DataSet();
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();

            try {
                MySqlCommand Comando = new MySqlCommand();
                Comando = mysqlComando;
                Comando.Connection = EstablecerConexion();
                Adaptador.SelectCommand = Comando;
                Conexion.Open();
                Adaptador.Fill(DS);
                Conexion.Close();
                return DS;
            }
            catch {
                return DS;
            }
        }

    }
}
