namespace AssessmentApplication.Domain.Common
{
    public abstract class PagedRequest
    {
        public uint Limit { get; set; } = 10;
        
        public uint Offset { get; set; } = 0;

        public string SortBy { get; set; }

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
