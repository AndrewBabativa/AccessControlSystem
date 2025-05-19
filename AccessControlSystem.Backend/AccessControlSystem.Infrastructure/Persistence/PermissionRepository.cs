using AccessControlSystem.Domain.Entities;
using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Infrastructure.Persistence;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context) : base(context)
    {
    }

}
