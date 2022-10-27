using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class PlanPermiso
    {
        public int IdPlan { get; set; }
        public bool? Permiso1 { get; set; }
        public bool? Permiso2 { get; set; }
        public bool? Permiso3 { get; set; }
        public bool? Permiso4 { get; set; }
        public bool? Permiso5 { get; set; }
        public bool? Permiso6 { get; set; }
    }
}
