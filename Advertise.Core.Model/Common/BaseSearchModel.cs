namespace Advertise.Core.Model.Common
{
    public abstract class BaseSearchModel : SearchModel
    {
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public string SortDirection { get; set; }
        public string SortMember { get; set; }
        public int Take { get; set; }
        public int TotalCount { get; set; }
    }
}