#nullable disable

namespace Web20.Models
{
    public partial class PreferenciaEditor
    {
        public int Id { get; set; }
        public int CodPublicacao { get; set; }
        public int CodEditor { get; set; }

        public virtual Editor CodEditorNavigation { get; set; }
    }
}
