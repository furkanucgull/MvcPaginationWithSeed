namespace Gorev12.Data
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int EndPage { get; set; }
        public int StartPage { get; set; }

        public int PageCount => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
