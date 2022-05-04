using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Proyecciones.Models;
using System.Security.Claims;

namespace Proyecciones.Controllers
{
    public class LoginController : Controller
    {
        //GET: Login
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            var user = User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                return View();
            }
            else
            {
                SAADSTJEntities db = new SAADSTJEntities();
                ProyeccionEntities dbs = new ProyeccionEntities();
                var usuEst = db.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
                var usuAdm = dbs.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name && x.estado == true);
                if (usuEst != null)
                {
                    var modeloEstudio = db.InscripcionCarrera.OrderByDescending(x => x.Fecha).FirstOrDefault(x => x.IdEstudiante == usuEst.Id && x.PlanEstudio.IdModeloEstudio == 2);
                    if (modeloEstudio != null)
                    {
                        return RedirectToAction("Index", "HomeEstudiante");

                    }
                    else
                    {
                        return View();
                    }

                }
                else if(usuAdm != null)
                {
                    if (dbs.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "HOME") != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (dbs.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "HOMERYC") != null)
                    {
                        return RedirectToAction("Index", "HomeRyC");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(string inputUser, string inputPassword)
        {
            SAADSTJEntities db = new SAADSTJEntities();
            ProyeccionEntities dbs = new ProyeccionEntities();
            Status s = new Status();
            if (ModelState.IsValid)
            {
                Persona userEst = db.Persona.SingleOrDefault(x => x.DocumentoIdentidad == inputUser & x.DocumentoIdentidad == inputPassword);
                Usuario userAdm = dbs.Usuario.SingleOrDefault(x => x.usuario1 == inputUser && x.contraseña == inputPassword && x.estado == true);
                if (userEst != null)
                {
                    var modeloEstudio = db.InscripcionCarrera.OrderByDescending(x => x.Fecha).FirstOrDefault(x => x.IdEstudiante == userEst.Id && x.PlanEstudio.IdModeloEstudio == 2);
                    if (modeloEstudio != null)
                    {
                        IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                        ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                        identity.AddClaim(new Claim(ClaimTypes.Name, inputUser));
                        AuthenticationProperties props = new AuthenticationProperties();
                        props.IsPersistent = true;
                        authenticationManager.SignIn(props, identity);
                        return RedirectToAction("Index", "HomeEstudiante");
                    }
                    else
                    {
                        ViewBag.UsuarioInvalido = 2;
                    }
                }
                else if(userAdm != null)
                {
                    if (dbs.RolPermisos.SingleOrDefault(x => x.idRol == userAdm.idRol && x.Permisos.nombre == "HOME") != null)
                    {
                        IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                        ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                        identity.AddClaim(new Claim(ClaimTypes.Name, inputUser));
                        AuthenticationProperties props = new AuthenticationProperties();
                        props.IsPersistent = true;
                        authenticationManager.SignIn(props, identity);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (dbs.RolPermisos.SingleOrDefault(x => x.idRol == userAdm.idRol && x.Permisos.nombre == "HOMERYC") != null)
                    {
                        IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                        ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                        identity.AddClaim(new Claim(ClaimTypes.Name, inputUser));
                        AuthenticationProperties props = new AuthenticationProperties();
                        props.IsPersistent = true;
                        authenticationManager.SignIn(props, identity);
                        return RedirectToAction("Index", "HomeRyC");
                    }
                    else
                    {
                        ViewBag.UsuarioInvalido = 1;
                    }
                }
                else
                {
                    ViewBag.UsuarioInvalido = 1;
                }
            }
            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}