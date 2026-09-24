using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInvoiceSystem.E2E
{
    public static class Helper
    {

        /// <summary>
        /// Podmienia uszkodzone znaki (krzaki/) na poprawne polskie litery w komunikatach paginacji.
        /// </summary>
        /// <param name="inputText">Surowy tekst pobrany ze strony (np. "Pokazuj 0 z 0 klientw | Strona 1 z 0")</param>
        /// <returns>Poprawiony tekst (np. "Pokazuję 0 z 0 klientów | Strona 1 z 0")</returns>
        public static string FixPaginationText(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
                return string.Empty;
                        
            string cleaned = Regex.Replace(inputText, @"Pokazuj[\uFFFD\?]+", "Pokazuję");
            cleaned = Regex.Replace(cleaned, @"klient[\uFFFD\?]+w", "klientów");

            return cleaned.Trim();
        }

        /// <summary>
        /// Zastępuje uszkodzone znaki () na kropkę Regexa, żeby Playwright mógł dopasować tekst niezależnie od kodowania.
        /// </summary>
        public static Regex ToSafeRegex(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return new Regex(".*");
                        
            string pattern = Regex.Replace(text, @"[\uFFFD\?]", ".");

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }
    }
}
