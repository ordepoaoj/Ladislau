using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Web20.Models
{
    public partial class Revistum
    {
        public Revistum()
        {
            Atualizacaos = new HashSet<Atualizacao>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "O Campo é obrigatório")]
        public string Aleph { get; set; }

        [Required(ErrorMessage = "O Campo é obrigatório")]
        [Display(Name = "Revista")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O Campo é obrigatório")]
        public string Ibict { get; set; }
        [Required(ErrorMessage = "O Campo é obrigatório")]
        public string Issn { get; set; }
        public bool Ativo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O Campo é obrigatório")]
        public DateTime Chegada { get; set; }
        [Display(Name = "Aquisição")]
        [Required(ErrorMessage = "A aquisição é necessária")]
        public int? CdAquisicao { get; set; }
        [Display(Name = "Editor")]
        [Required(ErrorMessage = "O Editor é necessário")]
        public int? CdEditor { get; set; }
        [Display(Name = "Periodicidade")]
        [Required(ErrorMessage = "A periodicidade é necessária")]
        public int? CdPeriodicidade { get; set; }

        [Display(Name = "Aquisição")]
        public virtual Aquisicao CdAquisicaoNavigation { get; set; }
        [Display(Name = "Editor")]
        public virtual Editor CdEditorNavigation { get; set; }
        [Display(Name = "Periodicidade")]
        public virtual Periodicidade CdPeriodicidadeNavigation { get; set; }
        public virtual ICollection<Atualizacao> Atualizacaos { get; set; }
    }
}
