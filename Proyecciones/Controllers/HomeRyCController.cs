using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class HomeRyCController : Controller
    {
        // GET: HomeRyC
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProyeccionEntities db = new ProyeccionEntities();
                var usuAdm = db.Usuario.SingleOrDefault(x => x.usuario1 == User.Identity.Name);
                if (usuAdm != null)
                {
                    if (db.RolPermisos.SingleOrDefault(x => x.idRol == usuAdm.idRol && x.Permisos.nombre == "HOMERYC") != null)
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

        public ActionResult BuscarEstudiante(string ci)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object> estudiante = new List<object>();
            var lista = dbs.Persona.SqlQuery("select top 4 * from Persona where DocumentoIdentidad like '" + ci + "%'");
            foreach (var item in lista)
            {
                object o = new
                {
                    Id = item.Id,
                    NombreCompleto = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno
                };
                estudiante.Add(o);
            }
            return Json(estudiante, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarCabeceraBoleta(int idEstudiante)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            string obj = "";
            Persona p = dbs.Persona.Single(x => x.Id == idEstudiante);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
            if (b != null)
            {
                if (b.estado == 3)
                {
                    obj += 2;
                }
                else if (b.estado == 0 || b.estado == 2)
                {
                    obj += 1;
                }
                else if (b.estado == 1 && b.BoletaDetalle.Count < 1)
                {
                    obj += 1;
                }
                else
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

                        obj = "<tr><th width='8%' rowspan='2' >" + imagen + "</th><th colspan='4' style='text-align:center;'>BOLETA DE PROYECCION SEMESTRAL</th><td scope='col' ><b>FECHA: </b>" + fechaboleta + "</td><th scope='col' style='text-align:center;'>DF.002</th></tr>";
                        obj += "<tr><td colspan='3'><b>NOMBRE DEL ESTUDIANTE: </b>" + p.Nombre + " " + p.ApellidoPaterno + " " + p.ApellidoMaterno + "</td><td><b>CI: </b>" + p.DocumentoIdentidad + "</td><td colspan='2'><b> CARRERA: </b>" + carrera + "</td></tr>";
                        obj += mod;
                    }
                }
            }
            else
            {
                obj += 1;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarPieBoleta()
        {
            string obj = "<tr><td colspan='7'><b>Nota</b><br/>La presente boleta se considera “Proyección semestral de materias”, pudiendo ser modificada por la institución de acuerdo con la demanda.</td></tr>";
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //campos de la materias 1
        int idm1columna1, idm1columna2, idm1columna3, idm1columna4, idm1columna5, idm1columna6;
        string m1columna1, m1columna2, m1columna3, m1columna4, m1columna5, m1columna6;

        //campos de los turnos de materia de la 1
        string t1columna1, t1columna2, t1columna3, t1columna4, t1columna5, t1columna6;

        //campos de la materias 1
        int idm2columna1, idm2columna2, idm2columna3, idm2columna4, idm2columna5, idm2columna6;
        string m2columna1, m2columna2, m2columna3, m2columna4, m2columna5, m2columna6;

        //campos de los turnos de materia de la 1
        string t2columna1, t2columna2, t2columna3, t2columna4, t2columna5, t2columna6;


        string tabla, fila1, fila2, fila3, fila4;

        //Listar Boleta de Proyeccion
        public ActionResult ListarBoleta(int idEstudiante)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.Id == idEstudiante);
            Status s = new Status();
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
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
                            m1columna1 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna1 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna1 = item.id;
                            m2columna1 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna1 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 2)
                    {
                        if (m1columna2 == null)
                        {
                            idm1columna2 = item.id;
                            m1columna2 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna2 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna2 = item.id;
                            m2columna2 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna2 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 3)
                    {
                        if (m1columna3 == null)
                        {
                            idm1columna3 = item.id;
                            m1columna3 = "<label(" + idm1columna3 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna3 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna3 = item.id;
                            m2columna3 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna3 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 4)
                    {
                        if (m1columna4 == null)
                        {
                            idm1columna4 = item.id;
                            m1columna4 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna4 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna4 = item.id;
                            m2columna4 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna4 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 5)
                    {
                        if (m1columna5 == null)
                        {
                            idm1columna5 = item.id;
                            m1columna5 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna5 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna5 = item.id;
                            m2columna5 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna5 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                    if (item1.id == admProyeccionSelecciona.idModulo && c == 6)
                    {
                        if (m1columna6 == null)
                        {
                            idm1columna6 = item.id;
                            m1columna6 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t1columna6 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        else
                        {
                            idm2columna6 = item.id;
                            m2columna6 = "<label>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString() + "</label>";
                            t2columna6 = dbs.Turno.Single(x => x.Id == admMateriaSeleccionada.idTurno).Nombre.ToString();
                        }
                        break;
                    }
                }
            }
            fila1 += "<tr><th width='8%' style='text-align:center;' scope='row'>MATERIA 1</th><td width='15.3%' style='text-align:center;'>" + m1columna1 + "</td><td width='15.3%' style='text-align:center;'>" + m1columna2 + "</td><td width='15.3%' style='text-align:center;'>" + m1columna3 + "</td><td width='15.3%' style='text-align:center;'>" + m1columna4 + "</td><td width='15.3%' style='text-align:center;'>" + m1columna5 + "</td><td width='15.3%' style='text-align:center;'>" + m1columna6 + "</td></tr>";

            fila2 += "<tr><th width='8%' style='text-align:center;' scope='row'>TURNO MAT. 1</th><td width='15.3%' style='text-align:center;'>" + t1columna1 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna2 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna3 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna4 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna5 + "</td><td width='15.3%' style='text-align:center;'>" + t1columna6 + "</td></tr>";

            fila3 += "<tr><th width='8%' style='text-align:center;' scope='row'>MATERIA 2</th><td width='15.3%' style='text-align:center;'>" + m2columna1 + "</td><td width='15.3%' style='text-align:center;'>" + m2columna2 + "</td><td width='15.3%' style='text-align:center;'>" + m2columna3 + "</td><td width='15.3%' style='text-align:center;'>" + m2columna4 + "</td><td width='15.3%' style='text-align:center;'>" + m2columna5 + "</td><td width='15.3%' style='text-align:center;'>" + m2columna6 + "</td></tr>";

            fila4 += "<tr><th width='8%' style='text-align:center;' scope='row'>TURNO MAT. 2 1</th><td width='15.3%' style='text-align:center;'>" + t2columna1 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna2 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna3 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna4 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna5 + "</td><td width='15.3%' style='text-align:center;'>" + t2columna6 + "</td></tr>";


            tabla += fila1 + fila2 + fila3 + fila4;
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }


        //Mostrar Descripcion del Estudiante
        public ActionResult MostarRespuesta(int idEstudiante)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Status s = new Status();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == idEstudiante && x.estado == 4);
            if (b != null)
            {
                if (b.respuesta != null)
                {
                    s.Tipo = 1;
                    s.Msj = b.respuesta;
                }
                else
                {
                    s.Tipo = 0;
                }
            }
            else
            {
                s.Tipo = 0;
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}