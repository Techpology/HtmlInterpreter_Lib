using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using htmlInterpreter.Compiler.CPFM;
using htmlInterpreter.Components;
using System.IO;

namespace htmlInterpreter.Compiler.CPFM.Vanilla
{
    public static class VPA
    {
        [DllImport("vanilla.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getTags([MarshalAs(UnmanagedType.LPUTF8Str)] string _ToParse);
        static IntPtr cPtr;

        public static void _init(string _filePath)
        {
            lines       = new List<string>();
            filePath    = _filePath;

            /* --------------------------------------------------------- */

            Debug.Debuger.Log("getting C pointer to initiate Vanilla parser");
            fileToLines();
            cPtr = getTags(ListToStr(lines));

            Debug.Debuger.Log($"C pointer grabbed succesfully: {(IntPtr)cPtr}");
        }

        static List<string> lines { get; set; }
        static string filePath { get; set; }

        public static List<Node> VanillaParse()
        {
            List<Node> _ret = new List<Node>();
            CTag.cTag _tempTag = (CTag.cTag)Marshal.PtrToStructure(cPtr, typeof(CTag.cTag));

            unsafe
            {
                byte[] arr = new byte[STMH.strlen(_tempTag.tagName)];
                Marshal.Copy((IntPtr)_tempTag.tagName, arr, 0, STMH.strlen(_tempTag.tagName));
                string _str_TagName = System.Text.Encoding.Default.GetString(arr);

                Tag _Tag = new Tag();
                _Tag.tagName = _str_TagName;

                Node _x = new Node(_Tag);
                _ret.Add(_x);
            }

            return _ret;
        }

        static void fileToLines()
        {
            List<string> _ret = new List<string>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string l;

                while((l = sr.ReadLine()) != null)
                {
                    _ret.Add(l);
                }
            }

            lines = _ret;
        }

        static string ListToStr(List<string> _list)
        {
            string toRet = "";
            foreach(string x in _list)
            {
                toRet += x;
            }
            return toRet;
        }

    }
}
