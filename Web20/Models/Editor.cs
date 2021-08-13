using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web20.Entities;


#nullable disable

namespace Web20.Models
{
    public partial class Editor
    {
        private readonly Field field;
        public Editor()
        {
            MuseuEditors = new HashSet<MuseuEditor>();
            PreferenciaEditors = new HashSet<PreferenciaEditor>();
            Revista = new HashSet<Revistum>();
        }

        

        public int Id { get; set; }
        [Display(Name = "Nome do Editor")]
        public string NomeEditor { get; set; }
        public string Endereco { get; set; }
        [Display(Name = "Pais")]
        public int CodPais { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        public virtual PaisEditor CodPaisNavigation { get; set; }
        public virtual ICollection<MuseuEditor> MuseuEditors { get; set; }
        public virtual ICollection<PreferenciaEditor> PreferenciaEditors { get; set; }
        public virtual ICollection<Revistum> Revista { get; set; }
    }
}
