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
    [Obsolete("Converting to C based compiler using ")]
    public static class HTOC
    {
        // Create a dictionary with <string(key), Type(Value[int,string,char,etc...])>
        public static HTMLi_Lgrammar get_regularGrammar;
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
            bool setArgVal = false;
            string argVal = "";

            // Tag Text value
            bool setStr = true;
            string strVal = "";

            try
            {
                foreach (char L in line)
                {
                    Debug.Debuger.Logged += "\n, letter running " + L + " ";
                    Debug.Debuger.Logged += "\n, Hex " + toHex(L) + " ";
                    if (!start && !stop && isInstruct(L)) // If no instruction is defined and the letter is a syntax
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            Debug.Debuger.Logged += "\n, instruct returned " + t + " ";
                            start = (t == "start");
                            stop = (t == "stop");
                            op_eq = (t == "op_eq");
                            op_str = (t == "op_str");

                            type = "";
                            arg = "";
                            argVal = "";
                        }
                    }
                    else if (start && !stop && !op_eq && !op_str && !isInstruct(L) && setType && setArg)
                    {
                        Debug.Debuger.Logged += "\n, running second sect returned " + toHex(L) + " ";
                        if (toHex(L) != "32")
                        {
                            type += L;
                        }
                        else
                        {
                            Debug.Debuger.Logged += "\n, letter was empty ";
                            Debug.Debuger.Logged += "\n, sending (a,type) instruct ";
                            string t = executeInstruction("a", type); // Set Tag name
                            setType = false;
                        }
                    }
                    else if (start && !stop && !op_eq && !op_str && !isInstruct(L) && !setType && setArg)
                    {
                        Debug.Debuger.Logged += "\n, letter " + L + " is an argument";
                        if (toHex(L) != "32")
                        {
                            Debug.Debuger.Logged += "\n, adding to arg ";
                            arg += L;
                        }
                    }
                    else if (start && !stop && !op_eq && !op_str && isInstruct(L) && !setType && setArg) // equal operator
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            Debug.Debuger.Logged += "\n, equal operator ";
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            Debug.Debuger.Logged += "\n, instruct returned " + t;
                            op_eq = (t == "op_eq");
                            op_str = false;
                        }
                    }
                    else if (start && !stop && op_eq && !op_str && isInstruct(L) && !setType && setArg && !setArgVal) // start argVal
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            Debug.Debuger.Logged += "\n, start str ";
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            Debug.Debuger.Logged += "\n, instruct returned " + t;
                            op_eq = (t == "op_eq");
                            op_str = (t == "op_str");

                            setArgVal = true;
                            setArg = false;
                            Debug.Debuger.Logged += "\n, bool values: " + start.ToString() + stop.ToString() + op_eq.ToString() + op_str.ToString() + setArg.ToString();
                        }
                    }
                    else if (start && !stop && !op_eq && op_str && !isInstruct(L) && !setType && !setArg && setArgVal) // set argVal
                    {
                        Debug.Debuger.Logged += "\n, set to argval " + argVal;
                        if (toHex(L) != "32")
                        {
                            Debug.Debuger.Logged += "\n, setting " + L + " to argval";
                            argVal += L;
                        }
                    }
                    else if (start && !stop && !op_eq && op_str && isInstruct(L) && !setType && !setArg && setArgVal) // end argVal
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            Debug.Debuger.Logged += "\n, end str ";
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            op_str = false;

                            setArg = true;
                            setArgVal = false;
                            executeInstruction("b", arg, argVal);
                            type = "";
                            arg = "";
                            argVal = "";
                        }
                    }else if (start && !stop && !op_eq && !op_str && isInstruct(L) && setType && setArg && !setArgVal) // >
                    {
                        if (get_syntaxGrammar.syntaxGrammar.ContainsKey(L.ToString()))
                        {
                            Debug.Debuger.Logged += "\n, end tag ";
                            string t = executeInstruction(get_syntaxGrammar.syntaxGrammar.GetValueOrDefault(L.ToString()));
                            stop = (t == "stop");
                            start = (t == "start");

                            type = "";
                            arg = "";
                            argVal = "";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debuger.Log(ex.Message);
            }

            /*if(linesParsed < _lines.Count)
            {
                linesParsed++;
                compileToNode(_lines);
            }*/

            return new Node();  //Error
        }

        static string[] std_nInstruct = { "48", "49", "5", "51", "52", "53", "54", "55", "56", "57",
        "97", "98", "99", "100", "101", "102", "103", "104", "105" , "106", "107", "108", "109", "110", "111",
        "112", "113", "114", "115", "116", "117", "118", "119", "120", "121" , "122", "123", "32"};

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

        public static List<Node> NodesCompiled = new List<Node>();
        static Tag TagCompiling;
        static Node NodeCompiling;

        public static string executeInstruction(string _instructNum, string arg = "", string argVal = "")
        {
            switch (_instructNum)
            {
                case "0":
                    Debug.Debuger.Logged += "\n, starting (0) ";
                    TagCompiling = new Tag();
                    NodeCompiling = new Node();
                    return "start";
                case "1":
                    Debug.Debuger.Logged += "\n, ending (1) ";
                    NodeCompiling.tag = TagCompiling;
                    NodesCompiled.Add(NodeCompiling);
                    return "stop";
                case "2":
                    return "op_eq";
                case "3":
                    return "op_str";
                case "a":
                    Debug.Debuger.Logged += "\n, can assign tagname? ";
                    if (get_regularGrammar.LGrammar.ContainsKey(arg))
                    {
                        Debug.Debuger.Logged += "\n, assigned tagname ";
                        TagCompiling.tagName = arg;
                    }
                    break;
                case "b":
                    Debug.Debuger.Logged += "\n, can assign Arg? " + arg;
                    if (get_regularGrammar.LGrammar.ContainsKey(arg))
                    {
                        Debug.Debuger.Logged += "\n, assigned " + arg;
                        switch (arg)
                        {
                            case "style":
                                TagCompiling.Style = argVal;
                                break;
                            case "class":
                                TagCompiling.Class = argVal;
                                break;
                            case "id":
                                TagCompiling.Id = argVal;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }

            return "";
        }
    }
}
