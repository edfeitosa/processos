using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyEncryption;
using MySql.Data.MySqlClient;

namespace processos.Models
{
    public class UsuariosModels
    {
        public int Usu_id { get; set; }
        public string Usu_nome { get; set; }
        public string Usu_login { get; set; }
        public string Usu_senha { get; set; }
        public int Usu_status { get; set; }
        public string Action { get; set; }
        public string Pagination { get; set; }

        MySqlConnection db = new MySqlConnection(new ConnectionModels().Connection());

        private string Actions(UsuariosModels usuarios)
        {
            if (usuarios.Action == "create".ToString())
            {
                return "INSERT INTO tbl_usuarios (usu_nome, usu_login, usu_senha, usu_status) " +
                    "VALUES " +
                    "('" + usuarios.Usu_nome + "', " +
                    "'" + usuarios.Usu_login + "', " +
                    "'" + EasyEncryption.MD5.ComputeMD5Hash(usuarios.Usu_senha) + "', " +
                    usuarios.Usu_status + ")".ToString();
            }
            else if (usuarios.Action == "update".ToString())
            {
                return "UPDATE tbl_usuarios SET " +
                    "usu_nome = '" + usuarios.Usu_nome + "', " +
                    "usu_login = '" + usuarios.Usu_login + "', " +
                    "usu_status = " + usuarios.Usu_status + " " +
                    "WHERE usu_id = " + usuarios.Usu_id + "".ToString();
            }
            else if (usuarios.Action == "delete".ToString())
            {
                return "DELETE FROM tbl_usuarios WHERE usu_id = " + usuarios.Usu_id.ToString();
            }
            else
            {
                return "".ToString();
            }
        }

        public bool Crud(UsuariosModels usuarios)
        {
            MySqlCommand query = new MySqlCommand(this.Actions(usuarios), db);
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

        private string QueryListar(UsuariosModels usuarios)
        {
            if (usuarios.Usu_id == Convert.ToInt32(0) || usuarios.Action == "listar".ToString())
            {
                return "SELECT usu_id, usu_nome, usu_login, usu_senha, usu_status " +
                    "FROM tbl_usuarios ORDER BY usu_nome ASC " + usuarios.Pagination + "".ToString();
            }
            else if (usuarios.Action == "total".ToString())
            {
                return "SELECT usu_id FROM tbl_usuarios".ToString();
            }
            else
            {
                return "SELECT usu_id, usu_nome, usu_login, usu_senha, usu_status " +
                    "FROM tbl_usuarios WHERE usu_id = " + usuarios.Usu_id + "".ToString();
            }
        }

        public IEnumerable<UsuariosModels> Read(UsuariosModels usuarios)
        {
            MySqlCommand query = new MySqlCommand(this.QueryListar(usuarios), db);
            IList<UsuariosModels> lista = new List<UsuariosModels>();
            try
            {
                db.Open();
                MySqlDataReader itens = query.ExecuteReader();
                while (itens.Read())
                {
                    lista.Add(new UsuariosModels
                    {
                        Usu_id = Convert.ToInt32(itens["usu_id"]),
                        Usu_nome = itens["usu_nome"].ToString(),
                        Usu_login = itens["usu_login"].ToString(),
                        Usu_senha = itens["usu_senha"].ToString(),
                        Usu_status = Convert.ToInt32(itens["usu_status"])
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