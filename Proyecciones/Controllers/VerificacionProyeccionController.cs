using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class VerificacionProyeccionController : Controller
    {
        // GET: VerificacionProyeccion
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "VERIFICACIONPROYECCION") != null)
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

        //Proyecciones enviadas a revicion Homologados
        public ActionResult ARevisar()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "REVISARPROYECCION") != null)
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

        //Proyecciones enviadas a Homologados 
        public ActionResult Homologados()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "VERIFICACIONPROYECCION") != null)
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

        //Proyecciones enviadas a Homologados 
        public ActionResult TotalEstudiantes()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "VERIFICACIONPROYECCION") != null)
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

        //Detalle de Proyeccion Seleccionada
        public ActionResult RevisarProyeccion(int idBole)
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "VERIFICACIONPROYECCION") != null)
                    {
                        ViewBag.idBoleta = idBole;
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
        public ActionResult Listar(int estadoBoleta)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Usuario uj = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
            int i = 0;
            List<object[]> lista = new List<object[]>();
            if (db.RolPermisos.SingleOrDefault(x => x.idRol == uj.idRol && x.Permisos.nombre == "FACULTAD DE CIENCIAS JURÍDICAS") != null)
            {
                var carreraFacultad = dbs.PlanEstudio.OrderBy(x=> x.Nombre).Where(x => x.IdModeloEstudio == 2 && x.Carrera.DireccionArea.Facultad.Nombre == "FACULTAD DE CIENCIAS JURÍDICAS");
                foreach (var item2 in carreraFacultad)
                {
                    var boletaProyeccione = db.Boleta.Where(x => x.estado == estadoBoleta && x.idCarrera == item2.Id && x.BoletaDetalle.Count > 0).ToList();
                    if (estadoBoleta == 3)
                    {
                        boletaProyeccione = db.Boleta.Where(x => x.estado == estadoBoleta && x.idCarrera == item2.Id).ToList();
                    }
                    foreach (var item in boletaProyeccione)
                    {
                        i++;
                        var estu = dbs.Persona.Single(x => x.Id == item.idEstudiante);
                        var carre = dbs.PlanEstudio.Single(x => x.Id == item.idCarrera).Nombre.ToString();
                        var botones = "";
                        if (estadoBoleta == 3)
                        {
                            botones += "<div style='float:left; margin-left:5px;'><a class='btn btn-primary text-white' href='" + Url.Action("RevisarProyeccion", "VerificacionProyeccion", new { idBole = item.id }) + "' ><i class='fas fa-eye'></i></a></div>";
                        }
                        else
                        {
                            botones += "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-primary' onclick='ListarCabeceraBoleta(" + item.id + ")' data-bs-toggle='modal' data-bs-target='#exampleModalBoletaEstudiante'><i class='fas fa-eye'></i></button></div>";
                        }
                        botones += "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-primary' onclick='Imprimir(" + item.id + ")'><i class='fas fa-print'></i></button>";
                        object[] o =
                        {
                    i,
                    item.fecha.ToShortDateString(),
                    carre,
                    estu.Nombre +" "+ estu.ApellidoPaterno+ " "+ estu.ApellidoMaterno,
                    estu.DocumentoIdentidad,
                    botones,
                    };
                        lista.Add(o);
                    }
                }
            }
            if (db.RolPermisos.SingleOrDefault(x => x.idRol == uj.idRol && x.Permisos.nombre == "TECNOLOGIA") != null)
            {
                var carreraFacultad = dbs.PlanEstudio.OrderBy(x => x.Nombre).Where(x => x.IdModeloEstudio == 2 && x.Carrera.DireccionArea.Facultad.Nombre == "TECNOLOGIA");
                foreach (var item2 in carreraFacultad)
                {
                    var boletaProyeccione = db.Boleta.Where(x => x.estado == estadoBoleta && x.idCarrera == item2.Id && x.BoletaDetalle.Count > 0).ToList();
                    if (estadoBoleta == 3)
                    {
                        boletaProyeccione = db.Boleta.Where(x => x.estado == estadoBoleta && x.idCarrera == item2.Id).ToList();
                    }
                    foreach (var item in boletaProyeccione)
                    {
                        i++;
                        var estu = dbs.Persona.Single(x => x.Id == item.idEstudiante);
                        var carre = dbs.PlanEstudio.Single(x => x.Id == item.idCarrera).Nombre.ToString();
                        var botones = "";
                        if (estadoBoleta == 3)
                        {
                            botones += "<div style='float:left; margin-left:5px;'><a class='btn btn-primary text-white' href='" + Url.Action("RevisarProyeccion", "VerificacionProyeccion", new { idBole = item.id }) + "' ><i class='fas fa-eye'></i></a></div>";
                        }
                        else
                        {
                            botones += "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-primary' onclick='ListarCabeceraBoleta(" + item.id + ")' data-bs-toggle='modal' data-bs-target='#exampleModalBoletaEstudiante'><i class='fas fa-eye'></i></button></div>";
                        }
                        botones += "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-primary' onclick='Imprimir(" + item.id + ")'><i class='fas fa-print'></i></button>";
                        object[] o =
                        {
                    i,
                    item.fecha.ToShortDateString(),
                    carre,
                    estu.Nombre +" "+ estu.ApellidoPaterno+ " "+ estu.ApellidoMaterno,
                    estu.DocumentoIdentidad,
                    botones,
                    };
                        lista.Add(o);
                    }
                }
            }
            if (db.RolPermisos.SingleOrDefault(x => x.idRol == uj.idRol && x.Permisos.nombre == "FACULTAD DE CIENCIAS EMPRESARIALES") != null)
            {
                var carreraFacultad = dbs.PlanEstudio.OrderBy(x => x.Nombre).Where(x => x.IdModeloEstudio == 2 && x.Carrera.DireccionArea.Facultad.Nombre == "FACULTAD DE CIENCIAS EMPRESARIALES");
                foreach (var item2 in carreraFacultad)
                {
                    var boletaProyeccione = db.Boleta.Where(x => x.estado == estadoBoleta && x.idCarrera == item2.Id && x.BoletaDetalle.Count > 0).ToList();
                    if (estadoBoleta == 3)
                    {
                        boletaProyeccione = db.Boleta.Where(x => x.estado == estadoBoleta && x.idCarrera == item2.Id).ToList();
                    }
                    foreach (var item in boletaProyeccione)
                    {
                        i++;
                        var estu = dbs.Persona.Single(x => x.Id == item.idEstudiante);
                        var carre = dbs.PlanEstudio.Single(x => x.Id == item.idCarrera).Nombre.ToString();
                        var botones = "";
                        if (estadoBoleta == 3)
                        {
                            botones += "<div style='float:left; margin-left:5px;'><a class='btn btn-primary text-white' href='" + Url.Action("RevisarProyeccion", "VerificacionProyeccion", new { idBole = item.id }) + "' ><i class='fas fa-eye'></i></a></div>";
                        }
                        else
                        {
                            botones += "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-primary' onclick='ListarCabeceraBoleta(" + item.id + ")' data-bs-toggle='modal' data-bs-target='#exampleModalBoletaEstudiante'><i class='fas fa-eye'></i></button></div>";
                        }
                        botones += "<div style='float:left; margin-left:5px;'><button type='button' class='btn btn-primary' onclick='Imprimir(" + item.id + ")'><i class='fas fa-print'></i></button>";
                        object[] o =
                        {
                    i,
                    item.fecha.ToShortDateString(),
                    carre,
                    estu.Nombre +" "+ estu.ApellidoPaterno+ " "+ estu.ApellidoMaterno,
                    estu.DocumentoIdentidad,
                    botones,
                    };
                        lista.Add(o);
                    }
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Mostrar Descripcion del Estudiante
        public ActionResult MostarMensaje(int idBoleta)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Boleta b = db.Boleta.SingleOrDefault(x => x.id == idBoleta);
            if (b.descripcion != "")
            {
                var s = new
                {
                    Tipo = 1,
                    Msj = b.descripcion,
                    Num = b.numReferencia
                };
                return Json(s, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var s = new
                {
                    Tipo = 2,
                    Num = b.numReferencia
                };
                return Json(s, JsonRequestBehavior.AllowGet);
            }
        }

        //Listar Cabecera de Boleta
        public ActionResult ListarCabeceraBoleta(int idBole)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.id == idBole);
            Persona p = dbs.Persona.SingleOrDefault(x => x.Id == b.idEstudiante);
            string obj = "";
            if (b != null)
            {
                string fechaboleta = b.fecha.ToShortDateString();
                string imagen = "<img width='80' height='80' src='/Proyecciones/Content/img/logo.jpg'/>";
                string modulos = "<th style='text-align: center;' scope='row'>MÓDULOS</th>";
                foreach (var item in db.Modulo.SqlQuery("select top 6 * from Modulo where estado=1 order by nombre asc").ToList())
                {
                    modulos += "<th style='text-align: center;' scope='row'>" + item.nombre + "</th>";
                }
                string mod = "<tr>" + modulos + "</tr>";
                string carrera = dbs.PlanEstudio.Single(x => x.Id == b.idCarrera).Nombre.ToString();
                if (b != null)
                {
                    obj = "<tr><th width='8%' rowspan='2' >" + imagen + "</th><th colspan='4' style='text-align:center;'>BOLETA DE PROYECCIÓN SEMESTRAL</th><td scope='col' ><b>FECHA: </b>" + fechaboleta + "</td><th scope='col' style='text-align:center;'>DF.002</th></tr>";
                    obj += "<tr><td colspan='3'><b>NOMBRE DEL ESTUDIANTE: </b>" + p.Nombre + " " + p.ApellidoPaterno + " " + p.ApellidoMaterno + "</td><td><b>CI: </b>" + p.DocumentoIdentidad + "</td><td colspan='2'><b> CARRERA: </b>" + carrera + "</td></tr>";
                    obj += mod;
                }
            }
            else
            {
                obj += 1;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        //campos de la materias 1
        int idm1columna1, idm1columna2, idm1columna3, idm1columna4, idm1columna5, idm1columna6;
        string m1columna1, m1columna2, m1columna3, m1columna4, m1columna5, m1columna6;

        //campos de los turnos de materia de la 1
        string t1columna1, t1columna2, t1columna3, t1columna4, t1columna5, t1columna6;

        //campos de la materias 2
        int idm2columna1, idm2columna2, idm2columna3, idm2columna4, idm2columna5, idm2columna6;
        string m2columna1, m2columna2, m2columna3, m2columna4, m2columna5, m2columna6;

        //campos de los turnos de materia de la 2
        string t2columna1, t2columna2, t2columna3, t2columna4, t2columna5, t2columna6;

        //tabla generada
        string tabla, fila1, fila2, fila3, fila4;

        //Listar Boleta de Proyeccion
        public ActionResult ListarBoleta(int idBole)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.id == idBole);
            Persona p = dbs.Persona.SingleOrDefault(x => x.Id == b.idEstudiante);
            foreach (var item in db.BoletaDetalle.Where(x => x.idBoleta == b.id))
            {
                int c = 0;
                tabla = "";
                var admMateriaSeleccionada = db.AdmMateria.Single(x => x.id == item.idAdmMateria);
                var admProyeccionSelecciona = db.AdmProyeccion.Single(x => x.id == admMateriaSeleccionada.idAdmProyeccion);
                foreach (var item1 in db.Modulo.SqlQuery("select top 6 * from Modulo where estado=1 order by nombre asc").ToList())
                {
                    c++;
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 1)
                    {
                        if (m1columna1 == null)
                        {
                            idm1columna1 = item.id;
                            m1columna1 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm1columna1 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna1 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna1 = item.id;
                            m2columna1 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm2columna1 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna1 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 2)
                    {
                        if (m1columna2 == null)
                        {
                            idm1columna2 = item.id;
                            m1columna2 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm1columna2 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna2 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna2 = item.id;
                            m2columna2 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm2columna2 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna2 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 3)
                    {
                        if (m1columna3 == null)
                        {
                            idm1columna3 = item.id;
                            m1columna3 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm1columna3 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna3 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna3 = item.id;
                            m2columna3 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm2columna3 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna3 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 4)
                    {
                        if (m1columna4 == null)
                        {
                            idm1columna4 = item.id;
                            m1columna4 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm1columna4 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna4 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna4 = item.id;
                            m2columna4 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm2columna4 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna4 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 5)
                    {
                        if (m1columna5 == null)
                        {
                            idm1columna5 = item.id;
                            m1columna5 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm1columna5 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna5 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna5 = item.id;
                            m2columna5 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm2columna5 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna5 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 6)
                    {
                        if (m1columna6 == null)
                        {
                            idm1columna6 = item.id;
                            m1columna6 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm1columna6 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna6 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna6 = item.id;
                            m2columna6 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm2columna6 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna6 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                }
            }
            fila1 += "<tr><th width='8%' style='text-align:center;' scope='row'>MATERIA 1</th><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m1columna1 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m1columna2 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m1columna3 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m1columna4 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m1columna5 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m1columna6 + "</td></tr>";

            fila2 += "<tr><th width='8%' style='text-align:center;' scope='row'>TURNO MAT. 1</th><td width='15.3%' style='text-align:center;'>" + t1columna1 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna2 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna3 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna4 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna5 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna6 + "</td></tr>";

            fila3 += "<tr><th width='8%' style='text-align:center;' scope='row'>MATERIA 2</th><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m2columna1 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m2columna2 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m2columna3 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m2columna4 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m2columna5 + "</td><td class='click-proyeccion' width='15.3%' style='text-align:center;'>" + m2columna6 + "</td></tr>";

            fila4 += "<tr><th width='8%' style='text-align:center;' scope='row'>TURNO MAT. 2</th><td width='15.3%' style='text-align:center;'>" + t2columna1 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna2 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna3 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna4 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna5 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna6 + "</td></tr>";


            tabla += fila1 + fila2 + fila3 + fila4;
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }

        //Agregar Materia a Boleta de Proyeccion
        public ActionResult AgregarMateriaBoleta(int idAdm, int idBole)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Boleta b = db.Boleta.SingleOrDefault(x => x.id == idBole);
            Status s = new Status();
            try
            {
                if (b != null)
                {
                    int idPlanEstudioSeleccionado = b.idCarrera;
                    int MateriaSeleccionada = db.AdmMateria.Single(x => x.id == idAdm).idMateria;
                    var o = db.AdmMateria.SingleOrDefault(x => x.id == idAdm);
                    //Forma parte del plan de estudio
                    //Si la materia seleccionada forma parte del plan de estudio el valor devuelto es 1 por lo tanto pasa a la siguiente validación
                    int MateriasPlanEstduio = dbs.PlanEstudioMateria.Count(x => x.PlanEstudio.Id == idPlanEstudioSeleccionado && x.Materia.Id == MateriaSeleccionada && x.IdEstadoMateria == 0);
                    if (MateriasPlanEstduio > 0)
                    {
                        //Máximo de 9 materias en la boleta de proyección
                        //Cuenta las materias que se tienen proyectadas hasta el momento incluyendo las de verano e invierno, si el resultado devuelto es menor a 9 pasa a la siguiente validación
                        int cantidadMaterias = db.BoletaDetalle.Count(x => x.idBoleta == b.id);
                        if (cantidadMaterias < 9)
                        {
                            //Primera validación de materia repetida
                            //MateriasRepetida ---> si la materia seleccionada ya existe en la boleta de proyección el valor devuelto es 0 en este caso pasa a la siguiente validación, pero si el valor devuelto es 1, vuelve a preguntar:
                            //MateriasRepetida2 ---> si la materia seleccionada ya existe en la boleta de proyección y si el módulo en el que queremos agregarla es en el 6 el valor devuelto es 0 en este caso la materia pasa a proyectarse como una materia de invierno / verano, si el resultado devuelto es 1 se muestra la advertencia
                            var MateriasRepetida = db.BoletaDetalle.Count(x => x.idBoleta == b.id && x.AdmMateria.idMateria == MateriaSeleccionada);
                            var MateriasRepetida2 = db.BoletaDetalle.Count(x => x.idBoleta == b.id && x.AdmMateria.idMateria == MateriaSeleccionada && o.AdmProyeccion.Modulo.numModulo < 6);
                            if (MateriasRepetida < 1)
                            {
                                var admMateriaSeleccionada = db.AdmMateria.Single(x => x.id == idAdm);
                                var admProyeccionSelecciona = db.AdmProyeccion.Single(x => x.id == admMateriaSeleccionada.idAdmProyeccion);
                                //Máximo de 2 Materias por Modulo en la boleta de proyección
                                //Cuenta las materias que se tienen proyectadas en el módulo que queremos agregar la materia seleccionada, si el resultado devuelto es menor a 2 pasa a la siguiente validación
                                int cantidadMateriaModulo = db.BoletaDetalle.Count(x => x.AdmMateria.AdmProyeccion.idModulo == admProyeccionSelecciona.idModulo && x.idBoleta == b.id);
                                if (cantidadMateriaModulo < 2)
                                {
                                    //Turno repetido dentro de un módulo
                                    //Busca si ya existe un materia proyectada con el turno en el que queremos agregar la materia seleccionada, si el valor devuelto es 0 pasa a la siguiente validación
                                    var TurnoRepetido = db.BoletaDetalle.Count(x => x.AdmMateria.AdmProyeccion.idModulo == admProyeccionSelecciona.idModulo && x.idBoleta == b.id && x.AdmMateria.idTurno == o.idTurno);
                                    if (TurnoRepetido < 1)
                                    {
                                        //Materia ya Aprobada o Pendiente
                                        //Busca si la materia seleccionada está aprobada o pendiente, si el resultado es 0 pasa a la siguiente validación
                                        int MateriasAprobadas = dbs.RegistroDeMateria.Count(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == b.idEstudiante && x.IdMateria == MateriaSeleccionada && (x.IdEstadoMateriaRegistrada == 1 || x.IdEstadoMateriaRegistrada == 0));
                                        if (MateriasAprobadas < 1)
                                        {
                                            BoletaDetalle bod = new BoletaDetalle();
                                            bod.id = 0;
                                            bod.idBoleta = b.id;
                                            bod.idAdmMateria = idAdm;
                                            db.BoletaDetalle.Add(bod);
                                            db.SaveChanges();
                                            s.Tipo = 1;
                                            s.Msj = "Se agregó la materia a su boleta con éxito";
                                        }
                                        else
                                        {
                                            s.Tipo = 2;
                                            s.Msj = "Usted está cursando o ya aprobó esta materia";
                                        }
                                    }
                                    else
                                    {
                                        s.Tipo = 2;
                                        s.Msj = "Usted no puede agregar materias dentro del mismo turno y módulo";
                                    }
                                }
                                else
                                {
                                    s.Tipo = 2;
                                    s.Msj = "Solo puede agregar un máximo de dos materias por módulo";
                                }
                            }
                            //Segunda validación de materia repetida
                            //MateriasRepetida2--->si la materia seleccionada ya existe en la boleta de proyección y si el módulo en el que queremos agregarla es en el 6 el valor devuelto es 0 en este caso la materia pasa a proyectarse como una materia de invierno / verano, si el resultado devuelto es 1 se muestra la advertencia
                            else if (MateriasRepetida2 < 1)
                            {
                                s.Tipo = 4;
                            }
                            else
                            {
                                s.Tipo = 2;
                                s.Msj = "No puede agregar la misma materia en la boleta de proyección";
                            }
                        }
                        else
                        {
                            s.Tipo = 2;
                            s.Msj = "Solo puede agregar un máximo de 9 materias por semestre";
                        }
                    }
                    else
                    {
                        s.Tipo = 2;
                        s.Msj = "No puede seleccionar esta materia, no forma parte de su Plan de Estudio";
                    }
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "En estos momentos tenemos problemas con nuestros servicios, hemos reportado esta situación a los administradores del sistema, intenta de nuevo más tarde";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Eliminar Materia de Boleta
        public ActionResult EliminarMateriaBoleta(int id)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                var mtb = db.BoletaDetalle.Single(x => x.id == id);
                var cantMaterias = db.Boleta.Count(x => x.id == mtb.idBoleta);
                db.BoletaDetalle.Remove(mtb);
                db.SaveChanges();
                s.Tipo = 1;
                s.Msj = "Se eliminó correctamente";
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "En estos momentos tenemos problemas con nuestros servicios, hemos reportado esta situación a los administradores del sistema, intenta de nuevo más tarde";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Guardar Materias de Ivierno/Verano
        public ActionResult GuardarMateriaInvierno(int idAdm, int idBole)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Boleta b = db.Boleta.SingleOrDefault(x=> x.id == idBole);
            Status s = new Status();
            try
            {
                BoletaDetalle bod = new BoletaDetalle();
                bod.id = 0;
                bod.idBoleta = b.id;
                bod.idAdmMateria = idAdm;
                db.BoletaDetalle.Add(bod);
                db.SaveChanges();
                s.Tipo = 1;
                s.Msj = "Se agregó la materia a su boleta con éxito";
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "En estos momentos tenemos problemas con nuestros servicios, hemos reportado esta situación a los administradores del sistema, intenta de nuevo más tarde";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //responder al estudiante
        public ActionResult EnviarRespuesta(int idBoleta, string respuesta)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Status s = new Status();
            try
            {
                Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
                Boleta b = db.Boleta.SingleOrDefault(x=> x.id == idBoleta);
                if (b != null)
                {
                    b.respuesta = respuesta;
                    b.estado = 4;
                    db.SaveChanges();
                    s.Tipo = 1;
                    s.Msj = "Se envío la boleta a revisión";
                }
                else
                {
                    s.Tipo = 2;
                    s.Msj = "Usted no cuenta con una boleta de proyección";
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "En estos momentos tenemos problemas con nuestros servicios, hemos reportado esta situación a los administradores del sistema, intenta de nuevo más tarde";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Cargar Facultad
        public ActionResult CargarFacultad()
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            ProyeccionEntities db = new ProyeccionEntities();
            List<object> facultades = new List<object>();
            List<Facultad> lista = dbs.Facultad.OrderBy(x=> x.Nombre).Where(x =>  x.IdEstado == 0 && (x.Id == 3 || x.Id == 9 || x.Id == 5)).ToList();
            foreach (var item in lista)
            {
                object o = new
                {
                    id = item.Id,
                    nombre = item.Nombre
                };
                facultades.Add(o);
            }
            return Json(facultades, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarCantidadEstudintes(int idFacultad)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            ProyeccionEntities db = new ProyeccionEntities();
            List<object[]> lista = new List<object[]>();
            int i = 0;
            if (idFacultad != 100)
            {
                var carreraFacultad = dbs.PlanEstudio.OrderBy(x => x.Nombre).Where(x => x.IdModeloEstudio == 2 && x.Carrera.DireccionArea.Facultad.Id == idFacultad).ToList();
                foreach (var item2 in carreraFacultad)
                {
                    var boletaProyeccione = db.Boleta.Where(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == item2.Id && x.BoletaDetalle.Count > 0).ToList();
                    foreach (var item in boletaProyeccione)
                    {
                        i++;
                        var estu = dbs.Persona.Single(x => x.Id == item.idEstudiante);
                        var carre = dbs.PlanEstudio.Single(x => x.Id == item.idCarrera).Nombre.ToString();
                        object[] o =
                        {
                            i,
                            estu.Nombre +" "+ estu.ApellidoPaterno+ " "+ estu.ApellidoMaterno,
                            estu.DocumentoIdentidad,
                            carre,
                            };
                        lista.Add(o);
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            else
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    if (ii == 0)
                    {
                        idFacultad = 3;
                    }
                    else if( ii == 1)
                    {
                        idFacultad = 5;
                    }
                    else if (ii == 2)
                    {
                        idFacultad = 9;
                    }
                    var carreraFacultad = dbs.PlanEstudio.OrderBy(x => x.Nombre).Where(x => x.IdModeloEstudio == 2 && x.Carrera.DireccionArea.Facultad.Id == idFacultad).ToList();
                    foreach (var item2 in carreraFacultad)
                    {
                        var boletaProyeccione = db.Boleta.Where(x => (x.estado == 1 || x.estado == 4) && x.idCarrera == item2.Id && x.BoletaDetalle.Count > 0).ToList();
                        foreach (var item in boletaProyeccione)
                        {
                            i++;
                            var estu = dbs.Persona.Single(x => x.Id == item.idEstudiante);
                            var carre = dbs.PlanEstudio.Single(x => x.Id == item.idCarrera).Nombre.ToString();
                            object[] o =
                            {
                                i,
                                estu.Nombre +" "+ estu.ApellidoPaterno+ " "+ estu.ApellidoMaterno,
                                estu.DocumentoIdentidad,
                                carre,
                                };
                            lista.Add(o);
                        }
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
        }
    }
}