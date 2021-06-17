using System.Collections.Generic;
using System.Linq;

namespace Web20.Models
{
    public class paisRevista
    {
        private readonly AppDbContext _context;

        public paisRevista(AppDbContext consulta)
        {
            _context = consulta;
        }
        public string gerarMapa()
        {
            int nPaises = 216;
            string[,] valor = new string[2, nPaises];
            string[] pais = new string[nPaises];
            var tpais = new object[nPaises];
            int total = 1;

            List<PaisEditor> listPaises = _context.PaisEditors.ToList();

            for (total = 1; total < nPaises; total++)
            {
                valor[0, total] = _context.Revista.Where(r => r.CdEditorNavigation.CodPais == total).Count().ToString();
                valor[1, total] = _context.PaisEditors.Where(r => r.Id == total).Select(r => r.NomePais).FirstOrDefault().ToString();
                pais[total] = "['" + valor[1, total] + "', " + valor[0, total] + "],";
            }

            return pais.ToString();
        }
    }
}
