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
    
    public partial class Materia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materia()
        {
            this.RegistroDeMateria = new HashSet<RegistroDeMateria>();
            this.PlanEstudioMateria = new HashSet<PlanEstudioMateria>();
            this.DetalleConvalidacion = new HashSet<DetalleConvalidacion>();
            this.DetalleHomologacion = new HashSet<DetalleHomologacion>();
        }
    
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nombre { get; set; }
        public short IdEstado { get; set; }
        public short IdMateriaContenido { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroDeMateria> RegistroDeMateria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanEstudioMateria> PlanEstudioMateria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleConvalidacion> DetalleConvalidacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleHomologacion> DetalleHomologacion { get; set; }
    }
}