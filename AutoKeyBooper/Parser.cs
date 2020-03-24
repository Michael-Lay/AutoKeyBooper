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
        }

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
            string keyStroke = "";
            foreach (String keyToPress in LineOfCode) {
                keyStroke = keyToPress.ToUpper();
                switch (keyStroke)
                {
                    // Press Arrows
                    case "UP":
                        System.Windows.Forms.SendKeys.Send("DOWN");
                        break;
                    // Press letters
                    case "A":
                        System.Windows.Forms.SendKeys.Send("A");
                        break;
                    case "B":
                        System.Windows.Forms.SendKeys.Send("B");
                        break;
                    case "C":
                        System.Windows.Forms.SendKeys.Send("C");
                        break;
                    case "D":
                        System.Windows.Forms.SendKeys.Send("D");
                        break;
                    case "E":
                        System.Windows.Forms.SendKeys.Send("E");
                        break;
                    case "F":
                        System.Windows.Forms.SendKeys.Send("F");
                        break;
                    case "G":
                        System.Windows.Forms.SendKeys.Send("G");
                        break;
                    case "H":
                        System.Windows.Forms.SendKeys.Send("H");
                        break;
                    case "I":
                        System.Windows.Forms.SendKeys.Send("I");
                        break;
                    case "J":
                        System.Windows.Forms.SendKeys.Send("J");
                        break;
                    case "K":
                        System.Windows.Forms.SendKeys.Send("K");
                        break;
                    case "L":
                        System.Windows.Forms.SendKeys.Send("L");
                        break;
                    case "M":
                        System.Windows.Forms.SendKeys.Send("M");
                        break;
                    case "N":
                        System.Windows.Forms.SendKeys.Send("N");
                        break;
                    case "O":
                        System.Windows.Forms.SendKeys.Send("O");
                        break;
                    case "P":
                        System.Windows.Forms.SendKeys.Send("P");
                        break;
                    case "Q":
                        System.Windows.Forms.SendKeys.Send("Q");
                        break;
                    case "R":
                        System.Windows.Forms.SendKeys.Send("R");
                        break;
                    case "S":
                        System.Windows.Forms.SendKeys.Send("S");
                        break;
                    case "T":
                        System.Windows.Forms.SendKeys.Send("T");
                        break;
                    case "U":
                        System.Windows.Forms.SendKeys.Send("U");
                        break;
                    case "V":
                        System.Windows.Forms.SendKeys.Send("V");
                        break;
                    case "W":
                        System.Windows.Forms.SendKeys.Send("W");
                        break;
                    case "X":
                        System.Windows.Forms.SendKeys.Send("X");
                        break;
                    case "Y":
                        System.Windows.Forms.SendKeys.Send("Y");
                        break;
                    case "Z":
                        System.Windows.Forms.SendKeys.Send("Z");
                        break;

                }
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
