using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace htmlInterpreter.Compiler.CPFM
{
    public static class STMH
    {
        // Virgin strlen: long, heavy, made by a high level programmer
        /*
        public static unsafe int strlen(byte* ptrTarget)
        {
            int PtrLength = 0;
            char temp = '\0';

            while(true)
            {
                Encoding.UTF8.GetChars(ptrTarget, 1, &temp, 1);

                if (temp == '\0') break;

                PtrLength++;
                ptrTarget++;
            }

            return PtrLength;
        }
        */
        
        
        // Chad cStrLen B): Thank me later 
        public static unsafe int cStrLen(byte* str)
        {
            if (str == (byte*)0) return 0;
            byte *beg = str;
            while(*str != 0x0) str++;
            return (int)(str - beg);
        }

    }
}
