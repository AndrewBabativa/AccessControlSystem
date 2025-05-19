using MediatR;
using AccessControlSystem.Application.DTOs;

namespace AccessControlSystem.Application.Commands;

public class CreatePermissionTypeCommand : IRequest<PermissionTypeDto>
{
    public string Description { get; set; } = string.Empty;
}
