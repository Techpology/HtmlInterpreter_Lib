using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Components
{
    //Id: 50
    public class Masterpage : File
    {
        //Path to create master page at
        public string Path { get; set; }
        public string Name { get; set; }
        //Framework

        //PreviewFiles
        public string PreviewPath { get; set; }
        public string PreviewTagJsonPath { get; set; }
        public string PreviewCssPath { get; set; }
        public string PreviewJsPath { get; set; }
        //StandardFiles
        public string StandardPath { get; set; }
        public string TagJsonPath { get; set; }
        public string CssPath { get; set; }
        public string JsPath { get; set; }

        /// <summary>
        /// <para>A custom object that allows the creation of templates for webpages.</para>
        /// Masterpages are used to set the designs and functionality which will follow all child webpages.
        /// </summary>
        /// <param name="_Path">Project path stored as a string for future exports and caching</param>
        public Masterpage()
        {
            CreatePreview();
            CreatePage();
        }

        /// <summary>
        /// Creates a preview file used to view the website inside of the editor.
        /// The preview file is stored in the project solution file.
        /// </summary>
        public void CreatePreview()
        {
            PreviewPath = "Pages/Preview/" + Name + "/" + Name + ".p.html";
            PreviewTagJsonPath = "Pages/Preview/" + Name + "/" + Name + ".json";
            PreviewCssPath = "Pages/Preview/" + Name + "/" + Name + ".css";
            PreviewJsPath = "Pages/Preview/" + Name + "/" + Name + ".js";
        }

        /// <summary>
        /// Creates a webpage of type Master inside the given directory for future export.
        /// This file is only updated when the user requests to save the project (ctrl+s).
        /// </summary>
        public void CreatePage()
        {
            StandardPath = "Pages/Master/" + Name + "/" + Name + ".m.html";
            TagJsonPath = "Pages/Master/" + Name + "/" + Name + ".json";
            CssPath = "Pages/Master/" + Name + "/" + Name + ".css";
            JsPath = "Pages/Master/" + Name + "/" + Name + ".js";
        }

        /// <summary>
        /// Adds tag into preview html and save query.
        /// Also assigns index in the form of ID to group multiple components, etc...
        /// </summary>
        public virtual void Add()
        {
            
        }

        /// <summary>
        /// Loops through given index Id and removes the component from preview and query.
        /// </summary>
        public void Remove()
        {

        }
    }
}
