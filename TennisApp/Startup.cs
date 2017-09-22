using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Mapping;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.Services.Description;
using TennisApp.Models;

[assembly: OwinStartupAttribute(typeof(TennisApp.Startup))]
namespace TennisApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
            iniciaAplicacao();
        }
                
        private void iniciaAplicacao()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Administrador'
            if (!roleManager.RoleExists("Administrador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);

                // criar um utilizador 'Default'
                var user = new ApplicationUser();
                user.UserName = "admin@a.aa";
                user.Email = "admin@a.aa";
                string userPWD = "123_Aa";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Default-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrador");
                    user.EmailConfirmed = true;
                }
                var utilizadores = new List<Utilizador>
                {
                    new Utilizador {IdUtilizador = 1, Nome ="Admin", Email = "admin@a.aa",
                                    Telemovel = "960000001", DataNasc = new DateTime(1990,1,1), UserName = "admin@a.aa" }
                };
                utilizadores.ForEach(dd => db.Utilizadores.AddOrUpdate(d => d.Nome, dd));
                db.SaveChanges();
            }

            // Criar a role 'Default'
            if (!roleManager.RoleExists("Default"))
            {
                var role = new IdentityRole();
                role.Name = "Default";
                roleManager.Create(role);
            }
        }
    }
}
