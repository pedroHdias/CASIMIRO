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

            // criar um utilizador 'Jornalista'
            var user = new ApplicationUser();
            user.UserName = "a@a.aa";
            user.Email = "a@a.aa";
            string userPWD = "123_Aa";
            var chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Jornalista-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Jornalista");
                user.EmailConfirmed = true;
            }
            // criar um utilizador 'Jornalista'
            user = new ApplicationUser();
            user.UserName = "b@b.bb";
            user.Email = "b@b.bb";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Jornalista-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Jornalista");
                user.EmailConfirmed = true;
            }
            // criar um utilizador 'Moderador'
            user = new ApplicationUser();
            user.UserName = "c@b.cc";
            user.Email = "c@c.cc";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Moderador-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Moderador");
                user.EmailConfirmed = true;
            }
            // criar um utilizador 'Default'
            user = new ApplicationUser();
            user.UserName = "d@d.dd";
            user.Email = "d@d.dd";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Default-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Default");
                user.EmailConfirmed = true;
            }
            // criar um utilizador 'Default'
            user = new ApplicationUser();
            user.UserName = "e@e.ee";
            user.Email = "e@e.ee";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Default-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Default");
                user.EmailConfirmed = true;
            }
            // criar um utilizador 'Default'
            user = new ApplicationUser();
            user.UserName = "f@f.ff";
            user.Email = "f@f.ff";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Default-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Default");
                user.EmailConfirmed = true;
            }
            //Adicionar o Utilizador à respetiva Role-Default-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Default");
                user.EmailConfirmed = true;
            }
            // criar um utilizador 'Default'
            user = new ApplicationUser();
            user.UserName = "g@g.gg";
            user.Email = "g@g.gg";
            userPWD = "123_Aa";
            chkUser = userManager.Create(user, userPWD);
            var utilizadores = new List<Utilizador>
            {
                //jornalista
                new Utilizador {IdUtilizador = 2, Nome ="Pedro Dias", Tipo="Default", Email = "a@a.aa",
                                    Telemovel = "960000002", DataNasc = new DateTime(1990,1,1), UserName = "a@a.aa" },
                //jornalista
                new Utilizador {IdUtilizador = 3, Nome ="Fábio Salvador", Tipo="Default", Email = "b@b.bb",
                                    Telemovel = "960000003", DataNasc = new DateTime(1980,1,1), UserName = "b@b.bb" },
                //moderador
                new Utilizador {IdUtilizador = 4, Nome ="Fábio Ferreira", Tipo="Default", Email = "c@c.cc",
                                    Telemovel = "960000004", DataNasc = new DateTime(1970,1,1), UserName = "c@c.cc" },
                new Utilizador {IdUtilizador = 5, Nome ="Tiago Henriques", Tipo="Default", Email = "d@d.dd",
                                    Telemovel = "960000005", DataNasc = new DateTime(1990,1,1), UserName = "d@d.dd" },
                new Utilizador {IdUtilizador = 6, Nome ="António Paiva", Tipo="Default", Email = "e@e.ee",
                                    Telemovel = "960000006", DataNasc = new DateTime(1980,1,1), UserName = "e@e.ee" },
                new Utilizador {IdUtilizador = 7, Nome ="Ricardo Graça", Tipo="Default", Email = "f@f.ff",
                                    Telemovel = "960000007", DataNasc = new DateTime(1970,1,1), UserName = "f@f.ff" },
                new Utilizador {IdUtilizador = 8, Nome ="Rui Reis", Tipo="Default", Email = "g@g.gg",
                                    Telemovel = "960000008", DataNasc = new DateTime(2000,1,1), UserName = "g@g.gg" }
            };
            utilizadores.ForEach(dd => context.Utilizadores.AddOrUpdate(d => d.Nome, dd));
            context.SaveChanges();

            var categorias = new List<Categoria> {
                new Categoria  {IdCategoria = 1, Nome = "Notícias", Descricao = "Artigos referentes a assuntos diversos do ténis."},
                new Categoria  {IdCategoria = 2, Nome = "Torneio Escada", Descricao = "Artigos referentes ao torneio anual"},
                new Categoria  {IdCategoria = 3, Nome = "Torneios", Descricao = "Artigos referentes aos torneios realizados em Tomar"},
                new Categoria  {IdCategoria = 4, Nome = "Clube", Descricao = "Artigos referentes ao clube"}
            };
            categorias.ForEach(dd => context.Categorias.AddOrUpdate(d => d.Nome, dd));
            context.SaveChanges();

            var noticias = new List<Noticia> {
                new Noticia  {IdNoticia= 1, Titulo = "Treinos 2017/2018",
                                Descricao = "Horários e preços para treinos referentes à epoca 2017/2018.",
                                Foto = "horariosprecos.png", TipoImagem = "a", Relevancia = 1, DataPublicacao = new DateTime(2017,9,9,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,24,20,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0],categorias[3]}},
                new Noticia  {IdNoticia= 2, Titulo = "VI Torneio Tomar Jovem - Regulamento",
                                Descricao = "Realiza-se nos dias 9 e 10 o VI Torneio Tomar Jovem, prova oficial da FPT nos escalões Sub-14 e Sub-18. Mais informações consultar regulamento.",
                                Foto = "6torneiotomarjovem.jpg", TipoImagem = "b", Relevancia = 1, DataPublicacao = new DateTime(2017,9,10,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,25,20,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[0],categorias[2]}},
                new Noticia  {IdNoticia= 3, Titulo = "IV Torneio Cidade Tomar - Adiamento",
                                Descricao = "Devido à falta de inscrições, o torneio será adiado para data ainda a definir.",
                                Foto = "simboloclube.jpg", TipoImagem = "c", Relevancia = 1, DataPublicacao = new DateTime(2017,9,11,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,26,20,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0]}},
                new Noticia  {IdNoticia= 4, Titulo = "VI Torneio Cidade Tomar - Regulamento",
                                Descricao = "Irá ser realizado este fim de semana o VI Torneio Cidade Tomar, prova oficial da FPT no escalão de veteranos. Para mais informações consulte o regulamento.",
                                Foto = "simboloclube.jpg", TipoImagem = "d", Relevancia = 1, DataPublicacao = new DateTime(2017,9,12,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,20,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[0],categorias[2]}},
                new Noticia  {IdNoticia= 5, Titulo = "5 Jovens Tenistas do TCT no SmashTour_CTSantarém",
                                Descricao = "O Ténis Clube de Tomar esteve representado na Etapa do Smash Tour no CT Santarém, realizada no passado dia 24 de junho, por 5 atletas; Alexandre Ataíde, Gabriel Ataíde, Valentim Ataíde, Francisco Rodrigues e Martim Salvador. No torneio de sub7 (Nível Vermelho), a jovem promessa do ténis tomarense, Alexandre Ataíde, venceu todos os seus encontros, tendo alcançado o primeiro lugar. Com esta prestação, Alexandre Ataíde conquista o seu terceiro torneio consecutivo nas Etapas do Smash Tour, sendo considerado um dos melhores jogadores da zona centro no nível vermelho. No torneio de sub9 (Nível Laranja), o TCT foi representado por dois atletas, Valentim Ataíde e Gabriel Ataíde, tendo este último alcançado as meias finais numa prova com um elevado nível de participantes. No torneio de sub10 (Nível Verde) estiveram presentes Francisco Rodrigues e Martim Salvador do TCT. Ambos estiveram em bom nível tendo Martim Salvador alcançado o terceiro lugar na competição, apenas superado por João Morgado e Rodrigo Almeida do CAD, dois dos melhores atletas da zona centro. O ténis Clube de Tomar está de parabéns por ter participado com um elevado número de atletas numa etapa do Smash Tour e por ter conseguido alcançar excelentes classificações no circuito mais importante da Federação Portuguesa de Ténis, nos escalões sub7, sub9 e sub10. 1ºLugar - Nível Vermelho - Sub7 - Alexandre Ataíde 4ºLugar - Nível Laranja- Sub9 - Gabriel Ataíde 3ºLugar - Nível Verde- Sub10 - Martim Salvador",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 1, DataPublicacao = new DateTime(2017,9,12,21,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0],categorias[3]}},
                new Noticia  {IdNoticia= 6, Titulo = "João Sousa travado na primeira ronda em Shenzhen",
                                Descricao = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent eu hendrerit elit. Morbi mauris felis, tempus bibendum molestie at, dignissim ut massa. Nam maximus diam lectus, vel cursus neque volutpat ut. Aenean aliquet facilisis felis, quis gravida elit elementum quis. Donec at convallis justo. In rutrum elit elit, a sollicitudin enim commodo sodales. Integer id laoreet lorem. Phasellus vel lacus nisi.",
                                Foto = "jsousa.jpg", TipoImagem = "e", Relevancia = 5, DataPublicacao = new DateTime(2017,9,12,22,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[1],categorias[2]}},
                new Noticia  {IdNoticia= 7, Titulo = "Torneio Encerramento Juvenil - Regulamento",
                                Descricao = "Cras sit amet ipsum vitae erat dictum posuere varius vel libero. Ut ante nisl, porta nec consequat id, porttitor nec risus. Vivamus pretium pellentesque lacus vitae placerat. Etiam bibendum metus vel orci condimentum placerat a ut leo. Vivamus congue dui vel convallis lacinia. Mauris tincidunt metus libero, et interdum est bibendum eget. Praesent nisi lacus, porta ut felis pulvinar, pharetra consectetur mi. Nullam interdum purus ut urna venenatis maximus. Nulla quis nisl sed odio lacinia vestibulum.",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 1, DataPublicacao = new DateTime(2017,9,12,23,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0]}},
                new Noticia  {IdNoticia= 8, Titulo = "Torneio 27 Anos TCT - Regulamento",
                                Descricao = "Vivamus at mollis lorem. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed gravida diam eget augue aliquam pulvinar. Aliquam nibh est, rutrum sit amet varius id, pellentesque non nisl. Mauris a felis et tortor porta dignissim. Morbi non magna congue, ultrices arcu ac, maximus nulla. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed ipsum tellus, sollicitudin a malesuada ac, auctor non dolor. Etiam egestas posuere porta.",
                                Foto = "torneio27anostct.jpg", TipoImagem = "e", Relevancia = 2, DataPublicacao = new DateTime(2017,9,13,21,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[2],categorias[3]}},
                new Noticia  {IdNoticia= 9, Titulo = "[Foto] Gesto de Kyrgios causa polémica mas ele nega que tenha ligação a Trump: «Fuck no»",
                                Descricao = "Suspendisse sed ligula faucibus, scelerisque magna gravida, eleifend leo. Cras non ornare tortor. Vivamus nisl odio, fringilla ut sem eget, euismod sodales diam. Maecenas tristique rhoncus commodo. Quisque ac leo a ex maximus pharetra id et urna. Aliquam convallis dapibus erat vel porttitor. Praesent et libero vulputate, fermentum erat non, egestas velit. Morbi bibendum varius metus, at posuere mauris pulvinar auctor. Proin faucibus odio ut lorem rutrum tempus. Donec eget luctus mauris. Cras finibus, odio et luctus dignissim, neque est facilisis dolor, nec ullamcorper arcu leo vitae lorem. Mauris scelerisque sed elit vel porttitor. Mauris sodales diam at lacus congue molestie. Integer sodales consectetur gravida.",
                                Foto = "nk.jpg", TipoImagem = "e", Relevancia = 3, DataPublicacao = new DateTime(2017,9,12,13,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[3]}},
                new Noticia  {IdNoticia= 10, Titulo = "Gastão Elias não resiste a batalha frente a ex-top 25 mundial",
                                Descricao = "Curabitur metus magna, aliquet in purus non, vulputate gravida metus. Suspendisse semper neque non lacus ultricies pellentesque sagittis non leo. Suspendisse ut justo consequat lacus vestibulum cursus. Aenean consectetur metus neque, et tincidunt nisi dictum sit amet. Cras vitae metus vitae nisl fermentum dignissim. Pellentesque fermentum lacus vel erat suscipit congue. Aliquam faucibus ex sit amet vehicula euismod. Nam facilisis porta urna, id fringilla elit lacinia in.",
                                Foto = "elias.png", TipoImagem = "e", Relevancia = 4, DataPublicacao = new DateTime(2017,9,14,16,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[1]}},
                new Noticia  {IdNoticia= 11, Titulo = "Quanto ganhou cada jogador por vencer a Laver Cup?",
                                Descricao = "Integer sed nisl pharetra, laoreet erat quis, efficitur elit. Aliquam tincidunt nec orci sit amet vulputate. Ut congue, magna id dictum egestas, nulla metus egestas ante, at suscipit elit purus at diam. Etiam vestibulum porta nulla, eget finibus leo auctor in. Nulla facilisi. Proin ornare mi vitae felis dictum porta. Nullam iaculis libero eget nibh pretium, eu lobortis nisi tempor. Quisque fringilla odio nec finibus bibendum. Nunc eget tincidunt leo. Vivamus sit amet lacus dignissim, commodo est sit amet, tincidunt urna. Suspendisse nisi libero, pellentesque ut vehicula nec, dictum a est. Fusce iaculis est in nulla ultrices tincidunt. Ut eu arcu pretium, eleifend ante blandit, sollicitudin ipsum. Morbi placerat turpis vitae cursus imperdiet. Sed faucibus, sem quis dapibus consequat, mauris diam ultrices lorem, quis gravida nunc tellus id lacus. Suspendisse ullamcorper nisi auctor varius dapibus.",
                                Foto = "Federer-LAver-Cup.jpg", TipoImagem = "e", Relevancia = 5, DataPublicacao = new DateTime(2017,9,14,01,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[2]}},
                new Noticia  {IdNoticia= 12, Titulo = "O momento em que Rafael Nadal vira treinador de… Roger Federer",
                                Descricao = "Ut vitae feugiat turpis. Maecenas vel eros non nunc iaculis imperdiet. Ut eu urna ligula. Nulla condimentum commodo lorem non elementum. Integer vehicula sem a arcu molestie dictum. Mauris imperdiet a nibh vel convallis. Duis eu odio lacinia orci venenatis finibus in luctus lacus. Proin ut tincidunt elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Phasellus lorem nulla, vestibulum a tellus eu, sodales mattis massa. Vivamus feugiat rutrum magna, sed aliquet sem fermentum convallis. Nullam scelerisque augue dapibus, vestibulum eros in, tempus ex. Maecenas gravida egestas dui nec ullamcorper. Nunc ex urna, molestie eget bibendum eget, vestibulum in lorem.",
                                Foto = "Federer-Nadal.png", TipoImagem = "e", Relevancia = 1, DataPublicacao = new DateTime(2017,9,15,18,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[0],categorias[2]}},
                new Noticia  {IdNoticia= 13, Titulo = "IV Torneio Francisco Leite - Regulamento",
                                Descricao = "Quisque pretium elit diam, nec commodo metus euismod sed. Vestibulum at eros varius dui tempor ullamcorper ac sit amet nulla. Nunc euismod sed arcu sit amet tempus. Aenean vitae ultrices dui, sit amet luctus erat. Nam suscipit, risus non cursus placerat, turpis purus dapibus ipsum, facilisis tincidunt nulla nulla sed ligula. Vivamus sit amet tempus est, in varius tellus. Cras a pulvinar enim, vel efficitur nulla. Ut id risus facilisis, maximus lacus et, varius velit. In vitae nulla ex. Sed eget nisi sit amet metus facilisis aliquet eu sit amet magna. Nulla bibendum ornare tincidunt.",
                                Foto = "franciscoleite.png", TipoImagem = "e", Relevancia = 2, DataPublicacao = new DateTime(2017,9,15,22,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0],categorias[1]}},
                new Noticia  {IdNoticia= 14, Titulo = "III Torneio Templários - Nova Data",
                                Descricao = "Donec iaculis convallis vestibulum. Quisque vel mi non urna lacinia condimentum et ac nisi. Duis semper dui ac cursus pharetra. Mauris est dui, gravida eu neque vel, hendrerit porttitor turpis. Nulla sodales leo vitae justo dictum, et lobortis libero aliquam. Suspendisse pretium leo et fringilla ullamcorper. Etiam nisl velit, congue sit amet lorem vitae, tincidunt dictum nibh. Curabitur lacus quam, tincidunt non dui sed, vestibulum tristique nunc. Maecenas ut tortor volutpat, dignissim risus sed, luctus justo. Nunc non vulputate tellus. Vivamus vitae nisi augue. Fusce semper et nulla quis vestibulum. In rutrum elementum felis, quis pharetra nisi dignissim sit amet. Vestibulum velit quam, hendrerit a ornare nec, venenatis eget purus. Aliquam consequat risus at diam rutrum, eu pellentesque lectus venenatis. Maecenas gravida augue in arcu tempor, quis tempor nisl varius.",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 3, DataPublicacao = new DateTime(2017,9,16,00,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[1],categorias[3]}},
                new Noticia  {IdNoticia= 15, Titulo = "V Torneio do Nabão - Adiamento",
                                Descricao = "Etiam vulputate sem sollicitudin eleifend lacinia. Sed faucibus libero sed mi volutpat, ut vestibulum ligula lacinia. Nam id blandit erat. Mauris lacinia augue quis malesuada suscipit. Nulla lorem felis, commodo et ornare a, sagittis et odio. Nullam nec aliquet lacus, a lacinia lorem. Morbi tempus orci ac magna venenatis iaculis. Nunc pulvinar leo ornare leo lobortis pharetra. Quisque est eros, ultricies facilisis risus ut, lobortis imperdiet tellus. Sed at dictum est. Sed et turpis nisl. In in enim mollis eros commodo malesuada. Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 4, DataPublicacao = new DateTime(2017,9,17,01,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[1],categorias[2]}},
                new Noticia  {IdNoticia= 16, Titulo = "McEnroe diz que «ATP cometeu um erro» na abordagem à Laver Cup",
                                Descricao = "Pellentesque nisl urna, pellentesque sed sagittis sed, tempor eget dui. Morbi odio nulla, scelerisque vel rhoncus mattis, posuere et leo. In et orci vitae lectus interdum congue. Maecenas sit amet eros a elit ornare vulputate eu ac tellus. Integer dictum eros in velit imperdiet, quis commodo eros fringilla. Phasellus ac lacus ut libero efficitur varius. Nulla vulputate dui justo, volutpat efficitur augue pretium dictum. Praesent suscipit orci eu elementum eleifend. Curabitur dignissim faucibus metus et vehicula. Praesent mattis velit sed arcu vestibulum, et maximus leo tincidunt.",
                                Foto = "mcenroe-1.jpg", TipoImagem = "e", Relevancia = 5, DataPublicacao = new DateTime(2017,9,18,07,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[0],categorias[3]}},
                new Noticia  {IdNoticia= 17, Titulo = "Torneio Templários - Regulamento",
                                Descricao = "Pellentesque vulputate ante ante, a auctor lacus pulvinar et. Maecenas a libero in elit sodales fringilla at a libero. Nullam at ullamcorper lorem, auctor euismod est. Nunc leo est, dictum quis ultricies quis, dignissim id mauris. Nam imperdiet elit id malesuada sollicitudin. Proin eu velit arcu. Donec fringilla a nisl vitae laoreet. Pellentesque mollis diam tincidunt malesuada hendrerit. Sed quis viverra velit.",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 1, DataPublicacao = new DateTime(2017,9,19,14,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[0],categorias[2]}},
                new Noticia  {IdNoticia= 18, Titulo = "Laver Cup já tem mais um confirmado para 2018… Nadal: «Uma das melhores semanas do ano»",
                                Descricao = "Vivamus at vestibulum enim, ac dictum ante. Curabitur dictum, turpis vel fermentum feugiat, lacus enim pharetra tellus, sed hendrerit ante nulla ut neque. Nunc felis turpis, congue nec eros at, volutpat vulputate purus. Morbi venenatis dictum nunc sed aliquet. Quisque imperdiet sapien lectus, quis tristique mi eleifend eget. Nullam varius justo a metus elementum commodo. Nullam ex odio, tempus at accumsan tempor, volutpat vel leo. Integer porttitor elementum nulla, quis dapibus lectus dapibus malesuada. Nulla in metus mi. Nulla quis sapien scelerisque, auctor ligula non, venenatis tortor. Sed dictum maximus enim. Maecenas accumsan risus nec nulla faucibus, lacinia iaculis lacus imperdiet.",
                                Foto = "reat.jpg", TipoImagem = "e", Relevancia = 2, DataPublicacao = new DateTime(2017,9,20,19,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[1],categorias[3]}},
                new Noticia  {IdNoticia= 19, Titulo = "III Torneio Templários - Adiamento",
                                Descricao = "Nunc cursus pellentesque dui vitae ultricies. Duis facilisis nec nibh nec sollicitudin. Maecenas mollis nibh non sapien porta, eleifend blandit eros eleifend. Vestibulum ut eros vestibulum, facilisis magna sit amet, euismod leo. Vestibulum id eleifend tortor. Aliquam non est a arcu vehicula suscipit. Praesent vel convallis orci. Donec vehicula justo at lacus tristique faucibus. Nullam tincidunt ornare sapien quis lobortis. Curabitur eget libero non odio pharetra fringilla. Maecenas luctus, nunc vitae interdum scelerisque, tellus lacus placerat metus, sed facilisis lacus orci vitae arcu.",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 3, DataPublicacao = new DateTime(2017,9,20,20,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 2,
                                Categorias=new List<Categoria>{categorias[3]}},
                new Noticia  {IdNoticia= 20, Titulo = "III Torneio Templários - Nova Data",
                                Descricao = "Vivamus eu rhoncus mauris. Pellentesque mattis, lorem quis vulputate blandit, lorem sem sollicitudin enim, sit amet consectetur arcu velit ut purus. Maecenas at libero accumsan lacus dignissim fermentum at non dui. Sed vulputate aliquet risus, quis blandit sapien faucibus et. Nullam faucibus volutpat ex eu euismod. Donec viverra at felis non vestibulum. Etiam nisl augue, viverra nec quam non, lacinia sodales erat. Aliquam placerat gravida nisi, vel varius risus dignissim eget. Quisque scelerisque, urna ac mollis feugiat, felis odio imperdiet odio, ut sagittis velit ligula sit amet turpis. Sed posuere, eros pretium ullamcorper scelerisque, dolor odio tristique dui, ac ornare dui mi et lacus. Nulla eu tortor leo. Vestibulum feugiat est elementum justo bibendum, vel tempor felis egestas. Nullam vitae erat et mauris dapibus vehicula ac in quam. Nunc lorem ipsum, convallis non felis ornare, vestibulum vestibulum mauris.",
                                Foto = "simboloclube.jpg", TipoImagem = "e", Relevancia = 4, DataPublicacao = new DateTime(2017,9,21,17,00,00),
                                DataLimiteVisualizacao =  new DateTime(2017,9,27,21,00,00), Visivel = true, CriadorFK = 3,
                                Categorias=new List<Categoria>{categorias[0]}}

                };
            noticias.ForEach(dd => context.Noticias.AddOrUpdate(d => d.Titulo, dd));
            context.SaveChanges();
            
            var comentarios = new List<Comentario> {
                new Comentario  {IdComentario = 1, Texto = "Siga Começar!",
                                    DataComentario =  new DateTime(2017,9,13,23,00,00), Visivel = true, NoticiaFK = 1, CriadorFK = 4},
                new Comentario  {IdComentario = 2, Texto = "Borá lá pessoal!",
                                    DataComentario =  new DateTime(2017,9,13,23,30,00), Visivel = true, NoticiaFK = 1, CriadorFK = 5},
                new Comentario  {IdComentario = 3, Texto = "Come on!",
                                    DataComentario =  new DateTime(2017,9,14,10,30,00), Visivel = true, NoticiaFK = 1, CriadorFK = 6},
                new Comentario  {IdComentario = 4, Texto = "Finalmente!",
                                    DataComentario =  new DateTime(2017,9,14,13,30,00), Visivel = true, NoticiaFK = 2, CriadorFK = 4},
                new Comentario  {IdComentario = 5, Texto = "Oh.. a sério??",
                                    DataComentario =  new DateTime(2017,9,15,17,00,00), Visivel = true, NoticiaFK = 3, CriadorFK = 4},
                new Comentario  {IdComentario = 6, Texto = "Ninguém pára Del Potro!",
                                    DataComentario =  new DateTime(2017,9,16,19,30,00), Visivel = true, NoticiaFK = 6, CriadorFK = 5},
                new Comentario  {IdComentario = 7, Texto = "Pena Roger ter perdido..",
                                    DataComentario =  new DateTime(2017,9,17,23,30,00), Visivel = true, NoticiaFK = 7, CriadorFK = 6},
                new Comentario  {IdComentario = 8, Texto = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                                    DataComentario =  new DateTime(2017,9,17,14,30,00), Visivel = true, NoticiaFK = 8, CriadorFK = 7},
                new Comentario  {IdComentario = 9, Texto = "Praesent scelerisque leo quis viverra imperdiet.",
                                    DataComentario =  new DateTime(2017,9,17,15,30,00), Visivel = true, NoticiaFK = 7, CriadorFK = 7},
                new Comentario  {IdComentario = 10, Texto = "Sed porttitor libero non enim tempor, eu molestie nunc pulvinar.",
                                    DataComentario =  new DateTime(2017,9,18,10,30,00), Visivel = true, NoticiaFK = 5, CriadorFK = 4},
                new Comentario  {IdComentario = 11, Texto = "Quisque non libero eu est porttitor aliquam id ut dolor.",
                                    DataComentario =  new DateTime(2017,9,19,01,30,00), Visivel = true, NoticiaFK = 4, CriadorFK = 6},
                new Comentario  {IdComentario = 12, Texto = "Maecenas fringilla quam sit amet diam cursus, vitae cursus ante posuere.",
                                    DataComentario =  new DateTime(2017,9,19,02,30,00), Visivel = true, NoticiaFK = 9, CriadorFK = 7},
                new Comentario  {IdComentario = 13, Texto = "Maecenas eget massa faucibus, blandit turpis et, efficitur nunc.",
                                    DataComentario =  new DateTime(2017,9,19,05,30,00), Visivel = true, NoticiaFK = 5, CriadorFK = 5},
                new Comentario  {IdComentario = 14, Texto = "Duis placerat orci vitae vehicula iaculis.",
                                    DataComentario =  new DateTime(2017,9,20,08,30,00), Visivel = true, NoticiaFK = 1, CriadorFK = 5},
                new Comentario  {IdComentario = 15, Texto = "Quisque viverra elit sit amet lacus blandit, sit amet porttitor turpis placerat.",
                                    DataComentario =  new DateTime(2017,9,20,16,30,00), Visivel = true, NoticiaFK = 2, CriadorFK = 5},
                new Comentario  {IdComentario = 16, Texto = "Fusce sed dolor sed felis molestie auctor.",
                                    DataComentario =  new DateTime(2017,9,22,17,30,00), Visivel = true, NoticiaFK = 3, CriadorFK = 4},
                new Comentario  {IdComentario = 17, Texto = "Proin semper elit eget leo mollis dapibus.",
                                    DataComentario =  new DateTime(2017,9,25,18,30,00), Visivel = true, NoticiaFK = 7, CriadorFK = 4},
                new Comentario  {IdComentario = 18, Texto = "Nam ac augue ac turpis aliquam egestas facilisis at felis.",
                                    DataComentario =  new DateTime(2017,9,27,22,30,00), Visivel = true, NoticiaFK = 4, CriadorFK = 7},
                new Comentario  {IdComentario = 19, Texto = "Duis rutrum velit sit amet nibh facilisis suscipit.",
                                    DataComentario =  new DateTime(2017,9,28,00,30,00), Visivel = true, NoticiaFK = 5, CriadorFK = 4},
                new Comentario  {IdComentario = 20, Texto = "Sed viverra eros eget vestibulum posuere.",
                                    DataComentario =  new DateTime(2017,9,28,06,30,00), Visivel = true, NoticiaFK = 10, CriadorFK = 6}
            };
            comentarios.ForEach(dd => context.Comentarios.AddOrUpdate(d => d.IdComentario, dd));
            context.SaveChanges();
        }
    }
}
