using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TennisApp.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Página de descrição da aplicação.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "A sua página de contacto.";

            return View();
        }
    }
}