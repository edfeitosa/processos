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
        public int Sec_id { get; set; }
        public string Sec_titulo { get; set; }
        public string Arq_diario { get; set; }
        public string Arq_dtDiario { get; set; }
        public string Arq_dtSessao { get; set; }
        public string Arq_dtExpediente { get; set; }
        public string Arq_titulo { get; set; }
        public string Arq_conteudo { get; set; }
        public string Arq_data { get; set; }
        public string Action { get; set; }
        public string Pagination { get; set; }

        MySqlConnection db = new MySqlConnection(new ConnectionModels().Connection());

        private string Actions(ArquivosModels arquivos)
        {
            if (arquivos.Action == "create".ToString())
            {
                return "INSERT INTO tbl_arquivos " +
                    "(sec_id, arq_diario, arq_dtDiario, arq_dtSessao, arq_dtExpediente, arq_titulo, arq_conteudo) VALUES (" +
                    arquivos.Sec_id + ", " +
                    "'" + arquivos.Arq_diario + "'," +
                    "DATE_FORMAT(STR_TO_DATE('" + arquivos.Arq_dtDiario + "', '%d/%m/%Y'), '%Y-%m-%d'), " +
                    "DATE_FORMAT(STR_TO_DATE('" + arquivos.Arq_dtSessao + "', '%d/%m/%Y'), '%Y-%m-%d'), " +
                    "DATE_FORMAT(STR_TO_DATE('" + arquivos.Arq_dtExpediente + "', '%d/%m/%Y'), '%Y-%m-%d'), " +
                    "'" + arquivos.Arq_titulo + "', " +
                    "'" + arquivos.Arq_conteudo + "')".ToString();
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

        private string QueryListar(ArquivosModels arquivos)
        {
            if (arquivos.Sec_id == Convert.ToInt32(0) || arquivos.Action == "listar".ToString())
            {
                return "SELECT a.arq_id, a.arq_diario, " +
                    "DATE_FORMAT(STR_TO_DATE(a.arq_dtDiario, '%Y-%m-%d'), '%d/%m/%Y') AS dtDiario, " +
                    "DATE_FORMAT(STR_TO_DATE(a.arq_data, '%Y-%m-%d'), '%d/%m/%Y') AS data, " +
                    "a.arq_titulo, b.sec_id, b.sec_titulo " +
                    "FROM tbl_arquivos a LEFT JOIN tbl_secoes b ON b.sec_id = a.sec_id " +
                    "GROUP BY a.arq_id ORDER BY a.arq_id DESC " + arquivos.Pagination + "".ToString();
            }
            else if (arquivos.Action == "total".ToString())
            {
                return "SELECT sec_id FROM tbl_secoes".ToString();
            }
            else
            {
                return "SELECT sec_id, sec_titulo " +
                    "FROM tbl_secoes WHERE sec_id = " + arquivos.Arq_id + "".ToString();
            }
        }

        public IEnumerable<ArquivosModels> Read(ArquivosModels arquivos)
        {
            MySqlCommand query = new MySqlCommand(this.QueryListar(arquivos), db);
            IList<ArquivosModels> lista = new List<ArquivosModels>();
            try
            {
                db.Open();
                MySqlDataReader itens = query.ExecuteReader();
                while (itens.Read())
                {
                    lista.Add(new ArquivosModels
                    {
                        Arq_id = Convert.ToInt32(itens["arq_id"]),
                        Arq_diario = itens["arq_diario"].ToString(),
                        Arq_dtDiario = itens["dtDiario"].ToString(),
                        Arq_data = itens["data"].ToString(),
                        Arq_titulo = itens["arq_titulo"].ToString(),
                        Sec_id = Convert.ToInt32(itens["sec_id"]),
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