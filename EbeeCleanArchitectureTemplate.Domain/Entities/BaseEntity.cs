using System.ComponentModel.DataAnnotations;

namespace EbeeCleanArchitectureTemplate.Domain.Entities;
public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(450)]
    public string CreatedBy { get; set; } = "System";
    public DateTime? UpdatedAt { get; set; }

    [MaxLength(450)]
    public string? UpdatedBy { get; set; }
}
