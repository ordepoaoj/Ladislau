using System.Globalization;
using System.Text;

namespace Web20.Models
{
    public class Mapa
    {
        public static string gerarMapa(string dados)
        {

            StringBuilder sbReturn = new StringBuilder();
            var arrayText = dados.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark) sbReturn.Append(letter);
            }

            string traduzirAlemanha = sbReturn.ToString().Replace("Alemanha", "Germany");
            string traduzirEUA = traduzirAlemanha.Replace("Estados Unidos", "United States");
            string traduzirEspanha = traduzirEUA.Replace("Espanha", "Spain");
            string traduzirFranca = traduzirEspanha.Replace("Franca", "France");
            string traduzirReinoUnido = traduzirFranca.Replace("Reino Unido", "United Kingdom");
            string traduzirsuica = traduzirReinoUnido.Replace("Suica", "Switzerland");
            string traduzirsuecia = traduzirsuica.Replace("Suecia", "Sweden");
            string traduziritalia = traduzirsuecia.Replace("Italia", "Italy");
            string traduzirafricadosul = traduziritalia.Replace("Africa do Sul", "South Africa");
            string traduzirbrasil = traduzirafricadosul.Replace("Brasil", "Brazil");
            return traduzirbrasil;
        }

    }
}
