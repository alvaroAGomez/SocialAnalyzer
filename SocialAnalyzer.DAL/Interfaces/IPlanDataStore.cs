using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Interfaces
{
    public interface IPlanDataStore : IDataStore<Plan>
    {
        Task<Plan> GetByIdAsync(int id);
    }
}
