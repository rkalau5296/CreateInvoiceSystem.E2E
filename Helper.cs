using Microsoft.Extensions.Configuration;

namespace CreateInvoiceSystem.E2E
{
    public static class Helper
    {        
        public static string FixPaginationText(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
                return string.Empty;
                        
            string cleaned = Regex.Replace(inputText, @"Pokazuj[\uFFFD\?]+", "Pokazuję");
            cleaned = Regex.Replace(cleaned, @"klient[\uFFFD\?]+w", "klientów");

            return cleaned.Trim();
        }
        
        public static Regex ToSafeRegex(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return new Regex(".*");
                        
            string pattern = Regex.Replace(text, @"[\uFFFD\?]", ".");

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }        
    }
}
