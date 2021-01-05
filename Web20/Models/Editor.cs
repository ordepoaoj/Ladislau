using System;
using System.Collections.Generic;

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
        public string NomeEditor { get; set; }
        public string Endereco { get; set; }
        public int? CodPais { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public virtual PaisEditor CodPaisNavigation { get; set; }
        public virtual ICollection<MuseuEditor> MuseuEditors { get; set; }
        public virtual ICollection<PreferenciaEditor> PreferenciaEditors { get; set; }
        public virtual ICollection<Revistum> Revista { get; set; }
    }
}
