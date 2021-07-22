using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Reflection;

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
                    dir_PreviewPage.Delete();
                    dir_PreviewPage = archive.CreateEntry(_path);
                    using(StreamWriter writer = new StreamWriter(dir_PreviewPage.Open()))
                    {
                        writer.BaseStream.Seek(0, SeekOrigin.End);
                        writer.WriteLine((string)_Package);
                    }
                    dir_PreviewPage.LastWriteTime = DateTimeOffset.UtcNow.LocalDateTime;
                }
            }
        }

        // Standard solution layout
        //      -Pages
        //          -Preview    *On update (Realtime)
        //              -PageNames[]->(Files)
        //          -Main       *On start (Saved data)
        //              -PageNames[]->(Files)
        // Extracts solution to same directory, deletes solution, copies previews to main, recompresses directory
        public static void Save_FromPreview()
        {
            using(FileStream openSolution = new FileStream(path + solutionName + solutionExtension, FileMode.Open))
            {
                using(ZipArchive archive = new ZipArchive(openSolution, ZipArchiveMode.Update))
                {
                    // Creating folder to extract file to from zip
                    Directory.CreateDirectory($"{path}/{solutionName}");
                    // Extracting files from zip to directory
                    archive.ExtractToDirectory($"{path}/{solutionName}");
                }
            }

            // Delete zip
            File.Delete(path + solutionName + solutionExtension);

            // Copy files in preview folder in to a new folder named main in the same directory as the preview folder
            if (!Directory.Exists($"{path}/{solutionName}/Pages/Main"))
            {
                Directory.CreateDirectory($"{path}/{solutionName}/Pages/Main");
            }

            foreach (var file in Directory.GetFiles($"{path}/{solutionName}/Pages/Preview"))
            {
                File.Copy(file, $"{path}/{solutionName}/Pages/Main",true);
            }

            // Compress directory to zip form again
            ZipFile.CreateFromDirectory($"{path}/{solutionName}", $"{path}/{solutionName}{solutionExtension}", CompressionLevel.Fastest, false);

        }
    }
}