using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace processos.Models
{
    public class ArquivosModels
    {
        public int Arq_id { get; set; }
        public string Arq_conteudo { get; set; }
        public string Action { get; set; }

        MySqlConnection db = new MySqlConnection(new ConnectionModels().Connection());

        private string Actions(ArquivosModels arquivos)
        {
            if (arquivos.Action == "create".ToString())
            {
                return "INSERT INTO tbl_arquivos (arq_conteudo) VALUES ('" + arquivos.Arq_conteudo + "')".ToString();
            }
            else if (arquivos.Action == "read".ToString())
            {
                return "".ToString();
            }
            else if (arquivos.Action == "update".ToString())
            {
                return "".ToString();
            }
            else if (arquivos.Action == "delete".ToString())
            {
                return "".ToString();
            }
            else
            {
                return "".ToString();
            }
        }

        public bool Crud(ArquivosModels arquivos)
        {
            MySqlCommand query = new MySqlCommand(this.Actions(arquivos), db);
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