//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecciones.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apaterno { get; set; }
        public string amaterno { get; set; }
        public string usuario1 { get; set; }
        public string contraseña { get; set; }
        public bool estado { get; set; }
        public int idRol { get; set; }
    
        public virtual Rol Rol { get; set; }
    }
}
