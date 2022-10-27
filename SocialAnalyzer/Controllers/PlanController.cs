using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialAnalyzer.Models;
using SocialAnalyzer.Services.Interfaces;
using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialAnalyzer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlanService _planService;


        public PlanController(IMapper mapper, IPlanService planService)
        {
            _mapper = mapper;
            _planService = planService;

        }

        /// <summary>
        /// Devuelve todos los planes disponibles
        /// </summary>
        /// <remarks> Son todos los planes disponibles del sistema Analyzer</remarks>
        /// <response code="200">Todo Ok</response>
        ///  <response code="400">Error por execption</response>
        ///  <response code="401">No Autorizado falta token</response>
        /// <response code="404">No se encontraron planes</response>
        /// <response code="500">Error de servidor</response>
        /// [SwaggerResponse(200, typeof(CustomModel))]
        /// <returns>Listado de planes</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PlanDTO), 200)]
        public async Task<IActionResult> GetAllAssync()
        {
            try
            {
                var estado = await _planService.GetAllAsync();

                if (estado == null) return NotFound("No se encontró ningun Plan");

                return Ok(_mapper.Map<IList<PlanDTO>>(estado));
            }
            catch (System.Exception e)
            {
               
                return BadRequest(e.Message);
                throw;
            }
         
        }

        /// <summary>
        /// Devuelve el usuario correspondiente al ID
        /// </summary>
        /// <remarks> </remarks>
        /// <response code="200">Todo Ok</response>
        ///  <response code="400">Error por execption</response>
        ///  <response code="401">No Autorizado falta token</response>
        /// <response code="404">No se encontro el usuario</response>
        /// <response code="500">Error de servidor</response>
        /// <returns>un usuario</returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlanDTO), 200)]
        public async Task<IActionResult> GetPLan(int id)
        {
            try
            {
                var plan = await _planService.GetPlanByIdAsync(id);
                if (plan == null)
                {
                    return NotFound("Plan no encontrado");
                }
                var planDTO = Ok(_mapper.Map<PlanDTO>(plan));

                return planDTO;
            }
            catch (Exception e)
            {
                return BadRequest("Error de " + e.Message);
                throw;
            }
        }


        /// <summary>
        /// Alta de Usuarios
        /// </summary>
        /// <remarks> </remarks>
        /// <response code="200">Todo Ok</response>
        ///  <response code="400">Error por execption</response>
        ///  <response code="401">No Autorizado falta token</response>
        /// <response code="500">Error de servidor</response>
        /// <returns>Nuevo Usuario</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PlanDTO), 200)]
        public async Task<IActionResult> PostUsuario(PlanInsertDTO usuario)
        {
            try
            {
                var planToInsert = _mapper.Map<Plan>(usuario);

                var planResult = await _planService.InsertAndSaveAsync(planToInsert);

                if (planResult.ResultMessage != null) return BadRequest(planResult.ResultMessage);

                return Ok(_mapper.Map<PlanDTO>(planResult));
            }
            catch (Exception e)
            {
                return BadRequest("Sucedio la siguiente excepcion : " + e.Message);

                throw;
            }

        }

        /// <summary>
        /// Actualizacion de datos del usuario
        /// </summary>
        /// <remarks> </remarks>
        /// <response code="200">Todo Ok</response>
        ///  <response code="400">Error por execption</response>
        ///  <response code="401">No Autorizado falta token</response>
        /// <response code="500">Error de servidor</response>
        /// <returns>Usuario Modificado</returns>
        [ProducesResponseType(typeof(Plan), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, PlanInsertDTO usuario)
        {
            if (id != usuario.IdPlan)
            {
                return Conflict("El id enviado no se corresponde con el usuario enviado");
            }

            try
            {
                var userToInsert = _mapper.Map<Plan>(usuario);

                var userResult = await _planService.InsertAndSaveAsync(userToInsert);

                if (userResult.ResultMessage != null) return BadRequest(userResult.ResultMessage);

                return Ok(_mapper.Map<UsuarioDTO>(userResult));
            }
            catch (Exception e)
            {
                return BadRequest("Sucedio la siguiente excepcion : " + e.Message);

                throw;
            }
        }

}
