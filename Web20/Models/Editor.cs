using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Web20.Models
{
    public partial class Editor
    {
        public Editor()
        {
            MuseuEditors = new HashSet<MuseuEditor>();
            PreferenciaEditors = new HashSet<PreferenciaEditor>();
            Revista = new HashSet<Revistum>();
        }

        public int Id { get; set; }
        [Required]
        [Display (Name ="Editor")]
        public string NomeEditor { get; set; }
        [Required]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }
        [Display(Name = "País do Editor")]
        public int? CodPais { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        [Display(Name = "País do Editor")]
        public virtual PaisEditor CodPaisNavigation { get; set; }
        public virtual ICollection<MuseuEditor> MuseuEditors { get; set; }
        public virtual ICollection<PreferenciaEditor> PreferenciaEditors { get; set; }
        public virtual ICollection<Revistum> Revista { get; set; }
    }
}
