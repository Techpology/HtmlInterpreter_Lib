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

        static string toHex(char a)
        {
            return Convert.ToByte(a).ToString();
        }

        /*static Node compileToNode(string path)
        {
            // A set of instruction values
            bool start = false;
            bool stop = false;
            bool op_eq = false;
            bool op_str = false;

            string currentLine;
            Tag t = new Tag();
            Node n = new Node();

            // We read the file at path
            using (StreamReader sr = new StreamReader(path))
            {
                
            }
        }*/
    }
}
