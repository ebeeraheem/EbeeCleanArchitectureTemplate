using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EbeeCleanArchitectureTemplate.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(450)]
    public string CreatedBy { get; set; } = "System";
    public DateTime? UpdatedAt { get; set; }

    [MaxLength(450)]
    public string? UpdatedBy { get; set; }
}
