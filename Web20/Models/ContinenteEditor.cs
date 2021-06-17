using System.Collections.Generic;

#nullable disable

namespace Web20.Models
{
    public partial class ContinenteEditor
    {
        public ContinenteEditor()
        {
            PaisEditors = new HashSet<PaisEditor>();
        }

        public int Id { get; set; }
        public string NomeContinente { get; set; }

        public virtual ICollection<PaisEditor> PaisEditors { get; set; }
    }
}
