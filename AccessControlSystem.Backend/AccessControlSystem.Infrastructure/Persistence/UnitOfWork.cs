using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IPermissionRepository Permissions { get; }
    public IPermissionTypeRepository PermissionTypes { get; }

    public UnitOfWork(ApplicationDbContext context,
                      IPermissionRepository permissions,
                      IPermissionTypeRepository permissionTypes)
    {
        _context = context;
        Permissions = permissions;
        PermissionTypes = permissionTypes;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
