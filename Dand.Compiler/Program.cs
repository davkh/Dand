using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Dand.Analyzing;
using Dand.RunTime;

namespace Dand.Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = null;
            Scanner scn = null;
            Parser parser = null;
            MainProgram program = null;

            if (args.Length < 2)
            {
                Console.WriteLine("Arguments of compiler are missing.");
                return;
            }

            try
            {
                file = new FileStream(args[0], FileMode.Open);
                scn = new Scanner(file);

                parser = new Parser(scn);
                if (parser.Parse())
                {
                    program = parser.program;

                    if (program != null)
                    {
                        System.IO.File.WriteAllText(args[1], program.Translate());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(file != null)
                    file.Close();
            } 
        }
    }
}
