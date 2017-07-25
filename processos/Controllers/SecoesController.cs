using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using processos.Models;

namespace processos.Controllers
{
    public class SecoesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.title = "Seções";
            return View();
        }

        public ActionResult Save()
        {
            ViewBag.title = "Adicionando/editando seções";
            return View();
        }

        [HttpPost]
        public ActionResult Save(SecoesModels secoes)
        {
            try
            {
                if (secoes.Sec_titulo == null)
                {
                    return Json(new
                    {
                        indice = "erro".ToString(),
                        titulo = "Campo vazio".ToString(),
                        mensagem = "Todos os campos marcados com <strong>(*)</strong> são de preehcimento obrigatório.",
                        url = "".ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (new SecoesModels().Crud(secoes) == false)
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
                    return Json(new
                    {
                        indice = "sucesso".ToString(),
                        titulo = "Sucesso".ToString(),
                        mensagem = "Dados adicionados com sucesso.".ToString(),
                        url = "/Secoes/Save".ToString()
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

        [HttpGet]
        public JsonResult Listar(SecoesModels secoes)
        {
            return Json(new SecoesModels().Listar(secoes), JsonRequestBehavior.AllowGet);
        }
    }
}