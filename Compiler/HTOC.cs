using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Compiler
{
    public static class HTOC
    {
        // Create a dictionary with <string(key), Type(Value[int,string,char,etc...])>
        static HTMLi_Lgrammar get_regularGrammar;

        static HTOC()
        {
            get_regularGrammar = new HTMLi_Lgrammar();
            get_regularGrammar.setLGrammar();               // Set grammar
        }
    }
}
