using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace htmlInterpreter
{
    class Save
    {
        //Project solution path
        string path;
        public Save(string _path)
        {
            path = _path;
        }

        public void Save_Masterpage(List<Masterpage> Queue)
        {
            //Write to solution as [.m.html] file
            foreach (Masterpage mp in Queue)
            {
                //Specifies the solution file that we are trying to manipulate.
                using (FileStream openSolution = new FileStream(path, FileMode.Open))
                {
                    //Creates a ziparchive which allows us to write files in compressed format to solution.
                    using (ZipArchive archive = new ZipArchive(openSolution, ZipArchiveMode.Update))
                    {
                        //Creates directories [EditorUI, and Pages] inside of the solution in compressed format.
                        //ZipArchiveEntry dir_Pages = archive.CreateEntry("Pages/Master/" + mp.Name + ".m.html");

                        //Copy files from preview and save them to standard partition.
                        /*ZipArchiveEntry dir_Pages = archive.CreateEntry(mp.StandardPath);
                        ZipArchiveEntry dir_StandardJson = archive.CreateEntry(mp.TagJsonPath);
                        ZipArchiveEntry dir_StandardCss = archive.CreateEntry(mp.CssPath);
                        ZipArchiveEntry dir_StandardJs = archive.CreateEntry(mp.JsPath);*/

                        var result = from currentEntry in archive.Entries
                                     where Path.GetDirectoryName(currentEntry.FullName) == "Pages/Preview/" + mp.Name
                                     where !String.IsNullOrEmpty(currentEntry.Name)
                                     select currentEntry;

                        string test = "";
                        foreach (ZipArchiveEntry entry in result)
                        {
                            entry.ExtractToFile(Path.Combine(path + "/temp", entry.Name));
                            test = entry.Name;
                        }
                        Directory.CreateDirectory("F:/newtestFolder/" + "entry/");
                        File.WriteAllText("F:/newtestFolder/" + "entry/test.txt", test);
                    }
                }
            }
        }

        public void Save_PreviewMasterPage(List<Masterpage> Queue)
        {
            foreach (Masterpage mp in Queue)
            {
                //Specifies the solution file that we are trying to manipulate.
                using (FileStream openSolution = new FileStream(path, FileMode.Open))
                {
                    //Creates a ziparchive which allows us to write files in compressed format to solution.
                    using (ZipArchive archive = new ZipArchive(openSolution, ZipArchiveMode.Update))
                    {
                        //Creates directories [EditorUI, and Pages] inside of the solution in compressed format.
                        ZipArchiveEntry dir_PreviewPage = archive.CreateEntry(mp.PreviewPath);
                        ZipArchiveEntry dir_PreviewJson = archive.CreateEntry(mp.PreviewTagJsonPath);
                        ZipArchiveEntry dir_PreviewCss = archive.CreateEntry(mp.PreviewCssPath);
                        ZipArchiveEntry dir_PreviewJs = archive.CreateEntry(mp.PreviewJsPath);
                    }
                }
            }
        }



    }
}