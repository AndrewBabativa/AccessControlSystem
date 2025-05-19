using Microsoft.EntityFrameworkCore;
using AccessControlSystem.Domain.Entities;

namespace AccessControlSystem.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

    public DbSet<Permission> Permission => Set<Permission>();
    public DbSet<PermissionType> PermissionType => Set<PermissionType>();
}

