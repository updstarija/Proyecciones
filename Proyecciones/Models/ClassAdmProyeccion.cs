using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecciones.Models
{
    public class ClassAdmProyeccion
    {
        private int id;
        private int idadmproyecion;
        private string carrera;
        private int idcarrera;
        private string matemanana;
        private string matemediodia;
        private string matetarde;
        private string matenoche; 
        private int idmatemanana;
        private int idmatemediodia;
        private int idmatetarde;
        private int idmatenoche;

        public int Id { get => id; set => id = value; }
        public string Matemanana { get => matemanana; set => matemanana = value; }
        public string Matemediodia { get => matemediodia; set => matemediodia = value; }
        public string Matetarde { get => matetarde; set => matetarde = value; }
        public string Matenoche { get => matenoche; set => matenoche = value; }
        public string Carrera { get => carrera; set => carrera = value; }
        public int Idadmproyecion { get => idadmproyecion; set => idadmproyecion = value; }
        public int Idmatemanana { get => idmatemanana; set => idmatemanana = value; }
        public int Idmatemediodia { get => idmatemediodia; set => idmatemediodia = value; }
        public int Idmatetarde { get => idmatetarde; set => idmatetarde = value; }
        public int Idmatenoche { get => idmatenoche; set => idmatenoche = value; }
        public int Idcarrera { get => idcarrera; set => idcarrera = value; }
    }
}