using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web20.Enumeradores;
using Web20.Interfaces;
using Web20.Models;

namespace Web20.Services.Pendencias
{
    public class PendenciasServicos : IPendenciasServicos
    {
        
        DbFunctions db = null;
        DateTime hoje = DateTime.Today;

        public IEnumerable<Revistum> PendenciasAnuais(AppDbContext context)
        {
            IEnumerable<Revistum> Anuais = context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= (int)PrazoEnum.Anual && r.CdPeriodicidade == (int)PeriodoEnum.Anual)
                 .Include(r => r.CdEditorNavigation)
                     .Include(r => r.CdPeriodicidadeNavigation)
                         .Include(r => r.CdAquisicaoNavigation)
                             .OrderBy(r => r.Titulo);

            return Anuais;
        }

        public IEnumerable<Revistum> PendenciasBimestrais(AppDbContext context)
        {
            IEnumerable<Revistum> bimestre = context.Revista
                .Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= (int)PrazoEnum.Bimestral && r.CdPeriodicidade == (int)PeriodoEnum.Bimestral)
                    .Include(r => r.CdEditorNavigation)
                        .Include(r => r.CdPeriodicidadeNavigation)
                            .Include(r => r.CdAquisicaoNavigation)
                                .OrderBy(r => r.Titulo);

            return bimestre;
        }

        public IEnumerable<Revistum> PendenciasMensais(AppDbContext context)
        {
            IEnumerable<Revistum> Mensal = context.Revista
                .Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= (int)PrazoEnum.Mensal && r.CdPeriodicidade == (int)PeriodoEnum.Mensal)
                    .Include(r => r.CdAquisicaoNavigation)
                        .Include(r => r.CdEditorNavigation)
                            .Include(r => r.CdPeriodicidadeNavigation)
                                .OrderBy(r => r.CdPeriodicidade).OrderBy(r => r.Titulo);

            return Mensal;
        }

        public IEnumerable<Revistum> PendenciasQuadrimestrais(AppDbContext context)
        {
            IEnumerable<Revistum> Quadrimestrais = context.Revista
                .Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= (int)PrazoEnum.Quadrimestral && r.CdPeriodicidade == (int)PeriodoEnum.Quadrimestral)
                    .Include(r => r.CdEditorNavigation)
                        .Include(r => r.CdPeriodicidadeNavigation)
                            .Include(r => r.CdAquisicaoNavigation)
                                .OrderBy(r => r.Titulo);

            return Quadrimestrais;
        }

        public IEnumerable<Revistum> PendenciasSemestrais(AppDbContext context)
        {
            IEnumerable<Revistum> Semestral = context.Revista
                .Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= (int)PrazoEnum.Semestral && r.CdPeriodicidade == (int)PeriodoEnum.Semestral)
                    .Include(r => r.CdEditorNavigation)
                        .Include(r => r.CdPeriodicidadeNavigation)
                            .Include(r => r.CdAquisicaoNavigation)
                                .OrderBy(r => r.Titulo);

            return Semestral;
        }

        public IEnumerable<Revistum> PendenciasTrimestrais(AppDbContext context)
        {
            IEnumerable<Revistum> Trimestral = context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= (int)PrazoEnum.Trimestral && r.CdPeriodicidade == (int)PeriodoEnum.Trimestral)
                .Include(r => r.CdEditorNavigation)
                    .Include(r => r.CdPeriodicidadeNavigation)
                        .Include(r => r.CdAquisicaoNavigation)
                            .OrderBy(r => r.Titulo);

            return Trimestral;
        }
    }
}