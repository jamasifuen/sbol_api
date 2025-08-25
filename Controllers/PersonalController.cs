using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAPI.Models;
using PersonalAPI.Services;

namespace PersonalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere autenticación JWT para todos los endpoints
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalService _personalService;
        private readonly ILogger<PersonalController> _logger;

        public PersonalController(IPersonalService personalService, ILogger<PersonalController> logger)
        {
            _personalService = personalService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todo el personal con tipo de trabajo E (Empleado) u O (Obrero)
        /// </summary>
        /// <returns>Lista de personal filtrada por tipo de trabajo</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personal>>> GetAll()
        {
            try
            {
                var personal = await _personalService.GetAllAsync();
                return Ok(personal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todo el personal");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene un empleado por su código
        /// </summary>
        /// <param name="codigo">Código del empleado</param>
        /// <returns>Datos del empleado</returns>
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Personal>> GetByCodigo(string codigo)
        {
            try
            {
                var personal = await _personalService.GetByCodigoAsync(codigo);
                if (personal == null)
                {
                    return NotFound($"No se encontró personal con código: {codigo}");
                }
                return Ok(personal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener personal por código: {Codigo}", codigo);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // /// <summary>
        // /// Crea un nuevo empleado
        // /// </summary>
        // /// <param name="personal">Datos del nuevo empleado</param>
        // /// <returns>Empleado creado</returns>
        // [HttpPost]
        // public async Task<ActionResult<Personal>> Create([FromBody] Personal personal)
        // {
        //     try
        //     {
        //         if (!ModelState.IsValid)
        //         {
        //             return BadRequest(ModelState);
        //         }

        //         var nuevoPersonal = await _personalService.CreateAsync(personal);
        //         return CreatedAtAction(nameof(GetByCodigo), new { codigo = nuevoPersonal.Codigo }, nuevoPersonal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al crear personal");
        //         return StatusCode(500, "Error interno del servidor");
        //     }
        // }

        // /// <summary>
        // /// Actualiza un empleado existente
        // /// </summary>
        // /// <param name="codigo">Código del empleado</param>
        // /// <param name="personal">Datos actualizados del empleado</param>
        // /// <returns>Empleado actualizado</returns>
        // [HttpPut("{codigo}")]
        // public async Task<ActionResult<Personal>> Update(string codigo, [FromBody] Personal personal)
        // {
        //     try
        //     {
        //         if (!ModelState.IsValid)
        //         {
        //             return BadRequest(ModelState);
        //         }

        //         var personalActualizado = await _personalService.UpdateAsync(codigo, personal);
        //         if (personalActualizado == null)
        //         {
        //             return NotFound($"No se encontró personal con código: {codigo}");
        //         }

        //         return Ok(personalActualizado);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al actualizar personal con código: {Codigo}", codigo);
        //         return StatusCode(500, "Error interno del servidor");
        //     }
        // }

        // /// <summary>
        // /// Elimina un empleado
        // /// </summary>
        // /// <param name="codigo">Código del empleado</param>
        // /// <returns>Resultado de la operación</returns>
        // [HttpDelete("{codigo}")]
        // public async Task<ActionResult> Delete(string codigo)
        // {
        //     try
        //     {
        //         var eliminado = await _personalService.DeleteAsync(codigo);
        //         if (!eliminado)
        //         {
        //             return NotFound($"No se encontró personal con código: {codigo}");
        //         }

        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al eliminar personal con código: {Codigo}", codigo);
        //         return StatusCode(500, "Error interno del servidor");
        //     }
        // }

        // /// <summary>
        // /// Obtiene el personal por departamento
        // /// </summary>
        // /// <param name="departamento">Nombre del departamento</param>
        // /// <returns>Lista de personal del departamento</returns>
        // [HttpGet("departamento/{departamento}")]
        // public async Task<ActionResult<IEnumerable<Personal>>> GetByDepartamento(string departamento)
        // {
        //     try
        //     {
        //         var personal = await _personalService.GetByDepartamentoAsync(departamento);
        //         return Ok(personal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por departamento: {Departamento}", departamento);
        //         return StatusCode(500, "Error interno del servidor");
        //     }
        // }

        /// <summary>
        /// Obtiene el personal por DNI
        /// </summary>
        /// <param name="dni">DNI del empleado</param>
        /// <returns>Lista de personal con ese DNI</returns>
        [HttpGet("dni/{dni}")]
        public async Task<ActionResult<IEnumerable<Personal>>> GetByDNI(string dni)
        {
            try
            {
                var personal = await _personalService.GetByDNIAsync(dni);
                return Ok(personal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener personal por DNI: {DNI}", dni);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // /// <summary>
        // /// Busca personal por nombre
        // /// </summary>
        // /// <param name="nombre">Nombre a buscar</param>
        // /// <returns>Lista de personal que coincide con el nombre</returns>
        // [HttpGet("nombre/{nombre}")]
        // public async Task<ActionResult<IEnumerable<Personal>>> GetByNombre(string nombre)
        // {
        //     try
        //     {
        //         var personal = await _personalService.GetByNombreAsync(nombre);
        //         return Ok(personal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por nombre: {Nombre}", nombre);
        //         return StatusCode(500, "Error interno del servidor");
        //     }
        // }

        // /// <summary>
        // /// Obtiene el personal por centro de costo
        // /// </summary>
        // /// <param name="cenCosto">Centro de costo</param>
        // /// <returns>Lista de personal del centro de costo</returns>
        // [HttpGet("centro-costo/{cenCosto}")]
        // public async Task<ActionResult<IEnumerable<Personal>>> GetByCenCosto(string cenCosto)
        // {
        //     try
        //     {
        //         var personal = await _personalService.GetByCenCostoAsync(cenCosto);
        //         return Ok(personal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por centro de costo: {CenCosto}", cenCosto);
        //         return StatusCode(500, "Error interno del servidor");
        //     }
        // }

        // /// <summary>
        // /// Obtiene el personal por tipo de trabajo
        // /// </summary>
        // /// <param name="tipTrab">Tipo de trabajo</param>
        // /// <returns>Lista de personal del tipo de trabajo</returns>
        // [HttpGet("tipo-trabajo/{tipTrab}")]
        // public async Task<ActionResult<IEnumerable<Personal>>> GetByTipoTrabajo(string tipTrab)
        // {
        //     try
        //     {
        //         var personal = await _personalService.GetByTipoTrabajoAsync(tipTrab);
        //         return Ok(personal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por tipo de trabajo: {TipTrab}", tipTrab);
        //         return StatusCode(500, "Error interno del servidor");
        //     }
        // }
    }
}
