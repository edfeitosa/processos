using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using processos.Models;

namespace processos.Controllers
{
    public class UsuariosController : Controller
    {
        // página inicial
        public ActionResult Index()
        {
            ViewBag.title = "Usuários";
            return View();
        }

        // lista dados
        [HttpGet]
        public JsonResult Read(UsuariosModels usuarios)
        {
            return Json(new UsuariosModels().Read(usuarios), JsonRequestBehavior.AllowGet);
        }

        // adiciona/edita dados
        public ActionResult Save()
        {
            ViewBag.title = "Adicionando/editando usuários";
            return View();
        }

        // salva dados
        [HttpPost]
        public ActionResult Save(UsuariosModels usuarios)
        {
            try
            {
                if (usuarios.Usu_nome == null || usuarios.Usu_login == null || usuarios.Usu_senha == null)
                {
                    return Json(new
                    {
                        indice = "erro".ToString(),
                        titulo = "Campo vazio".ToString(),
                        mensagem = "Todos os campos marcados com <strong>(*)</strong> são de preehcimento obrigatório.",
                        url = "".ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (new UsuariosModels().Crud(usuarios) == false)
                {
                    return Json(new
                    {
                        indice = "erro".ToString(),
                        titulo = "Não salvou".ToString(),
                        mensagem = "Ocorreu um erro no momento de salvar os dados. O sistema não pôde continuar.",
                        url = "".ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string url;
                    if (usuarios.Usu_id == 0)
                    {
                        url = "/Usuarios/Save".ToString();
                    }
                    else
                    {
                        url = "/Usuarios/Save?id=" + usuarios.Usu_id.ToString();
                    }
                    return Json(new
                    {
                        indice = "sucesso".ToString(),
                        titulo = "Sucesso".ToString(),
                        mensagem = "Dados adicionados com sucesso.".ToString(),
                        url = url
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    indice = "erro".ToString(),
                    titulo = "Ocorreu um erro".ToString(),
                    mensagem = "Erro: " + ex.ToString(),
                    url = "".ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // delete
        [HttpPost]
        public JsonResult Delete(UsuariosModels usuarios)
        {
            return Json(new UsuariosModels().Crud(usuarios), JsonRequestBehavior.AllowGet);
        }
    }
}