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
        // index
        public ActionResult Index()
        {
            if (Session["Usu_nome"] == null)
            {
                return RedirectToAction("Login", "Home", new { log = "nao" });
            }
            else
            {
                ViewBag.title = "Arquivos";
                return View();
            }
        }

        // inserir/editar
        public ActionResult Save()
        {
            if (Session["Usu_nome"] == null)
            {
                return RedirectToAction("Login", "Home", new { log = "nao" });
            }
            else
            {
                ViewBag.title = "Arquivos";
                return View();
            }
        }

        // detalhes
        public ActionResult Details()
        {
            if (Session["Usu_nome"] == null)
            {
                return RedirectToAction("Login", "Home", new { log = "nao" });
            }
            else
            {
                ViewBag.title = "Detalhe de processo";
                return View();
            }
        }

        // lista dados
        [HttpGet]
        public JsonResult Read(ArquivosModels arquivos)
        {
            return Json(new ArquivosModels().Read(arquivos), JsonRequestBehavior.AllowGet);
        }

        // lista get por id
        [HttpGet]
        public JsonResult ReadGet(ArquivosModels arquivos)
        {
            return Json(new ArquivosModels().ReadGet(arquivos), JsonRequestBehavior.AllowGet);
        }

        // delete
        [HttpPost]
        public JsonResult Delete(ArquivosModels arquivos)
        {
            return Json(new ArquivosModels().Crud(arquivos), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(ArquivosModels arquivos)
        {
            try
            {
                if (arquivos.Sec_id == Convert.ToInt32("0") ||
                    string.IsNullOrEmpty(arquivos.Arq_diario) || 
                    string.IsNullOrEmpty(arquivos.Arq_dtDiario) || 
                    string.IsNullOrEmpty(arquivos.Arq_conteudo) 
                ){
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
                    string separador = arquivos.Arq_conteudo.Replace("\n\n \n", "#");
                    string[] dados = separador.Split('#');
                    for (int i = 0; i < dados.Count(); i++)
                    {
                        // prepara título
                        string separadorTit = dados[i].Replace("\n", "#");
                        string[] dadosTit = separadorTit.Split('#');
                        ArquivosModels _arquivos = new ArquivosModels();
                        _arquivos.Sec_id = arquivos.Sec_id;
                        _arquivos.Arq_diario = arquivos.Arq_diario;
                        _arquivos.Arq_pauta = arquivos.Arq_pauta;
                        _arquivos.Arq_dtDiario = arquivos.Arq_dtDiario;
                        _arquivos.Arq_dtSessao = arquivos.Arq_dtSessao;
                        _arquivos.Arq_dtExpediente = arquivos.Arq_dtExpediente;
                        _arquivos.Arq_titulo = dadosTit[0];
                        _arquivos.Arq_conteudo = dados[i].ToString();
                        _arquivos.Action = arquivos.Action;
                        new ArquivosModels().Crud(_arquivos);
                    }
                    string url;
                    if (arquivos.Arq_id == 0)
                    {
                        url = "/Arquivos/Save".ToString();
                    }
                    else
                    {
                        url = "/Arquivos/Save?id=" + arquivos.Arq_id.ToString();
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

        // busca
        public ActionResult Buscar()
        {
            if (Session["Usu_nome"] == null)
            {
                return RedirectToAction("Login", "Home", new { log = "nao" });
            }
            else
            {
                ViewBag.title = "Busca por processos";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Buscar(ArquivosModels arquivos)
        {
            ViewBag.title = "Busca por processos";
            if (Request.QueryString["buscar"] != null)
            {
                ArquivosModels _arquivos = new ArquivosModels();
                _arquivos.Arq_titulo = Request.QueryString["titulo"];
                _arquivos.Arq_conteudo = Request.QueryString["titulo"];
                _arquivos.Sec_id = Convert.ToInt32(Request.QueryString["secao"]);
                _arquivos.Arq_diario = Request.QueryString["diario"];
                _arquivos.Action = Request.QueryString["action"];
                return View(new ArquivosModels().Search(_arquivos));
            }
            else
            {
                return View();
            }
        }
    }
}