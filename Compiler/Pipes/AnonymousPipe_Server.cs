using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace htmlInterpreter.Compiler.Pipes
{
    [Obsolete("Depricated, switching to C# based instead of using pipes with C based parser")]
    public static class AnonymousPipe_Server
    {
        public static void Main()
        {
            Process pipeClient = new Process();

            pipeClient.StartInfo.FileName = "lexer.exe";

            using (AnonymousPipeServerStream pipeServer = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
            {
                Console.WriteLine("[SERVER] Current TransmissionMode: {0}", pipeServer.TransmissionMode);

                pipeClient.StartInfo.Arguments = pipeServer.GetClientHandleAsString();
                pipeClient.StartInfo.UseShellExecute = false;
                pipeClient.Start();

                pipeServer.DisposeLocalCopyOfClientHandle();

                try
                {
                    using (StreamWriter sw = new StreamWriter(pipeServer))
                    {
                        sw.AutoFlush = true;

                        sw.WriteLine("SYNC");
                        pipeServer.WaitForPipeDrain();

                        Console.Write("[SERVER] Enter text: ");
                        sw.WriteLine(Console.ReadLine());
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("[SERVER] Error: {0}", e.Message);
                }

                pipeClient.WaitForExit();
                pipeClient.Close();
                Console.WriteLine("[SERVER] Client quit. Server terminating.");
            }
        }
    }
}
