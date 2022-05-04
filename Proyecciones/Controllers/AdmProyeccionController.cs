using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class AdmProyeccionController : Controller
    {
        // GET: AdmProyeccion
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

        public ActionResult BuscarOferta()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.IsAuthenticated)
                {
                    ProyeccionEntities db = new ProyeccionEntities();
                    var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                    if (usuAdm != null)
                    {
                        if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "BUSCAROFERTA") != null)
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //Cargar Modulos al Menu

        public ActionResult CargarMenu(int activo)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            var obj = "";
            //int cont = 0;
            foreach (var item in db.Modulo.SqlQuery("select top 6 * from Modulo where estado=1 order by nombre asc").ToList())
            {
                var mes = dbs.Mes.Single(x => x.Id == item.idMes).Abreviacion.ToString();
                if (activo == item.id)
                {
                    obj += "<a onclick='SeleccionarModulo(" + item.id + ")' class='nav-item nav-link active bg-primary text-white' id='" + item.id + "' data-toggle='tab' role='tab' aria-selected='true'>" + mes + " - " + item.nombre + "</a>";
                }
                else
                {
                    obj += "<a onclick='SeleccionarModulo(" + item.id + ")' class='nav-item nav-link ' id='" + item.id + "' data-toggle='tab' role='tab' aria-selected='false'>" + mes + " - " + item.nombre + "</a>";
                }
                //cont++;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //Listar Modulos
        public ActionResult ListarModulos()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            List<object> modulos = new List<object>();
            var lista = db.Modulo.SqlQuery("select top 6 * from Modulo where estado=1 order by nombre asc").ToList();
            foreach (var item in lista)
            {
                object o = new
                {
                    id = item.id,
                    nombre = item.nombre
                };
                modulos.Add(o);

            }
            return Json(modulos, JsonRequestBehavior.AllowGet);
        }

        //Primer Modulo
        public ActionResult ModuloActual()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            var lista = db.Modulo.SqlQuery("select top 1 * from Modulo where estado = 1 order by nombre asc").ToList();
            Modulo o = new Modulo();
            object obj = null;
            if (lista.Count > 0)
            {
                o = lista[0];
                obj = new
                {
                    id = o.id,
                    nombre = o.nombre
                };
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //Listar
        public ActionResult Listar(int idmodulos, int idSemestres)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object[]> lista = new List<object[]>();
            ClassAdmProyeccion cadm = new ClassAdmProyeccion();
            List<ClassAdmProyeccion> l = new List<ClassAdmProyeccion>();
            int c = 0;
            foreach (var item in db.AdmMateria.Where(x=>x.AdmProyeccion.idModulo == idmodulos && x.AdmProyeccion.idSemestre == idSemestres && x.AdmProyeccion.estado == true))
            {
                var carre = dbs.PlanEstudio.SingleOrDefault(x => x.Id == item.AdmProyeccion.idCarrera).Nombre.ToString();
                if (c == 0)
                {
                    cadm.Id = item.idAdmProyeccion;
                    cadm.Carrera = carre;
                    cadm.Idadmproyecion = item.idAdmProyeccion;
                    switch (item.idTurno)
                    {
                        case 1:
                            cadm.Matemanana = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                        case 2:
                            cadm.Matetarde = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                        case 3:
                            cadm.Matenoche = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                        case 4:
                            cadm.Matemediodia = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                    }
                    l.Add(cadm);
                }
                else
                {
                    var objadm = l.SingleOrDefault(x => x.Idadmproyecion == item.idAdmProyeccion );
                    if (objadm==null)
                    {
                        ClassAdmProyeccion obj = new ClassAdmProyeccion();
                        obj.Id = item.idAdmProyeccion;
                        obj.Carrera = carre;
                        obj.Idadmproyecion = item.idAdmProyeccion;
                        switch (item.idTurno)
                        {
                            case 1:
                                obj.Matemanana = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 2:
                                obj.Matetarde = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 3:
                                obj.Matenoche = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 4:
                                obj.Matemediodia = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                        }
                        l.Add(obj);
                    }
                    else
                    {
                        switch (item.idTurno)
                        {
                            case 1:
                                objadm.Matemanana = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 2:
                                objadm.Matetarde = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 3:
                                objadm.Matenoche = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 4:
                                objadm.Matemediodia = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                        }
                    }
                   
                }
                c++;
            }
            foreach (var item in l)
            {
                var botones = "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-sm btn-link' data-bs-toggle='modal' onclick='Editar(" + item.Idadmproyecion + ")' data-bs-target='#exampleModal'><i class='fas fa-pencil-alt'></i></button></div>";
                botones += "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-sm btn-link' onclick='EliminarProyeccion(" + item.Idadmproyecion + ")'><i class='fas fa-trash-alt'></i></button></div>";
                object[] o = {
                        item.Carrera,
                        item.Matemanana,
                        item.Matemediodia,
                        item.Matetarde,
                        item.Matenoche,
                        botones
                    };
                lista.Add(o);
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Mostrar
        public ActionResult Mostrar(int id)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            var ap = db.AdmProyeccion.Single(x => x.id == id);
            var carre = dbs.PlanEstudio.SingleOrDefault(x => x.Id == ap.idCarrera).Nombre.ToString();
            List<object> ListarMateriasAgregadas = new List<object>();
            List<AdmMateria> lista = db.AdmMateria.SqlQuery("select * from AdmMateria where idAdmProyeccion="+id+"").ToList();
            foreach (var item in lista)
            {
                object ob = new
                {
                    id = item.id,
                    idMateria = item.idMateria,
                    idTurno = item.idTurno
                };
                ListarMateriasAgregadas.Add(ob);
            }
            var o = new
            {
                id = ap.id,
                idCarrera = ap.idCarrera,
                nombreCarrera = carre,
                idModulo = ap.idModulo,
                idSemestre = ap.idSemestre,
                ListarMateriasAgregadas

            };
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        //Guardar
        public ActionResult Guardar(AdmProyeccion ap)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                var obj = db.AdmProyeccion.SingleOrDefault(x => x.id == ap.id);
                if (ap.AdmMateria.Count == 0 && ap.id == 0)
                {
                    s.Tipo = 2;
                    s.Msj = "Debe agregar al menos una materia para la carrera seleccionada";
                }
                if (ap.idSemestre == 0)
                {
                    s.Tipo = 2;
                    s.Msj = "Debe seleccionar un semestre";
                }
                if (ap.idModulo == 0)
                {
                    s.Tipo = 2;
                    s.Msj = "Debe seleccionar un módulo";
                }
                if (ap.idCarrera == 0)
                {
                    s.Tipo = 2;
                    s.Msj = "Debe seleccionar una carrera";
                }
                if (s.Msj == null)
                {
                    if (obj == null)
                    {
                        db.AdmProyeccion.Add(ap);
                        db.SaveChanges();
                        foreach (var item in ap.AdmMateria)
                        {
                            item.idAdmProyeccion = ap.id;
                        }
                        db.SaveChanges();
                        s.Tipo = 1;
                        s.Msj = "Se registró correctamente";
                    }
                    else
                    {
                        obj.id = ap.id;
                        obj.idCarrera = ap.idCarrera;
                        obj.idModulo = ap.idModulo;
                        obj.idSemestre = ap.idSemestre;
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

        //Cargar Lista de Materias Nuevas
        public ActionResult ListarMateriasAgregadas(int idAP)
        { 
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object> lista = new List<object>();
            var detalle = db.AdmMateria.Where(x => x.idAdmProyeccion == idAP).ToList();
            foreach (var item in detalle)
            {
                string Nombre = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                string Turno = dbs.Turno.Single(x => x.Id == item.idTurno).Nombre.ToString();
                var materias = new { idAdmMateria = item.id, nombre = Nombre, turno = Turno };
                lista.Add(materias);
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Borrar Materia en la Edicion
        public ActionResult BorrarMateriaEditada(int Id, int IdProyeccion)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                List<AdmMateria> lista = db.AdmMateria.SqlQuery("select * from AdmMateria where idAdmProyeccion=" + IdProyeccion + "").ToList();
                int c = 0;
                foreach (var item in lista)
                {
                    c++;
                }
                if (c > 1)
                {
                    var am = db.AdmMateria.Single(x => x.id == Id);
                    db.AdmMateria.Remove(am);
                    db.SaveChanges();
                    s.Tipo = 1;
                }
                else
                {
                    s.Tipo = 2;
                    s.Msj = "La carrera debe tener al menos una materia Agregada";
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuníquese con el Administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Borar Materia en la Edicion
        public ActionResult GurdarMateriaEditada(AdmMateria m)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            db.AdmMateria.Add(m);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        //Buscar Carrera
        public ActionResult BuscarCarrera(string carrera)
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> carreras = new List<object>();
            // IdModeloEstudio = 2 ---> competencia
            // IdModeloEstudio = 1 ---> objetivo
            List<PlanEstudio> lista = db.PlanEstudio.SqlQuery("select top 4 * from PlanEstudio where IdEstadoPlan = 0 and IdModeloEstudio = 2 and Nombre like '%" + carrera + "%'").ToList();
            foreach (var item in lista)
            {
                object o = new
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Sigla = item.Sigla
                };
                carreras.Add(o);
            }
            return Json(carreras, JsonRequestBehavior.AllowGet);
        }

        //Seleccionar Carrera
        public ActionResult SeleccionarCarrera(int id)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            var carreraSeleccionada = dbs.PlanEstudio.Single(x => x.Id == id);
            object o = new
            {
                id = carreraSeleccionada.Id,
                nombre = carreraSeleccionada.Nombre
            };
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        //Buscar Materia
        public ActionResult BuscarMateria(string nombreM)
        {
            Status s = new Status();
            try
            {
                SAADSTJEntities db = new SAADSTJEntities();
                List<object> Materias = new List<object>();
                // IdModeloEstudio = 2 ---> competencia
                // IdModeloEstudio = 1 ---> objetivo
                List<Materia> lista = db.Materia.SqlQuery("Select top 4 m.* from Materia m, MateriaContenido mc where m.IdMateriaContenido = mc.Id and mc.IdModeloEstudio = 2 and m.Nombre like '%" + nombreM + "%'").ToList();
                foreach (var item in lista)
                {
                    object o = new
                    {
                        Id = item.Id,
                        Sigla = item.Sigla,
                        Nombre = item.Nombre
                    };
                    Materias.Add(o);
                }
                return Json(Materias, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "Para Buscar debe utilizar solo letras";
                return Json(s, JsonRequestBehavior.AllowGet);
            }
        }

        //Agregar Materia en Proyeccion
        public ActionResult AgregarMateria(AdmMateria am)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                var turnoExiste = db.AdmMateria.SingleOrDefault(x => x.idTurno == am.idTurno && x.idAdmProyeccion == am.idAdmProyeccion);
                if (turnoExiste == null)
                {
                    db.AdmMateria.Add(am);
                    db.SaveChanges();
                    s.Tipo = 1;
                }
                else
                {
                    s.Tipo = 2;
                    s.Msj = "Ya existe una materia en el turno seleccionado";
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuníquese con el Administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Seleccionar Materia
        public ActionResult SeleccionarMateria(int id)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            var materiaSeleccionada = dbs.Materia.Single(x => x.Id == id);
            object o = new
            {
                Id = materiaSeleccionada.Id,
                Nombre = materiaSeleccionada.Nombre
            };
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        
        //Cargar combo de Modulo
        public ActionResult CargarModulo()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            List<object> Modulos = new List<object>();
            List<Modulo> lista = db.Modulo.SqlQuery("select top 6 * from Modulo where estado=1 order by nombre asc").ToList();
            foreach (var item in lista)
            {
                object o = new
                {
                    id = item.id,
                    nombre = item.nombre
                };
                Modulos.Add(o);
            }
            return Json(Modulos, JsonRequestBehavior.AllowGet);
        }
        
        //Primer Modulo Seleccion Automatica
        public ActionResult PrimerModulo()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            var lista = db.Modulo.SqlQuery("select top 1 * from Modulo where estado = 1 order by nombre asc").ToList();
            Modulo o = new Modulo();
            object obj = null;
            if (lista.Count > 0)
            {
                o = lista[0];
                obj = new
                {
                    id = o.id,
                    nombre = o.nombre
                };
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //Cargar combo de Semestre
        public ActionResult CargarSemestre()
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> Semestres = new List<object>();
            List<DescripcionSemestre> lista = db.DescripcionSemestre.SqlQuery("select top 10 * from DescripcionSemestre order by(Id)").ToList();
            foreach (var item in lista)
            {
                object o = new
                {
                    Id = item.Id,
                    Nombre = item.Nombre
                };
                Semestres.Add(o);
            }
            return Json(Semestres, JsonRequestBehavior.AllowGet);
        }

        //Cargar combo de Turno
        public ActionResult CargarTurnos()
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> Turnos = new List<object>();
            List<Turno> lista = db.Turno.SqlQuery("select * from Turno order by DescripcionHorario").ToList();
            foreach (var item in lista)
            {
                object o = new
                {
                    Id = item.Id,
                    Nombre = item.Nombre
                };
                Turnos.Add(o);
            }
            return Json(Turnos, JsonRequestBehavior.AllowGet);
        }

        //Eliminar Proyeccion
        public ActionResult EliminarProyeccion(int id)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                var admproyec = db.AdmProyeccion.Single(x => x.id == id);
                admproyec.estado = false;
                db.SaveChanges();
                s.Tipo = 1;
                s.Msj = "Se eliminó correctamente";
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