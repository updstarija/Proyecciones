using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class ProyeccionController : Controller
    {
        // GET: Proyeccion
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                SAADSTJEntities db = new SAADSTJEntities();
                var persona = db.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
                if (persona != null)
                {
                    ViewBag.NombreCompleto = persona.Nombre + " " + persona.ApellidoPaterno + " " + persona.ApellidoMaterno;
                    ViewBag.CI = persona.DocumentoIdentidad;
                    ViewBag.IdEstudiante = persona.Id;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "SinAcceso");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //Listar Proyeccion Semestral
        public ActionResult Listar(int idmodulos, int idSemestres)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object[]> lista = new List<object[]>();
            ClassAdmProyeccion cadm = new ClassAdmProyeccion();
            List<ClassAdmProyeccion> l = new List<ClassAdmProyeccion>();
            int c = 0;
            string mañana = "";
            string mediodia = "";
            string tarde = "";
            string noche = "";
            foreach (var item in db.AdmMateria.Where(x => x.AdmProyeccion.idModulo == idmodulos && x.AdmProyeccion.idSemestre == idSemestres && x.AdmProyeccion.estado == true))
            {
                var carre = dbs.PlanEstudio.SingleOrDefault(x => x.Id == item.AdmProyeccion.idCarrera).Nombre.ToString();
                var idcarre = dbs.PlanEstudio.SingleOrDefault(x => x.Id == item.AdmProyeccion.idCarrera).Id;
                if (c == 0)
                {
                    cadm.Id = item.idAdmProyeccion;
                    cadm.Carrera = carre;
                    cadm.Idcarrera = idcarre;
                    cadm.Idadmproyecion = item.idAdmProyeccion;
                    switch (item.idTurno)
                    {
                        case 1:
                            cadm.Idmatemanana = item.id;
                            cadm.Matemanana = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                        case 2:
                            cadm.Idmatetarde = item.id;
                            cadm.Matetarde = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                        case 3:
                            cadm.Idmatenoche = item.id;
                            cadm.Matenoche = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                        case 4:
                            cadm.Idmatemediodia = item.id;
                            cadm.Matemediodia = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                            break;
                    }
                    l.Add(cadm);
                }
                else
                {
                    var objadm = l.SingleOrDefault(x => x.Idadmproyecion == item.idAdmProyeccion);
                    if (objadm == null)
                    {
                        ClassAdmProyeccion obj = new ClassAdmProyeccion();
                        obj.Id = item.idAdmProyeccion;
                        obj.Carrera = carre;
                        obj.Idcarrera = idcarre;
                        obj.Idadmproyecion = item.idAdmProyeccion;
                        switch (item.idTurno)
                        {
                            case 1:
                                obj.Idmatemanana = item.id;
                                obj.Matemanana = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 2:
                                obj.Idmatetarde = item.id;
                                obj.Matetarde = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 3:
                                obj.Idmatenoche = item.id;
                                obj.Matenoche = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 4:
                                obj.Idmatemediodia = item.id;
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
                                objadm.Idmatemanana = item.id;
                                objadm.Matemanana = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 2:
                                objadm.Idmatetarde = item.id;
                                objadm.Matetarde = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 3:
                                objadm.Idmatenoche = item.id;
                                objadm.Matenoche = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                            case 4:
                                objadm.Idmatemediodia = item.id;
                                objadm.Matemediodia = dbs.Materia.Single(x => x.Id == item.idMateria).Nombre.ToString();
                                break;
                        }
                    }
                }
                c++;
            }
            foreach (var item in l)
            {
                if (item.Matemanana != null)
                {
                    int idmat = db.AdmMateria.Single(x => x.id == item.Idmatemanana).idMateria;
                    if (dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == idmat && x.IdPlanEstudio == item.Idcarrera) != null)
                    {
                        item.Matemanana = "Electiva - " + item.Matemanana;
                    }
                }
                if (item.Matemediodia != null)
                {
                    int idmat = db.AdmMateria.Single(x => x.id == item.Idmatemediodia).idMateria;
                    if (dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == idmat && x.IdPlanEstudio == item.Idcarrera) != null)
                    {
                        item.Matemediodia = "Electiva - " + item.Matemediodia;
                    }
                }
                if (item.Matetarde != null)
                {
                    int idmat = db.AdmMateria.Single(x => x.id == item.Idmatetarde).idMateria;
                    if (dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == idmat && x.IdPlanEstudio == item.Idcarrera) != null)
                    {
                        item.Matetarde = "Electiva - " + item.Matetarde;
                    }
                }
                if (item.Matenoche != null)
                {
                    int idmat = db.AdmMateria.Single(x => x.id == item.Idmatenoche).idMateria;
                    if (dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == idmat && x.IdPlanEstudio == item.Idcarrera) != null)
                    {
                        item.Matenoche = "Electiva - " + item.Matenoche;
                    }
                }
                object[] o = {
                        item.Carrera,
                        mañana = "<label class='dato-materia' onclick='AgregarMateriaBoleta("+item.Idmatemanana+")'>"+item.Matemanana+"</label>",
                        mediodia = "<label class='dato-materia' onclick='AgregarMateriaBoleta("+item.Idmatemediodia+")'>"+item.Matemediodia+"</label>",
                        tarde =  "<label class='dato-materia' onclick='AgregarMateriaBoleta("+item.Idmatetarde+")'>"+item.Matetarde+"</label>",
                        noche = "<label class='dato-materia' onclick='AgregarMateriaBoleta("+item.Idmatenoche+")'>"+item.Matenoche+"</label>",
                    };
                lista.Add(o);
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Cargar combo de la carrera en la boleta
        public ActionResult CargarCarrera(string ci)
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> Carreras = new List<object>();
            foreach (var item in db.InscripcionCarrera.Where(x => x.Estudiante.Persona.DocumentoIdentidad == ci && x.PlanEstudio.IdEstadoPlan == 0 && x.PlanEstudio.IdModeloEstudio == 2))
            {
                object o = new
                {
                    Id = item.PlanEstudio.Id,
                    Nombre = item.PlanEstudio.Nombre
                };
                Carreras.Add(o);
            }
            return Json(Carreras, JsonRequestBehavior.AllowGet);
        }

        //Crear nueva Boleta de Proyeccion
        public ActionResult CrearBoleta(Boleta b)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            ProyeccionEntities db = new ProyeccionEntities();
            Status s = new Status();
            try
            {
                Traspaso traspaso = dbs.Traspaso.OrderByDescending(x => x.Id).FirstOrDefault(x => x.InscripcionCarrera.Estudiante.Id == b.idEstudiante);
                Boleta bo = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == b.idEstudiante);
                if (traspaso != null)
                {
                    if (bo != null)
                    {
                        bo.estado = 0;
                        db.SaveChanges();
                        b.estado = 2;
                        db.Boleta.Add(b);
                        db.SaveChanges();
                        s.Tipo = 1;
                        s.Msj = "Se creó la boleta con éxito";
                    }
                    else
                    {
                        b.estado = 2;
                        db.Boleta.Add(b);
                        db.SaveChanges();
                        s.Tipo = 1;
                        s.Msj = "Se creó la boleta con éxito";
                    }
                }
                else
                {
                    if (bo != null)
                    {
                        bo.estado = 0;
                        db.SaveChanges();
                        db.Boleta.Add(b);
                        db.SaveChanges();
                        s.Tipo = 1;
                        s.Msj = "Se creó la boleta con éxito";
                    }
                    else
                    {
                        db.Boleta.Add(b);
                        db.SaveChanges();
                        s.Tipo = 1;
                        s.Msj = "Se creó la boleta con éxito";
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

        //Listar Cabecera de la Boleta
        public ActionResult ListarCabeceraBoleta()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
            string obj = "";
            if (b != null)
            {
                if (b.estado == 3)
                {
                    obj += 2;
                }
                else if (b.estado == 0)
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
                        obj += "<tr><th width='8%' rowspan='2' >" + imagen + "</th><th colspan='4' style='text-align:center;'>BOLETA DE PROYECCIÓN SEMESTRAL</th><td scope='col' ><b>FECHA: </b>" + fechaboleta + "</td><th scope='col' style='text-align:center;'>DF.002</th></tr>";
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

        //Listar Pie de la Boleta
        public ActionResult CargarPieBoleta()
        {
            string obj = "<tr><td colspan='7'><b>Nota:</b><br/>La presente boleta se considera “Proyección semestral de materias”, pudiendo ser modificada por la institución de acuerdo con la demanda.</td></tr>";
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //Agregar Materia a Boleta de Proyeccion
        public ActionResult AgregarMateriaBoleta(int idAdm)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
            Status s = new Status();
            try
            {
                if (b != null)
                {
                    if (b.estado == 0)
                    {
                        s.Tipo = 2;
                        s.Msj = "Debe crearse una boleta de proyección para agregar materias";
                    }
                    else if (b.estado == 3)
                    {
                        s.Tipo = 2;
                        s.Msj = "No puede agregar materias debido a que su boleta se encuentra en proceso de revisión.";
                    }
                    else if (b.estado == 4)
                    {
                        s.Tipo = 2;
                        s.Msj = "No puede agregar materias a esta boleta ya que se encuentra revisada, puede crearse otra boleta de proyección.";
                    }
                    else
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
                                            int MateriasAprobadas = dbs.RegistroDeMateria.Count(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && (x.IdEstadoMateriaRegistrada == 1 || x.IdEstadoMateriaRegistrada == 0));
                                            if (MateriasAprobadas < 1)
                                            {
                                                //verifica si el estudiante es homologado o realizo un traspaso
                                                //si el estudiante tiene un traspaso devuelve null por lo tanto pasa a realizar las validaciones
                                                //si el resultado es distinto a nulo se agrega la materia sin ninguna restriccion
                                                Traspaso traspaso = dbs.Traspaso.OrderByDescending(x => x.Id).FirstOrDefault(x => x.InscripcionCarrera.Estudiante.Id == b.idEstudiante);
                                                if (traspaso != null)
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
                                                    //Prerrequisito
                                                    //Busca si la materia seleccionada tiene un prerrequisito, si el valor devuelto es distinto a nulo pasa y continua con la validación --materiaPrerequisitoAprobada--,
                                                    //si el valor devuelto es igual a nulo, pasa a validar por un else si la materia seleccionada es o no una materia electiva
                                                    var MateriaPrerequisito = dbs.Prerequisitos.OrderByDescending(x => x.IdMateria).FirstOrDefault(x => x.IdMateria == MateriaSeleccionada && x.IdPlanEstudio == idPlanEstudioSeleccionado);
                                                    if (MateriaPrerequisito != null)
                                                    {
                                                        //Prerrequisito Aprobado
                                                        //Busca si el prerrequisito de la materia seleccionada este aprobado, si el resultado devuelto es 1 pasa a la siguiente validación, si el valor devuelto es 0 muestre la advertencia
                                                        int materiaPrerequisitoAprobada = dbs.RegistroDeMateria.Count(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaPrerequisito.IdMateriaPrerequisito && x.IdEstadoMateriaRegistrada == 1);
                                                        if (materiaPrerequisitoAprobada > 0)
                                                        {
                                                            //saca el id del prerrequisito de la materia seleccionada
                                                            var idMateriaPrerequisitoAprobada = dbs.RegistroDeMateria.Single(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaPrerequisito.IdMateriaPrerequisito && x.IdEstadoMateriaRegistrada == 1).IdMateria;
                                                            //var prueba = dbs.RegistroDeMateria.SingleOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == idMateriaPrerequisitoAprobada && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.NumeroPeriodo == o.AdmProyeccion.Modulo.semestre && x.IdEstadoMateriaRegistrada != 6);

                                                            //Prerrequisito aprobado en la misma gestión en la que se oferto la materia seleccionada
                                                            //Si el valor devuelto es nulo pasa y continua con la validación.
                                                            //si el valor devuelto es distinto a nulo pasa a validar por un else if si el prerrequisito fue aprobado en el mismo semestre en el que se oferto la materia seleccionada
                                                            if (dbs.RegistroDeMateria.SingleOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == idMateriaPrerequisitoAprobada && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion && x.IdEstadoMateriaRegistrada == 1) == null)
                                                            {
                                                                //Materia seleccionada reprobada en la misma gestión
                                                                //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                                //si el valor devuelto es distinto a nulo continua con la validación
                                                                //var prueba = dbs.RegistroDeMateria.Where(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.NumeroPeriodo == o.AdmProyeccion.Modulo.semestre);
                                                                if (dbs.RegistroDeMateria.OrderByDescending(x => x.ComprobanteRegistro.FechaHora).FirstOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                                //Materia seleccionada reprobada en el mismo semestre
                                                                //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                                //si el valor devuelto es distinto a nulo continua con la validación
                                                                else if (dbs.RegistroDeMateria.SingleOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.NumeroPeriodo == o.AdmProyeccion.Modulo.semestre && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                                //ya que la materia seleccionada fue reprobada en la misma gestión y en el mismo semestre solo puede ser agregada como invierno o verano en el último módulo de la oferta
                                                                //si la materia seleccionada pertenece al último módulo, pasa a pedir una confirmación para agregarla como invierno o verano
                                                                //si la materia seleccionada pertenece a los otros módulos se muestra la advertencia
                                                                else if (o.AdmProyeccion.Modulo.numModulo > 5)
                                                                {
                                                                    s.Tipo = 4;
                                                                }
                                                                else
                                                                {
                                                                    s.Tipo = 2;
                                                                    s.Msj = "Usted reprobó esta materia en el mismo semestre, solo podrá cursarla como 'Invierno o Verano' en el último módulo";
                                                                }
                                                            }
                                                            //Prerrequisito aprobado en el mismo semestre en el que se oferto la materia seleccionada
                                                            //Si el valor devuelto es nulo pasa y continua con la validación.
                                                            else if (dbs.RegistroDeMateria.SingleOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == idMateriaPrerequisitoAprobada && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.NumeroPeriodo == o.AdmProyeccion.Modulo.semestre && x.IdEstadoMateriaRegistrada == 1) == null)
                                                            {
                                                                //Materia seleccionada reprobada en la misma gestión
                                                                //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                                //si el valor devuelto es distinto a nulo continua con la validación
                                                                if (dbs.RegistroDeMateria.OrderByDescending(x => x.ComprobanteRegistro.FechaHora).FirstOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                                //Materia seleccionada reprobada en el mismo semestre
                                                                //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                                //si el valor devuelto es distinto a nulo continua con la validación
                                                                else if (dbs.RegistroDeMateria.SingleOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.NumeroPeriodo == o.AdmProyeccion.Modulo.semestre && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                                //ya que la materia seleccionada fue reprobada en la misma gestión y en el mismo semestre solo puede ser agregada como invierno o verano en el último módulo de la oferta
                                                                //si la materia seleccionada pertenece al último módulo, pasa a pedir una confirmación para agregarla como invierno o verano
                                                                //si la materia seleccionada pertenece a los otros módulos se muestra la advertencia
                                                                else if (o.AdmProyeccion.Modulo.numModulo > 5)
                                                                {
                                                                    s.Tipo = 4;
                                                                }
                                                                else
                                                                {
                                                                    s.Tipo = 2;
                                                                    s.Msj = "Usted reprobó esta materia en el mismo semestre, solo podrá cursarla como 'invierno o verano' en el último módulo";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                s.Tipo = 2;
                                                                s.Msj = "Usted no podrá cursar esta materia porque llevó su prerrequisito en el mismo semestre, solo puede agregarla como invierno o verano en el último módulo";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            s.Tipo = 2;
                                                            s.Msj = "Aún no puede cursar esta materia por favor seleccione su prerrequisito";
                                                        }
                                                    }
                                                    //Materias que no Tienen Prerrequisito
                                                    else
                                                    {
                                                        //Busca si la materia seleccionada es una materia electiva,
                                                        //si el resultado devuelto es distinto a nulo valida si se cumple el requerimiento que se necesita para cursar una materia electiva
                                                        //si el resultado devuelto es igual a nulo pasa a validar la materia que no es electiva y que no tiene un prerrequisito
                                                        var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == MateriaSeleccionada && x.IdPlanEstudio == idPlanEstudioSeleccionado);
                                                        if (materiaElectiva != null)
                                                        {
                                                            //la materia seleccionada es una electiva, las materias electivas solo pueden llevarse si se tiene hasta el 5to semestre aprobado
                                                            List<Materia> materiasQuintoSemestreApprobado = dbs.Materia.SqlQuery("select m.Nombre,* from Persona p, Estudiante e, InscripcionCarrera ic, PlanEstudio pe, PlanEstudioMateria pem, TipoMateria tm, ComprobanteRegistro cr, RegistroDeMateria rdm, Materia m where p.Id = e.Id and ic.IdEstudiante = e.Id and ic.IdPlanEstudio = pe.Id and pem.IdPlanEstudio = pe.Id and tm.Id = pem.IdTipoMateria and pem.IdMateria = m.Id  and cr.IdInscripcionCarrera = ic.id and m.Id = rdm.IdMateria and cr.Id = rdm.IdComprobanteRegistro and rdm.IdEstadoMateriaRegistrada = 1 and pem.NumeroPeriodo between 1 and 5 and p.Id = '" + p.Id + "'").ToList();
                                                            //el total de materias aprobadas hasta el 5to semestre son 30, si el resultado es menor a 30 se muestra la advertencia
                                                            if (materiasQuintoSemestreApprobado.Count == 30)
                                                            {
                                                                //Materia selecciona reprobada en la misma gestión
                                                                //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                                //si el valor devuelto es distinto a nulo continua con la validación
                                                                if (dbs.RegistroDeMateria.OrderByDescending(x => x.ComprobanteRegistro.FechaHora).FirstOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                                //Materia seleccionada reprobada en el mismo semestre
                                                                //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                                //si el valor devuelto es distinto a nulo continua con la validación
                                                                else if (dbs.RegistroDeMateria.SingleOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.NumeroPeriodo == o.AdmProyeccion.Modulo.semestre && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                                //ya que la materia seleccionada fue reprobada en la misma gestión y en el mismo semestre solo puede ser agregada como invierno o verano en el último módulo de la oferta
                                                                //si la materia seleccionada pertenece al último módulo, pasa a pedir una confirmación para agregarla como invierno o verano
                                                                //si la materia seleccionada pertenece a los otros módulos se muestra la advertencia
                                                                else if (o.AdmProyeccion.Modulo.numModulo > 5)
                                                                {
                                                                    s.Tipo = 4;
                                                                }
                                                                else
                                                                {
                                                                    s.Tipo = 2;
                                                                    s.Msj = "Usted reprobó esta materia en el mismo semestre, solo podrá cursarla como 'invierno o verano' en el último módulo";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                s.Tipo = 2;
                                                                s.Msj = "No puede llevar materias electivas hasta tener aprobado todo el quinto semestre";
                                                            }
                                                        }
                                                        //Materias Sin Prerequisito y No Electivas
                                                        else
                                                        {
                                                            //Materia selecciona reprobada en la misma gestión
                                                            //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                            //si el valor devuelto es distinto a nulo continua con la validación
                                                            if (dbs.RegistroDeMateria.OrderByDescending(x => x.ComprobanteRegistro.FechaHora).FirstOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                            //Materia selecciona reprobada en el mismo semestre
                                                            //si el valor devuelto es nulo pasa y agrega la materia a la boleta de proyección,
                                                            //si el valor devuelto es distinto a nulo continua con la validación
                                                            else if (dbs.RegistroDeMateria.SingleOrDefault(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdMateria == MateriaSeleccionada && x.IdEstadoMateriaRegistrada == 2 && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.NumeroPeriodo == o.AdmProyeccion.Modulo.semestre && x.GrupoAperturado.OfertaSistemaEstudio.Oferta.IdGestion == o.AdmProyeccion.Modulo.gestion) == null)
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
                                                            //ya que la materia seleccionada fue reprobada en la misma gestión y en el mismo semestre solo puede ser agregada como invierno o verano en el último módulo de la oferta
                                                            //si la materia seleccionada pertenece al último módulo, pasa a pedir una confirmación para agregarla como invierno o verano
                                                            //si la materia seleccionada pertenece a los otros módulos se muestra la advertencia
                                                            else if (o.AdmProyeccion.Modulo.numModulo > 5)
                                                            {
                                                                s.Tipo = 4;
                                                            }
                                                            else
                                                            {
                                                                s.Tipo = 2;
                                                                s.Msj = "Usted reprobó esta materia en el mismo semestre, solo podrá cursarla como 'invierno o verano' en el último módulo";
                                                            }
                                                        }
                                                    }
                                                }
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
                else
                {
                    s.Tipo = 2;
                    s.Msj = "Debe crearse una boleta de proyección para agregar materias";
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
                var estadoBoleta = db.Boleta.Single(x => x.id == mtb.idBoleta);
                if (estadoBoleta.estado == 4)
                {
                    s.Tipo = 2;
                    s.Msj = "No puede eliminar materias de esta boleta ya que se encuentra revisada, puede crearse otra boleta de proyección.";
                }
                else
                {
                    db.BoletaDetalle.Remove(mtb);
                    db.SaveChanges();
                    s.Tipo = 1;
                    s.Msj = "Se eliminó correctamente";
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "En estos momentos tenemos problemas con nuestros servicios, hemos reportado esta situación a los administradores del sistema, intenta de nuevo más tarde";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
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
        string t2columna1,t2columna2,t2columna3, t2columna4, t2columna5, t2columna6;


        string tabla, fila1, fila2, fila3, fila4;

        //Listar Boleta de Proyeccion
        public ActionResult ListarBoleta()
        {
                ProyeccionEntities db = new ProyeccionEntities();
                SAADSTJEntities dbs = new SAADSTJEntities();
                Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
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
                                m1columna4 = "<label class='click-proyeccion' onclick='EliminarMateriaBoleta(" + idm1columna4 + ")'>" + dbs.Materia.Single(x => x.Id == admMateriaSeleccionada.idMateria).Nombre.ToString()+"</label>";
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

        //Guardar Materias de Ivierno/Verano
        public ActionResult GuardarMateriaInvierno(int idAdm)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
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

        //Buscar Carrera
        public ActionResult BuscarMateria(string materia)
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> Materias = new List<object>();
            List<Materia> lista = db.Materia.SqlQuery("Select top 4 m.* from Materia m, MateriaContenido mc where m.IdMateriaContenido = mc.Id and mc.IdModeloEstudio = 2 and m.Nombre like '%" + materia + "%'").ToList();
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

        //Seleccionar Materia Buscada
        public ActionResult SeleccionarMateriaBuscada(int id)
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

        //Buscar Materia de la Oferta
        public ActionResult BuscarMateriaOferta(int id)
        {

            Status s = new Status();
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            List<object> tabla = new List<object>();
            int c = 1;
            var materiaSeleccionada = db.AdmMateria.Where(x => x.idMateria == id && x.AdmProyeccion.estado == true && x.AdmProyeccion.Modulo.estado == true);
            foreach (var item in materiaSeleccionada)
            {

                var admpro = db.AdmProyeccion.Single(x => x.id == item.idAdmProyeccion);
                var carrera = dbs.PlanEstudio.Single(x => x.Id == admpro.idCarrera).Nombre.ToString();
                var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item.idMateria && x.IdPlanEstudio == admpro.idCarrera);
                var materia = "";
                if (materiaElectiva != null)
                {
                    materia = "Electiva - " + dbs.Materia.Single(x => x.Id == id).Nombre.ToString();
                }
                else
                {
                    materia = dbs.Materia.Single(x => x.Id == id).Nombre.ToString();
                }
                var modulo = db.Modulo.Single(x => x.id == admpro.idModulo).nombre.ToString();
                var semestre = dbs.DescripcionSemestre.Single(x => x.Id == admpro.idSemestre).Nombre.ToString();
                var turno = dbs.Turno.Single(x => x.Id == item.idTurno).Nombre.ToString();
                object o = new
                {
                    idadm = item.id,
                    num = c,
                    carrera = carrera,
                    materia = materia,
                    modulo = modulo,
                    semestre = semestre,
                    turno = turno,
                };
                c++;
                tabla.Add(o);
            }
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }

        //Verificar Boleta de Proyeccion
        public ActionResult VerificarBoleta()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Status s = new Status();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
            if (b != null)
            {
                if (b.estado == 0)
                {
                    s.Tipo = 2;
                    s.Msj = "Usted no cuenta con una boleta de proyección";
                }
                else if ((b.estado == 1 || b.estado == 2 ) && b.BoletaDetalle.Count < 1)
                {
                    s.Tipo = 2;
                    s.Msj = "Debe tener al menos una materia proyectada";
                }
                else if (b.estado == 3)
                {
                    s.Tipo = 2;
                    s.Msj = "Tu boleta esta en revision";
                }
                else
                {
                    s.Tipo = 1;
                }
            }
            else
            {
                s.Tipo = 2;
                s.Msj = "Usted no cuenta con una boleta de proyección";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Verificar Boleta Estado de Boleta
        public ActionResult VerificarEstadoBoleta()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Status s = new Status();
            s.Tipo = 1;
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id);
            if (b != null)
            {
                if (b.estado == 1 || b.estado == 0 ||  b.estado == 4)
                {
                    s.Tipo = 1;
                }
                if (b.estado == 2 || b.estado == 3)
                {
                    //boleta creada por homologado
                    s.Tipo = 2;
                }
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //Enviar Proyeccion a Jefatura
        public ActionResult EnviarProyeccion(string descripcion, string numReferencia)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Status s = new Status();
            try
            {
                Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
                Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id );
                if (b != null)
                {
                    if (b.BoletaDetalle.Count > 0 && b.estado == 2)
                    {
                        if (numReferencia != "")
                        {
                            b.descripcion = descripcion;
                            b.numReferencia = numReferencia;
                            b.estado = 3;
                            db.SaveChanges();
                            s.Tipo = 1;
                            s.Msj = "Se envío la boleta a revisión";
                        }
                        else
                        {
                            s.Tipo = 2;
                            s.Msj = "Debe ingresar su número de celular";
                        }
                    }
                    else if (b.estado == 3)
                    {
                        s.Tipo = 2;
                        s.Msj = "Tu boleta ya fue enviada a revisión";
                    }
                    else
                    {
                        s.Tipo = 2;
                        s.Msj = "Debe tener al menos una materia proyectada";
                    }
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

        //Mostrar Descripcion del Estudiante
        public ActionResult MostarRespuesta()
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Status s = new Status();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id && x.estado == 4);
            if (b != null)
            {
                s.Tipo = 1;
                s.Msj = b.respuesta;
            }
            else
            {
                s.Tipo = 0;
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}