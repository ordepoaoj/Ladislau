using System.Collections.Generic;
using System.Linq;


namespace Web20.Models
{
    public class UniqueRevistum
    {
        private readonly AppDbContext _consulta;

        public UniqueRevistum(AppDbContext consulta)
        {
            _consulta = consulta;
        }
        public bool verificar(string Titulo, string IBICT, string ISSN, string Aleph)
        {

            var Revista = from r in _consulta.Revista
                          select r;
            IList<Revistum> consultas = null;

            Revista = _consulta.Revista.Where(r => r.Titulo == Titulo || r.Ibict == IBICT || r.Issn == ISSN || r.Aleph == Aleph);

            consultas = Revista.ToList();

            if (consultas.Count > 0)
            {
                return true;
            }
            return false;
        }

        public string nome(string Titulo, string IBICT, string ISSN, string Aleph)
        {
            var Revista = _consulta.Revista.Where(r => r.Titulo == Titulo || r.Ibict == IBICT || r.Issn == ISSN || r.Aleph == Aleph).First();
            return Revista.Titulo.ToString();
        }
    }
}
