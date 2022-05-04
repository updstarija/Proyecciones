using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecciones.Models;

namespace Proyecciones.Controllers
{
    public class HomeEstudianteController : Controller
    {
        // GET: HomeEstudiante
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
        // GET: Contactos
        public ActionResult Contactos()
        {
            return View();
        }

        //Listar Cabecera de la Boleta
        public ActionResult ListarMalla(int carrera)
        {
            ProyeccionEntities db = new ProyeccionEntities();
            SAADSTJEntities dbs = new SAADSTJEntities();
            Status s = new Status();
            var duracionPlanEstudio = dbs.PlanEstudio.Single(x => x.Id == carrera).Duracion;
            int duracion = Convert.ToInt32(duracionPlanEstudio);
            string columnacabecera = "";
            string idcolumna1cuerpo1 = "", idcolumna2cuerpo1 = "", idcolumna3cuerpo1 = "", idcolumna4cuerpo1 = "", idcolumna5cuerpo1 = "", idcolumna6cuerpo1 = "";
            string idcolumna1cuerpo2 = "", idcolumna2cuerpo2 = "", idcolumna3cuerpo2 = "", idcolumna4cuerpo2 = "", idcolumna5cuerpo2 = "", idcolumna6cuerpo2 = "";
            string idcolumna1cuerpo3 = "", idcolumna2cuerpo3 = "", idcolumna3cuerpo3 = "", idcolumna4cuerpo3 = "", idcolumna5cuerpo3 = "", idcolumna6cuerpo3 = "";
            string idcolumna1cuerpo4 = "", idcolumna2cuerpo4 = "", idcolumna3cuerpo4 = "", idcolumna4cuerpo4 = "", idcolumna5cuerpo4 = "", idcolumna6cuerpo4 = "";
            string idcolumna1cuerpo5 = "", idcolumna2cuerpo5 = "", idcolumna3cuerpo5 = "", idcolumna4cuerpo5 = "", idcolumna5cuerpo5 = "", idcolumna6cuerpo5 = "";
            string idcolumna1cuerpo6 = "", idcolumna2cuerpo6 = "", idcolumna3cuerpo6 = "", idcolumna4cuerpo6 = "", idcolumna5cuerpo6 = "", idcolumna6cuerpo6 = "";
            string idcolumna1cuerpo7 = "", idcolumna2cuerpo7 = "", idcolumna3cuerpo7 = "", idcolumna4cuerpo7 = "", idcolumna5cuerpo7 = "", idcolumna6cuerpo7 = "";
            string idcolumna1cuerpo8 = "", idcolumna2cuerpo8 = "", idcolumna3cuerpo8 = "", idcolumna4cuerpo8 = "", idcolumna5cuerpo8 = "", idcolumna6cuerpo8 = "";
            string idcolumna1cuerpo9 = "", idcolumna2cuerpo9 = "", idcolumna3cuerpo9 = "", idcolumna4cuerpo9 = "", idcolumna5cuerpo9 = "", idcolumna6cuerpo9 = "";
            string idcolumna1cuerpo10 = "", idcolumna2cuerpo10 = "", idcolumna3cuerpo10 = "", idcolumna4cuerpo10 = "", idcolumna5cuerpo10 = "", idcolumna6cuerpo10 = "";
            string columna1cuerpo1 = "", columna2cuerpo1 = "", columna3cuerpo1 = "", columna4cuerpo1 = "", columna5cuerpo1 = "", columna6cuerpo1 = "";
            string columna1cuerpo2 = "", columna2cuerpo2 = "", columna3cuerpo2 = "", columna4cuerpo2 = "", columna5cuerpo2 = "", columna6cuerpo2 = "";
            string columna1cuerpo3 = "", columna2cuerpo3 = "", columna3cuerpo3 = "", columna4cuerpo3 = "", columna5cuerpo3 = "", columna6cuerpo3 = "";
            string columna1cuerpo4 = "", columna2cuerpo4 = "", columna3cuerpo4 = "", columna4cuerpo4 = "", columna5cuerpo4 = "", columna6cuerpo4 = "";
            string columna1cuerpo5 = "", columna2cuerpo5 = "", columna3cuerpo5 = "", columna4cuerpo5 = "", columna5cuerpo5 = "", columna6cuerpo5 = "";
            string columna1cuerpo6 = "", columna2cuerpo6 = "", columna3cuerpo6 = "", columna4cuerpo6 = "", columna5cuerpo6 = "", columna6cuerpo6 = "";
            string columna1cuerpo7 = "", columna2cuerpo7 = "", columna3cuerpo7 = "", columna4cuerpo7 = "", columna5cuerpo7 = "", columna6cuerpo7 = "";
            string columna1cuerpo8 = "", columna2cuerpo8 = "", columna3cuerpo8 = "", columna4cuerpo8 = "", columna5cuerpo8 = "", columna6cuerpo8 = "";
            string columna1cuerpo9 = "", columna2cuerpo9 = "", columna3cuerpo9 = "", columna4cuerpo9 = "", columna5cuerpo9 = "", columna6cuerpo9 = "";
            string columna1cuerpo10 = "", columna2cuerpo10 = "", columna3cuerpo10 = "", columna4cuerpo10 = "", columna5cuerpo10 = "", columna6cuerpo10 = "";
            int ancho = 100 / duracion;
            for (int i = 1; i <= duracion; i++)
            {
                columnacabecera += "<td height='45px' width='" + ancho + "%'><p class='nombreTituloSemestre'> Semestre " + i + "</p></td>";
                int contadorlinea = 0;
                foreach (var item in dbs.PlanEstudioMateria.Where(x => x.IdPlanEstudio == carrera && x.NumeroPeriodo == i))
                {
                    contadorlinea++;
                    var idmate = dbs.Materia.Single(x => x.Id == item.IdMateria).Id.ToString();
                    var mate = dbs.Materia.Single(x => x.Id == item.IdMateria).Nombre.ToString();
                    switch (i)
                    {
                        case 1:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo1 = idmate;
                                columna1cuerpo1 += "<td width='" + ancho + "%'><div id='"+ idcolumna1cuerpo1 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if(contadorlinea == 2)
                            {
                                idcolumna2cuerpo1 = idmate;
                                columna2cuerpo1 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo1 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo1 = idmate;
                                columna3cuerpo1 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo1 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo1 = idmate;
                                columna4cuerpo1 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo1 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo1 = idmate;
                                columna5cuerpo1 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo1 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo1 = idmate;
                                columna6cuerpo1 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo1 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 2:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo2 = idmate;
                                columna1cuerpo2 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo2 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo2 = idmate;
                                columna2cuerpo2 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo2 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo2 = idmate;
                                columna3cuerpo2 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo2 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo2 = idmate;
                                columna4cuerpo2 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo2 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo2 = idmate;
                                columna5cuerpo2 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo2 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo2 = idmate;
                                columna6cuerpo2 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo2 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 3:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo3 = idmate;
                                columna1cuerpo3 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo3 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo3 = idmate;
                                columna2cuerpo3 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo3 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo3 = idmate;
                                columna3cuerpo3 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo3 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo3 = idmate;
                                columna4cuerpo3 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo3 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo3 = idmate;
                                columna5cuerpo3 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo3 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo3 = idmate;
                                columna6cuerpo3 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo3 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 4:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo4 = idmate;
                                columna1cuerpo4 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo4 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo4 = idmate;
                                columna2cuerpo4 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo4 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo4 = idmate;
                                columna3cuerpo4 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo4 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo4 = idmate;
                                columna4cuerpo4 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo4 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo4 = idmate;
                                columna5cuerpo4 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo4 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo4 = idmate;
                                columna6cuerpo4 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo4 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 5:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo5 = idmate;
                                columna1cuerpo5 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo5 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo5 = idmate;
                                columna2cuerpo5 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo5 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo5 = idmate;
                                columna3cuerpo5 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo5 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo5 = idmate;
                                columna4cuerpo5 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo5 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo5 = idmate;
                                columna5cuerpo5 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo5 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo5 = idmate;
                                columna6cuerpo5 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo5 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 6:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo6 = idmate;
                                columna1cuerpo6 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo6 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo6 = idmate;
                                columna2cuerpo6 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo6 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo6 = idmate;
                                columna3cuerpo6 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo6 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo6 = idmate;
                                columna4cuerpo6 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo6 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo6 = idmate;
                                columna5cuerpo6 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo6 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo6 = idmate;
                                columna6cuerpo6 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo6 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 7:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo7 = idmate;
                                columna1cuerpo7 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo7 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo7 = idmate;
                                columna2cuerpo7 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo7 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo7 = idmate;
                                columna3cuerpo7 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo7 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo7 = idmate;
                                columna4cuerpo7 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo7 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo7 = idmate;
                                columna5cuerpo7 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo7 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo7 = idmate;
                                columna6cuerpo7 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo7 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 8:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo8 = idmate;
                                columna1cuerpo8 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo8 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo8 = idmate;
                                columna2cuerpo8 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo8 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo8 = idmate;
                                columna3cuerpo8 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo8 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo8 = idmate;
                                columna4cuerpo8 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo8 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo8 = idmate;
                                columna5cuerpo8 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo8 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo8 = idmate;
                                columna6cuerpo8 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo8 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 9:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo9 = idmate;
                                columna1cuerpo9 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo9 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo9 = idmate;
                                columna2cuerpo9 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo9 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo9 = idmate;
                                columna3cuerpo9 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo9 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo9 = idmate;
                                columna4cuerpo9 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo9 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo9 = idmate;
                                columna5cuerpo9 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo9 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo9 = idmate;
                                columna6cuerpo9 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo9 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;

                        case 10:
                            if (contadorlinea == 1)
                            {
                                idcolumna1cuerpo10 = idmate;
                                columna1cuerpo10 += "<td width='" + ancho + "%'><div id='" + idcolumna1cuerpo10 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 2)
                            {
                                idcolumna2cuerpo10 = idmate;
                                columna2cuerpo10 += "<td width='" + ancho + "%'><div id='" + idcolumna2cuerpo10 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 3)
                            {
                                idcolumna3cuerpo10 = idmate;
                                columna3cuerpo10 += "<td width='" + ancho + "%'><div id='" + idcolumna3cuerpo10 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 4)
                            {
                                idcolumna4cuerpo10 = idmate;
                                columna4cuerpo10 += "<td width='" + ancho + "%'><div id='" + idcolumna4cuerpo10 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 5)
                            {
                                idcolumna5cuerpo10 = idmate;
                                columna5cuerpo10 += "<td width='" + ancho + "%'><div id='" + idcolumna5cuerpo10 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            else if (contadorlinea == 6)
                            {
                                idcolumna6cuerpo10 = idmate;
                                columna6cuerpo10 += "<td width='" + ancho + "%'><div id='" + idcolumna6cuerpo10 + "' class='card materiaMalla materiaMallaPendiente'><div class='card-body'><p class='nombreMaterias'>" + mate + "</p></div></div></td>";
                            }
                            break;
                    }
                }
            }
            string cabecera = "<thead><tr class='text-center'>" + columnacabecera+"</tr></thead>";
            string fila1 = "<tr>" + columna1cuerpo1 + columna1cuerpo2 + columna1cuerpo3 + columna1cuerpo4 + columna1cuerpo5 + columna1cuerpo6 + columna1cuerpo7 + columna1cuerpo8 + columna1cuerpo9 + columna1cuerpo10 + "</tr>";
            string fila2 = "<tr>" + columna2cuerpo1 + columna2cuerpo2 + columna2cuerpo3 + columna2cuerpo4 + columna2cuerpo5 + columna2cuerpo6 + columna2cuerpo7 + columna2cuerpo8 + columna2cuerpo9 + columna2cuerpo10 + "</tr>";
            string fila3 = "<tr>" + columna3cuerpo1 + columna3cuerpo2 + columna3cuerpo3 + columna3cuerpo4 + columna3cuerpo5 + columna3cuerpo6 + columna3cuerpo7 + columna3cuerpo8 + columna3cuerpo9 + columna3cuerpo10 + "</tr>";
            string fila4 = "<tr>" + columna4cuerpo1 + columna4cuerpo2 + columna4cuerpo3 + columna4cuerpo4 + columna4cuerpo5 + columna4cuerpo6 + columna4cuerpo7 + columna4cuerpo8 + columna4cuerpo9 + columna4cuerpo10 + "</tr>";
            string fila5 = "<tr>" + columna5cuerpo1 + columna5cuerpo2 + columna5cuerpo3 + columna5cuerpo4 + columna5cuerpo5 + columna5cuerpo6 + columna5cuerpo7 + columna5cuerpo8 + columna5cuerpo9 + columna5cuerpo10 + "</tr>";
            string fila6 = "<tr>" + columna6cuerpo1 + columna6cuerpo2 + columna6cuerpo3 + columna6cuerpo4 + columna6cuerpo5 + columna6cuerpo6 + columna6cuerpo7 + columna6cuerpo8 + columna6cuerpo9 + columna6cuerpo10 + "</tr>";
            string cuerpo = "<tbody>" + fila1 + fila2 + fila3 + fila4 + fila5 + fila6 + "</tbody>";
            string tabla = ""+cabecera+""+cuerpo+"";
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }

        //Marcar Materias Aprobadas en la Malla
        public ActionResult MarcarMateriasAprobadas(int carrera)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            var MateriasAprovadas = dbs.RegistroDeMateria.Where(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdEstadoMateriaRegistrada == 1 && x.IdPlanEstudio == carrera).ToList();
            if (MateriasAprovadas != null)
            {
                List<object> Materias = new List<object>();
                foreach (var item in MateriasAprovadas)
                {
                    object o = new
                    {
                        Id = item.IdMateria
                    };
                    Materias.Add(o);
                }
                return Json(Materias, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int valorMensaje = 01;
                return Json(valorMensaje, JsonRequestBehavior.AllowGet);
            }
        }

        //Marcar Materias Reprobadas en la Malla
        public ActionResult MarcarMateriasReprobadas(int carrera)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            var MateriasAprovadas = dbs.RegistroDeMateria.Where(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdEstadoMateriaRegistrada == 2 && x.IdPlanEstudio == carrera).ToList();
            if (MateriasAprovadas != null)
            {
                List<object> Materias = new List<object>();
                foreach (var item in MateriasAprovadas)
                {
                    object o = new
                    {
                        Id = item.IdMateria
                    };
                    Materias.Add(o);
                }
                return Json(Materias, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int valorMensaje = 01;
                return Json(valorMensaje, JsonRequestBehavior.AllowGet);
            }
        }        

        //Marcar Materias Electivas Aprobadas en la Malla
        public ActionResult MarcarMateriasElectivasAprobadas(int carrera)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            var MateriasAprovadas = dbs.RegistroDeMateria.Where(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdEstadoMateriaRegistrada == 1 && x.IdPlanEstudio == carrera).ToList();
            if (MateriasAprovadas != null)
            {
                int c = 0;
                //Inicio Contabiliza materias electivas convalidadas
                var traspaso = dbs.Traspaso.Where(x => x.InscripcionCarrera.Estudiante.Id == p.Id && x.InscripcionCarrera.IdPlanEstudio == carrera).ToList();
                if (traspaso.Count > 0)
                {
                    foreach (var item in traspaso)
                    {
                        var MateriasHomologadas = dbs.DetalleHomologacion.Where(x => x.IdTraspaso == item.Id).ToList();
                        var MateriasConvalidadas = dbs.DetalleConvalidacion.Where(x => x.IdTraspaso == item.Id).ToList();
                        foreach (var item1 in MateriasHomologadas)
                        {
                            var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item1.IdMateria && x.IdPlanEstudio == carrera);
                            if (materiaElectiva != null)
                            {
                                c++;
                            }
                        }
                        foreach (var item1 in MateriasConvalidadas)
                        {
                            var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item1.IdMateria && x.IdPlanEstudio == carrera);
                            if (materiaElectiva != null)
                            {
                                c++;
                            }
                        }
                    }
                }
                //Fin Contabiliza materias electivas convalidadas
                List<object> Materias = new List<object>();
                foreach (var item in MateriasAprovadas)
                {
                    var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item.IdMateria && x.IdPlanEstudio == carrera);
                    if (materiaElectiva != null)
                    {
                        c++;
                    }
                }
                int c1 = 1;
                foreach (var item in dbs.PlanEstudioMateria.SqlQuery("select * from PlanEstudio pe, PlanEstudioMateria pem, Materia m where pe.Id = pem.IdPlanEstudio and pem.IdMateria = m.Id and pe.IdModeloEstudio = 2 and m.Nombre like 'Electiva%' and pe.Id = " + carrera + ""))
                {
                    if (c1 <= c)
                    {
                        object o = new
                        {
                            Id = item.Materia.Id
                        };
                        Materias.Add(o);
                    }
                    c1++;
                }
                return Json(Materias, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int valorMensaje = 01;
                return Json(valorMensaje, JsonRequestBehavior.AllowGet);
            }
        }

        //Marcar Materias Electivas Reprobadas en la Malla
        public ActionResult MarcarMateriasElectivasReprobadas(int carrera)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            var MateriasAprovadas = dbs.RegistroDeMateria.Where(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdEstadoMateriaRegistrada == 2 && x.IdPlanEstudio == carrera).ToList();
            if (MateriasAprovadas != null)
            {
                int c = 0;
                List<object> Materias = new List<object>();
                foreach (var item in MateriasAprovadas)
                {
                    var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item.IdMateria && x.IdPlanEstudio == carrera);
                    if (materiaElectiva != null)
                    {
                        c++;
                    }
                }
                int c1 = 1;
                foreach (var item in dbs.PlanEstudioMateria.SqlQuery("select * from PlanEstudio pe, PlanEstudioMateria pem, Materia m where pe.Id = pem.IdPlanEstudio and pem.IdMateria = m.Id and pe.IdModeloEstudio = 2 and m.Nombre like 'Electiva%' and pe.Id = " + carrera + ""))
                {
                    if (c1 <= c)
                    {
                        object o = new
                        {
                            Id = item.Materia.Id
                        };
                        Materias.Add(o);
                    }
                    c1++;
                }
                return Json(Materias, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int valorMensaje = 01;
                return Json(valorMensaje, JsonRequestBehavior.AllowGet);
            }
        }

        //Marcar Materias que se tienen proyectadas 
        public ActionResult MarcarMateriasEnBoleta(int carrera)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            ProyeccionEntities db = new ProyeccionEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id && x.estado != 0);
            if (b != null)
            {
                var BolesDetalles = db.BoletaDetalle.Where(x => x.idBoleta == b.id);
                List<object> Materias = new List<object>();
                foreach (var item in BolesDetalles)
                {
                    object o = new
                    {
                        Id = item.AdmMateria.idMateria
                    };
                    Materias.Add(o);
                }
                return Json(Materias, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int valorMensaje = 01;
                return Json(valorMensaje, JsonRequestBehavior.AllowGet);
            }
        }

        ////Marcar Materias Electivas que se tienen proyectadas 
        public ActionResult MarcarMateriasElectivasEnBoleta(int carrera)
        {
            SAADSTJEntities dbs = new SAADSTJEntities();
            ProyeccionEntities db = new ProyeccionEntities();
            Persona p = dbs.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            Boleta b = db.Boleta.OrderByDescending(x => x.id).FirstOrDefault(x => x.idEstudiante == p.Id && x.estado != 0);
            if (b != null)
            {
                var MateriasAprovadas = dbs.RegistroDeMateria.Where(x => x.ComprobanteRegistro.InscripcionCarrera.Estudiante.Id == p.Id && x.IdEstadoMateriaRegistrada == 1 && x.IdPlanEstudio == carrera).ToList();
                if (MateriasAprovadas != null)
                {
                    int c = 0;
                    //Inicio Contabiliza materias electivas convalidadas
                    var traspaso = dbs.Traspaso.Where(x => x.InscripcionCarrera.Estudiante.Id == p.Id && x.InscripcionCarrera.IdPlanEstudio == carrera).ToList();
                    if (traspaso.Count > 0)
                    {
                        foreach (var item in traspaso)
                        {
                            var MateriasHomologadas = dbs.DetalleHomologacion.Where(x => x.IdTraspaso == item.Id).ToList();
                            var MateriasConvalidadas = dbs.DetalleConvalidacion.Where(x => x.IdTraspaso == item.Id).ToList();
                            foreach (var item1 in MateriasHomologadas)
                            {
                                var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item1.IdMateria && x.IdPlanEstudio == carrera);
                                if (materiaElectiva != null)
                                {
                                    c++;
                                }
                            }
                            foreach (var item1 in MateriasConvalidadas)
                            {
                                var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item1.IdMateria && x.IdPlanEstudio == carrera);
                                if (materiaElectiva != null)
                                {
                                    c++;
                                }
                            }
                        }
                    }
                    //Fin Contabiliza materias electivas convalidadas
                    List<object> Materias = new List<object>();
                    foreach (var item in MateriasAprovadas)
                    {
                        var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item.IdMateria && x.IdPlanEstudio == carrera);
                        if (materiaElectiva != null)
                        {
                            c++;
                        }
                    }
                    int c1 = 0;
                    var BolesDetalles = db.BoletaDetalle.Where(x => x.idBoleta == b.id);
                    foreach (var item1 in BolesDetalles)
                    {
                        var materiaElectiva = dbs.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item1.AdmMateria.idMateria && x.IdPlanEstudio == carrera);
                        if (materiaElectiva != null)
                        {
                            c1++;
                        }
                    }
                    int r = c + c1;
                    int c3 = 1;
                    foreach (var item in dbs.PlanEstudioMateria.SqlQuery("select * from PlanEstudio pe, PlanEstudioMateria pem, Materia m where pe.Id = pem.IdPlanEstudio and pem.IdMateria = m.Id and pe.IdModeloEstudio = 2 and m.Nombre like 'Electiva%' and pe.Id = " + carrera + ""))
                    {
                        if (c3 > c && c3 <= r )
                        {
                            object o = new
                            {
                                Id = item.Materia.Id
                            };
                            Materias.Add(o);
                        }
                        c3++;
                    }
                    return Json(Materias, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int valorMensaje = 01;
                    return Json(valorMensaje, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                int valorMensaje = 01;
                return Json(valorMensaje, JsonRequestBehavior.AllowGet);
            }
        }

        //Verificar Traspaso
        public ActionResult VerificarTraspaso(int carrera)
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> Materias = new List<object>();
            Persona p = db.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            var traspaso = db.Traspaso.Where(x => x.InscripcionCarrera.Estudiante.Id == p.Id && x.InscripcionCarrera.IdPlanEstudio == carrera).ToList();
            if (traspaso.Count > 0)
            {
                foreach (var item in traspaso)
                {
                    var MateriasHomologadas = db.DetalleHomologacion.Where(x => x.IdTraspaso == item.Id).ToList();
                    var MateriasConvalidadas = db.DetalleConvalidacion.Where(x => x.IdTraspaso == item.Id).ToList();
                    foreach (var item1 in MateriasHomologadas)
                    {
                        object o = new
                        {
                            Id = item1.IdMateria,
                        };
                        Materias.Add(o);
                    }
                    foreach (var item1 in MateriasConvalidadas)
                    {
                        object o = new
                        {
                            Id = item1.IdMateria,
                        };
                        Materias.Add(o);
                    }
                }
            }
            return Json(Materias, JsonRequestBehavior.AllowGet);
        }

        //Verificar Traspaso
        public ActionResult MateriasElectivasConvalidadas(int carrera)
        {
            SAADSTJEntities db = new SAADSTJEntities();
            List<object> Materias = new List<object>();
            Persona p = db.Persona.SingleOrDefault(x => x.DocumentoIdentidad == User.Identity.Name);
            var traspaso = db.Traspaso.Where(x => x.InscripcionCarrera.Estudiante.Id == p.Id && x.InscripcionCarrera.IdPlanEstudio == carrera).ToList();
            if (traspaso.Count > 0)
            {
                int c = 0;
                foreach (var item in traspaso)
                {
                    var MateriasHomologadas = db.DetalleHomologacion.Where(x => x.IdTraspaso == item.Id).ToList();
                    var MateriasConvalidadas = db.DetalleConvalidacion.Where(x => x.IdTraspaso == item.Id).ToList();
                    foreach (var item1 in MateriasHomologadas)
                    {
                        var materiaElectiva = db.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item1.IdMateria && x.IdPlanEstudio == carrera);
                        if (materiaElectiva != null)
                        {
                            c++;
                        }
                    }
                    foreach (var item1 in MateriasConvalidadas)
                    {
                        var materiaElectiva = db.PlanEstudioMateria.SingleOrDefault(x => x.IdTipoMateria == 10 && x.IdMateria == item1.IdMateria && x.IdPlanEstudio == carrera);
                        if (materiaElectiva != null)
                        {
                            c++;
                        }
                    }
                }
                int c1 = 1;
                foreach (var item in db.PlanEstudioMateria.SqlQuery("select * from PlanEstudio pe, PlanEstudioMateria pem, Materia m where pe.Id = pem.IdPlanEstudio and pem.IdMateria = m.Id and pe.IdModeloEstudio = 2 and m.Nombre like 'Electiva%' and pe.Id = " + carrera + ""))
                {
                    if (c1 <= c)
                    {
                        object o = new
                        {
                            Id = item.Materia.Id
                        };
                        Materias.Add(o);
                    }
                    c1++;
                }
            }
            return Json(Materias, JsonRequestBehavior.AllowGet);
        }
    }
}