using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using htmlInterpreter.Debug;
using htmlInterpreter.Components;

namespace htmlInterpreter.Caching
{
    //Id: 70
    //*2 https://drive.google.com/file/d/1v3lS3j4NBXnTDO1yJsc2uvXlliJgprTQ/view?usp=sharing
    public static class STACS
    {
        public static void saveMaster(Masterpage _mp)
        {
            // Root html to string
            ObjQ.insert(write_RecursiveTree(_mp.tree.root), _mp.PreviewPath);
        }

        // recursivly run through the root node to every child and right parent then child until all are written, then work your way back
        static string write_RecursiveTree(Node _n)
        {
            //string nodeTree_Text = "<!DOCTYPE html>";
            string nodeTree_Text = " ";

            if (_n.Children.Count > 0)
            {
                nodeTree_Text += $"\n{_n.tag.ToString()}";
                foreach (Node nodeChild in _n.Children)
                {
                    nodeTree_Text += "\n" + write_RecursiveTree(nodeChild);
                }
                nodeTree_Text += $"\n</{_n.tag.tagName}>";
            }
            else
            {
                nodeTree_Text += $"\n{_n.tag.ToString()}</{_n.tag.tagName}>";
            }

            return nodeTree_Text;
        }
    }
}
