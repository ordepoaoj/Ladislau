using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Linq;
using Web20.Models;

namespace Web20.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {
        private readonly AppDbContext _context;

        public RelatoriosController (AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            #region Total

            ViewData["nRevista"] = _context.Revista.Count();
            ViewData["nEditor"] = _context.Editors.Count();

            #region Total-Brasil
            int nBrasil = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 1).Count();
            ViewData["eBrasil"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 1).Count();
            ViewData["rBrasileiras"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 1).Count();
            #endregion Total-Brasil

            #region Total-America
            int nAmerica = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 2).Count();
            ViewData["eAmerica"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 2).Count();
            ViewData["rAmerica"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 2).Count();
            ViewData["tAmerica"] = nBrasil + nAmerica;

            #endregion Total-America

            #region Total-Asia
            ViewData["eAsia"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 3).Count();
            ViewData["rAsia"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 3).Count();
            #endregion

            #region Total-Europa
            ViewData["eEuropa"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 4).Count();
            ViewData["rEuropa"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 4).Count();
            #endregion Total-Europa

            #region Total-Africa
            ViewData["eAfrica"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 5).Count();
            ViewData["rAfrica"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 5).Count();
            #endregion Total-Africa

            #region Total-Oceania
            ViewData["eOceania"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 6).Count();
            ViewData["rOceania"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 6).Count();
            #endregion Total-Oceania

            #endregion Total

            #region Revista-Pais
            int nPaises = 216;
            string[,] valor = new string[2,nPaises];
            string[] pais = new string[nPaises];
            var tpais = new object[nPaises];
            
            for (int total = 1; total < nPaises; total++)
            {
                valor[0,total] = _context.Revista.Where(r => r.CdEditorNavigation.CodPais == total).Count().ToString();
                valor[1, total] = _context.PaisEditors.Where(r => r.Id == total).Select(r => r.NomePais).FirstOrDefault().ToString();
                pais[total] = "['" + valor[1, total] + "', " + valor[0, total] + "],";

            }

            string dados = "";
            for (int c =0; c < pais.Length; c++)
            {
                dados += pais[c];
            }

            dados = dados.Substring(0, dados.Length - 1);
            ViewBag.geoGrafico = Mapa.gerarMapa(dados);
            #endregion

            #region Pendencias

            #region variaveis-Auxiliares
            DbFunctions db = null;
            DateTime hoje = DateTime.Today;
            int mensal = 1;
            int anual = 2;
            int trimestral = 3;
            int bimestral = 4;
            int quadrimestral = 5;
            int semestral = 8;
            #endregion variaveis-Auxiliares

            #region contadores-Pendencias
            int pendenciaMensal = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 60 && r.CdPeriodicidade == mensal).Count();
            int pendenciaBimestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 90 && r.CdPeriodicidade == bimestral).Count();
            int pendenciaTrimestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 120 && r.CdPeriodicidade == trimestral).Count();
            int pendenciaQuadrimestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 150 && r.CdPeriodicidade == quadrimestral).Count();
            int pendenciaSemestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 210 && r.CdPeriodicidade == semestral).Count();
            int pendenciaAnual = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 395 && r.CdPeriodicidade == anual).Count();
            int pendenciaTotal = pendenciaMensal + pendenciaBimestral + pendenciaTrimestral + pendenciaQuadrimestral + pendenciaSemestral + pendenciaAnual;
            #endregion contadores-Pendencias

            #region ViewData-Pendencias
            ViewData["pTotal"] = pendenciaTotal.ToString();
            ViewData["pMensal"] = pendenciaMensal.ToString();
            ViewData["pBimestral"] = pendenciaBimestral.ToString();
            ViewData["pTrimestral"] = pendenciaTrimestral.ToString();
            ViewData["pQuadrimestral"] = pendenciaQuadrimestral.ToString();
            ViewData["pSemestral"] = pendenciaSemestral.ToString();
            ViewData["pAnual"] = pendenciaAnual.ToString();
            #endregion ViewData-Pendencias


            #endregion Pendencias

            #region Recebimento-Tempo

            #region contador-Revista
            int revistasAno =_context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) <= 365).Count();
            int revistas1Ano = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) > 365 && SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) <= 730).Count();
            int revistas2Anos = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) > 730 && SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) <= 1095).Count();
            int revistasMais3anos = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) > 1095).Count();
            #endregion contador-Revista

            #region ViewData-Tempo
            ViewData["rAno"] = revistasAno.ToString();
            ViewData["r1Ano"] = revistas1Ano.ToString();
            ViewData["r2Ano"] = revistas2Anos.ToString();
            ViewData["r3Ano"] = revistasMais3anos.ToString();
            #endregion ViewData-Tempo

            #endregion Recebimento-Tempo

            #region Caracteristicas-Revistas

            #region Revista-Antiga
            var ultimaRevista = _context.Revista.OrderBy(r => r.Chegada).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).Include(r => r.CdAquisicaoNavigation).First();
            ViewData["rAntiga"] = ultimaRevista.Titulo.ToString();
            ViewData["drAntiga"] = ultimaRevista.Chegada;
            ViewData["erAntiga"] = ultimaRevista.CdEditorNavigation.NomeEditor.ToString();
            ViewData["prAntiga"] = ultimaRevista.CdPeriodicidadeNavigation.TipoPeriodicidade;
            ViewData["aqAntiga"] = ultimaRevista.CdAquisicaoNavigation.TipoAquisicao;
            #endregion

            #region Revista-Nova
            var primeiraRevista = _context.Revista.OrderByDescending(r => r.Chegada).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).Include(r => r.CdAquisicaoNavigation).First();
            ViewData["rNova"] = primeiraRevista.Titulo.ToString();
            ViewData["drNova"] = primeiraRevista.Chegada;
            ViewData["erNova"] = primeiraRevista.CdEditorNavigation.NomeEditor.ToString();
            ViewData["prNova"] = primeiraRevista.CdPeriodicidadeNavigation.TipoPeriodicidade;
            ViewData["aqNova"] = primeiraRevista.CdAquisicaoNavigation.TipoAquisicao;
            #endregion

            #region Aquisicao-Revista
            var compraRevista = _context.Revista.Where(r => r.CdAquisicaoNavigation.TipoAquisicao == "Compra").Include(r => r.CdAquisicaoNavigation).Count();
            var doacaoRevista = _context.Revista.Where(r => r.CdAquisicaoNavigation.TipoAquisicao == "Doação").Include(r => r.CdAquisicaoNavigation).Count();
            var permutaRevista = _context.Revista.Where(r => r.CdAquisicaoNavigation.TipoAquisicao == "Permuta").Include(r => r.CdAquisicaoNavigation).Count();

            ViewData["aqCompra"] = compraRevista.ToString();
            ViewData["aqDoacao"] = doacaoRevista.ToString();
            ViewData["aqPermuta"] = permutaRevista.ToString();
            #endregion


            #endregion

            return View(pais);
        }

        public IActionResult PDF ()
        {
            #region Total

            ViewData["nRevista"] = _context.Revista.Count();
            ViewData["nEditor"] = _context.Editors.Count();

            #region Total-Brasil
            int nBrasil = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 1).Count();
            ViewData["eBrasil"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 1).Count();
            ViewData["rBrasileiras"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 1).Count();
            #endregion Total-Brasil

            #region Total-America
            int nAmerica = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 2).Count();
            ViewData["eAmerica"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 2).Count();
            ViewData["rAmerica"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 2).Count();
            ViewData["tAmerica"] = nBrasil + nAmerica;

            #endregion Total-America

            #region Total-Asia
            ViewData["eAsia"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 3).Count();
            ViewData["rAsia"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 3).Count();
            #endregion

            #region Total-Europa
            ViewData["eEuropa"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 4).Count();
            ViewData["rEuropa"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 4).Count();
            #endregion Total-Europa

            #region Total-Africa
            ViewData["eAfrica"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 5).Count();
            ViewData["rAfrica"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 5).Count();
            #endregion Total-Africa

            #region Total-Oceania
            ViewData["eOceania"] = _context.Editors.Where(e => e.CodPaisNavigation.CodContinente == 6).Count();
            ViewData["rOceania"] = _context.Revista.Where(r => r.CdEditorNavigation.CodPaisNavigation.CodContinente == 6).Count();
            #endregion Total-Oceania

            #endregion Total

            #region Revista-Pais
            int nPaises = 216;
            string[,] valor = new string[2, nPaises];
            string[] pais = new string[nPaises];
            var tpais = new object[nPaises];

            for (int total = 1; total < nPaises; total++)
            {
                valor[0, total] = _context.Revista.Where(r => r.CdEditorNavigation.CodPais == total).Count().ToString();
                valor[1, total] = _context.PaisEditors.Where(r => r.Id == total).Select(r => r.NomePais).FirstOrDefault().ToString();
                pais[total] = "['" + valor[1, total] + "', " + valor[0, total] + "],";

            }

            string dados = "";
            for (int c = 0; c < pais.Length; c++)
            {
                dados += pais[c];
            }

            dados = dados.Substring(0, dados.Length - 1);
            ViewBag.geoGrafico = Mapa.gerarMapa(dados);
            #endregion

            #region Pendencias

            #region variaveis-Auxiliares
            DbFunctions db = null;
            DateTime hoje = DateTime.Today;
            int mensal = 1;
            int anual = 2;
            int trimestral = 3;
            int bimestral = 4;
            int quadrimestral = 5;
            int semestral = 8;
            #endregion variaveis-Auxiliares

            #region contadores-Pendencias
            int pendenciaMensal = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 60 && r.CdPeriodicidade == mensal).Count();
            int pendenciaBimestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 90 && r.CdPeriodicidade == bimestral).Count();
            int pendenciaTrimestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 120 && r.CdPeriodicidade == trimestral).Count();
            int pendenciaQuadrimestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 150 && r.CdPeriodicidade == quadrimestral).Count();
            int pendenciaSemestral = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 210 && r.CdPeriodicidade == semestral).Count();
            int pendenciaAnual = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 395 && r.CdPeriodicidade == anual).Count();
            int pendenciaTotal = pendenciaMensal + pendenciaBimestral + pendenciaTrimestral + pendenciaQuadrimestral + pendenciaSemestral + pendenciaAnual;
            #endregion contadores-Pendencias

            #region ViewData-Pendencias
            ViewData["pTotal"] = pendenciaTotal.ToString();
            ViewData["pMensal"] = pendenciaMensal.ToString();
            ViewData["pBimestral"] = pendenciaBimestral.ToString();
            ViewData["pTrimestral"] = pendenciaTrimestral.ToString();
            ViewData["pQuadrimestral"] = pendenciaQuadrimestral.ToString();
            ViewData["pSemestral"] = pendenciaSemestral.ToString();
            ViewData["pAnual"] = pendenciaAnual.ToString();
            #endregion ViewData-Pendencias


            #endregion Pendencias

            #region Recebimento-Tempo

            #region contador-Revista
            int revistasAno = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) <= 365).Count();
            int revistas1Ano = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) > 365 && SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) <= 730).Count();
            int revistas2Anos = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) > 730 && SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) <= 1095).Count();
            int revistasMais3anos = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) > 1095).Count();
            #endregion contador-Revista

            #region ViewData-Tempo
            ViewData["rAno"] = revistasAno.ToString();
            ViewData["r1Ano"] = revistas1Ano.ToString();
            ViewData["r2Ano"] = revistas2Anos.ToString();
            ViewData["r3Ano"] = revistasMais3anos.ToString();
            #endregion ViewData-Tempo

            #endregion Recebimento-Tempo

            #region Caracteristicas-Revistas

            #region Revista-Antiga
            var ultimaRevista = _context.Revista.OrderBy(r => r.Chegada).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).Include(r => r.CdAquisicaoNavigation).First();
            ViewData["rAntiga"] = ultimaRevista.Titulo.ToString();
            ViewData["drAntiga"] = ultimaRevista.Chegada;
            ViewData["erAntiga"] = ultimaRevista.CdEditorNavigation.NomeEditor.ToString();
            ViewData["prAntiga"] = ultimaRevista.CdPeriodicidadeNavigation.TipoPeriodicidade;
            ViewData["aqAntiga"] = ultimaRevista.CdAquisicaoNavigation.TipoAquisicao;
            #endregion

            #region Revista-Nova
            var primeiraRevista = _context.Revista.OrderByDescending(r => r.Chegada).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).Include(r => r.CdAquisicaoNavigation).First();
            ViewData["rNova"] = primeiraRevista.Titulo.ToString();
            ViewData["drNova"] = primeiraRevista.Chegada;
            ViewData["erNova"] = primeiraRevista.CdEditorNavigation.NomeEditor.ToString();
            ViewData["prNova"] = primeiraRevista.CdPeriodicidadeNavigation.TipoPeriodicidade;
            ViewData["aqNova"] = primeiraRevista.CdAquisicaoNavigation.TipoAquisicao;
            #endregion

            #region Aquisicao-Revista
            var compraRevista = _context.Revista.Where(r => r.CdAquisicaoNavigation.TipoAquisicao == "Compra").Include(r => r.CdAquisicaoNavigation).Count();
            var doacaoRevista = _context.Revista.Where(r => r.CdAquisicaoNavigation.TipoAquisicao == "Doação").Include(r => r.CdAquisicaoNavigation).Count();
            var permutaRevista = _context.Revista.Where(r => r.CdAquisicaoNavigation.TipoAquisicao == "Permuta").Include(r => r.CdAquisicaoNavigation).Count();

            ViewData["aqCompra"] = compraRevista.ToString();
            ViewData["aqDoacao"] = doacaoRevista.ToString();
            ViewData["aqPermuta"] = permutaRevista.ToString();
            #endregion


            #endregion


            return new ViewAsPdf()
            {
                FileName = "Relatorio.pdf",
                CustomSwitches = "--no-stop-slow-scripts --javascript-delay 1000 "
            }; 
        }
    }
}
