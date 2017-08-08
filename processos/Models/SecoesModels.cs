using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace processos.Models
{
    public class SecoesModels
    {
        public int Sec_id { get; set; }
        public int Sec_idPai { get; set; }
        public string Sec_titulo { get; set; }
        public string Action { get; set; }
        public int Total { get; set; }
        public string Pagination { get; set; }

        MySqlConnection db = new MySqlConnection(new ConnectionModels().Connection());

        private string Actions(SecoesModels secoes)
        {
            if (secoes.Action == "create".ToString())
            {
                return "INSERT INTO tbl_secoes (sec_idPai, sec_titulo) VALUES (" + secoes.Sec_idPai + ", '" + secoes.Sec_titulo + "')".ToString();
            }
            else if (secoes.Action == "update".ToString())
            {
                return "UPDATE tbl_secoes SET sec_idPai = " + secoes.Sec_idPai + ", sec_titulo = '" + secoes.Sec_titulo + "' " +
                    "WHERE sec_id = " + secoes.Sec_id + "".ToString();
            }
            else if (secoes.Action == "delete".ToString())
            {
                return "".ToString();
            }
            else
            {
                return "".ToString();
            }
        }

        public bool Crud(SecoesModels secoes)
        {
            MySqlCommand query = new MySqlCommand(this.Actions(secoes), db);
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

        private string QueryListar(SecoesModels secoes)
        {
            if (secoes.Sec_id == Convert.ToInt32(0) || secoes.Action == "listar".ToString())
            {
                return "SELECT sec_id, sec_idPai, sec_titulo " +
                    "FROM tbl_secoes ORDER BY sec_titulo ASC " + secoes.Pagination + "".ToString();
            }
            else if (secoes.Action == "total".ToString())
            {
                return "SELECT sec_id FROM tbl_secoes".ToString();
            }
            else
            {
                return "SELECT sec_id, sec_idPai, sec_titulo " +
                    "FROM tbl_secoes WHERE sec_id = " + secoes.Sec_id + "".ToString();
            }
        }

        public IEnumerable<SecoesModels> Read(SecoesModels secoes)
        {
            MySqlCommand query = new MySqlCommand(this.QueryListar(secoes), db);
            IList<SecoesModels> lista = new List<SecoesModels>();
            try
            {
                db.Open();
                MySqlDataReader itens = query.ExecuteReader();
                while (itens.Read())
                {
                    lista.Add(new SecoesModels
                    {
                        Sec_id = Convert.ToInt32(itens["sec_id"]),
                        Sec_idPai = Convert.ToInt32(itens["sec_idPai"]),
                        Sec_titulo = itens["sec_titulo"].ToString()
                    });
                }
                return lista;
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