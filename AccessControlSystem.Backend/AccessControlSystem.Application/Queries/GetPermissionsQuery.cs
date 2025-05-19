using MediatR;
using AccessControlSystem.Application.DTOs;

namespace AccessControlSystem.Application.Queries;

public class GetPermissionsQuery : IRequest<List<PermissionDto>> { }
