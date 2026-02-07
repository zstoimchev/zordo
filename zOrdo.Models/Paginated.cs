namespace zOrdo.Models;

public abstract class Paginated<T>
{
    public List<T> Items { get; set; }
    private int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    private int TotalPages { get; set; }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    protected Paginated(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}