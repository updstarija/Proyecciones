using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        //Imprimier Boleta Rol Estudiante
        public ActionResult ImprimirBoleta(string Tipo) 
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities bd = new SAADSTJEntities();
            var reporte = "RepBoleta.rdlc";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), reporte);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                ViewBag.dir = path;
                return View("Index");
            }
            List<MateriaClass> lista = new List<MateriaClass>();
            Persona p = bd.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
            MateriaClass m = new MateriaClass();
            foreach (var item in db.BoletaDetalle.Where(x=>x.idBoleta==b.id))
            {
                var ins = bd.InscripcionCarrera.SingleOrDefault(x => x.IdEstudiante == item.Boleta.idEstudiante && x.IdPlanEstudio==item.Boleta.idCarrera);
                var materia = bd.Materia.SingleOrDefault(x => x.Id == item.AdmMateria.idMateria);
                var turno = bd.Turno.SingleOrDefault(x => x.Id == item.AdmMateria.idTurno);
                var modulo = item.AdmMateria.AdmProyeccion.Modulo.nombre.Split('.');
                m.Estudiante = ins.Estudiante.Persona.Nombre + " " + ins.Estudiante.Persona.ApellidoPaterno + " " + ins.Estudiante.Persona.ApellidoMaterno;
                m.Carrera = ins.PlanEstudio.Nombre;
                m.Documento = ins.Estudiante.Persona.DocumentoIdentidad;
                m.Fecha = item.Boleta.fecha.Date.ToShortDateString();
                switch (Convert.ToInt32(modulo[0]))
                {
                    case 1:
                        if (m.Materia_1==null)
                        {
                            m.Materia_1 = materia.Nombre;
                            m.Turno_1 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_1 = materia.Nombre;
                            m.Turno_2_1 = turno.Nombre;
                        }
                        break;
                    case 2:
                        if (m.Materia_2 == null)
                        {
                            m.Materia_2 = materia.Nombre;
                            m.Turno_2 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_2 = materia.Nombre;
                            m.Turno_2_2 = turno.Nombre;
                        }
                        break;
                    case 3:
                        if (m.Materia_3 == null)
                        {
                            m.Materia_3 = materia.Nombre;
                            m.Turno_3 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_3 = materia.Nombre;
                            m.Turno_2_3 = turno.Nombre;
                        }
                        break;
                    case 4:
                        if (m.Materia_4 ==null)
                        {
                            m.Materia_4 = materia.Nombre;
                            m.Turno_4 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_4 = materia.Nombre;
                            m.Turno_2_4 = turno.Nombre;
                        }
                        break;
                    case 5:
                        if (m.Materia_5 == null)
                        {
                            m.Materia_5 = materia.Nombre;
                            m.Turno_5 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_5 = materia.Nombre;
                            m.Turno_2_5 = turno.Nombre;
                        }
                        break;
                    case 6:
                        if (m.Materia_6 == null)
                        {
                            m.Materia_6 = materia.Nombre;
                            m.Turno_6 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_6 = materia.Nombre;
                            m.Turno_2_6 = turno.Nombre;
                        }
                        break;
                }
            }
            lista.Add(m);
            ReportDataSource rd = new ReportDataSource("DataSet1", lista);
            lr.DataSources.Add(rd);
            string reportType = Tipo;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Tipo + "</OutputFormat>" +
            "  <PageWidth>11in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);

        }

        //Imprimir Boleta Vertical Rol Estudiante APK
        public ActionResult ImprimirBoletaAPK(int idBoleta)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities bd = new SAADSTJEntities();
            string Tipo = "PDF";
            var reporte = "ReporteBoletaApp.rdlc";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), reporte);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                ViewBag.dir = path;
                return View("Index");
            }
            List<MateriaClass> lista = new List<MateriaClass>();
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.id == idBoleta);
            Persona p = bd.Persona.SingleOrDefault(x => x.Id == b.idEstudiante);
            MateriaClass m = new MateriaClass();
            foreach (var item in db.BoletaDetalle.Where(x => x.idBoleta == b.id))
            {
                var ins = bd.InscripcionCarrera.SingleOrDefault(x => x.IdEstudiante == item.Boleta.idEstudiante && x.IdPlanEstudio == item.Boleta.idCarrera);
                var materia = bd.Materia.SingleOrDefault(x => x.Id == item.AdmMateria.idMateria);
                var turno = bd.Turno.SingleOrDefault(x => x.Id == item.AdmMateria.idTurno);
                var modulo = item.AdmMateria.AdmProyeccion.Modulo.nombre.Split('.');
                m.Estudiante = ins.Estudiante.Persona.Nombre + " " + ins.Estudiante.Persona.ApellidoPaterno + " " + ins.Estudiante.Persona.ApellidoMaterno;
                m.Carrera = ins.PlanEstudio.Nombre;
                m.Documento = ins.Estudiante.Persona.DocumentoIdentidad;
                m.Fecha = item.Boleta.fecha.Date.ToShortDateString();
                switch (Convert.ToInt32(modulo[0]))
                {
                    case 1:
                        if (m.Materia_1 == null)
                        {
                            m.Materia_1 = materia.Nombre;
                            m.Turno_1 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_1 = materia.Nombre;
                            m.Turno_2_1 = turno.Nombre;
                        }
                        break;
                    case 2:
                        if (m.Materia_2 == null)
                        {
                            m.Materia_2 = materia.Nombre;
                            m.Turno_2 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_2 = materia.Nombre;
                            m.Turno_2_2 = turno.Nombre;
                        }
                        break;
                    case 3:
                        if (m.Materia_3 == null)
                        {
                            m.Materia_3 = materia.Nombre;
                            m.Turno_3 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_3 = materia.Nombre;
                            m.Turno_2_3 = turno.Nombre;
                        }
                        break;
                    case 4:
                        if (m.Materia_4 == null)
                        {
                            m.Materia_4 = materia.Nombre;
                            m.Turno_4 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_4 = materia.Nombre;
                            m.Turno_2_4 = turno.Nombre;
                        }
                        break;
                    case 5:
                        if (m.Materia_5 == null)
                        {
                            m.Materia_5 = materia.Nombre;
                            m.Turno_5 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_5 = materia.Nombre;
                            m.Turno_2_5 = turno.Nombre;
                        }
                        break;
                    case 6:
                        if (m.Materia_6 == null)
                        {
                            m.Materia_6 = materia.Nombre;
                            m.Turno_6 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_6 = materia.Nombre;
                            m.Turno_2_6 = turno.Nombre;
                        }
                        break;
                }
            }
            lista.Add(m);
            ReportDataSource rd = new ReportDataSource("DataSet1", lista);
            lr.DataSources.Add(rd);
            string reportType = Tipo;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Tipo + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);

        }

        //Imprimir Boleta Rol Jefe de Carrera
        public ActionResult ImprimirBoletaEstudiante(int idBole)
        {
            string Tipo = "PDF";
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities bd = new SAADSTJEntities();
            var reporte = "RepBoleta.rdlc";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), reporte);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                ViewBag.dir = path;
                return View("Index");
            }
            List<MateriaClass> lista = new List<MateriaClass>();
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.id == idBole);
            Persona p = bd.Persona.SingleOrDefault(x => x.Id == b.idEstudiante);
            MateriaClass m = new MateriaClass();
            foreach (var item in db.BoletaDetalle.Where(x => x.idBoleta == b.id))
            {
                var ins = bd.InscripcionCarrera.SingleOrDefault(x => x.IdEstudiante == item.Boleta.idEstudiante && x.IdPlanEstudio == item.Boleta.idCarrera);
                var materia = bd.Materia.SingleOrDefault(x => x.Id == item.AdmMateria.idMateria);
                var turno = bd.Turno.SingleOrDefault(x => x.Id == item.AdmMateria.idTurno);
                var modulo = item.AdmMateria.AdmProyeccion.Modulo.nombre.Split('.');
                m.Estudiante = ins.Estudiante.Persona.Nombre + " " + ins.Estudiante.Persona.ApellidoPaterno + " " + ins.Estudiante.Persona.ApellidoMaterno;
                m.Carrera = ins.PlanEstudio.Nombre;
                m.Documento = ins.Estudiante.Persona.DocumentoIdentidad;
                m.Fecha = item.Boleta.fecha.Date.ToShortDateString();
                switch (Convert.ToInt32(modulo[0]))
                {
                    case 1:
                        if (m.Materia_1 == null)
                        {
                            m.Materia_1 = materia.Nombre;
                            m.Turno_1 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_1 = materia.Nombre;
                            m.Turno_2_1 = turno.Nombre;
                        }
                        break;
                    case 2:
                        if (m.Materia_2 == null)
                        {
                            m.Materia_2 = materia.Nombre;
                            m.Turno_2 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_2 = materia.Nombre;
                            m.Turno_2_2 = turno.Nombre;
                        }
                        break;
                    case 3:
                        if (m.Materia_3 == null)
                        {
                            m.Materia_3 = materia.Nombre;
                            m.Turno_3 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_3 = materia.Nombre;
                            m.Turno_2_3 = turno.Nombre;
                        }
                        break;
                    case 4:
                        if (m.Materia_4 == null)
                        {
                            m.Materia_4 = materia.Nombre;
                            m.Turno_4 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_4 = materia.Nombre;
                            m.Turno_2_4 = turno.Nombre;
                        }
                        break;
                    case 5:
                        if (m.Materia_5 == null)
                        {
                            m.Materia_5 = materia.Nombre;
                            m.Turno_5 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_5 = materia.Nombre;
                            m.Turno_2_5 = turno.Nombre;
                        }
                        break;
                    case 6:
                        if (m.Materia_6 == null)
                        {
                            m.Materia_6 = materia.Nombre;
                            m.Turno_6 = turno.Nombre;
                        }
                        else
                        {
                            m.Materia_2_6 = materia.Nombre;
                            m.Turno_2_6 = turno.Nombre;
                        }
                        break;
                }
            }
            lista.Add(m);
            ReportDataSource rd = new ReportDataSource("DataSet1", lista);
            lr.DataSources.Add(rd);
            string reportType = Tipo;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Tipo + "</OutputFormat>" +
            "  <PageWidth>11in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);

        }

        //Imprimir cantidad de Estudiantes que proyectaron materia en la manana
        public ActionResult ImprimirReporteManana(int idMod)
        {
            string Tipo = "PDF";
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            var reporte = "RepCantEstM.rdlc";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), reporte);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                ViewBag.dir = path;
                return View("Index");
            }
            List<object> materias = new List<object>();            
            var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 1 && x.AdmProyeccion.idModulo == idMod).GroupBy(x => x.idMateria).ToList();
            string nombreModulos = db.Modulo.Single(x => x.id == idMod).nombre;
            foreach (var item2 in MateriasManana)
            {
                string nombreMateria = dbs.Materia.Single(x => x.Id == item2.Key).Nombre;
                var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item2.Key && x.AdmMateria.idTurno == 1 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                object o = new
                {
                    nombreModulos,
                    nombreMateria,
                    cantidadEst
                };
                materias.Add(o);
            }
            ReportDataSource rd = new ReportDataSource("DataSet1", materias);
            lr.DataSources.Add(rd);
            string reportType = Tipo;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Tipo + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

        //Reporte Cantidad de Estudiantes que proyectaron una materia al medio dia
        public ActionResult ImprimirReporteMedioDia(int idMod)
        {
            string Tipo = "PDF";
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            var reporte = "RepCantEstMD.rdlc";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), reporte);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                ViewBag.dir = path;
                return View("Index");
            }
            List<object> materias = new List<object>();
            var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 4 && x.AdmProyeccion.idModulo == idMod).GroupBy(x => x.idMateria).ToList();
            string nombreModulos = db.Modulo.Single(x => x.id == idMod).nombre;
            foreach (var item2 in MateriasManana)
            {
                string nombreMateria = dbs.Materia.Single(x => x.Id == item2.Key).Nombre;
                var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item2.Key && x.AdmMateria.idTurno == 4 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                object o = new
                {
                    nombreModulos,
                    nombreMateria,
                    cantidadEst
                };
                materias.Add(o);
            }
            ReportDataSource rd = new ReportDataSource("DataSet1", materias);
            lr.DataSources.Add(rd);
            string reportType = Tipo;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Tipo + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

        //Reporte Cantidad de Estudiantes que proyectaron una materia en la Tarde
        public ActionResult ImprimirReporteTarde(int idMod)
        {
            string Tipo = "PDF";
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            var reporte = "RepCantEstT.rdlc";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), reporte);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                ViewBag.dir = path;
                return View("Index");
            }
            List<object> materias = new List<object>();
            var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 2 && x.AdmProyeccion.idModulo == idMod).GroupBy(x => x.idMateria).ToList();
            string nombreModulos = db.Modulo.Single(x => x.id == idMod).nombre;
            foreach (var item2 in MateriasManana)
            {
                string nombreMateria = dbs.Materia.Single(x => x.Id == item2.Key).Nombre;
                var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item2.Key && x.AdmMateria.idTurno == 2 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                object o = new
                {
                    nombreModulos,
                    nombreMateria,
                    cantidadEst,
                };
                materias.Add(o);
            }
            ReportDataSource rd = new ReportDataSource("DataSet1", materias);
            lr.DataSources.Add(rd);
            string reportType = Tipo;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Tipo + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

        //Reporte Cantidad de Estudiantes que proyectaron una materia en la Noche
        public ActionResult ImprimirReporteNoche(int idMod)
        {
            string Tipo = "PDF";
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            var reporte = "RepCantEstN.rdlc";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), reporte);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                ViewBag.dir = path;
                return View("Index");
            }
            List<object> materias = new List<object>();
            var MateriasManana = db.AdmMateria.Where(x => x.idTurno == 3 && x.AdmProyeccion.idModulo == idMod).GroupBy(x => x.idMateria).ToList();
            string nombreModulos = db.Modulo.Single(x => x.id == idMod).nombre;
            foreach (var item2 in MateriasManana)
            {
                string nombreMateria = dbs.Materia.Single(x => x.Id == item2.Key).Nombre;
                var cantidadEst = db.BoletaDetalle.Count(x => (x.Boleta.estado == 1 || x.Boleta.estado == 4) && x.AdmMateria.idMateria == item2.Key && x.AdmMateria.idTurno == 3 && x.AdmMateria.AdmProyeccion.idModulo == idMod);
                object o = new
                {
                    nombreModulos,
                    nombreMateria,
                    cantidadEst
                };
                materias.Add(o);
            }
            ReportDataSource rd = new ReportDataSource("DataSet1", materias);
            lr.DataSources.Add(rd);
            string reportType = Tipo;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Tipo + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);

        }
    }
}