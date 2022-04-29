using JMusik.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace JMusik.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apelldio del Usuario")]
        public string Apellidos { get; set; }
        [Required]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Cuenta")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public EstatusUsuario Estatus { get; set; }
        [Required]
        [Display(Name = "Perfil Usuario")]
        public int PerfilId { get; set; }
        public string Perfil { get; set; }
    }
}
