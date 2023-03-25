using PichTabs_API.Modelos;

namespace PichTabs_API.Repositorio.IRepositorio
{
    public interface IEquipoRepositorio : IRepositorio<EquipoModel>
    {
        Task<EquipoModel> Actualizar(EquipoModel entidad);
    }
}
