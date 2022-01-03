using API.Interfaces;

namespace API.Models.Queries
{
    public class ReservationQuery : IQueryObject
    {
        public int? Genre { get; set; }
        public string Title { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}