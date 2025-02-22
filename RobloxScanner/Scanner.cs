using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobloxFiles;
using RobloxFiles.DataTypes;

namespace RobloxModelScanner
{
    internal class Scanner
    {
        public Scanner() { }
       
        public async Task ScanPage(int page, string search)
        {
            RobloxModel[]? models = await RobloxEndpoint.getPage(page, search);

            if (models == null)
            {
                Console.WriteLine("Failed to get models");
                return;
            }

            foreach (var model in models)
            {
                string? modelStream = await RobloxEndpoint.getModelFromId(model.id);

                if (modelStream == null)
                {
                    Console.WriteLine($"Failed to get model {model.id}");
                    continue;
                }

                LoadModelAndScan(modelStream, model);
            }
        }

        public void LoadModelAndScan(string modelStream, RobloxModel model)
        {
            Console.WriteLine("Loading model: " + model.name);

            RobloxFile file;

            try
            {
                file = RobloxFile.Open(modelStream);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to load model {model.id}");
                Console.WriteLine(e.Message);
                return;
            }

            foreach (var obj in file.GetDescendants())
            {
                if (obj.ClassName == "Script" || obj.ClassName == "ModuleScript")
                {
                    Property source = obj.GetProperty("Source");
                    ProtectedString sourceValue = source.Value as ProtectedString;
                    string sourceString = sourceValue.ToString();

                    if (sourceString.Contains("require"))
                    {
                        string[] newLines = sourceString.Split('\n');
                        int lineNumber = newLines.ToList().FindIndex(x => x.Contains("require")) + 1;
                        string line = newLines[lineNumber - 1];

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Model {model.id} with Script {obj.Name} contains 'require' on line {lineNumber}");

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(line);

                        if (line.Contains("MaterialService") || line.Contains("JointsService") || line.Contains("require"))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("↑ ↑ ↑ This script is likely malicious ↑ ↑ ↑");
                        }

                        Console.ResetColor();
                    }
                }
            }
            Console.WriteLine("Finished Loading model " + model.name);
        }
    }
}