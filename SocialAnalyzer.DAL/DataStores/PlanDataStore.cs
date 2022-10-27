using Microsoft.EntityFrameworkCore;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.DataStores
{
    public class PlanDataStore : DataStore<Plan>, IPlanDataStore
    {
        private readonly analizerContext _dbContext;
        public PlanDataStore(analizerContext context) : base(context)
        {
            _dbContext = context;
        }


        public new async Task<Plan> GetByIdAsync(int id)
        {
            var plan = await _dbContext.Plans
                .FirstOrDefaultAsync(x => x.IdPlan == id);


            return plan;
        }
    }
}
