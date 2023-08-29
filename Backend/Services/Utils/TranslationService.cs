using Core.IUtils;
using Newtonsoft.Json;

namespace Services.Utils
{
    public class TranslationService : ITranslationService
    {
        private Dictionary<string, Dictionary<string, string>>? translations;

        public TranslationService()
        {
            LoadTranslationFile();
        }



        private void LoadTranslationFile()
        {
            if (translations is not null) return;
            string filePath = Path.Combine(AppContext.BaseDirectory, "Translations.json");
            string json = File.ReadAllText(filePath);
            translations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
        }




        public string GetByKey(string key, string langKey, params string[] args)
        {
            LoadTranslationFile();
            if (translations is null) return key;

            Dictionary<string, string>? langs = translations.GetValueOrDefault(key);
            if (langs is null)
                return key;

            string? translation = langs.GetValueOrDefault(langKey.ToUpper());

            if (translation is null) return key;

            return string.Format(translation, args.Select(s => translations.GetValueOrDefault(s) != null ? translations[s].GetValueOrDefault(langKey) : s).ToArray());
        }
    }
}
