using i18nTool.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace i18nTool
{
    public class Program
    {

        private static string currentDirectory;

        public static void Main(string[] args)
        {

            string[] input;
            List<string> argsList;
            Dictionary<String, String> inputArgs;

            try
            {
                currentDirectory = Environment.CurrentDirectory;
                input = args;
                argsList = input.ToList();
                inputArgs = new Dictionary<string, string>();

                foreach (var arg in argsList)
                {
                    if (arg.StartsWith("-"))
                    {
                        if (arg.Equals("--translate", StringComparison.InvariantCultureIgnoreCase) ||
                                 arg.Equals("-t", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (argsList.Count > argsList.IndexOf(arg) + 1 && !argsList[argsList.IndexOf(arg) + 1].StartsWith("-"))
                            {
                                inputArgs.Add("PATH", argsList[argsList.IndexOf(arg) + 1]);
                            }
                            else
                            {
                                throw new InvalidOperationException("Must inform the origin file, on --translate");
                            }

                            if (argsList.Count > argsList.IndexOf(arg) + 2 && !argsList[argsList.IndexOf(arg) + 2].StartsWith("-"))
                            {
                                inputArgs.Add("TARGET", argsList[argsList.IndexOf(arg) + 2]);
                            }
                            else
                            {
                                throw new InvalidOperationException("Must inform the target language, on --translate");
                            }
                        }
                        else if (arg.Equals("--list", StringComparison.InvariantCultureIgnoreCase) ||
                                 arg.Equals("-l", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (argsList.Count > argsList.IndexOf(arg) + 1 && !argsList[argsList.IndexOf(arg) + 1].StartsWith("-"))
                            {
                                inputArgs.Add("FOLDER", argsList[argsList.IndexOf(arg) + 1]);
                            }
                            else
                            {
                                throw new InvalidOperationException("Must inform the folder path, on --list");
                            }

                            if (argsList.Count > argsList.IndexOf(arg) + 2 && !argsList[argsList.IndexOf(arg) + 2].StartsWith("-"))
                            {
                                inputArgs.Add("LANG", argsList[argsList.IndexOf(arg) + 2]);
                            }
                            else
                            {
                                throw new InvalidOperationException("Must inform the language, on --list");
                            }
                        }
                        else if (arg.Equals("--help", StringComparison.InvariantCultureIgnoreCase) ||
                                 arg.Equals("-h", StringComparison.InvariantCultureIgnoreCase))
                        {
                            inputArgs.Add("HELP", "");
                        }
                        else if (arg.Equals("--version", StringComparison.InvariantCultureIgnoreCase) ||
                                 arg.Equals("-v", StringComparison.InvariantCultureIgnoreCase))
                        {
                            inputArgs.Add("VERSION", "");
                        }
                        else if (arg.Equals("--env", StringComparison.InvariantCultureIgnoreCase) ||
                                 arg.Equals("-env", StringComparison.InvariantCultureIgnoreCase))
                        {
                            inputArgs.Add("ENV", "");
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid command use -h or --help to a list of valid commands.");
                        }
                    }
                }



                if (inputArgs.ContainsKey("PATH"))
                {
                    I18nToolBusiness toolBusiness = new I18nToolBusiness();
                    string path = inputArgs.GetValueOrDefault("PATH");
                    if (!Path.IsPathRooted(path))
                    {
                        path = Path.Combine(currentDirectory, path);
                    }                    
                    Console.WriteLine(toolBusiness.TranslateFile(path, inputArgs.GetValueOrDefault("TARGET")));
                }
                else if (inputArgs.ContainsKey("FOLDER"))
                {
                    I18nToolBusiness toolBusiness = new I18nToolBusiness();
                    string path = inputArgs.GetValueOrDefault("FOLDER");
                    if (!Path.IsPathRooted(path))
                    {
                        path = Path.Combine(currentDirectory, path);
                    }
                    path = Path.Combine(path, inputArgs.GetValueOrDefault("LANG") + ".json");
                    toolBusiness.ListFile(path).ForEach(item => Console.WriteLine(item));
                }
                else if (inputArgs.ContainsKey("VERSION"))
                {
                    Console.WriteLine("i18nTool version: " + Assembly.GetEntryAssembly().GetName().Version);
                }
                else if (inputArgs.ContainsKey("ENV"))
                {
                    Console.WriteLine("Current working directory: " + currentDirectory);
                }
                else if (inputArgs.ContainsKey("HELP"))
                {
                    help();
                }
                else
                {
                    Console.WriteLine("Invalid command, use -h or --help to see a list of valid commands.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static void help()
        {
            Console.WriteLine(" Translate:");
            Console.WriteLine("     -t or --translate <Origin i18n file path> <Target language (example pt-br)>");
            Console.WriteLine("");

            Console.WriteLine(" List all the keys from a language file:");
            Console.WriteLine("     -l or --list <i18n folder path> <language (example en-us)>");
            Console.WriteLine("");

            Console.WriteLine(" Get current dir:");
            Console.WriteLine("     -env or --env");
            Console.WriteLine("");

            Console.WriteLine(" Program Version:");
            Console.WriteLine("     -v or --version");

            Console.WriteLine(" Help:");
            Console.WriteLine("     -h or --help");
        }

    }
}
