using System;
using System.Collections.Generic;

#nullable disable

namespace Web20.Models
{
    public partial class RevistaMuseu
    {
        public RevistaMuseu()
        {
            MuseuEditors = new HashSet<MuseuEditor>();
        }

        public int Id { get; set; }
        public int? Aleph { get; set; }
        public string Ibict { get; set; }
        public string Issn { get; set; }
        public string Titulo { get; set; }
        public int? CdPeriodicidade { get; set; }

        public virtual ICollection<MuseuEditor> MuseuEditors { get; set; }
    }
}
