using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Parser
{
    public class Token
    {
        public string type { get; set; }
        public string value { get; set; }

        public Token(string type, string value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
