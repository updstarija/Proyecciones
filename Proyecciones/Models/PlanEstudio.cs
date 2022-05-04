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
    
    public partial class PlanEstudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlanEstudio()
        {
            this.InscripcionCarrera = new HashSet<InscripcionCarrera>();
            this.PlanEstudioMateria = new HashSet<PlanEstudioMateria>();
            this.DetalleHomologacion = new HashSet<DetalleHomologacion>();
        }
    
        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public Nullable<short> Duracion { get; set; }
        public Nullable<short> IdPeriodicidad { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<short> IdEstadoPlan { get; set; }
        public Nullable<short> IdCarrera { get; set; }
        public Nullable<short> CantidadMaterias { get; set; }
        public Nullable<short> CantidadElectivas { get; set; }
        public Nullable<short> TotalHorasTeoricas { get; set; }
        public Nullable<short> TotalHorasPracticas { get; set; }
        public Nullable<short> TotalHorasAutonomas { get; set; }
        public Nullable<short> TotalHorasTeoricasVirtuales { get; set; }
        public Nullable<short> TotalHorasPracticasVirtuales { get; set; }
        public Nullable<byte> PeriodicidadInicialElectivas { get; set; }
        public Nullable<int> PeriodoRequeridoPractica { get; set; }
        public string ResolucionMinisterial { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public string PanelesDeGrado { get; set; }
        public string CitePlanEstudio { get; set; }
        public Nullable<System.DateTime> fechaAprovacion { get; set; }
        public Nullable<int> RegimenComplementario { get; set; }
        public Nullable<int> HorasPracticasProfesional { get; set; }
        public Nullable<int> HorasMDG { get; set; }
        public Nullable<int> IdModeloEstudio { get; set; }
        public Nullable<int> IdTipoPlanEstudio { get; set; }
        public Nullable<short> IdSistemaEstudio { get; set; }
    
        public virtual Carrera Carrera { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InscripcionCarrera> InscripcionCarrera { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanEstudioMateria> PlanEstudioMateria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleHomologacion> DetalleHomologacion { get; set; }
    }
}
