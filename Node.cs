using System.Collections.Generic;

namespace DatalogiAssignment2
{
    public class Node
    {

        /// <summary>
        /// Empty constructor, if needed
        /// </summary>
        public Node()
        { }

        /// <summary>
        /// Creates a Node that stores information of a search result
        /// </summary>
        /// <param name="searchWord">The word that was searched for</param>
        /// <param name="searchResult">The result of the search</param>
        public Node(string searchWord, List<(string Filename, int count)> searchResult)
        {
            SearchWord = searchWord;
            SearchResult = searchResult;
        }
        public string SearchWord { get; set; }
        public List<(string Filename, int count)> SearchResult { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}