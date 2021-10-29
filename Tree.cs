using System.Collections.Generic;

namespace DatalogiAssignment2
{
    public class Tree
    {
        public Node Root { get; set; }

        /// <summary>
        /// Adds a new Node to the tree. If the tree is empty, sets the Node as
        /// Root
        /// </summary>
        /// <param name="searchword">The search word</param>
        /// <param name="searchResult">The result of the search</param>
        /// <returns>True if the Node was added, false if it failed</returns>
        public bool Add(
            string searchword, List<(string FileName, int count)> searchResult)
        {
            if (Root is null) // Sets input as Root if Root is empty.
            {
                Root = new Node(searchword, searchResult);
                return true;
            }
            Node current = Root, previous = new();
            while (current is not null)
            {
                switch (current.SearchWord.CompareTo(searchword))
                {
                    case 1:
                        previous = current;
                        current = current.Left;
                        break;
                    case 0:
                        return false; // Same searchword. Return false
                    case -1:
                        previous = current;
                        current = current.Right;
                        break;
                }
            }
            switch (previous.SearchWord.CompareTo(searchword))
            {
                case 1:
                    previous.Left = new Node(searchword, searchResult);
                    break;
                case -1:
                    previous.Right = new Node(searchword, searchResult);
                    break;
            }
            return true;
        }

        /// <summary>
        /// Returns all nodes under parameter node, together with the parameter
        /// node.
        /// </summary>
        /// <param name="node">The node to get all children nodes</param>
        /// <returns>The parameter node, and all of its children</returns>
        public List<Node> GetNodes(Node node)
        {
            List<Node> result = new() { node };
            List<Node> left = new(), right = new();
            if (node.Left is not null)
            {
                left = GetNodes(node.Left);
            }
            if (node.Right is not null)
            {
                right = GetNodes(node.Right);
            }

            for (int i = 0; i < left.Count; i++)
            {
                result.Insert(i, left[i]);
            }
            foreach (var item in right)
            {
                result.Add(item);
            }
            return result;
        }
    }
}