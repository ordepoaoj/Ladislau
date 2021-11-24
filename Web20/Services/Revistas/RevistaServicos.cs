using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web20.Interfaces;
using Web20.Models;

namespace Web20.Services.Revistas
{
    public class RevistaServicos : IRevistaServicos
    {
        public Revistum CriarRevista(AppDbContext context, Revistum Revista)
        {
            throw new NotImplementedException();
        }



        public Revistum DetalharRevista(AppDbContext context, int? id)
        {
            if (id == null)
            {
                return null;
            }

            var revistum = context.Revista
                .Include(r => r.CdAquisicaoNavigation)
                .Include(r => r.CdEditorNavigation)
                .Include(r => r.CdPeriodicidadeNavigation)
                .FirstOrDefault(m => m.Id == id);

            return revistum;

        }

        public IEnumerable<Atualizacao> ListarAtualizacao(AppDbContext context, int id)
        {
                var atualizacao = context.Atualizacaos
                    .Where(a => a.CdRevista == id)
                        .Include(a => a.CdUsuarioNavigation)
                            .OrderByDescending(a => a.DtAtualizacao)
                                .OrderByDescending(a => a.DtChegada);

                return atualizacao;
        }

        public IEnumerable<Revistum> ListarRevista(AppDbContext context, string search)
        {
            var appDbContext = new List<Revistum>();

            var Revista = from r in context.Revista
                          select r;
            if (!String.IsNullOrEmpty(search))
            {
                Revista =
                    Revista.Where(r => r.Titulo.Contains(search) || r.Aleph.Equals(search) || r.Issn.Contains(search)).Where(r => r.Ativo.Equals(true)).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo);
                return Revista;
            }
            return appDbContext;
        }
    }
}
