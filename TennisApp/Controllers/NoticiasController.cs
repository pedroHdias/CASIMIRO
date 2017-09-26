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
using System.IO;

namespace TennisApp.Controllers
{
    [Authorize]
    public class NoticiasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Noticias
        [AllowAnonymous]
        [OutputCache(Duration = 10)]
        public ActionResult Index(string searchBy, string search, int? page, string sortBy, string categoriaString)
        {
            ViewBag.SortAutorParameter = string.IsNullOrEmpty(sortBy) ? "Autor desc" : "";
            ViewBag.SortTituloParameter = sortBy == "Titulo" ? "Titulo desc" : "Titulo";
            ViewBag.SortRelevanciaParameter = sortBy == "Relevancia" ? "Relevancia desc" : "Relevancia";

            var categoriaList = new List<String>();
            var categoriasQry = from d in db.Categorias orderby d.Nome select d.Nome;

            categoriaList.AddRange(categoriasQry);
            ViewBag.categoriaString = new SelectList(categoriaList);

            var noticias = db.Noticias.AsQueryable();

            if (searchBy == "Autor")
            {
                noticias = noticias.Where(x => x.Criador.Nome.Contains(search) || search == null);
            }
            else if (searchBy == "Titulo")
            {
                noticias = noticias.Where(x => x.Titulo.Contains(search) || search == null);
            }
            else if (searchBy == "Relevancia")
            {
                noticias = noticias.Where(x => x.Relevancia.ToString() == search || search == null);
            }
            else if (!String.IsNullOrEmpty(categoriaString))
            {
                noticias = noticias.Where(x => x.Categorias.Select(c=>c.Nome).Contains(categoriaString));
                // https://stackoverflow.com/questions/13405568/linq-unable-to-create-a-constant-value-of-type-xxx-only-primitive-types-or-enu
            }
            else
            {
                noticias = noticias.Where(x => x.Descricao.Contains(search) || search == null);
            }
            switch (sortBy)
            {
                case "Autor desc":
                    noticias = noticias.OrderByDescending(x => x.Criador.Nome);
                    break;
                case "Titulo desc":
                    noticias = noticias.OrderByDescending(x => x.Titulo);
                    break;
                case "Titulo":
                    noticias = noticias.OrderBy(x => x.Titulo);
                    break;
                case "Relevancia desc":
                    noticias = noticias.OrderByDescending(x => x.Relevancia);
                    break;
                case "Relevancia":
                    noticias = noticias.OrderBy(x => x.Relevancia);
                    break;
                default:
                    noticias = noticias.OrderBy(x => x.Criador.Nome);
                    break;
            }
            return View(noticias.ToPagedList(page ?? 1, 8));
        }

        // GET: Noticias/Details/5
       // [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Noticia inválida";
                return RedirectToAction("Index");
            }
            Noticia noticia = db.Noticias.Find(id);
            if (noticia == null)
            {
                TempData["Error"] = "Noticia inválida";
                return RedirectToAction("Index");
            }
            return View(noticia);
        }

        // GET: Noticias/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            // obtem a lista de Categorias");
            ViewBag.Categorias = db.Categorias.OrderBy(c => c.Nome);
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titulo,Descricao,Foto,TipoImagem,Relevancia,DataPublicacao,DataLimiteVisualizacao,Visivel,CriadorFK")] Noticia noticia,
            string[] categorias, HttpPostedFile fotografia)
        {
            if (categorias != null)
            {
                if (fotografia != null)
                {
                    //Selecciona o último id  e incrementa 1
                    var NoticiaID = db.Noticias.OrderByDescending(d => d.IdNoticia).FirstOrDefault().IdNoticia + 1;

                    // atribui o novo ID ao objeto que veio da View
                    noticia.IdNoticia = NoticiaID;

                    //Recolhe a data actual
                    noticia.DataPublicacao = DateTime.Now;

                    string pic = System.IO.Path.GetFileName(fotografia.FileName);
                    noticia.Foto = pic;
                    //guardar caminho para a foto
                    string path = System.IO.Path.Combine(Server.MapPath("/Imagens"), pic);

                    fotografia.SaveAs(path);// file is uploaded

                    // save the image path path to the database or you can send image
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fotografia.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                    //atribuir as categorias à notícia
                    // iterar pelo array de categorias e criar uma lista de categorias
                    var listaCategorias = new List<Categoria>();
                    foreach (var cat in categorias)
                    {
                        listaCategorias.Add(db.Categorias.Find(cat));
                    }
                    noticia.Categorias = listaCategorias;

                    // atribuir o ID do dono da notícia à notícia
                    noticia.CriadorFK = db.Utilizadores.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault().IdUtilizador;

                    StringBuilder sbNoticias = new StringBuilder();
                    sbNoticias.Append(HttpUtility.HtmlEncode(noticia.Descricao));

                    //evitar ataques SQL injection
                    sbNoticias.Replace("&lt;b&gt;", "<b>");
                    sbNoticias.Replace("&lt;/b&gt;", "</b>");
                    sbNoticias.Replace("&lt;u&gt;", "<u>");
                    sbNoticias.Replace("&lt;/u&gt;", "</u>");
                    sbNoticias.Replace("\r\n", "<br />");

                    noticia.Descricao = sbNoticias.ToString();

                    string strTitulo = HttpUtility.HtmlEncode(noticia.Titulo);
                    noticia.Titulo = strTitulo;

                    if (ModelState.IsValid)
                    {
                        db.Noticias.Add(noticia);
                        db.SaveChanges();
                        TempData["Create"] = "" + noticia.Titulo;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Error"] = "Não adicionou uma Imagem à Notícia";
                }
            }
            else
            {
                TempData["Error"] = "Não slecionou pelo menos uma Categoria para a Notícia";
            }
            // obtem a lista de Categorias;
            ViewBag.Categorias = db.Categorias.OrderBy(c => c.Nome);
            return View(noticia);
        }

        // GET: Noticias/Edit/5
        [ValidateInput(false)]
    //    [Authorize(Roles = "Jornalista,Moderador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Noticia inválida";
                return RedirectToAction("Index");
            }
            Noticia noticia = db.Noticias.Find(id);
            if (noticia == null)
            {
                TempData["Error"] = "Notícia inválida";
                return RedirectToAction("Index");
            }

            // obtem a lista de Categorias;
            ViewBag.Categorias = db.Categorias.OrderBy(c => c.Nome);

            // verificar se a pessoa que edita a notícia é um Moderador
            if (User.IsInRole("Moderador"))
            {
                return View(noticia);
            }

            // verificar se a pessoa que edita a notícia é o seu autor
            if (noticia.Criador.UserName.Equals(User.Identity.Name))
            {
                return View(noticia);
            }

            return RedirectToAction("Index");
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
     //   [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "IdNoticia,Titulo,Descricao,Foto,TipoImagem,Relevancia,DataPublicacao,DataLimiteVisualizacao,Visivel,CriadorFK")] Noticia noticia,
             string[] categorias, HttpPostedFile fotografia)
        {
            if (categorias != null)
            {
                if (fotografia != null)
                {
                    //Se existir uma foto para esta noticia, apaga-a
                    if (System.IO.File.Exists("/Imagens" + noticia.Foto))
                    {
                        System.IO.File.Delete("/Imagens" + noticia.Foto);
                    }
                    //Adiciona uma nova
                    string pic = System.IO.Path.GetFileName(fotografia.FileName);
                    noticia.Foto = pic;
                    string path = System.IO.Path.Combine(Server.MapPath("/Imagens"), pic);

                    fotografia.SaveAs(path);// file is uploaded

                    // save the image path path to the database or you can send image
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fotografia.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                    //atribuir as categorias à notícia
                    // iterar pelo array de categorias e criar uma lista de categorias
                    var listaCategorias = new List<Categoria>();
                    foreach (var cat in categorias)
                    {
                        listaCategorias.Add(db.Categorias.Find(cat));
                    }
                    noticia.Categorias = listaCategorias;


                    StringBuilder sbNoticias = new StringBuilder();
                    sbNoticias.Append(HttpUtility.HtmlEncode(noticia.Descricao));

                    sbNoticias.Replace("&lt;b&gt;", "<b>");
                    sbNoticias.Replace("&lt;/b&gt;", "</b>");
                    sbNoticias.Replace("&lt;u&gt;", "<u>");
                    sbNoticias.Replace("&lt;/u&gt;", "</u>");
                    sbNoticias.Replace("\r\n", "<br />");

                    noticia.Descricao = sbNoticias.ToString();

                    string strTitulo = HttpUtility.HtmlEncode(noticia.Titulo);
                    noticia.Titulo = strTitulo;

                    if (ModelState.IsValid)
                    {
                        db.Entry(noticia).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Edit"] = "" + noticia.Titulo;
                        return RedirectToAction("Index");
                    }
                }
                
            }
            else
            {
                TempData["Error"] = "Não slecionou pelo menos uma Categoria para a Notícia";
            }
            // obtem a lista de Categorias;
            ViewBag.Categorias = db.Categorias.OrderBy(c => c.Nome);

            return View(noticia);
        }

        // GET: Noticias/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Noticia inválida";
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticias.Find(id);
            if (noticia == null)
            {
                TempData["Error"] = "Noticia inválida";
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(noticia);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Noticia noticia = db.Noticias.Find(id);
            db.Noticias.Remove(noticia);
            db.SaveChanges();
            TempData["Delete"] = noticia.Titulo;
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
