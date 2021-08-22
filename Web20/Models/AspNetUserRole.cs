using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Web20.Models
{
    public partial class AspNetUserRole
    {
        [Display(Name = "Usuário")]
        public string UserId { get; set; }
        [Display(Name = "Perfil do usuário")]
        public string RoleId { get; set; }

        public virtual AspNetRole Role { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
