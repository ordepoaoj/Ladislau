using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web20.Models;

namespace Web20.Interfaces
{
    public interface IRevistaServicos
    {
        IEnumerable<Revistum> ListarRevista(AppDbContext context, string search);
        Revistum DetalharRevista(AppDbContext context, int? id);

        IEnumerable<Atualizacao> ListarAtualizacao(AppDbContext context, int id);
        Revistum CriarRevista(AppDbContext context, Revistum Revista);
    }
}
