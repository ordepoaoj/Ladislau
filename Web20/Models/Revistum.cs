using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Web20.Models
{
    public partial class Revistum
    {
        public int Id { get; set; }
        public string Aleph { get; set; }

        [Display(Name = "Revista")]
        public string Titulo { get; set; }
        public string Ibict { get; set; }
        public string Issn { get; set; }
        public bool Ativo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Chegada { get; set; }
        [Display (Name ="Aquisição")]
        public int? CdAquisicao { get; set; }
        [Display(Name = "Editor")]
        public int? CdEditor { get; set; }
        [Display(Name = "Periodicidade")]
        public int? CdPeriodicidade { get; set; }

        [Display(Name = "Aquisição")]
        public virtual Aquisicao CdAquisicaoNavigation { get; set; }
        [Display(Name = "Editor")]
        public virtual Editor CdEditorNavigation { get; set; }
        [Display(Name = "Periodicidade")]
        public virtual Periodicidade CdPeriodicidadeNavigation { get; set; }
    }
}
