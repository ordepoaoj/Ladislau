#nullable disable

namespace Web20.Models
{
    public partial class MuseuEditor
    {
        public int Id { get; set; }
        public int IdEditor { get; set; }
        public int IdMuseu { get; set; }

        public virtual Editor IdEditorNavigation { get; set; }
        public virtual RevistaMuseu IdMuseuNavigation { get; set; }
    }
}
