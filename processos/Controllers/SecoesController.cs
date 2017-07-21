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

        [HttpGet]
        public JsonResult Listar(SecoesModels secoes)
        {
            return Json(new SecoesModels().Listar(secoes), JsonRequestBehavior.AllowGet);
        }
    }
}