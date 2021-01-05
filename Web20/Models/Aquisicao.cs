using System;
using System.Collections.Generic;

#nullable disable

namespace Web20.Models
{
    public partial class Aquisicao
    {
        public Aquisicao()
        {
            Revista = new HashSet<Revistum>();
        }

        public int Id { get; set; }
        public string TipoAquisicao { get; set; }

        public virtual ICollection<Revistum> Revista { get; set; }
    }
}
