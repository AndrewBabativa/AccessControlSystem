using AccessControlSystem.Application.DTOs;

namespace AccessControlSystem.Application.External;

public interface IElasticsearchService
{
    Task IndexPermissionAsync(PermissionDto permission);
}
