using AccessControlSystem.Domain.Entities;
using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Infrastructure.Persistence;

public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
{
    public PermissionTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
