using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Debug
{
    public static class Debuger
    {
        public static void Log(string _toLog)
        {
            Logged += _toLog;
        }

        public static string Logged { get; set; }
    }
}