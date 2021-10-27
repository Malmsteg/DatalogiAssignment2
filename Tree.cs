using System.Collections.Generic;

namespace DatalogiAssignment2
{
    public class Tree
    {
        //TODO: Methods Add, Remove. Maybe more?
        public Node Root { get; set; }

        /// <summary>
        /// Adds a new Node to the tree. If the tree is empty, sets the Node as Root
        /// </summary>
        /// <param name="searchword">The search word</param>
        /// <param name="searchResult">The result of the search</param>
        /// <returns>True if the Node was added, false if it failed</returns>
        public bool Add(string searchword, List<(string FileName, int count)> searchResult)
        {
            if (Root is null) // Sets input as Root if Root is empty.
            {
                Root = new Node(searchword, searchResult);
                return true;
            }





            return false;
        }

    }
}