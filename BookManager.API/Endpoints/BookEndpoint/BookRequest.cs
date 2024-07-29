namespace BookManager.API.Endpoints.BookEndpoint
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string PublisherBook { get; set; }
        public int Edition { get; set; }
        public string YearPublication { get; set; }
        public IEnumerable<int> AuthorIds { get; set; }
        public IEnumerable<int> SubjectIds { get; set; }
    }
}
