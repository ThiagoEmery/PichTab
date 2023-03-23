using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PichTabs_API.Datos;
using PichTabs_API.Modelos;
using PichTabs_API.Modelos.Dto;

namespace PichTabs_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ILogger<EquipoController> _logger;
        private readonly AplicationDbContext _db;
        public EquipoController(ILogger<EquipoController> logger, AplicationDbContext db)
        {

            _logger = logger;
            _db = db;
            
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<EquipoDto>> GetEquipoModels()
        {
            _logger.LogInformation("Obtener equipos");
            return Ok(_db.equipoModels.ToList());

        }
        [HttpGet("id", Name = "GetEquipo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<EquipoDto> GetEquipoModel(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer el equipo con Id " + id);
                return BadRequest();
            }
            // var equipo = EquipoStore.EquipoList.FirstOrDefault(v => v.Id == id);
            var equipo = _db.equipoModels.FirstOrDefault(v => v.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<EquipoDto> CrearEquipo([FromBody] EquipoDto equipoDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (equipoDto == null)
            {
                return BadRequest();
            }
            if (equipoDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            EquipoModel modelo = new()
            {
                
                Nombre = equipoDto.Nombre,
                Sede = equipoDto.Sede,
                Acronimo = equipoDto.Acronimo
            };

            _db.equipoModels.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetEquipo", new {id = equipoDto.Id}, equipoDto);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEquipo(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var equipo = _db.equipoModels.FirstOrDefault(v => v.Id == id);
            if(equipo == null)
            {
                return NotFound();
            }
            _db.equipoModels.Remove(equipo);
            _db.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEquipo(int id, [FromBody] EquipoDto equipoDto)
        {
            if(equipoDto==null || id!=equipoDto.Id)
            {
                return BadRequest();
            }
            //var equipo = EquipoStore.EquipoList.FirstOrDefault(v => v.Id == id);
            //equipo.Nombre = equipoDto.Nombre;
            //equipo.Sede = equipoDto.Sede;
            //equipo.Acronimo = equipoDto.Acronimo;

            EquipoModel modelo = new()
            {
                Id = equipoDto.Id,
                Nombre = equipoDto.Nombre,
                Sede = equipoDto.Sede,
                Acronimo = equipoDto.Acronimo
            };
            _db.equipoModels.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialEquipo(int id, JsonPatchDocument<EquipoDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
           var equipo = _db.equipoModels.AsNoTracking().FirstOrDefault(v => v.Id == id);

            EquipoDto equipoDto = new()
            {
                Id = equipo.Id,
                Nombre = equipo.Nombre,
                Sede = equipo.Sede,
                Acronimo = equipo.Acronimo
            };

            if(equipoDto == null) return BadRequest();

            patchDto.ApplyTo(equipoDto, ModelState);
           
           

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }

            EquipoModel modelo = new()
            {
                Id = equipoDto.Id,
                Nombre = equipoDto.Nombre,
                Sede = equipoDto.Sede,
                Acronimo = equipoDto.Acronimo
            };
            _db.equipoModels.Add(modelo);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
