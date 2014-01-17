using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Dand.Analyzing;
using Dand.RunTime;

namespace Dand.Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = null;
            Scanner scn = null;
            Parser parser = null;
            MainProgram program = null;

            try
            {
                file = new FileStream("Power.ss", FileMode.Open);
                scn = new Scanner(file);

                //int tok = 0;
                //do
                //{
                //    tok = scn.yylex();
                //    Console.WriteLine(scn.yytext + (int)scn.yytext[0]);
                //} while (tok != (int)Tokens.EOF);

                parser = new Parser(scn);
                if (parser.Parse())
                {
                    program = parser.program;

                    if (program != null)
                    {
                        Console.WriteLine(program.Translate());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                file.Close();
            }
            
            
        }
    }
}
