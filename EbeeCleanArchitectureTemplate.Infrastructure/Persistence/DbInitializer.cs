using EbeeCleanArchitectureTemplate.Domain.Entities;
using EbeeCleanArchitectureTemplate.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EbeeCleanArchitectureTemplate.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedRoles(this IServiceProvider serviceProvider, ILogger logger)
    {
        logger.LogInformation("Seeding roles...");
        var roleManager = serviceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        // Check if roles already exist
        if (await roleManager.Roles.AnyAsync())
        {
            logger.LogInformation("Roles already exist. Skipping seeding.");
            return;
        }

        // Get the list of roles from Roles enum
        var roles = Enum.GetValues(typeof(Roles))
            .Cast<Roles>()
            .Select(role => new IdentityRole(role.ToString()))
            .ToList();

        // Create each role
        foreach (var role in roles)
        {
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                logger.LogInformation("Role {RoleName} created successfully.", role.Name);
            }
            else
            {
                logger.LogError("Error creating role {RoleName}: {@Errors}",
                    role.Name, result.Errors);
            }
        }

        logger.LogInformation("Roles seeding completed.");
    }

    public static async Task SeedUsers(
        this IServiceProvider serviceProvider,
        IConfiguration configuration,
        ILogger logger)
    {
        logger.LogInformation("Seeding default users...");
        var userManager = serviceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        // Check if users already exist
        if (await userManager.Users.AnyAsync())
        {
            logger.LogInformation("Users already exist. Skipping seeding.");
            return;
        }

        // Get seed user from configuration
        var seedUser = configuration.GetSection(nameof(SeedUser)).Get<SeedUser>();
        if (seedUser is null)
        {
            logger.LogError("Seed user configuration is missing.");
            throw new InvalidOperationException("Seed user configuration is not set.");
        }

        // Create the admin user
        var admin = new ApplicationUser
        {
            FirstName = seedUser.FirstName,
            LastName = seedUser.LastName,
            Email = seedUser.Email,
            UserName = seedUser.Email,
        };

        var result = await userManager.CreateAsync(admin, seedUser.Password);
        if (result.Succeeded)
        {
            // Assign the user to the Admin role
            await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            logger.LogInformation("Default user '{Email}' created and assigned to role {RoleName}.",
                 admin.Email, Roles.Admin.ToString());
        }
        else
        {
            logger.LogError("Error creating default user: {@Errors}", result.Errors);
            throw new InvalidOperationException("Failed to create default user. See logs for details.");
        }
    }

}

internal class SeedUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
