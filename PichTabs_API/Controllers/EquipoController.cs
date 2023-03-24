using AutoMapper;
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
        private readonly IMapper _mapper;
        public EquipoController(ILogger<EquipoController> logger, AplicationDbContext db, IMapper mapper)
        {

            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task <ActionResult<IEnumerable<EquipoDto>>> GetEquipoModels()
        {
            _logger.LogInformation("Obtener equipos");

            IEnumerable<EquipoModel> equipoList = await _db.equipoModels.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<EquipoDto>>(equipoList));

        }
        [HttpGet("id", Name = "GetEquipo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task <ActionResult<EquipoDto>> GetEquipoModel(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer el equipo con Id " + id);
                return BadRequest();
            }
            // var equipo = EquipoStore.EquipoList.FirstOrDefault(v => v.Id == id);
            var equipo = await _db.equipoModels.FirstOrDefaultAsync(v => v.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EquipoDto>(equipo));
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<EquipoDto>> CrearEquipo([FromBody] EquipoCreateDto createDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
           
            EquipoModel modelo = _mapper.Map<EquipoModel>(createDto);


            await _db.equipoModels.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetEquipo", new {id = modelo.Id}, modelo);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEquipo(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var equipo =await _db.equipoModels.FirstOrDefaultAsync(v => v.Id == id);
            if(equipo == null)
            {
                return NotFound();
            }
            _db.equipoModels.Remove(equipo);
           await _db.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEquipo(int id, [FromBody] EquipoUpdateDto updateDto)
        {
            if(updateDto==null || id!=updateDto.Id)
            {
                return BadRequest();
            }
            

            EquipoModel modelo = _mapper.Map<EquipoModel>(updateDto);

            
            _db.equipoModels.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialEquipo(int id, JsonPatchDocument<EquipoUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
           var equipo =await _db.equipoModels.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            EquipoUpdateDto equipoDto = _mapper.Map<EquipoUpdateDto>(equipo);

            

            if(equipoDto == null) return BadRequest();

            patchDto.ApplyTo(equipoDto, ModelState);
           
           

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }

            EquipoModel modelo = _mapper.Map<EquipoModel>(equipoDto);
            
            _db.equipoModels.Add(modelo);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
