using MediatR;
using AccessControlSystem.Application.DTOs;

namespace AccessControlSystem.Application.Commands;

public class ModifyPermissionCommand : IRequest<PermissionDto>
{
    public int Id { get; set; }
    public string EmployeeFirstName { get; set; } = string.Empty;
    public string EmployeeLastName { get; set; } = string.Empty;
    public int PermissionTypeId { get; set; }
}
