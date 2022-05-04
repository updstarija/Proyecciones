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
    
    public partial class Facultad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Facultad()
        {
            this.DireccionArea = new HashSet<DireccionArea>();
        }
    
        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<short> IdEstado { get; set; }
        public Nullable<short> IdCargo { get; set; }
        public string NombreDecanatura { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DireccionArea> DireccionArea { get; set; }
    }
}