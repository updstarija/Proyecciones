using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "ADMPROYECCION") != null)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "SinAcceso");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "HomeEstudiante");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Estadisticas()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbS = new SAADSTJEntities();
            //int admEmpresas, comuSocial, contPublica, derecho, civil, comercial, sistemas, ambiental, petrolera, redes, industrial, psicologia;
            int admEmpresas = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && (x.idCarrera == 1002 || x.idCarrera == 1025));
            int comuSocial = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && (x.idCarrera == 1015 || x.idCarrera == 1024));
            int contPublica = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && (x.idCarrera == 1003 || x.idCarrera == 1017));
            int derecho = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && (x.idCarrera == 1008 || x.idCarrera == 1022));
            int civil = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == 1007);
            int comercial = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && (x.idCarrera == 1009 || x.idCarrera == 1026));
            int sistemas = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == 1004);
            int ambiental = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == 1010);
            int petrolera = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == 1006);
            int redes = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == 1014);
            int industrial = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == 1005);
            int psicologia = db.Boleta.Count(x => (x.estado == 1 || x.estado == 4) && (x.idCarrera == 1013 || x.idCarrera == 1023));
            object o = new
            {
                admEmpresas,
                comuSocial,
                contPublica,
                derecho,
                civil,
                comercial,
                sistemas,
                ambiental,
                petrolera,
                redes,
                industrial,
                psicologia
            };
            return Json(o, JsonRequestBehavior.AllowGet);
        }
    }
}