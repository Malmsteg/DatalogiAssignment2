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
        /// <summary>
        /// Information is stored as a searchword representing a 
        /// search word, and filename and count as a tuple as searchresult
        /// Node Left is a child Node to the left
        /// Node Right is a child Node to the right
        /// </summary>
        /// <value></value>
        public string SearchWord { get; set; }
        public List<(string Filename, int count)> SearchResult { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}