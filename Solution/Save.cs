using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace htmlInterpreter.Components
{
    public static class Save
    {
        //Project solution path
        public static string path = "";
        public static string solutionName = "";
        public static string solutionExtension = "";

        public static void Save_Masterpage(List<Masterpage> Queue)
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
                        System.IO.File.WriteAllText("F:/newtestFolder/" + "entry/test.txt", test);
                    }
                }
            }
        }

        public static void Save_PreviewMasterPage(List<Masterpage> Queue)
        {
            foreach (Masterpage mp in Queue)
            {
                //Specifies the solution file that we are trying to manipulate.
                using (FileStream openSolution = new FileStream(path + solutionName + solutionExtension, FileMode.Open))
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

        public static void Save_intoPreview(Object _Package, string _path)
        {
            //Specifies the solution file that we are trying to manipulate.
            using (FileStream openSolution = new FileStream(path +solutionName + solutionExtension, FileMode.Open))
            {
                //Creates a ziparchive which allows us to write files in compressed format to solution.
                using (ZipArchive archive = new ZipArchive(openSolution, ZipArchiveMode.Update))
                {
                    //Creates directories [EditorUI, and Pages] inside of the solution in compressed format.
                    ZipArchiveEntry dir_PreviewPage = archive.GetEntry(_path);
                    using(StreamWriter writer = new StreamWriter(dir_PreviewPage.Open()))
                    {
                        writer.BaseStream.Seek(0, SeekOrigin.End);
                        writer.WriteLine((string)_Package);
                    }
                    dir_PreviewPage.LastWriteTime = DateTimeOffset.UtcNow.LocalDateTime;
                }
            }
        }

    }
}