using SocialAnalyzer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Models
{
    public class LoginUsuarios : IEntity
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }


        public DateTime? FechaUltimoIntento { get; set; }
    }
}
