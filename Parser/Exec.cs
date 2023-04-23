using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Parser
{
    public class Exec
    {
        public string type { get; set; }
        public List<string> vals = new List<string>();

        public Exec(string type)
        {
            this.type = type;
        }
    }
}
