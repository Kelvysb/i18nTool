using System;
using Xunit;
using i18nTool;
using System.IO;

namespace i18nToolTests
{
    public class ProgramTest
    {
        [Fact]
        public void Translate()
        {
            Helper.GenerateTestFiles();

            string[] inputaArgs = { "-t", Path.Combine(".", "locales", "en-us.json"), "pt-br" };
            Program.Main(inputaArgs);
            Assert.True(File.Exists(Path.Combine(".", "locales", "pt-br.json")));

        }
    }
}
