using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceBook
{
    public class Code
    {
        public int Deep { get; set; }

        public Dictionary<int, ICollection<Node>> Paths = new Dictionary<int, ICollection<Node>>(); 

        public void FindPaths(Graph graph, Node a, Node b)
        {
            var path = new List<Node>();
            path.Add(a); // путь

            var nodes = graph.GetNodes(a); // соседние узлы

            foreach (var node in nodes)
            {
                FindPathsInternal(graph, node, b, path, 0);
            }
        }

        private void FindPathsInternal(Graph graph, Node a, Node b, ICollection<Node> path, int deep)
        {
            if(a.Id == b.Id)
            {
                path.Add(b);
                Paths.Add(Paths.Count, path);
                return;
            }

            deep += 1;
            if (deep >= Deep)
            {
                return;
            }

            var newPath = new List<Node>(path);
            newPath.Add(a); // путь

            var nodes = graph.GetNodes(a); // соседние узлы
            foreach (var node in nodes.Where(n => !path.Any(i => i.Id == n.Id)))
            {
                FindPathsInternal(graph, node, b, newPath, deep);
            }
        }

        public class Graph
        {
            public Dictionary<Node, ICollection<Node>> Value { get; } = new Dictionary<Node, ICollection<Node>>()
            {
                { new Node(1), new List<Node>() { new Node(2), new Node(8) } },
                { new Node(2), new List<Node>() { new Node(3), new Node(1) } },
                { new Node(3), new List<Node>() { new Node(4), new Node(2) } },
                { new Node(4), new List<Node>() { new Node(5), new Node(3), new Node(8) } },
                { new Node(5), new List<Node>() { new Node(6), new Node(4) } },
                { new Node(6), new List<Node>() { new Node(7), new Node(5) } },
                { new Node(7), new List<Node>() { new Node(8), new Node(6) } },
                { new Node(8), new List<Node>() { new Node(1), new Node(7), new Node(4) } },
            };

            public IEnumerable<Node> GetNodes(Node node)
            {
                return Value[node];
            }
        }

        public class Node
        {
            public Node(int id)
            {
                Id = id;
            }
            public int Id { get; set; }

            public override bool Equals(object obj)
            {
                return Id == (obj as Node)?.Id;
            }

            public override int GetHashCode()
            {
                return Id;
            }

            public override string ToString()
            {
                return Id.ToString();
            }
        }
    }
}
