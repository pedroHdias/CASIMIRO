namespace TennisApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TennisApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TennisApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //  ContextKey = "TennisApp.Models.ApplicationDbContext";
        }
        //  This method will be called after migrating to the latest version.
        protected override void Seed(TennisApp.Models.ApplicationDbContext context)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar um utilizador 'Default'
            var user = new ApplicationUser();
            user.UserName = "a@a.aa";
            user.Email = "a@a.aa";
            string userPWD = "123_Aa";
            var chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador � respetiva Role-Default-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Default");
                user.EmailConfirmed = true;
            }

            // criar um utilizador 'Default'
            user = new ApplicationUser();
            user.UserName = "b@b.bb";
            user.Email = "b@b.bb";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador � respetiva Role-Default-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Default");
                user.EmailConfirmed = true;
            }

            // criar um utilizador 'Default'
            user = new ApplicationUser();
            user.UserName = "c@b.cc";
            user.Email = "c@c.cc";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador � respetiva Role-Default-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Default");
                user.EmailConfirmed = true;
            }

            var utilizadores = new List<Utilizador>
            {
                new Utilizador {IdUtilizador = 2, Nome ="Pedro Dias", Email = "a@a.aa",
                                    Telemovel = "960000002", DataNasc = new DateTime(1990,1,1), UserName = "a@a.aa" },
                new Utilizador {IdUtilizador = 3, Nome ="F�bio Salvador", Email = "b@b.bb",
                                    Telemovel = "960000003", DataNasc = new DateTime(1990,1,1), UserName = "b@b.bb" },
                new Utilizador {IdUtilizador = 4, Nome ="F�bio Ferreira", Email = "c@c.cc",
                                    Telemovel = "960000004", DataNasc = new DateTime(1990,1,1), UserName = "c@c.cc" }
            };
            utilizadores.ForEach(dd => context.Utilizadores.AddOrUpdate(d => d.Nome, dd));
            context.SaveChanges();

            var categorias = new List<Categoria> {
                new Categoria  {IdCategoria = 1, Nome = "Not�cias", Descricao = "Artigos referentes a assuntos diversos do t�nis."},
                new Categoria  {IdCategoria = 2, Nome = "Torneio Escada", Descricao = "Artigos referentes ao torneio anual"},
                new Categoria  {IdCategoria = 3, Nome = "Torneios", Descricao = "Artigos referentes aos torneios realizados em Tomar"},
                new Categoria  {IdCategoria = 4, Nome = "Clube", Descricao = "Artigos referentes ao clube"},
            };
            categorias.ForEach(dd => context.Categorias.AddOrUpdate(d => d.Nome, dd));
            context.SaveChanges();

            var noticias = new List<Noticia> {
                new Noticia  {IdNoticia= 1, Titulo = "Treinos 2017/2018",
                                Descricao = "Hor�rios e pre�os para treinos referentes � epoca 2017/2018.",
                                Foto = "horariosprecos.png", TipoImagem = "a", Relevancia = 1, DataPublicacao = new DateTime(2017,9,9,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,24,20,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0],categorias[3]}},
                new Noticia  {IdNoticia= 2, Titulo = "VI Torneio Tomar Jovem - Regulamento",
                                Descricao = "Realiza-se nos dias 9 e 10 o VI Torneio Tomar Jovem, prova oficial da FPT nos escal�es Sub-14 e Sub-18. Mais informa��es consultar regulamento.",
                                Foto = "6torneiotomarjovem.jpg", TipoImagem = "b", Relevancia = 1, DataPublicacao = new DateTime(2017,9,10,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,25,20,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[0],categorias[2]}},
                new Noticia  {IdNoticia= 3, Titulo = "IV Torneio Cidade Tomar - Adiamento",
                                Descricao = "Devido � falta de inscri��es, o torneio ser� adiado para data ainda a definir.",
                                Foto = "simboloclube.jpg", TipoImagem = "c", Relevancia = 1, DataPublicacao = new DateTime(2017,9,11,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,26,20,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0]}},
                new Noticia  {IdNoticia= 4, Titulo = "VI Torneio Cidade Tomar - Regulamento",
                                Descricao = "Ir� ser realizado este fim de semana o VI Torneio Cidade Tomar, prova oficial da FPT no escal�o de veteranos. Para mais informa��es consulte o regulamento.",
                                Foto = "simboloclube.jpg", TipoImagem = "d", Relevancia = 1, DataPublicacao = new DateTime(2017,9,12,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,20,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[0],categorias[2]}},
                new Noticia  {IdNoticia= 5, Titulo = "5 Jovens Tenistas do TCT no SmashTour_CTSantar�m",
                                Descricao = "O T�nis Clube de Tomar esteve representado na Etapa do Smash Tour no CT Santar�m, realizada no passado dia 24 de junho, por 5 atletas; Alexandre Ata�de, Gabriel Ata�de, Valentim Ata�de, Francisco Rodrigues e Martim Salvador. No torneio de sub7 (N�vel Vermelho), a jovem promessa do t�nis tomarense, Alexandre Ata�de, venceu todos os seus encontros, tendo alcan�ado o primeiro lugar. Com esta presta��o, Alexandre Ata�de conquista o seu terceiro torneio consecutivo nas Etapas do Smash Tour, sendo considerado um dos melhores jogadores da zona centro no n�vel vermelho. No torneio de sub9 (N�vel Laranja), o TCT foi representado por dois atletas, Valentim Ata�de e Gabriel Ata�de, tendo este �ltimo alcan�ado as meias finais numa prova com um elevado n�vel de participantes. No torneio de sub10 (N�vel Verde) estiveram presentes Francisco Rodrigues e Martim Salvador do TCT. Ambos estiveram em bom n�vel tendo Martim Salvador alcan�ado o terceiro lugar na competi��o, apenas superado por Jo�o Morgado e Rodrigo Almeida do CAD, dois dos melhores atletas da zona centro. O t�nis Clube de Tomar est� de parab�ns por ter participado com um elevado n�mero de atletas numa etapa do Smash Tour e por ter conseguido alcan�ar excelentes classifica��es no circuito mais importante da Federa��o Portuguesa de T�nis, nos escal�es sub7, sub9 e sub10. 1�Lugar - N�vel Vermelho - Sub7 - Alexandre Ata�de 4�Lugar - N�vel Laranja- Sub9 - Gabriel Ata�de 3�Lugar - N�vel Verde- Sub10 - Martim Salvador",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 1, DataPublicacao = new DateTime(2017,9,12,21,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0],categorias[3]}},
                new Noticia  {IdNoticia= 6, Titulo = "A Torre de tandil... est� de volta!",
                                Descricao = "Juan Mart�n Del Potro qualificou-se esta quarta-feira pela quarta vez na carreira para as meias-finais de um torneio do Grand Slam, segunda no US Open, ao derrotar o cinco vezes campe�o Roger Federer nos quartos-de-final, em plena sess�o noturna do Arthur Ashe Stadium, por 7-5, 3-6, 7-6(8) e 6-4, 2h50. Num encontro com v�rios pontos espetaculares, mas muitos erros n�o for�ados, um break resolveu os dois primeiros sets, com o primeiro a cair para Del Potro e o segundo para Federer.Na terceira partida, o argentino entrou melhor, abriu a 3 - 0 e a 4 - 1, mas permitiu a recupera��o do su��o, que empurrou a decis�o do encontro at� um tiebreak.Federer teve quatro(!) set points, dois dos quais no seu servi�o, mas Del Potro salvou todos e aproveitou o seu primeiro para arrumar a quest�o. A quarta partida foi mais parecida com as duas primeiras e um break foi suficiente. O argentino derrota Federer pela s�tima vez em 22 encontros(16 - 6 agora) e vai desafiar o n�mero um mundial, Rafael Nadal.",
                                Foto = "delpotro.jpg", TipoImagem = "f", Relevancia = 1, DataPublicacao = new DateTime(2017,9,13,22,30,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,28,22,30,00), Visivel = true, CriadorFK = 4,
                                Categorias=new List<Categoria>{categorias[0]}},
                };
            noticias.ForEach(dd => context.Noticias.AddOrUpdate(d => d.Titulo, dd));
            context.SaveChanges();
            
            var comentarios = new List<Comentario> {
                new Comentario  {IdComentario = 1, Texto = "Siga Come�ar!",
                                    DataComentario =  new DateTime(2017,9,13,23,00,00), Visivel = true, NoticiaFK = 1, CriadorFK = 2},
                new Comentario  {IdComentario = 2, Texto = "Bor� l� pessoal!",
                                    DataComentario =  new DateTime(2017,9,13,23,30,00), Visivel = true, NoticiaFK = 1, CriadorFK = 3},
                new Comentario  {IdComentario = 3, Texto = "Come on!",
                                    DataComentario =  new DateTime(2017,9,14,10,30,00), Visivel = true, NoticiaFK = 1, CriadorFK = 2},
                new Comentario  {IdComentario = 4, Texto = "Finalmente!",
                                    DataComentario =  new DateTime(2017,9,14,13,30,00), Visivel = true, NoticiaFK = 2, CriadorFK = 4},
                new Comentario  {IdComentario = 5, Texto = "Oh.. a s�rio??",
                                    DataComentario =  new DateTime(2017,9,15,17,00,00), Visivel = true, NoticiaFK = 3, CriadorFK = 4},
                new Comentario  {IdComentario = 6, Texto = "Ningu�m p�ra Del Potro!",
                                    DataComentario =  new DateTime(2017,9,16,19,30,00), Visivel = true, NoticiaFK = 6, CriadorFK = 2},
                new Comentario  {IdComentario = 7, Texto = "Pena Roger ter perdido..",
                                    DataComentario =  new DateTime(2017,9,17,23,30,00), Visivel = true, NoticiaFK = 6, CriadorFK = 3},
            };
            comentarios.ForEach(dd => context.Comentarios.AddOrUpdate(d => d.IdComentario, dd));
            context.SaveChanges();
        }
    }
}
