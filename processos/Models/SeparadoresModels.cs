using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace processos.Models
{
    public class SeparadoresModels
    {
        public int Sep_id { get; set; }
        public string Sep_titulo { get; set; }
        public string Sep_separador { get; set; }
        public string Action { get; set; }

        MySqlConnection db = new MySqlConnection(new ConnectionModels().Connection());

        private string Actions(SeparadoresModels separadores)
        {
            if (separadores.Action == "create".ToString())
            {
                return "INSERT INTO tbl_separadores (sep_titulo, sep_separador) " +
                    "VALUES ('" + separadores.Sep_titulo + "', '" + separadores.Sep_separador + "')".ToString();
            }
            else if (separadores.Action == "read".ToString())
            {
                return "".ToString();
            }
            else if (separadores.Action == "update".ToString())
            {
                return "".ToString();
            }
            else if (separadores.Action == "delete".ToString())
            {
                return "".ToString();
            }
            else
            {
                return "".ToString();
            }
        }

        public bool Crud(SeparadoresModels separadores)
        {
            MySqlCommand query = new MySqlCommand(this.Actions(separadores), db);
            try
            {
                db.Open();
                if (Convert.ToInt32(query.ExecuteNonQuery()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }
}