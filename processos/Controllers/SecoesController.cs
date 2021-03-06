﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using processos.Models;

namespace processos.Controllers
{
    public class SecoesController : Controller
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
                ViewBag.title = "Seções";
                return View();
            }
        }

        // lista dados
        [HttpGet]
        public JsonResult Read(SecoesModels secoes)
        {
            return Json(new SecoesModels().Read(secoes), JsonRequestBehavior.AllowGet);
        }

        // adiciona/edita dados
        public ActionResult Save()
        {
            if (Session["Usu_nome"] == null)
            {
                return RedirectToAction("Login", "Home", new { log = "nao" });
            }
            else
            {
                ViewBag.title = "Adicionando/editando seções";
                return View();
            }
        }

        // salva dados
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
                    string url;
                    if (secoes.Sec_id == 0)
                    {
                        url = "/Secoes/Save".ToString();
                    }
                    else
                    {
                        url = "/Secoes/Save?id=" + secoes.Sec_id.ToString();
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
        public JsonResult Delete(SecoesModels secoes)
        {
            return Json(new SecoesModels().Crud(secoes), JsonRequestBehavior.AllowGet);
        }
    }
}