namespace AssessmentApplication.Domain.Common
{
    public abstract class PagedRequest
    {
        public uint CurrentPage { get; set; } = 1;

        public uint PageSize { get; set; } = 10;

        public string SortBy { get; set; }

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
