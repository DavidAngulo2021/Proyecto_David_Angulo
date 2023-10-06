using Proyecto_David_Angulo.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_David_Angulo.Controllers
{
    [ValidarSesion]
    public class LibrosController : Controller
    {
        // GET: Libros
        public ActionResult Princi()
        {
            return View();
        }

        public ActionResult CienAños()
        {
            return View();
        }

        public ActionResult Odicea()
        {
            return View();
        }

        public ActionResult Harry()
        {
            return View();
        }
        public ActionResult Esclavitud()
        {
            return View();
        }

        public ActionResult Cronica()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}