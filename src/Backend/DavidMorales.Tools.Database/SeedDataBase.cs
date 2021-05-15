using DavidMorales.Domain.Authorization;
using DavidMorales.Domain.Entities;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DavidMorales.Tools.Database
{
    public class SeedDataBase
    {
        private readonly Infrastructure.Context.AppContext _context;
        private readonly UserManager<AppUser> _usuarioManager;
        private readonly RoleManager<AppRole> _rolManager;

        //private static JObject _data;

        public SeedDataBase(Infrastructure.Context.AppContext context,
                            UserManager<AppUser> userManager,
                            RoleManager<AppRole> roleManager)
        {
            _context = context;
            _usuarioManager = userManager;
            _rolManager = roleManager;

            //var path = Path.Combine(Environment.CurrentDirectory, "SeedData.json");
            //_data = JObject.Parse(File.ReadAllText(path, Encoding.GetEncoding("iso-8859-1")));
        }

        public async Task EnsureSeedData()
        {
            try
            {
                // Creación de roles
                foreach (var rol in AppRoles.Get())
                {
                    var exists = await _rolManager.FindByNameAsync(rol.Name);
                    if (exists != null)
                        continue;

                    var result = await _rolManager.CreateAsync(new AppRole { Name = rol.Name });

                    if (!result.Succeeded)
                        continue;

                    var claims = new List<string>();
                    if (rol == AppRoles.DocumentManager)
                    {
                        claims.Add(AppPermissions.Document.Add);
                        claims.Add(AppPermissions.Document.Query);
                        claims.Add(AppPermissions.Document.Edit);
                        claims.Add(AppPermissions.Document.View);

                        claims.Add(AppPermissions.Person.Query);
                    }

                    if (rol == AppRoles.User)
                    {
                        claims.Add(AppPermissions.Document.Query);
                        claims.Add(AppPermissions.Document.View);
                    }

                    var roleName = await _rolManager.FindByNameAsync(rol.Name);
                    foreach (var claim in claims)
                    {
                        await _rolManager.AddClaimAsync(roleName, new Claim(AppClaimTypes.Permission, claim));
                    }
                }

                if(!_context.Companies.Any())
                {
                    var companies = new List<Company>
                    {
                        new Company { Name = "Alpha" },
                        new Company { Name = "Patitos" },
                    };

                    _context.Companies.AddRange(companies);

                    await _context.SaveChangesAsync();
                }

                if (!_context.People.Any())
                {
                    var person = new Person
                    {
                        CompanyId = 2,
                        Name = "Jose",
                        LastName = "Mejia",
                        Phone = "30212222222"
                    };
                    _context.People.Add(person);
                    await _context.SaveChangesAsync();
                }

                // Creación de usuario administrador
                if (await _usuarioManager.FindByEmailAsync("admin@app.com") == null)
                {
                    var user = new AppUser
                    {
                        Email = "admin@app.com",
                        UserName = "admin@app.com",
                        Person = new Person
                        {
                            CompanyId = 1,
                            Name = "Admin",
                            LastName = "Admin",
                            Phone = "00000000"
                        }
                    };

                    var result = await _usuarioManager.CreateAsync(user, "P4$$w0rd");

                    if (result.Succeeded)
                    {
                        await _usuarioManager.AddToRoleAsync(user, AppRoles.SuperAdmin.Name);
                    }
                }

                // Creación de usuario
                if (await _usuarioManager.FindByEmailAsync("andres@app.com") == null)
                {
                    var user = new AppUser
                    {
                        Email = "andres@app.com",
                        UserName = "andres@app.com",
                        Person = new Person
                        {
                            CompanyId = 1,
                            Name = "Andres",
                            LastName = "Iniesta",
                            Phone = "300080808"
                        }
                    };

                    var result = await _usuarioManager.CreateAsync(user, "P4$$w0rd");

                    if (result.Succeeded)
                    {
                        await _usuarioManager.AddToRoleAsync(user, AppRoles.User.Name);
                    }
                }

                // Creación de gestor de documentos
                if (await _usuarioManager.FindByEmailAsync("xabi@app.com") == null)
                {
                    var user = new AppUser
                    {
                        Email = "xabi@app.com",
                        UserName = "xabi@app.com",
                        Person = new Person
                        {
                            CompanyId = 1,
                            Name = "Xabi",
                            LastName = "Hernandez",
                            Phone = "300080808"
                        }
                    };

                    var result = await _usuarioManager.CreateAsync(user, "P4$$w0rd");

                    if (result.Succeeded)
                    {
                        await _usuarioManager.AddToRoleAsync(user, AppRoles.DocumentManager.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} --- {ex.InnerException}");
            }
        }
    }
}
