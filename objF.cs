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
            using(StreamReader sr = new StreamReader(Path+Name+Extension))
            {
                string currentline = ""; 
                while ((currentline = sr.ReadLine())!=null) 
                {// Loops though all the lines and adds them to a single string
                    currentline += currentline+"\n";
                }
            }
            return alllines;
        }

        /// <summary>
        /// Return the line with the line number corresponding to the index given
        /// </summary>
        /// <param name="_index">line number to be returned</param>
        string getLineAtIndex(int _index)
        {
            using (StreamReader sr = new StreamReader(Path + Name + Extension))
            {
                int lineNum = 0;
                while (sr.ReadLine()!=null)
                {
                    //Checks if the index is the same as the line number
                    //if that is the case we return the line
                    if (lineNum == _index)
                    {
                        return sr.ReadLine();
                    }
                    lineNum++;
                }
            }
            //If the index is outside the the line numbers we return string with value null 
            return "Null";
        }
    }
}
