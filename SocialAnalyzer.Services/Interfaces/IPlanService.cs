using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Interfaces
{
    public  interface IPlanService 
    {
        Task<IList<Plan>> GetAllAsync();
        Task<Plan> GetPlanByIdAsync(int id);

        Task<Plan> InsertAndSaveAsync(Plan plan);

    }
}
