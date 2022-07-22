using KeikoObfuscator;
using Mono.Cecil;
using System;
using System.IO;

namespace SleakDirectFile.Tools
{
    class Obfuscate
    {
        public static bool Rename(string path)
        {
            try
            {

                using (var inputStream = File.OpenRead(path))
                {
                    // Read assembly.
                    var assembly = AssemblyDefinition.ReadAssembly(inputStream);
                    var logOutput = new ConsoleLogOutput();

                    // Obfuscate assembly.
                    Obfuscator.Obfuscate(assembly, logOutput);

                    // Write obfuscated assembly to disk.
                    var outputDirectory = Path.Combine(Path.GetDirectoryName(path), "Obfuscated");
                    if (!Directory.Exists(outputDirectory))
                        Directory.CreateDirectory(outputDirectory);

                    using (var outputStream = File.Create(Path.Combine(outputDirectory, Path.GetFileName(path))))
                        assembly.Write(outputStream);

                    Console.WriteLine("Obfuscated.");
                    return true;
                }

            }
            catch (Exception er) { return false; }
        }
    }
    class ConsoleLogOutput : ILogOutput
    {
        public void ReportError(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
