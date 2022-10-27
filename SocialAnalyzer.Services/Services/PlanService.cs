using AutoMapper;
using SocialAnalyzer.DAL.DataStores;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.DAL.Mapping;
using SocialAnalyzer.DAL.Models;
using SocialAnalyzer.SDK.Options;
using SocialAnalyzer.Services.Interfaces;
using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Plan = SocialAnalyzer.Services.Models.Plan;

namespace SocialAnalyzer.Services.Services
{
    public class PlanService : IPlanService
    {
        private readonly IMapper _mapper;
        private readonly IPlanDataStore _PlanDataStore;
        private readonly MessageOptions _msgOptions;


        public PlanService(IMapper mapper, IPlanDataStore actorDataStore, MessageOptions messageOptions)
        {
            _mapper = mapper;
            _PlanDataStore = actorDataStore;
            _msgOptions = messageOptions;
        }

        public async Task<Plan> GetPlanByIdAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    var usuario = await _PlanDataStore.GetByIdAsync(id);
                    return _mapper.Map<Plan>(usuario);
                }
                return null;
            }
            catch (Exception e)
            {
                var user = new Plan();
                user.ResultMessage = _msgOptions.errorException + e.Message;
                return user;
            }


        }

        public async Task<IList<Plan>> GetAllAsync()
        {
            var roles = await _PlanDataStore.GetAllAsync();

            return _mapper.Map<IList<Plan>>(roles);
        }

        public async Task<Plan> InsertAndSaveAsync(Plan plan)
        {
            var planDAL = _mapper.Map<DAL.Models.Plan>(plan);
            try
            {
                if (await _PlanDataStore.ExistsAsync(x => x.Nombre.ToLower() == plan.Nombre.ToLower()))
                {
                    plan.ResultMessage = "Ya existe un plan con el mismo nombre";
                    return plan;
                }
                else
                {
                    //nuevo usuario

                    var insertedPlan = await _PlanDataStore.InsertAndSaveAsync(planDAL);
                    if (insertedPlan == null)
                    {
                        var u = new Plan();
                        u.ResultMessage = _msgOptions.errorSave;
                        return u;
                    }

                    var user = await GetPlanByIdAsync(insertedPlan.IdPlan);

                    return user;
                }
            }
            catch (Exception e)
            {
                var user = new Plan();
                user.ResultMessage = _msgOptions.errorException + e.Message;
                return user;
            }

        }

        public async Task<Plan> UpdateAndSaveAsync(Plan plan)
        {
            try
            {
                var planDAL = _mapper.Map<DAL.Models.Plan>(plan);

                if (await _PlanDataStore.ExistsAsync(x => (x.Nombre.ToLower() == plan.Nombre.ToLower()) && x.IdPlan != plan.IdPlan))
                {
                    plan.ResultMessage = "Ya existe un plan con el mismo nombre";
                    return plan;
                }
                else
                {

                    var excludedValues = new string[] { nameof(plan.IdPlan) };

                    var updatedUsr = await _PlanDataStore.UpdateAndSaveAsync(x => x.IdPlan == plan.IdPlan, planDAL, excludedValues);
                    if (updatedUsr == null)
                    {
                        var u = new Plan();
                        u.ResultMessage = _msgOptions.errorUpdate;
                        return u;
                    }

                    return _mapper.Map<Plan>(updatedUsr);
                }
            }
            catch (Exception e )
            {
                var user = new Plan();
                user.ResultMessage = _msgOptions.errorException + e.Message;
                return user;
            }
           


        }


    }
}
