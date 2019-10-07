using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BI18n.Models;
using BI18n.Helpers;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;

namespace i18nTool.Business
{
    class I18nToolBusiness
    {

        private readonly HttpClient client = new HttpClient();

        public string TranslateFile(string inputPath, string targetLanguage)
        {
            string translatedText = "";
            BLanguageSet languageSet = LanguageFileConverter.LoadLanguageFile(inputPath);
            BLanguageSet resultLangageSet;
            List<BLanguageItem> bLanguageItens = new List<BLanguageItem>();
            JArray jsonRsult;

            languageSet.Itens.ForEach(async item => 
            {
                translatedText = getTranslationAsync(item.Value, languageSet.LanguageKey, targetLanguage).Result;
                jsonRsult = JArray.Parse(translatedText);
                bLanguageItens.Add(new BLanguageItem(item.Key, jsonRsult[0][0][0].ToString()));
            });


            resultLangageSet = new BLanguageSet(bLanguageItens, targetLanguage);
            LanguageFileConverter.SaveLanguageFile(resultLangageSet, Path.GetDirectoryName(inputPath));

            return Path.Combine(Path.GetDirectoryName(inputPath), targetLanguage + ".json");
        }

        public List<string> ListFile(string inputPath)
        {
            List<string> result = new List<string>();
            BLanguageSet languageSet = LanguageFileConverter.LoadLanguageFile(inputPath);

            result.Add($"File: {inputPath}");
            result.Add($"Language: {languageSet.LanguageKey}");

            languageSet.Itens.ForEach(item => result.Add($"{item.Key} : {item.Value}"));

            return result;
        }

        private async Task<string> getTranslationAsync(string input, string sourceLanguage, string targetLanguage)
        {
            string result = input;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            result = await client.GetStringAsync($"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLanguage}&tl={targetLanguage}&dt=t&q={HttpUtility.UrlEncode(input)}");

            return result;
        }

    }
}
