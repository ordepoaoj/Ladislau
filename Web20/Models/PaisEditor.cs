using System;
using System.Collections.Generic;

#nullable disable

namespace Web20.Models
{
    public partial class PaisEditor
    {
        public PaisEditor()
        {
            Editors = new HashSet<Editor>();
        }

        public int Id { get; set; }
        public int? CodContinente { get; set; }
        public string NomePais { get; set; }

        public virtual ContinenteEditor CodContinenteNavigation { get; set; }
        public virtual ICollection<Editor> Editors { get; set; }

        public static implicit operator PaisEditor(List<PaisEditor> v)
        {
            throw new NotImplementedException();
        }
    }
}
