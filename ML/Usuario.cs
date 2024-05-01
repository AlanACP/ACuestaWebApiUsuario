using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Apellido paterno")]
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public int? Edad { get; set; } = null;

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Email { get; set; }

        public List<ML.Usuario> Usuarios { get; set; }
    }
}
