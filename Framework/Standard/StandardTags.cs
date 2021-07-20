using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Components
{
    public struct Tag
    {
        public int[] indexId  {get; set;}         // Used to access object through an id in its respective file or tree of <type>objects.
        public string tagName {get; set;}
        public string Text    {get; set;}         // Some items require a text argument, this is the value you would change to give it the argument.
        public string Style   {get; set;}         // For quick css styling with a html component, you can use this to insert css snipets.
        public string Class   {get; set;}         // To use seperate styling for an object (styling imported from a css object). This is used to assign classname.
        public string Id      {get; set;}         // For event handeling with JS and general web programming, use this to assign a usable JS Id.
        public string Src     {get; set;}         // Some html tags such as <img /> and <script /> require a src to access. Use this to assign a source path.
        public string href    {get; set;}         // Some html tags such as <a/> require a href which points them to a path or link of some sort.

        // We return a standard string of the set {tagName} such as for example:
        //      <html>,<img>,<h1>,etc...
        // The string is then returned to the node when needed. And the children nodes tag strings are also returned in to the respective root tag.
        // To properly create the objects into proper strings, we need to use {POST-ORDER TRAVERSEL} manner which in return creates the tags
        // into the script from child to parent, meaning no issues when it comes to subtree, etc... (child nodes who are internal nodes) (internal nodes = contain children)
        public override string ToString() => $"<{tagName} Style=\"{Style}\" Class=\"{Class}\" Id=\"{Id}\" src=\"{Src}\" href=\"{href}\">";
    }
}
