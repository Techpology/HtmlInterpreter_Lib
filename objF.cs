using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Components
{
    
    public class objF : Object
    {
        string Path { get; set; }
        string Type { get; set; }
        string Name { get; set; }
        string Extension { get; set; }

        
        ///<summary>
        /// Returns all lines in the file as a string
        ///</summary>
        string getAllLines()
        {
            string alllines = "";
            using(StreamReader sw = new StreamReader(Path+Name+Extension))
            {
                string currentline = ""; 
                while ((currentline = sw.ReadLine())!=null) 
                {// Loops though all the lines and adds them to a single string
                    currentline += currentline+"\n";
                }
            }
            return alllines;
        }

        string getLineAtIndex(int _index)
        {
            //TODO: getLineAtIndex      { Get all lines in current File into a single string }
            return "";
        }
    }
}
