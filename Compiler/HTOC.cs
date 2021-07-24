using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

using htmlInterpreter;
using htmlInterpreter.Caching;
using htmlInterpreter.Components;
using htmlInterpreter.Debug;

namespace htmlInterpreter.Compiler
{
    public static class HTOC
    {
        // Create a dictionary with <string(key), Type(Value[int,string,char,etc...])>
        static HTMLi_Lgrammar get_regularGrammar;
        // Create a dictionary with <string(key), string(value[instruction])>
        static HTMLi_SyntaxGrammar get_syntaxGrammar;

        static HTOC()
        {
            get_regularGrammar = new HTMLi_Lgrammar();
            get_regularGrammar.setLGrammar();               // Set grammar

            get_syntaxGrammar = new HTMLi_SyntaxGrammar();
            get_syntaxGrammar.setSyntaxGrammar();           // Set syntax
        }

        public static string toHex(char a)
        {
            return Convert.ToByte(a).ToString();
        }

        /// <summary>
        /// Run this method before parsing. It sets all lines in file to a list, needed for parsing
        /// </summary>
        /// <param name="_path">Path to file</param>
        /// <returns></returns>
        public static List<string> fileLinesToArr(string _path)
        {
            List<string> lines = new List<string>();
            int count = 0;
            string currentLine;

            using (StreamReader sr = new StreamReader(_path))
            {
                while ((currentLine = sr.ReadLine()) != null)
                {
                    lines.Add(currentLine);
                    count++;
                }
            }

            return lines;
        }

        static int linesParsed = 0;

        /// <summary>
        /// Use a loop based on the length of the list supplied from fileLinesToArr() [line(42)]
        /// </summary>
        /// <param name="_lines"></param>
        /// <returns></returns>
        public static Node compileToNode(List<string> _lines)
        {
            // A set of instruction values
            bool start = false;
            bool stop = false;
            bool op_eq = false;
            bool op_str = false;

            // Current line to chars
            char[] line = _lines[linesParsed].ToCharArray();

            // Tagname
            bool setType = true;
            string type = "";

            // Tag Argument active
            bool setArg = true;
            string arg = "";
            // Tag Argument value
            string argVal = "";

            // Tag Text value
            bool setStr = true;
            string strVal = "";

            try
            {
                foreach (char L in line)
                {
                    if (!start && !stop && isInstruct(L)) // If no instruction is defined and the letter is a syntax
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            start = (t == "start");
                            stop = (t == "stop");
                            op_eq = (t == "op_eq");
                            op_str = (t == "op_str");
                        }
                    }
                    else if (start && !op_eq && !op_str && !isInstruct(L) && setType)
                    {
                        if (toHex(L) != "20")
                        {
                            type += L;
                        }
                        else
                        {
                            executeInstruction("a", type); // Set Tag name
                            setType = false;
                        }
                    }
                    else if (start && !op_eq && !op_str && !isInstruct(L) && !setType && setArg)
                    {
                        if (toHex(L) != "20")
                        {
                            arg += L;
                        }
                    }
                    else if (start && !op_eq && !op_str && isInstruct(L) && !setType && setArg) // equal operator
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            start = (t == "start");
                            stop = (t == "stop");
                            op_eq = (t == "op_eq");
                            op_str = (t == "op_str");
                        }
                    }
                    else if (start && op_eq && !op_str && isInstruct(L) && !setType && setArg) // start argVal
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            start = (t == "start");
                            stop = (t == "stop");
                            op_eq = (t == "op_eq");
                            op_str = (t == "op_str");

                            setArg = (!op_str);
                        }
                    }
                    else if (start && op_eq && op_str && !isInstruct(L) && !setType && !setArg) // set argVal
                    {
                        if(toHex(L) != "20")
                        {
                            argVal += L;
                        }
                    }
                    else if (start && op_eq && op_str && isInstruct(L) && !setType && !setArg) // end argVal
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            start = (t == "start");
                            stop = (t == "stop");
                            op_eq = (t == "op_eq");
                            op_str = (t != "op_str");

                            setArg = (!op_str);
                        }
                    }
                    else if(isInstruct(L) && start && !stop && !op_eq && !op_str && !setType && !setArg)
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            start = (t == "start");
                            stop = (t == "stop");
                            op_eq = (t == "op_eq");
                            op_str = (t == "op_str");

                            setArg = (!op_str);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debuger.Log(ex.Message);
            }

            return new Node();  //Error
        }

        static string[] std_nInstruct = { "61", "62", "63", "64", "65", "66", "67", "68", "69" , "6a", "6b", "6c", "6d", "6e", "6f",
        "70", "71", "72", "73", "74", "75", "76", "77", "78", "79" , "7a", "20"};

        static bool isInstruct(char _l)
        {
            foreach (string hex in std_nInstruct)
            {
                if(toHex(_l) == hex)
                {
                    return false;
                }
            }
            return true;
        }

        static List<Node> NodesCompiled = new List<Node>();
        static Tag TagCompiling;
        static Node NodeCompiling;

        public static string executeInstruction(string _instructNum, string arg = "")
        {
            switch (_instructNum)
            {
                case "0":
                    TagCompiling = new Tag();
                    NodeCompiling = new Node();
                    return "start";
                case "1":
                    NodeCompiling.tag = TagCompiling;
                    NodesCompiled.Add(NodeCompiling);
                    break;
                case "2":
                    return "op_eq";
                case "3":
                    return "op_str";
                case "a":
                    if (get_regularGrammar.LGrammar.ContainsKey(arg))
                    {
                        TagCompiling.tagName = arg;
                    }
                    break;
                default:
                    break;
            }

            return "";
        }
    }
}
