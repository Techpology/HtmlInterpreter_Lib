using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using htmlInterpreter.Compiler.CPFM;

namespace htmlInterpreter.Compiler.CPFM.Vanilla
{
    public static class VPA
    {
        [DllImport("vanilla.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getTags();
        static IntPtr cPtr;

        static void _init()
        {
            Debug.Debuger.Log("getting C pointer to initiate Vanilla parser");
            cPtr = getTags();

            Debug.Debuger.Log($"C pointer grabbed succesfully: {(IntPtr)cPtr}");
        }

        static string[] lines { get; set; }
        static string filePath { get; set; }

        public static void VanillaParse(string _path)
        {

        }

        static void fileToLines()
        {
            foreach(string line in lines)
            {

            }
        }

    }
}
