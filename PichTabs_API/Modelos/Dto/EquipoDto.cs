using System.ComponentModel.DataAnnotations;

namespace PichTabs_API.Modelos.Dto
{
    public class EquipoDto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string Sede { get; set; }
        [Required]
        public string Acronimo { get; set; }

       // public EquipoDto()
       //{
       //     this.Id = 0;
       //     this.Nombre = string.Empty;
       //     this.Sede = string.Empty;
       //     this.Acronimo = string.Empty;

//        }
    }
}
