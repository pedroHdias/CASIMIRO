using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
    public class ComentariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comentarios
        [AllowAnonymous]
        [OutputCache(Duration = 10)]
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            ViewBag.SortAutorParameter = string.IsNullOrEmpty(sortBy) ? "Autor desc" : "";
            ViewBag.SortNoticiaParameter = sortBy == "Noticia" ? "Noticia desc" : "Noticia";

            var comentarios = db.Comentarios.AsQueryable();

            if (searchBy == "Autor")
            {
                comentarios = comentarios.Where(x => x.Criador.Nome.Contains(search) || search == null);
            }
            else if (searchBy == "Noticia")
            {
                comentarios = comentarios.Where(x => x.Noticia.Titulo.Contains(search) || search == null);
            }
            else
            {
                comentarios = comentarios.Where(x => x.Texto.Contains(search) || search == null);
            }
            switch (sortBy)
            {
                case "Autor desc":
                    comentarios = comentarios.OrderByDescending(x => x.Criador.Nome);
                    break;
                case "Noticia desc":
                    comentarios = comentarios.OrderByDescending(x => x.Noticia.Titulo);
                    break;
                case "Noticia":
                    comentarios = comentarios.OrderBy(x => x.Noticia.Titulo);
                    break;
                default:
                    comentarios = comentarios.OrderBy(x => x.Criador.Nome);
                    break;
            }
            return View(comentarios.ToPagedList(page ?? 1, 3));

            /*
            //Outra maneira de implementar search
            DateTime dataComentario = new DateTime();
            if (dataComentarioString == null)
            {
                dataComentario = new DateTime(1, 1, 1);
            }
            else
            {
                if (dataComentarioString != "")
                {
                    dataComentario = Convert.ToDateTime(dataComentarioString);
                }
            }
            //DateTime dataComentario = DateTime.ParseExact(dataComentarioString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var comentarioList = new List<DateTime>();
            var comentariosQry = from d in db.Comentarios orderby d.DataComentario select d.DataComentario;

            comentarioList.AddRange(comentariosQry.Distinct());
            ViewBag.dataComentarioString = new SelectList(comentarioList);

            var comentarios = from c in db.Comentarios select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                comentarios = comentarios.Where(s => s.Criador.Nome.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(dataComentarioString))
            {
                comentarios = comentarios.Where(x => x.DataComentario == dataComentario);
            }

            return View(comentarios);
            */
        }

        // GET: Comentarios/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            ViewBag.CriadorFK = new SelectList(db.Utilizadores, "IdUtilizador", "Nome");
            ViewBag.NoticiaFK = new SelectList(db.Noticias, "IdNoticia", "Titulo");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IdComentario,Texto,DataComentario,Visivel,NoticiaFK,CriadorFK")] Comentario comentario)
        {
            StringBuilder sbComentarios = new StringBuilder();
            sbComentarios.Append(HttpUtility.HtmlEncode(comentario.Texto));

            sbComentarios.Replace("&lt;b&gt;", "<b>");
            sbComentarios.Replace("&lt;/b&gt;", "</b>");
            sbComentarios.Replace("&lt;u&gt;", "<u>");
            sbComentarios.Replace("&lt;/u&gt;", "</u>");
            sbComentarios.Replace("\r\n", "<br />");

            comentario.Texto = sbComentarios.ToString();

            if (ModelState.IsValid)
            {
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CriadorFK = new SelectList(db.Utilizadores, "IdUtilizador", "Nome", comentario.CriadorFK);
            ViewBag.NoticiaFK = new SelectList(db.Noticias, "IdNoticia", "Titulo", comentario.NoticiaFK);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        [ValidateInput(false)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CriadorFK = new SelectList(db.Utilizadores, "IdUtilizador", "Nome", comentario.CriadorFK);
            ViewBag.NoticiaFK = new SelectList(db.Noticias, "IdNoticia", "Titulo", comentario.NoticiaFK);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "IdComentario,Texto,DataComentario,Visivel,NoticiaFK,CriadorFK")] Comentario comentario)
        {
            StringBuilder sbComentarios = new StringBuilder();
            sbComentarios.Append(HttpUtility.HtmlEncode(comentario.Texto));

            sbComentarios.Replace("&lt;b&gt;", "<b>");
            sbComentarios.Replace("&lt;/b&gt;", "</b>");
            sbComentarios.Replace("&lt;u&gt;", "<u>");
            sbComentarios.Replace("&lt;/u&gt;", "</u>");
            sbComentarios.Replace("\r\n", "<br />");

            comentario.Texto = sbComentarios.ToString();

            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CriadorFK = new SelectList(db.Utilizadores, "IdUtilizador", "Nome", comentario.CriadorFK);
            ViewBag.NoticiaFK = new SelectList(db.Noticias, "IdNoticia", "Titulo", comentario.NoticiaFK);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
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
