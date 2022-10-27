using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialAnalyzer.DAL.DataStores;
using SocialAnalyzer.DAL.Models;
using SocialAnalyzer.Models;
using SocialAnalyzer.Services.Interfaces;
using SocialAnalyzer.Services.Models;
using SocialAnalyzer.Services.Services;
using Usuario = SocialAnalyzer.Services.Models.Usuario;

namespace SocialAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IMapper mapper, IUsuarioService usuarioService)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Devuelve todos los usuarios activos
        /// </summary>
        /// <remarks> Son todos los usuarios activos del sistema Analyzer</remarks>
        /// <response code="200">Todo Ok</response>
        ///  <response code="400">Error por execption</response>
        ///  <response code="401">No Autorizado falta token</response>
        /// <response code="404">No se encontraron usuarios</response>
        /// <response code="500">Error de servidor</response>
        /// <returns>Listado de usuarios</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();

                if (usuarios == null) return NotFound("No se encontró ningun Plan");

                return Ok(_mapper.Map<IList<UsuarioDTO>>(usuarios));
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
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }
                var userDTO = Ok(_mapper.Map<UsuarioDTO>(usuario));

                return userDTO;
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
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
        public async Task<IActionResult> PostUsuario(UsuarioNewDTO usuario)
        {
            try
            {
                var userToInsert = _mapper.Map<Usuario>(usuario);

                var userResult = await _usuarioService.SaveUsuarioAsync(userToInsert);

                if (userResult.ResultMessage != null) return BadRequest(userResult.ResultMessage);

                return Ok(_mapper.Map<UsuarioDTO>(userResult));
            }
            catch (Exception e)
            {
                return BadRequest("Sucedio la siguiente excepcion : "+e.Message);

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
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioNewDTO usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return Conflict("El id enviado no se corresponde con el usuario enviado");
            }

            try
            {
                var userToInsert = _mapper.Map<Usuario>(usuario);

                var userResult = await _usuarioService.SaveUsuarioAsync(userToInsert);

                if (userResult.ResultMessage != null) return BadRequest(userResult.ResultMessage);

                return Ok(_mapper.Map<UsuarioDTO>(userResult));
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
        [ProducesResponseType(typeof(bool), 200)]
        [HttpPut("resetPassword")]
        public async Task<IActionResult> PutUsuarioChangePassword(int id, string oldPass, string newPass)
        {
            if (oldPass == newPass)
            {
                return Conflict("El password nuevo no puede ser igual al anterios");
            }

            try
            {

                var result = await _usuarioService.ChangePasswordAsync(id, oldPass, newPass);

                if (result == false) return BadRequest("No se pudo cambiar la contraseña");

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Sucedio la siguiente excepcion : " + e.Message);

                throw;
            }
        }



        /// <summary>
        /// Baja de usuario
        /// </summary>
        /// <remarks> </remarks>
        /// <response code="200">Todo Ok</response>
        ///  <response code="400">Error por execption</response>
        ///  <response code="401">No Autorizado falta token</response>
        /// <response code="500">Error de servidor</response>
        /// <returns>Usuario Modificado</returns>
        [ProducesResponseType(typeof(bool), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var userResult = await _usuarioService.BajaUsuarioAsync(id);

                return Ok(userResult);
            }
            catch (Exception e)
            {
                return BadRequest("Sucedio la siguiente excepcion : " + e.Message);

                throw;
            }
        }
    }
}
