using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using processos.Models;

namespace processos.Controllers
{
    public class HomeController : Controller
    {
        // página inicial
        public ActionResult Index()
        {
            if (Session["Usu_nome"] == null)
            {
                return RedirectToAction("Login", "Home", new { log = "nao" });
            }
            else
            {
                ViewBag.title = "Página Inicial";
                return View();
            }
        }

        // login
        public ActionResult Login()
        {
            if (Session["Usu_nome"] == null)
            {
                ViewBag.title = "Faça seu login";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Login (UsuariosModels usuarios)
        {
            try
            {
                if (string.IsNullOrEmpty(usuarios.Usu_login) ||
                    string.IsNullOrEmpty(usuarios.Usu_senha))
                {
                    return Json(new
                    {
                        indice = "erro".ToString(),
                        titulo = "Campo vazio".ToString(),
                        mensagem = "Todos os campos marcados com <strong>(*)</strong> são de preehcimento obrigatório.",
                        url = "".ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var login = new UsuariosModels().Login(usuarios);
                    if (login.Count() == 0)
                    {
                        return Json(new
                        {
                            indice = "erro".ToString(),
                            titulo = "Nenhum usuário".ToString(),
                            mensagem = "Nenhum usuário foi encontrado. Verifique os dados informados e tente novamente",
                            url = "".ToString()
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        foreach (var item in login)
                        {
                            Session["Usu_nome"] = item.Usu_nome.ToString();
                        }
                        return Json(new
                        {
                            indice = "sucesso",
                            url = "/Home"
                        }, JsonRequestBehavior.AllowGet);
                    }
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

        // logout
        [HttpPost]
        public ActionResult Logout(UsuariosModels usuarios)
        {
            try
            {
                if (usuarios.Action == "sair".ToString())
                {
                    Session.Abandon();
                    return Json(new
                    {
                        indice = "sucesso".ToString(),
                        url = "/Home/Login".ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        indice = "erro",
                        titulo = "Ocorreu um erro",
                        mensagem = "Não foi possível completar seu pedido. Tente novamente".ToString(),
                        url = "".ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    indice = "erro",
                    titulo = "Ocorreu um erro",
                    mensagem = ex.ToString(),
                    url = "".ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}