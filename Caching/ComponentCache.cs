using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace htmlInterpreter
{
    //Id: 20
    //*2 https://drive.google.com/file/d/1isbaNi8V5JksCGyVtwIgzynncEWxV4cl/view?usp=sharing {DEPRICATED}
    [Obsolete("ComponentCache is depricated. It was part of the old system of caching which has now been replaced by ()")]
    public class ComponentCache
    {
        //Component which was added. [draggable]
        Object Component;

        /// <summary>
        /// <para>A caching system which uses json temp files to store components data instead of saving all data to memmory.</para>
        /// Works faster with more complex designs and allows for easier project integration later on as well
        /// </summary>
        /// <param name="_Component">The component which was dragged or added to queue</param>
        public ComponentCache(Object _Component)
        {
            Component = _Component;
        }

        //Checks if component dragged is of type tag.
        private void Component_isTag()
        {
            if (Component.GetType().ToString() == "")
            {
                //if true, send to project solution to write into M/W page.json.
                //And write changes to M/W pagePreview.html in side the project solution.
            }
            else
            {
                //if false, return; Error 2001 *2
                //Could not write write component to source. Type of component was invalid.
            }
        }
    }
}
