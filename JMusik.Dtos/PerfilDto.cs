using System.ComponentModel.DataAnnotations;

namespace JMusik.Dtos
{
    public class PerfilDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre del Perfil es requerido")]
        [Display(Name ="Perfil")]
        public string Nombre { get; set; }
    }
}
