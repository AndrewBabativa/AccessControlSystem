using MediatR;
using AccessControlSystem.Application.DTOs;

namespace AccessControlSystem.Application.Queries;

public class GetPermissionTypesQuery : IRequest<List<PermissionTypeDto>> { }
