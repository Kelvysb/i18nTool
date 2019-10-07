using BI18n.Helpers;
using BI18n.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace i18nToolTests
{
    class Helper
    {

        public static void GenerateTestFiles()
        {

            BLanguageSet objAuxTest;

            try
            {
                if (!Directory.Exists(Path.Combine(".", "locales")))
                {
                    Directory.CreateDirectory(Path.Combine(".", "locales"));
                }

                if (File.Exists(Path.Combine(".", "locales", "pt-br.json")))
                {
                    File.Delete(Path.Combine(".", "locales", "pt-br.json"));
                }

                if (File.Exists(Path.Combine(".", "locales", "en-us.json")))
                {
                    File.Delete(Path.Combine(".", "locales", "en-us.json"));
                }

                if (!File.Exists(Path.Combine(".", "locales", "en-us.json")))
                {
                    objAuxTest = new BLanguageSet(new List<BLanguageItem>()
                    {
                        new BLanguageItem("group_one.text_one", "Test text one, translate"),
                        new BLanguageItem("group_one.text_two", "Test text two, translate"),
                        new BLanguageItem("group_one.text_three", "Test text three, translate"),
                        new BLanguageItem("group_one.text_four", "Test text four, translate"),
                        new BLanguageItem("group_two.text_five", "Test text five, translate"),
                        new BLanguageItem("group_two.text_six", "Test text six, translate"),
                        new BLanguageItem("group_two.text_seven", "Test text seven, translate"),
                        new BLanguageItem("group_two.text_eight", "Test text eight, translate"),
                        new BLanguageItem("group_two.sub_group.text_nine", "Test text nine, translate"),
                    }, "en-us");
                    LanguageFileConverter.SaveLanguageFile(objAuxTest, Path.Combine(".", "locales"));
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
