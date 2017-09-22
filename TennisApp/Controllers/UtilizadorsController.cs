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
    [Authorize]
    public class UtilizadorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Utilizadors
        //metodo search por tipo e nome
        [AllowAnonymous]
        [OutputCache(Duration = 10)]
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortUsernameParameter = sortBy == "Username" ? "Username desc" : "Username";

            var utilizadores = db.Utilizadores.AsQueryable();

            if (searchBy == "Nome")
            {
                utilizadores = utilizadores.Where(x => x.Nome.Contains(search) || search == null);
            }
            else
            {
                utilizadores = utilizadores.Where(x => x.UserName.Contains(search) || search == null);
            }
            switch (sortBy)
            {
                case "Name desc":
                    utilizadores = utilizadores.OrderByDescending(x => x.Nome);
                    break;
                case "Username desc":
                    utilizadores = utilizadores.OrderByDescending(x => x.UserName);
                    break;
                case "Username":
                    utilizadores = utilizadores.OrderBy(x => x.UserName);
                    break;
                default:
                    utilizadores = utilizadores.OrderBy(x => x.Nome);
                    break;
            }
            return View(utilizadores.ToPagedList(page ?? 1, 3));
        }

        // GET: Utilizadors/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadores.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // GET: Utilizadors/Create
        [ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilizadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "IdUtilizador,Nome,Tipo,Email,Telemovel,DataNasc,UserName")] Utilizador utilizador)
        {
            string strNome = HttpUtility.HtmlEncode(utilizador.Nome);
            utilizador.Nome = strNome;

            string strUsername = HttpUtility.HtmlEncode(utilizador.UserName);
            utilizador.UserName = strUsername;

            if (ModelState.IsValid)
            {
                db.Utilizadores.Add(utilizador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utilizador);
        }

        // GET: Utilizadors/Edit/5
        [ValidateInput(false)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadores.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "IdUtilizador,Nome,Tipo,Email,Telemovel,DataNasc,UserName")] Utilizador utilizador)
        {
            string strNome = HttpUtility.HtmlEncode(utilizador.Nome);
            utilizador.Nome = strNome;

            string strUsername = HttpUtility.HtmlEncode(utilizador.UserName);
            utilizador.UserName = strUsername;

            if (ModelState.IsValid)
            {
                db.Entry(utilizador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadores.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizador utilizador = db.Utilizadores.Find(id);
            db.Utilizadores.Remove(utilizador);
            db.SaveChanges();
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
