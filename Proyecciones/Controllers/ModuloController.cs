using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;


namespace Proyecciones.Controllers
{
    public class ModuloController : Controller
    {
        // GET: Modulo
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "MODULO") != null)
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

        //Listar
        public ActionResult Listar()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            var m = db.Modulo.Where(x=>x.estado==true).ToList();
            List<object[]> lista = new List<object[]>();
            int i = 1;
            //string estado = ""; 
            var opcion = " ";
            foreach (var item in m)
            {
                var mes = dbs.Mes.Single(x => x.Id == item.idMes).Nombre.ToString();
                switch (item.estado)
                {
                    case true:
                        //estado = "Activado";
                        opcion = "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-sm btn-link' onclick='Desactivar(" + item.id + ")'><i class='fas fa-times'></i></button></div>";
                        break;

                    case false:
                        //estado = "Desacctivado";
                        opcion = "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-sm btn-link' onclick='Activar(" + item.id + ")'><i class='fas fa-check'></i></button></div>";
                        break;
                }
                var botones = "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-sm btn-link' data-bs-toggle='modal' onclick='Editar(" + item.id + ")' data-bs-target='#exampleModal'><i class='fas fa-pencil-alt'></i></button></div>";
                botones += opcion;
                object[] o =
                {
                    i,
                    item.nombre,
                    item.numModulo,
                    item.semestre,
                    item.gestion,
                    mes,
                    botones,
                };
                lista.Add(o);
                i++;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Mostrar
        public ActionResult Mostrar(int id)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            var m = db.Modulo.Single(x => x.id == id);
            var o = new
            {
                id = m.id,
                nombre = m.nombre,
                numModulo = m.numModulo,
                semestre = m.semestre,
                gestion = m.gestion,
                idMes = m.idMes,
                estado = m.estado
            };
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        //Cargar Mes
        public ActionResult CargarMes()
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> Meses = new List<object>();
            List<Mes> lista = db.Mes.SqlQuery("select * from Mes").ToList();
            foreach (var item in lista)
            {
                object o = new
                {
                    Id = item.Id,
                    Nombre = item.Nombre
                };
                Meses.Add(o);
            }
            return Json(Meses, JsonRequestBehavior.AllowGet);
        }

        //Guardar
        public ActionResult Guardar(Modulo m)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                var obj = db.Modulo.SingleOrDefault(x => x.id == m.id);
                if (m.nombre == null)
                {
                    s.Tipo = 2;
                    s.Msj = "La oferta no debe estar vacía";
                }
                if (m.numModulo == null)
                {
                    s.Tipo = 2;
                    s.Msj = "El módulo no debe estar vació";
                }
                if (m.semestre == null)
                {
                    s.Tipo = 2;
                    s.Msj = "El semestre no debe estar vació";
                }
                if (m.gestion == null)
                {
                    s.Tipo = 2;
                    s.Msj = "La gestión no debe estar vacía";
                }
                if (m.idMes == 0)
                {
                    s.Tipo = 2;
                    s.Msj = "Debe seleccionar un mes";
                }
                if (s.Msj == null)
                {
                    if (obj ==  null)
                    {
                        var moduloRepetido = db.Modulo.Count(x => x.nombre == m.nombre);
                        if (moduloRepetido > 0)
                        {
                            s.Tipo = 2;
                            s.Msj = "La oferta ya existe";
                        }
                        else
                        {
                            db.Modulo.Add(m);
                            db.SaveChanges();
                            s.Tipo = 1;
                            s.Msj = "Se registró correctamente";
                        }
                    }
                    else
                    {
                        obj.nombre = m.nombre;
                        obj.estado = m.estado;
                        obj.semestre = m.semestre;
                        obj.numModulo = m.numModulo;
                        obj.gestion = m.gestion;
                        obj.idMes = m.idMes;
                        db.SaveChanges();
                        s.Tipo = 1;
                        s.Msj = "Se editó correctamente";
                    }
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuníquese con el Administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Desactivar
        public ActionResult Desactivar(int id)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                var m = db.Modulo.Single(x => x.id == id);
                m.estado = false;
                db.SaveChanges();
                s.Tipo = 1;
                s.Msj = "Se desactivo correctamente";
            }
            catch
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuníquese con el Administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Activar
        public ActionResult Activar(int id)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                var m = db.Modulo.Single(x => x.id == id);
                m.estado = true;
                db.SaveChanges();
                s.Tipo = 1;
                s.Msj = "Se activo correctamente";
            }
            catch
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuníquese con el Administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}