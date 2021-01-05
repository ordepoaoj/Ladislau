using System;
using System.Collections.Generic;

#nullable disable

namespace Web20.Models
{
    public partial class Revistum
    {
        public int Id { get; set; }
        public string Aleph { get; set; }
        public string Titulo { get; set; }
        public string Ibict { get; set; }
        public string Issn { get; set; }
        public int? Ativo { get; set; }
        public DateTime? Chegada { get; set; }
        public int? CdAquisicao { get; set; }
        public int? CdEditor { get; set; }
        public int? CdPeriodicidade { get; set; }

        public virtual Aquisicao CdAquisicaoNavigation { get; set; }
        public virtual Editor CdEditorNavigation { get; set; }
        public virtual Periodicidade CdPeriodicidadeNavigation { get; set; }
    }
}
