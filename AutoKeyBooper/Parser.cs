using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoKeyBooper
{
    static class Parser
    {

        public static string Parse(string filePath) {
            string returnValue = "FileStream Created";
            string scriptText = "Script Text Not Found";
            if (File.Exists(filePath)){
                scriptText = File.ReadAllText(filePath);
                returnValue = scriptText;
            } else {
                returnValue = "File path \"" + filePath + "\" is invalid.";
            }            
            return returnValue;
        }// Method Parse()

        //Strip out comments
        public static string StripComments(string textWithComments) {
            string returnValue = "*Error stripping comments*";
            List<string> linesWithoutComments = new List<string>();
            String[] lines = Regex.Split(textWithComments,Environment.NewLine);
            foreach (string line in lines) {
                if (!(line.StartsWith("//") || (line.Trim() == ""))) { linesWithoutComments.Add(line); }
            }
            returnValue = String.Join(Environment.NewLine, linesWithoutComments);
            return returnValue;
        }

        //Split the file stream by 'key pressed' -  this returns indivual procedures and the keys that trigger them
        public static List<string> GetSubroutinesFrom(string wholeText) {
            List<string> returnValue = new List<string>();
            returnValue = Regex.Split(wholeText,"key pressed", RegexOptions.IgnoreCase).ToList<string>();
            return returnValue;
        }

        //Add a method here that takes an individual procedure (from the method above) and checks for curly open/close brackets
        public static List<string> GetLinesOfCodeFromSubroutine(String procedureText) {
            List<string> returnValue = new List<string>();
            string codeWithoutBrackets = procedureText;
            //Remove the everything outside (and including) the curly brackets
            codeWithoutBrackets = Regex.Split(codeWithoutBrackets, "{", RegexOptions.IgnoreCase).ToList<string>()[1];
            codeWithoutBrackets = Regex.Split(codeWithoutBrackets, "}", RegexOptions.IgnoreCase).ToList<string>()[0];
            returnValue = Regex.Split(codeWithoutBrackets, ";", RegexOptions.IgnoreCase).ToList<string>();
            returnValue = (StripNewLines(returnValue));
            returnValue.RemoveAll(x => x == ""); //Get rid of blank lines  
            return returnValue;
        }

        //Add a method here that takes each line in a procedure and presses the corresponding key.
        public static void PressKey(List<String> LineOfCode) {
            
            foreach (String keyToPress in LineOfCode) {
                System.Windows.Forms.SendKeys.Send("K"); 
            }      
        }

        public static List<String> KeyPressesIn(List<String> LineOfCode) {
            List<String> returnValue = new List<string>();
            foreach (String word in LineOfCode) {
                if (word.IsNotReservedInAutoKeyBooper()) returnValue.Add(word);
            }
            return returnValue;
        }

        public static List<String> StripNewLines(List<String> codeWithoutBrackets) {
            List<String> returnValue = new List<string>();

            foreach (String lineOfCode in codeWithoutBrackets) {
                returnValue.Add(Regex.Replace(lineOfCode, Environment.NewLine, "", RegexOptions.IgnoreCase));
            }
            return returnValue;
        }

        //In the future, add support for branching and looping

    }// Class Parser
}
