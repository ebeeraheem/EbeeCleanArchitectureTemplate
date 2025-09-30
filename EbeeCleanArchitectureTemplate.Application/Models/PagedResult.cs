namespace EbeeCleanArchitectureTemplate.Application.Models;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public string? SearchTerm { get; set; }
    public int TotalCount { get; set; }
    public int StartRecord => (CurrentPage - 1) * PageSize + 1;
    public int EndRecord => Math.Min(StartRecord + PageSize - 1, TotalCount);
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
