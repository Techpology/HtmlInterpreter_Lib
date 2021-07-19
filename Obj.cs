using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Components
{
    public class Obj
    {
        string Name { get; set; }
        Obj getComponent()
        {
            //TODO: getComponent        { Searches through references in object to find a specific type and or name which later gets returned }
            return new Obj();
        }
        void delete()
        {
            //TODO: delete              { Gets component reference and deletes it }
        }
        void deleteComponent()
        {
            //TODO: deleteComponent     { Gets component reference in object of specific type and or name which later gets deleted }
        }
    }
}
