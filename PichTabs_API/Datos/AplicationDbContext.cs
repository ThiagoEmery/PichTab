using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PichTabs_API.Controllers;
using PichTabs_API.Modelos;

namespace PichTabs_API.Datos
{
    public class AplicationDbContext :DbContext
    {
        private readonly ILogger<EquipoController> _logger;
        private readonly AplicationDbContext _db;
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) :base(options)
        {
     
        }
        public DbSet<EquipoModel> equipoModels {  get; set; }

        

    }
}
