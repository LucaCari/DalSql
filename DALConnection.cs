using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    class DALConnection
    {
        // DATA ACCESS LAYER ou CAMADA DE ACESSO A DADOS

        private string string_connection = "persist security info=false; server=localHost; database=sistemafinanceiro; uid=root; pwd=;";
        private MySqlConnection connection;

        private void Connection()
        {
            try
            {
                connection = new MySqlConnection(string_connection);
                connection.Open();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Conexão não foi estabelecida. Verifique.\n" + ex.Message);
            }
        }

        public void executeCommand(string sql, string[] values) 
        {
            try
            {
                Connection();
                MySqlCommand command = new MySqlCommand(sql, connection);
                for (int i = 0; i<values.Length;i++)
                {
                    command.Parameters.AddWithValue("val"+i,values[i]);
                }
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Instrução incorreta. Verifique.\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void executeCommand(string sql, string[] values, string[] conditions) 
        {
            try
            {
                Connection();
                MySqlCommand command = new MySqlCommand(sql, connection);
                for (int i = 0; i < values.Length; i++)
                {
                    command.Parameters.AddWithValue("val" + i, values[i]);
                }
                for (int i = 0; i < conditions.Length; i++)
                {
                    command.Parameters.AddWithValue("con" + i, conditions[i]);
                }
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Instrução incorreta. Verifique.\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable executeConsult(string sql) //select
        {
            try
            {
                Connection();
                MySqlDataAdapter consulta = new MySqlDataAdapter(sql, connection);
                DataTable query = new DataTable();
                consulta.Fill(query);
                return query;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Instrução incorreta. Verifique.\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable executeConsult(string sql, string[] values) //select
        {
            try
            {
                Connection();
                MySqlCommand command = new MySqlCommand(sql, connection);
                for (int i = 0; i < values.Length; i++)
                {
                    command.Parameters.AddWithValue("val" + i, values[i]);
                }
                command.Prepare();
                command.CommandType = CommandType.Text;
                MySqlDataAdapter consulta = new MySqlDataAdapter(command);
                DataTable query = new DataTable();
                consulta.Fill(query);
                return query;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Instrução incorreta. Verifique.\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
