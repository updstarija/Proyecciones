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
    
    public partial class AdmMateria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdmMateria()
        {
            this.BoletaDetalle = new HashSet<BoletaDetalle>();
        }
    
        public int id { get; set; }
        public int idTurno { get; set; }
        public int idAdmProyeccion { get; set; }
        public int idMateria { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoletaDetalle> BoletaDetalle { get; set; }
        public virtual AdmProyeccion AdmProyeccion { get; set; }
    }
}
