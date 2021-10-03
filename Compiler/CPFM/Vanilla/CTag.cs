using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Compiler.CPFM.Vanilla
{
    class CTag
    {
        public unsafe struct cTag
        {
            public int*     indexId { get; set; }
            public byte*    tagName { get; set; }
            public byte*    Text    { get; set; }
        };
    }
}
