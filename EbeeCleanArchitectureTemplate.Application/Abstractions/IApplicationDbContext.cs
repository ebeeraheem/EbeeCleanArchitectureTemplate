using EbeeCleanArchitectureTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EbeeCleanArchitectureTemplate.Application.Abstractions;
public interface IApplicationDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
