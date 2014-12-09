using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionaleKorePlaner
{
    public class Edge
    {
        public Node FromNode;
        public Node ToNode;
        public int Value;

        public Edge(Node fromNode,Node toNode,int value)
        {
            FromNode = fromNode;
            ToNode = toNode;
            Value = value;
        }
    }
}
