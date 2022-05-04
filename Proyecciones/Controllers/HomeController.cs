using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new  ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "HOME") != null)
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

        //Listar Materias Proyectadas en la Manana
        public ActionResult MateriasManana(int idMod)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object[]> lista = new List<object[]>();
            try
            {
                int c = 0;
                var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 1 && x.AdmProyeccion.idModulo == idMod ).GroupBy(x => x.idMateria).ToList();
                foreach (var item in MateriasManana)
                {
                    string nombreMateria = dbs.Materia.Single(x => x.Id == item.Key).Nombre;
                    var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item.Key && x.AdmMateria.idTurno == 1 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                    c++;
                    object[] o = {
                        c,
                        nombreMateria,
                        cantidadEst
                        };
                    lista.Add(o);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Listar Materias Proyectadas en la Manana
        public ActionResult MateriasMedioDia(int idMod)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object[]> lista = new List<object[]>();
            try
            {
                int c = 0;
                var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 4 && x.AdmProyeccion.idModulo == idMod).GroupBy(x => x.idMateria).ToList();
                foreach (var item in MateriasManana)
                {
                    string nombreMateria = dbs.Materia.Single(x => x.Id == item.Key).Nombre;
                    var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item.Key && x.AdmMateria.idTurno == 4 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                    c++;
                    object[] o = {
                        c,
                        nombreMateria,
                        cantidadEst
                    };
                    lista.Add(o);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Listar Materias Proyectadas en la Manana
        public ActionResult MateriasTarde(int idMod)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object[]> lista = new List<object[]>();
            try
            {
                int c = 0;
                var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 2 && x.AdmProyeccion.idModulo == idMod).GroupBy(x => x.idMateria).ToList();
                foreach (var item in MateriasManana)
                {
                    string nombreMateria = dbs.Materia.Single(x => x.Id == item.Key).Nombre;
                    var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item.Key && x.AdmMateria.idTurno == 2 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                    c++;
                    object[] o = {
                        c,
                        nombreMateria,
                        cantidadEst
                    };
                    lista.Add(o);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Listar Materias Proyectadas en la Manana
        public ActionResult MateriasNoche(int idMod)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object[]> lista = new List<object[]>();
            try
            {
                int c = 0;
                var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 3 && x.AdmProyeccion.idModulo == idMod).GroupBy(x => x.idMateria).ToList();
                foreach (var item in MateriasManana)
                {
                    string nombreMateria = dbs.Materia.Single(x => x.Id == item.Key).Nombre;
                    var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item.Key && x.AdmMateria.idTurno == 3 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                    c++;
                    object[] o = {
                        c,
                        nombreMateria,
                        cantidadEst
                    };
                    lista.Add(o);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}