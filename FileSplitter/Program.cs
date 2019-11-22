using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSplitter
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

            var size = 1024;
            var name = "chunk";
            var input = parsed.Arguments["/input"];
            var outdir = parsed.Arguments["/outdir"];

            if (parsed.Arguments.ContainsKey("/size"))
                size = Int32.Parse(parsed.Arguments["/size"]);
            if (parsed.Arguments.ContainsKey("/name"))
                name = parsed.Arguments["/name"];


            Console.WriteLine("Attempting to split file");
            FileStream filestream = File.OpenRead(input);
            Byte[] bytechunk = new Byte[size];
            var chunknumber = 1;
            var bytesread = 0;
            var running = true;
            while (running){
                bytesread = filestream.Read(bytechunk, 0, size);
                if (bytesread <= 0)
                {
                    running = false;
                    break;
                }
                var outputfile = name + chunknumber;
                var outstream = System.IO.File.OpenWrite(outdir + "\\" + outputfile);
                outstream.Write(bytechunk, 0, bytesread);
                outstream.Close();
                chunknumber += 1;
                
            }
            Console.WriteLine("Finished splitting file!");
        }

        public static bool runCheck(ArgumentParserResult parsed)
        {
            if(parsed.Arguments.ContainsKey("/input") && parsed.Arguments.ContainsKey("/outdir"))
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
