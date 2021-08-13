using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Web20.Models
{
    public partial class Atualizacao
    {
        public int Id { get; set; }
        public int CdRevista { get; set; }
        public string CdUsuario { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtAtualizacao { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtChegada { get; set; }

        public virtual Revistum CdRevistaNavigation { get; set; }
        public virtual AspNetUser CdUsuarioNavigation { get; set; }
    }
}
