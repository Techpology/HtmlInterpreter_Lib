using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using htmlInterpreter.Components;
using htmlInterpreter.Debug;

namespace htmlInterpreter.Caching
{
    public static class ObjQ
    {
        static Dictionary<object, string> queueToPreview;

        static ObjQ()
        {
            queueToPreview = new Dictionary<object, string>();
        }

        public static void insert(Object _Package, string _path)
        {
            queueToPreview.Clear();
            queueToPreview.Add(_Package, _path);
            Save.Save_intoPreview(_Package, _path);
        }
    }
}
