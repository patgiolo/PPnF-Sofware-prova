using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplicationProva.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalogo
        public ActionResult Home()
        {
            return View();
        }

        // POST: Catalogo
        public ActionResult Insert()
        {
            return View();
        }

        // GET: Catalogo
        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // PUT: Catalogo
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // DELETE: Catalogo
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
