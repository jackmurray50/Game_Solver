using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver.GameTree
{
    public class GameTree<T>
    {
        public GameTree(Node<T> _root){
            root = _root;
            cur = root;
        }
        public Node<T> root;
        public Node<T> cur;
        int depth = 0;
        public void AddToNode(Node<T> _parent, Node<T> newNode)
        {
            newNode.parent = _parent;
            if(_parent.children is null)
            {
                _parent.children = new List<Node<T>>();
            }
            _parent.children.Add(newNode);
        }



        public string DepthFirstTraverse()
        {
            return BFT(root);
        }
        private string BFT(Node<T> n)
        {
            string output = "";
            if(!(n is null))
            {
                if (!(n.children is null))
                {
                    foreach (var c in n.children)
                    {
                        output += BFT(c);
                    }
                }
            }
            return output += n.data.ToString();
        }


    }
    public class Node<T>
    {
        public Node<T> parent { get; set; }
        public List<Node<T>> children;
        public T data;
        public Node(Node<T> _parent, T _data)
        {
            data = _data;
            parent = _parent;
        }
        public Node(T _data)
        {
            data = _data;
            parent = null;
        }
    }
}
