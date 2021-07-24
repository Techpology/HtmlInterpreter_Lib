using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace htmlInterpreter.Compiler
{
    public class HTMLi_Lgrammar
    {
        // Grammar dictionary
        public Dictionary<string, Type> LGrammar { get; set; }

        public string regularGrammarFile_Name { get; set; } // File name and extension of the regular grammar file
        public Stream regularGrammarFile_Path { get; set; } // Can be changed for different grammar set (same syntax)

        // Constructor
        public HTMLi_Lgrammar()
        {
            LGrammar = new Dictionary<string, Type>();

            try
            {
                regularGrammarFile_Name = "regularGrammar.txt";
                // Getting relative path to class of regularGrammar.txt [1^]
                var asm = Assembly.GetExecutingAssembly();
                regularGrammarFile_Path = asm.GetManifestResourceStream(asm.GetManifestResourceNames().Single(file => file.EndsWith(regularGrammarFile_Name)));
            }
            catch (Exception e)
            {
                Debug.Debuger.Log(e.Message);
                throw;
            }
        }

        // Deconstructor
        ~HTMLi_Lgrammar()
        {
            
        }

        public void setLGrammar()
        {
            int lineCount = 0;  // Increments with each line read
            string currentLineContent;
            // Read regular grammar file
            using (StreamReader sr = new StreamReader(regularGrammarFile_Path))
            {
                while ((currentLineContent = sr.ReadLine()) != null)
                {
                    string[] key_Val = currentLineContent.Split(',');

                    switch (key_Val[1])     // Set type
                    {
                        case "s":
                            LGrammar.Add(key_Val[0], Type.GetType("System.string"));
                            break;
                        default:
                            break;
                    }
                    lineCount++;
                }
            }
        }

        // Read from regular grammar file in to dictionary<string(Tag.var), type(int,string,etc...)>
    }
}
