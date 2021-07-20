﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Components
{
    public struct Node              // This defines a node structure which will exist through out the tree data structure
    {
        public int[] IndexId;       // Used to access object through an id in its respective file or tree of <type>objects.
        public string Text;         // Some items require a text argument, this is the value you would change to give it the argument.
        public string Style;        // For quick css styling with a html component, you can use this to insert css snipets.
        public string Class;        // To use seperate styling for an object (styling imported from a css object). This is used to assign classname.
        public string Id;           // For event handeling with JS and general web programming, use this to assign a usable JS Id.
        public string Src;          // Some html tags such as <img /> and <script /> require a src to access. Use this to assign a source path.
        public string Href;         // Some html tags such as <a/> require a href which points them to a path or link of some sort.
        List<Node> Children;        // List of children contained with in the node. (Also are seperate nodes (children))
    }

    class Tree : Obj
    {
        // Create a root Node which will contain the root tree on instance decleration.
        Node root;

        Tree()
        {

        }
    }
}
