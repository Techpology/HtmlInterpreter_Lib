using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlInterpreter.Components
{
    public struct Node      // This defines a node structure which will exist through out the tree data structure
    {
        public Node(Tag _tag, List<Node> _Children)
        {
            tag = _tag;
            Children = _Children;
        }

        public Tag tag  {get; set;}                 // Struct Tag is a standard type of tags with a set of data needed or usable for respective node.
        public List<Node> Children  {get; set;}     // List of children contained with in the node. (Also are seperate nodes (children))
    }

    class Tree : Obj
    {
        // Create a root Node which will contain the root tree on instance decleration.
        Node root;

        //Create root node on first call.
        public Tree(Tag _tag = new Tag(), List<Node> _Children = null)
        {
            root = new Node(_tag, _Children);
        }
    }
}
