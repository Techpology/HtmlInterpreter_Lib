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
            childID = 0;
            tag = _tag;
            Children = _Children;
            childrenMap = new Dictionary<string, Node>();
        }

        int childID;
        public Tag tag  {get; set;}                 // Struct Tag is a standard type of tags with a set of data needed or usable for respective node.
        public List<Node> Children  {get; set;}     // List of children contained with in the node. (Also are seperate nodes (children))

        // We create a dictionary (Equivalent of a hash map in JAVA), where we store each nodes children IDs.
        // By doing this, we can handle more complex situtions easier, such as:
        //      Removing a child, getting a child.
        Dictionary<string, Node> childrenMap;

        // We insert a new child and add it to the dictionary with id from (string)incrementID().
        public void insert(Node _Child)
        {
            childrenMap.Add(incrementId(), _Child);
            updateChildren();
        }

        // We return an incremental value based on a number offseted from 0 {0,1,2,3,4...}
        string incrementId()
        {
            childID++;
            return childID.ToString();
        }

        public void remove(string _childId)
        {
            childrenMap.Remove(_childId);
            updateChildren();
        }

        public void updateChildren()
        {
            Children.Clear();

            foreach (Node item in childrenMap.Values)
            {
                Children.Add(item);
            }
        }
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
