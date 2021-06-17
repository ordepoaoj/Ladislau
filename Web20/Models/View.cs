using System;


#nullable disable

namespace Web20.Models
{
    public partial class View
    {
        public string Titulo { get; set; }
        public string Issn { get; set; }
        public string TipoPeriodicidade { get; set; }
        public string TipoAquisicao { get; set; }
        public DateTime? Chegada { get; set; }
        public string Aleph { get; set; }
        public string Ibict { get; set; }
        public string NomeEditor { get; set; }
    }
}
