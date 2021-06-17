using System.Collections.Generic;
using System.Linq;

namespace Web20.Models
{
    public class UniqueEditor
    {
        private readonly AppDbContext _consulta;

        public UniqueEditor(AppDbContext consulta)
        {
            _consulta = consulta;
        }

        public bool verificar (string Titulo)
        {
            var Editor = from e in _consulta.Editors
                         select e;
            IList<Editor> lista = null;
            Editor = _consulta.Editors.Where(e => e.NomeEditor == Titulo);
            lista = Editor.ToList();

            if(lista.Count() > 0)
            {
                return false;
            }          
            return true;
        }
    }
}
