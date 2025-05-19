using MediatR;
using AccessControlSystem.Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Application.Queries;

public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPermissionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _unitOfWork.Permissions
            .GetAll()
            .Include(p => p.PermissionType)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<PermissionDto>>(permissions);
    }
}
