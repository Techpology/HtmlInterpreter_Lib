using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

// Std Memory Handler
namespace htmlInterpreter.Compiler.CPFM
{
    public static class STMH
    {

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

    }
}
