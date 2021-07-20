using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

using htmlInterpreter.Debug;

namespace htmlInterpreter
{
    //Id: 40
    //*2 https://drive.google.com/file/d/1aqmFwocZJmv4CWa87QBIfj-K0lwtAJsR/view?usp=sharing
    public class CreateSolution
    {
        string Path;
        string ProjectName;
        string Extension;

        Debuger debuger;

        public CreateSolution(string _Path, string _ProjectName, string _Extension)
        {
            Path = _Path + _ProjectName + _Extension;
            ProjectName = _ProjectName;
            Extension = _Extension;

            //Create zip file with needed directories
            writeSolution();
        }

        /// <summary>
        /// <para>Creates a solution file at Path</para>
        /// </summary>
        private void writeSolution()
        {
            debuger.Log("Writing to solution");
            GZipStream zipOut = new GZipStream(File.OpenWrite(Path), CompressionMode.Compress);
            debuger.Log("Wrote to solution, closing");
            zipOut.Close();
            debuger.Log("Closed succesfully. Setting directories in solution");
            //Creates standard[TpVWC] directories inside of the solution
            setDirectories_Solution();
        }

        /// <summary>
        /// standard[TpVWC] directories inside of the solution in compressed format.
        /// </summary>
        private void setDirectories_Solution()
        {
            //Proj.json will be located inside the zip and not in any subdirectory of type (child of zip).
            //[EditorUI] is a directory located inside of the solution where cache files related to the editor will be stored.
            //[Pages] is a directory located inside of the solution where previews of webpages will be saved and cache relating M/W pages.

            //Specifies the solution file that we are trying to manipulate.
            using(FileStream openSolution = new FileStream(Path, FileMode.Open))
            {
                //Creates a ziparchive which allows us to write files in compressed format to solution.
                using(ZipArchive archive = new ZipArchive(openSolution, ZipArchiveMode.Update))
                {
                    debuger.Log("Writing files and directories (proj.json, EditorUI/, Pages/)");
                    //Creates directories [EditorUI, and Pages] inside of the solution in compressed format.
                    ZipArchiveEntry projJson = archive.CreateEntry("Proj.json");
                    ZipArchiveEntry dir_EditorUI = archive.CreateEntry("EditorUI/");
                    ZipArchiveEntry dir_Pages = archive.CreateEntry("Pages/");
                }
            }
        }
    }
}
