using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TennisApp.Models;
using PagedList;
using PagedList.Mvc;

namespace TennisApp.Controllers
{
    [Authorize(Roles = "Administrador,Moderador")]
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorias
        //metodo search por categoria e descricao
        [AllowAnonymous]
        //tempo(segundos) que fica em cache
        [OutputCache(Duration = 10)]
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            ViewBag.SortCategoriaParameter = string.IsNullOrEmpty(sortBy) ? "Categoria desc" : "";

            var categorias = db.Categorias.AsQueryable();

            if (searchBy == "Categoria")
            {
                categorias = categorias.Where(x => x.Nome.Contains(search) || search == null);
            }
            else
            {
                categorias = categorias.Where(x => x.Descricao.Contains(search) || search == null);
            }
            switch (sortBy)
            {
                case "Categoria desc":
                    categorias = categorias.OrderByDescending(x => x.Nome);
                    break;
                default:
                    categorias = categorias.OrderBy(x => x.Nome);
                    break;
            }
            return View(categorias.ToPagedList(page ?? 1, 5));
        }

        public JsonResult GetSearch(string term)
        {
            List<string> categorias;

            categorias = db.Categorias.Where(x => x.Nome.StartsWith(term)).Select(y => y.Nome).ToList();

            return Json(categorias, JsonRequestBehavior.AllowGet);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Categoria inválida";
                return RedirectToAction("Index");

            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {

                TempData["Error"] = "Categoria inválida";
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IdCategoria,Nome,Descricao")] Categoria categoria)
        {
            //Selecciona o último id da tabela de categorias e incrementa 1
            var CategoryID = db.Categorias.OrderByDescending(d => d.IdCategoria).FirstOrDefault().IdCategoria + 1;

            // atribui o novo ID ao objeto que veio da View
            categoria.IdCategoria = CategoryID;

            StringBuilder sbCategorias = new StringBuilder();
            sbCategorias.Append(HttpUtility.HtmlEncode(categoria.Descricao));

            sbCategorias.Replace("&lt;b&gt;", "<b>");
            sbCategorias.Replace("&lt;/b&gt;", "</b>");
            sbCategorias.Replace("&lt;u&gt;", "<u>");
            sbCategorias.Replace("&lt;/u&gt;", "</u>");
            sbCategorias.Replace("\r\n", "<br />");

            categoria.Descricao = sbCategorias.ToString();

            string strTitulo = HttpUtility.HtmlEncode(categoria.Nome);
            categoria.Nome = strTitulo;

            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                db.SaveChanges();
                TempData["Create"] = "" + categoria.Nome;
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Categoria inválida";
                return RedirectToAction("Index");
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                TempData["Error"] = "Categoria inválida";
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "IdCategoria,Nome,Descricao")] Categoria categoria)
        {
            StringBuilder sbCategorias = new StringBuilder();
            sbCategorias.Append(HttpUtility.HtmlEncode(categoria.Descricao));

            sbCategorias.Replace("&lt;b&gt;", "<b>");
            sbCategorias.Replace("&lt;/b&gt;", "</b>");
            sbCategorias.Replace("&lt;u&gt;", "<u>");
            sbCategorias.Replace("&lt;/u&gt;", "</u>");
            sbCategorias.Replace("\r\n", "<br />");

            categoria.Descricao = sbCategorias.ToString();

            string strTitulo = HttpUtility.HtmlEncode(categoria.Nome);
            categoria.Nome = strTitulo;

            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Edit"] = "" + categoria.Nome;
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Categoria inválida";
                return RedirectToAction("Index");
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                TempData["Error"] = "Categoria inválida";
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
            db.SaveChanges();
            TempData["Delete"] = categoria.Nome;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
