using AssessmentApplication.Domain.Common;

namespace AssessmentApplication.Data.Requests
{
    public class PagedRequest
    {
        public int CurrentPage { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; }

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}