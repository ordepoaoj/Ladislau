﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web20.Models
{
    public class Mapa
    {
        public static string gerarMapa (string dados)
        {
            
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = dados.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            string traduzirAlemanha = sbReturn.ToString().Replace("Alemanha", "Germany");
            string traduzirEUA = traduzirAlemanha.Replace("Estados Unidos", "United States");
            string traduzirEspanha = traduzirEUA.Replace("Espanha","Spain");
            string traduzirFranca = traduzirEspanha.Replace("Franca", "France");
            string traduzirReinoUnido = traduzirFranca.Replace("Reino Unido", "United Kingdom");
            string traduzirsuica = traduzirReinoUnido.Replace("Suica", "Switzerland");
            string traduzirsuecia = traduzirsuica.Replace("Suecia", "Sweden");
            return traduzirsuecia;
        }

    }
}