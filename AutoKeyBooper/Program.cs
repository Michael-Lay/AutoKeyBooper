using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKeyBooper
{
    class Program
    {
        static void Main(string[] args)
        {            
            string filePath = Environment.GetCommandLineArgs()[1];
            List<String> subroutines = Parser.GetSubroutinesFrom(Parser.StripComments(Parser.Parse(filePath)));
            List<List<String>> actions = new List<List<string>>();            
            foreach (string subroutine in subroutines)
            {
                if(!(subroutine.Trim() == ""))
                {
                    actions.Add(Parser.GetLinesOfCodeFromSubroutine(subroutine));
                }
            }
            foreach (List<string> action in actions)
            {
                foreach (string line in action)
                {                    
                    Console.WriteLine("line: " + line);
                }
            }
            Console.ReadKey(); //Display the message until someone hits a key
        }
    }
}
