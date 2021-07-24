using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace htmlInterpreter.Compiler
{
    class HTMLi_Lgrammar
    {
        // Grammar dictionary
        public Dictionary<string, Type> LGrammar { get; set; }

        string regularGrammarFile_Name { get; set; }        // File name and extension of the regular grammar file
        public string regularGrammarFile_Path { get; set; } // Can be changed for different grammar set (same syntax)

        // Constructor
        HTMLi_Lgrammar()
        {
            LGrammar = new Dictionary<string, Type>();

            regularGrammarFile_Name = "regularGrammar.txt";
            // Getting relative path to class of regularGrammar.txt [1^]
            regularGrammarFile_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            regularGrammarFile_Path += $"/{regularGrammarFile_Name}";
        }

        // Deconstructor
        ~HTMLi_Lgrammar()
        {

        }

        void setLGrammar()
        {
            // Read regular grammar file
            using (StreamReader sr = new StreamReader(regularGrammarFile_Path))
            {

            }
        }

        // Read from regular grammar file in to dictionary<string(Tag.var), type(int,string,etc...)>
    }
}
