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
    
    public partial class DetalleHomologacion
    {
        public int IdTraspaso { get; set; }
        public int IdMateria { get; set; }
        public short IdPlanEstudio { get; set; }
        public decimal Nota { get; set; }
        public decimal PorcentajeEquivale { get; set; }
        public Nullable<short> CargaHoraria { get; set; }
        public Nullable<int> Orden { get; set; }
    
        public virtual Materia Materia { get; set; }
        public virtual PlanEstudio PlanEstudio { get; set; }
        public virtual Traspaso Traspaso { get; set; }
    }
}