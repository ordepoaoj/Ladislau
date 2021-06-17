#nullable disable

namespace Web20.Models
{
    public partial class RevMuseu
    {
        public int Id { get; set; }
        public int? Aleph { get; set; }
        public string Ibict { get; set; }
        public string Issn { get; set; }
        public string Titulo { get; set; }
        public int? CdPeriodicidade { get; set; }
    }
}
