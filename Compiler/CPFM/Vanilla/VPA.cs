using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using htmlInterpreter.Compiler.CPFM;
using htmlInterpreter.Components;
using System.IO;
using htmlInterpreter.Interactive.RunTimeHandling;

namespace htmlInterpreter.Compiler.CPFM.Vanilla
{
    public static class VPA
    {
        [DllImport("vcp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getTags([MarshalAs(UnmanagedType.LPUTF8Str)] string _ToParse);
        static IntPtr cPtr;

        public static void _init(string _filePath)
        {
            lines       = new List<string>();
            filePath    = _filePath;

            /* --------------------------------------------------------- */

            Debug.Debuger.Log("\ngetting C pointer to initiate Vanilla parser");
            fileToLines();
            cPtr = getTags(ListToStr(lines));

            Debug.Debuger.Log($"\nC pointer grabbed succesfully: {(IntPtr)cPtr}");
        }

        static List<string> lines { get; set; }
        static string filePath { get; set; }

        struct _retFromC
        {
            public IntPtr tags;
            public uint sizeOfList;
        }

        struct _retToCS
        {
            public List<Tag> tags;
            public int sizeOfList;
        }

        public static List<Node> VanillaParse()
        {
            List<Node> _ret = new List<Node>();

            _retFromC _tempStructFromC = (_retFromC)Marshal.PtrToStructure(cPtr, typeof(_retFromC));

            Debug.Debuger.Log("\n" + _tempStructFromC.sizeOfList.ToString());

            _retToCS _toCs = new _retToCS();
            _toCs.tags = new List<Tag>();

            for (int i = 0; i < Convert.ToInt32(_tempStructFromC.sizeOfList); i++)
            {
                unsafe
                {
                    Debug.Debuger.Log("\n" + _tempStructFromC.tags + (i * sizeof(CTag.cTag)));
                    CTag.cTag _tempTag = (CTag.cTag)Marshal.PtrToStructure(_tempStructFromC.tags + (i * sizeof(CTag.cTag)), typeof(CTag.cTag));

                    byte[] arr = new byte[STMH.strlen(_tempTag.tagName)];
                    Marshal.Copy((IntPtr)_tempTag.tagName, arr, 0, STMH.strlen(_tempTag.tagName));
                    string _str_TagName = System.Text.Encoding.Default.GetString(arr);

                    /*if(_tempTag.args != null)
                    {
                        byte[] _arg = new byte[STMH.strlen(_tempTag.args)];
                        Marshal.Copy((IntPtr)_tempTag.args, arr, 0, STMH.strlen(_tempTag.args));
                        string _str_args = System.Text.Encoding.Default.GetString(arr);

                        Tag _Tag = new Tag();
                        _Tag.tagName = _str_TagName;
                        _Tag.args = _str_args;

                        _toCs.tags.Add(_Tag);
                        Node _tempNode = new Node(_Tag);

                        Debug.Debuger.Log("\nTag" + _Tag.tagName);
                        Debug.Debuger.Log("\nargument: " + _Tag.args);
                        _ret.Add(_tempNode);
                    }
                    else
                    {*/
                        Tag _Tag = new Tag();
                        _Tag.tagName = _str_TagName;

                        _toCs.tags.Add(_Tag);
                        Node _tempNode = new Node(_Tag);

                        Debug.Debuger.Log("\n" + _Tag.tagName);
                        _ret.Add(_tempNode);
                    //}


                }
            }

            Debug.Debuger.Log("\n length of ret" + _ret.Count.ToString());

            _Nodes_Active._CurrentNodes = _ret;
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
