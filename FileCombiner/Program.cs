using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCombiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsed = Parse(args);
            if (!runCheck(parsed))
            {
                return;
            }


            var input = parsed.Arguments["/input"];
            var outdir = parsed.Arguments["/outdir"];
            var outfile = parsed.Arguments["/outfile"];
            var inputprefix = "chunk";
            if (parsed.Arguments.ContainsKey("/prefix"))
                inputprefix = parsed.Arguments["/prefix"];

            if (input == "" || outdir == "")
            {
                Console.WriteLine("Please make sure that you entered the correct parameters");
                return;
            }
            Console.WriteLine("Attempting to combine file");
            FileStream outputstream = File.OpenWrite(outdir + "\\" + outfile);
            var chunknumber = 1;
            var inputfilename = input + "\\" + inputprefix + chunknumber;

            while (File.Exists(inputfilename))
            {
                var filebytes = System.IO.File.ReadAllBytes(inputfilename);
                outputstream.Write(filebytes, 0, filebytes.Count());
                chunknumber += 1;
                inputfilename = input + "\\" + inputprefix + chunknumber;
            }
            outputstream.Close();
            Console.WriteLine("Finished creating file!");
        }
        public static bool runCheck(ArgumentParserResult parsed)
        {
            if (parsed.Arguments.ContainsKey("/input") && parsed.Arguments.ContainsKey("/outdir") && parsed.Arguments.ContainsKey("/outfile"))
            {
                return true;
            }
            else
            {
                Console.WriteLine("You must specify the input file (/input:path/to/file.exe) and output directory (/outdir:path/to/output/directory)");
                return false;
            }
        }
        public static ArgumentParserResult Parse(IEnumerable<string> args)
        {
            var arguments = new Dictionary<string, string>();
            try
            {
                foreach (var argument in args)
                {
                    var idx = argument.IndexOf(':');
                    if (idx > 0)
                        arguments[argument.Substring(0, idx)] = argument.Substring(idx + 1);
                    else
                        arguments[argument] = string.Empty;
                }

                return ArgumentParserResult.Success(arguments);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ArgumentParserResult.Failure();
            }
        }
    }
}
