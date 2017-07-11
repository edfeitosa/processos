using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using processos.Models;

namespace processos.Controllers
{
    public class SeparadoresController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.title = "Separadores";
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.title = "Novo Separador";
            return View();
        }

        [HttpPost]
        public ActionResult Create(SeparadoresModels Separadores)
        {
            try
            {
                if (Separadores.Sep_titulo == null ||
                    Separadores.Sep_separador == null)
                {
                    return Json(new
                    {
                        indice = "alerta".ToString(),
                        titulo = "Campos vazios".ToString(),
                        mensagem = "Todos os campos marcados com <b>(*)</b> precisam ser preenchidos.".ToString(),
                        url = "".ToString()
                    }, JsonRequestBehavior.AllowGet);
                } else {
                    return Json(new
                    {
                        indice = "sucesso".ToString(),
                        titulo = "Sucesso".ToString(),
                        mensagem = "As informações foram salvas com sucesso.".ToString(),
                        url = "/Separadores/Create".ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    indice = "falha".ToString(),
                    titulo = "Ocorreu um erro".ToString(),
                    mensagem = "Erro: " + ex.ToString(),
                    url = "".ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}