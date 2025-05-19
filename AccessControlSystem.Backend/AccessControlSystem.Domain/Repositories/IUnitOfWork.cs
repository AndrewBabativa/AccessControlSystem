namespace AccessControlSystem.Domain.Repositories;

public interface IUnitOfWork
{
    IPermissionRepository Permissions { get; }
    IPermissionTypeRepository PermissionTypes { get; }
    Task<int> SaveChangesAsync(); 
}
