using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using processos.Models;

namespace processos.Controllers
{
    public class ArquivosController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.title = "Arquivos";
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.title = "Novo Arquivo";
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArquivosModels arquivos)
        {
            try
            {
                string separador = arquivos.Arq_conteudo.Replace("\n\n \n", "#");
                string[] dados = separador.Split('#');
                for(int i = 0; i < dados.Count(); i++)
                {
                    ArquivosModels _arquivos = new ArquivosModels();
                    _arquivos.Arq_conteudo = dados[i].ToString();
                    _arquivos.Action = "create";
                    new ArquivosModels().Crud(_arquivos);
                }
                return Json(new
                {
                    indice = "sucesso".ToString(),
                    titulo = "Sucesso".ToString(),
                    mensagem = "Dados adicionados com sucesso".ToString(),
                    url = "/Arquivos/Create".ToString()
                }, JsonRequestBehavior.AllowGet);
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
    }
}