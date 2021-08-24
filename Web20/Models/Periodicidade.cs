using System.Collections.Generic;

#nullable disable

namespace Web20.Models
{
    public partial class Periodicidade
    {
        public Periodicidade()
        {
            Revista = new HashSet<Revistum>();
        }

        public int Id { get; set; }
        public string TipoPeriodicidade { get; set; }

        public virtual ICollection<Revistum> Revista { get; set; }
    }
}
