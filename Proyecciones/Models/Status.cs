using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecciones.Models
{
    public class Status
    {
        private int tipo;
        private string msj;


        public int Tipo { get => tipo; set => tipo = value; }
        public string Msj { get => msj; set => msj = value; }
    }
}