using EbeeCleanArchitectureTemplate.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace EbeeCleanArchitectureTemplate.Application.Utilities;

public static class Pagination
{
    public static async Task<PagedResult<T>> PaginateAsync<T>(
        IQueryable<T> query,
        int pageNumber,
        int pageSize)
        where T : class
    {
        var result = new PagedResult<T>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = await query.CountAsync(),
            Items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync()
        };
        return result;
    }
}
