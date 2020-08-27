using System;

namespace AssessmentApplication.Domain.Common
{
    public struct PagedResponse<T> : IEquatable<PagedResponse<T>>
        where T : class
    {
        public uint CurrentPage { get; set; }

        public T Data { get; set; }

        public uint PageSize { get; set; }

        public uint RecordCount { get; set; }

        public string SortBy { get; set; }

        public SortDirection SortDirection { get; set; }

        public static bool operator !=(PagedResponse<T> left, PagedResponse<T> right)
        {
            return !(left == right);
        }

        public static bool operator ==(PagedResponse<T> left, PagedResponse<T> right)
        {
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((PagedResponse<T>)obj);
        }

        public bool Equals(PagedResponse<T> value)
        {
            return Equals(CurrentPage, value.CurrentPage) &&
                Equals(RecordCount, value.RecordCount) &&
                Equals(PageSize, value.PageSize) &&
                Data == value.Data;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = (hash * 23) * CurrentPage.GetHashCode();
                hash = (hash * 23) ^ RecordCount.GetHashCode();
                hash = (hash * 23) ^ PageSize.GetHashCode();
                hash = (hash * 23) ^ (!string.IsNullOrWhiteSpace(SortBy) ? SortBy.GetHashCode() : 0);
                hash = (hash * 23) ^ SortDirection.GetHashCode();
                hash = (hash * 23) ^ (Data is object ? Data.GetHashCode() : 0);

                return hash;
            }
        }
    }
}
