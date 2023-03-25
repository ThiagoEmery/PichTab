using PichTabs_API.Datos;
using PichTabs_API.Modelos;
using PichTabs_API.Repositorio.IRepositorio;

namespace PichTabs_API.Repositorio
{
    public class EquipoRepositorio : Repositorio<EquipoModel>, IEquipoRepositorio
    {
        private readonly AplicationDbContext _db;
        public EquipoRepositorio(AplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public async Task<EquipoModel> Actualizar(EquipoModel entidad)
        {
            _db.equipoModels.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}

