﻿using System;
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
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace TennisApp.Controllers
{
    [Authorize]
    public class ComentariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// adiciona um comentário
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(string comentario, int idNoticia)
        {

            if (!string.IsNullOrEmpty( comentario ))
            {
                StringBuilder sbComentarios = new StringBuilder();
                sbComentarios.Append(HttpUtility.HtmlEncode(comentario));

                sbComentarios.Replace("&lt;b&gt;", "<b>");
                sbComentarios.Replace("&lt;/b&gt;", "</b>");
                sbComentarios.Replace("&lt;u&gt;", "<u>");
                sbComentarios.Replace("&lt;/u&gt;", "</u>");
                sbComentarios.Replace("\r\n", "<br />");

                Comentario novoComentario = new Comentario();

                // adicionar dados ao obj que vai levar os dados para a BD
                novoComentario.NoticiaFK = idNoticia;
                novoComentario.CriadorFK = db.Utilizadores.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault().IdUtilizador;
                novoComentario.DataComentario = DateTime.Now;
                novoComentario.Texto = comentario;
                novoComentario.Visivel = true;

                if (ModelState.IsValid)
                {
                    db.Comentarios.Add(novoComentario);
                    db.SaveChanges();
                    TempData["Create"] = "Comentário adicionado";
                }
                else
                {
                    TempData["Error"] = "Comentário inválido";
                }
            }
            return RedirectToAction("Details", "Noticias", new { Id = idNoticia });
        }
        /// censura um comentário
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Censura(int idComentario, int idNoticia)
        {
            var comentario = db.Comentarios.Find(idComentario);
            comentario.Visivel = false;
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Edit"] = "";
                return RedirectToAction("Details", "Noticias", new { Id = idNoticia });
            }

            return RedirectToAction("Details", "Noticias", new { Id = idNoticia });
        }

        /// censura um comentário
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult MostraComentario(int idComentario, int idNoticia)
        {
            var comentario = db.Comentarios.Find(idComentario);
            comentario.Visivel = true;
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Edit"] = "";
                return RedirectToAction("Details", "Noticias", new { Id = idNoticia });
            }

            return RedirectToAction("Details", "Noticias", new { Id = idNoticia });
        }

        // GET: Comentarios
        [AllowAnonymous]
        [OutputCache(Duration = 10)]
        [Authorize(Roles = "Administrador,Moderador")]
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
            return View(comentarios.ToPagedList(page ?? 1, 6));
        }

        // GET: Comentarios/Details/5
        [Authorize(Roles = "Administrador,Moderador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Comentário inválido";
                return RedirectToAction("Index");
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                TempData["Error"] = "Comentário inválido";
                return RedirectToAction("Index");
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        [ValidateInput(false)]
        [Authorize(Roles = "Administrador,Moderador")]
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
        [Authorize(Roles = "Administrador,Moderador")]
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
                TempData["Create"] = "" + comentario.Criador.Nome;
                return RedirectToAction("Index");
            }

            ViewBag.CriadorFK = new SelectList(db.Utilizadores, "IdUtilizador", "Nome", comentario.CriadorFK);
            ViewBag.NoticiaFK = new SelectList(db.Noticias, "IdNoticia", "Titulo", comentario.NoticiaFK);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Comentário inválido";
                return RedirectToAction("Index");
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                TempData["Error"] = "Comentário inválido";
                return RedirectToAction("Index");
            }
            // verificar se a pessoa que edita a notícia é um Moderador
            if (User.IsInRole("Moderador"))
            {
                return View(comentario);
            }

            // verificar se a pessoa que edita a notícia é o seu autor
            if (comentario.Criador.UserName.Equals(User.Identity.Name))
            {
                return View(comentario);
            }

            ViewBag.CriadorFK = new SelectList(db.Utilizadores, "IdUtilizador", "Nome", comentario.CriadorFK);
            ViewBag.NoticiaFK = new SelectList(db.Noticias, "IdNoticia", "Titulo", comentario.NoticiaFK);
            TempData["Error"] = "Não tem permissões para editar este comentário";
            return RedirectToAction("Index");
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
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
                TempData["Edit"] = "" + comentario.Criador.Nome;
                return RedirectToAction("Index");
            }
            ViewBag.CriadorFK = new SelectList(db.Utilizadores, "IdUtilizador", "Nome", comentario.CriadorFK);
            ViewBag.NoticiaFK = new SelectList(db.Noticias, "IdNoticia", "Titulo", comentario.NoticiaFK);
            return View(comentario);
        }
       
        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Comentário inválido";
                return RedirectToAction("Index");
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                TempData["Error"] = "Comentário inválido";
                return RedirectToAction("Index");
            }
            // verificar se a pessoa que edita a notícia é um Moderador
            if (User.IsInRole("Moderador"))
            {
                return View(comentario);
            }

            // verificar se a pessoa que edita a notícia é o seu autor
            if (comentario.Criador.UserName.Equals(User.Identity.Name))
            {
                return View(comentario);
            }
            TempData["Error"] = "Não tem permissões para remover este comentário";
            return RedirectToAction("Index");
        }

        // POST: Commentaries/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int IdComentario)
        {
            Comentario comentarios = db.Comentarios.Find(IdComentario);
            var Id = comentarios.Noticia.IdNoticia;
            try
            {
                db.Comentarios.Remove(comentarios);
                db.SaveChanges();
                TempData["Delete"] = "";

            }
            catch (Exception ex)
            {

                TempData["Erro"] = "";
            }

            return RedirectToAction("Details", "Noticias", new { Id = Id });
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
            db.SaveChanges();
            TempData["Delete"] = comentario.Criador.Nome;
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
