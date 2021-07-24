using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace htmlInterpreter.Compiler
{
    class HTMLi_SyntaxGrammar
    {
        public Dictionary<string, string> syntaxGrammar { get; set; }
        public string syntaxGrammar_Name { get; set; }
        public string syntaxGrammar_Path { get; set; }

        public HTMLi_SyntaxGrammar()
        {
            syntaxGrammar = new Dictionary<string, string>();

            syntaxGrammar_Name = "syntaxGrammar.txt";
            syntaxGrammar_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            syntaxGrammar_Path += $"/{syntaxGrammar_Name}";
        }

        ~HTMLi_SyntaxGrammar()
        {

        }

        public void setSyntaxGrammar()
        {
            int LineCount = 0;
            string CurrentLine;
            using (StreamReader sr = new StreamReader(syntaxGrammar_Path))
            {
                while ((CurrentLine = sr.ReadLine()) != null)
                {
                    string[] key_val = CurrentLine.Split(" ");

                    syntaxGrammar.Add(key_val[0], key_val[1]);

                    LineCount++;
                }
            }
        }
    }
}
