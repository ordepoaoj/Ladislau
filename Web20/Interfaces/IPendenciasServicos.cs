using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web20.Models;

namespace Web20.Interfaces
{
    public interface IPendenciasServicos
    {
        IEnumerable<Revistum> PendenciasMensais(AppDbContext context);
        IEnumerable<Revistum> PendenciasBimestrais(AppDbContext context);
        IEnumerable<Revistum> PendenciasTrimestrais(AppDbContext context);
        IEnumerable<Revistum> PendenciasQuadrimestrais(AppDbContext context);
        IEnumerable<Revistum> PendenciasSemestrais(AppDbContext context);
        IEnumerable<Revistum> PendenciasAnuais(AppDbContext context);

    }
}
