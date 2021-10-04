using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Compiler.CPFM.Vanilla
{
    public class _C_LinkedList
    {
        public struct cNode
        {
            htmlInterpreter.Components.Tag tag;
            IntPtr next;
        };

        public struct cLinkedList
        {
            IntPtr head, tail;
        };
    }
}
