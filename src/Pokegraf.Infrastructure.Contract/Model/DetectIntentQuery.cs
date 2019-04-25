namespace Pokegraf.Infrastructure.Contract.Model
{
    public class DetectIntentQuery
    {
        public string Text { get; set; }
        public string LanguageCode { get; set; }

        public DetectIntentQuery(string text, string languageCode)
        {
            Text = text;
            LanguageCode = languageCode;
        }
    }
}