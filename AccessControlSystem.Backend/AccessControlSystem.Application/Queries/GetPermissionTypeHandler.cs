using MediatR;
using AccessControlSystem.Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Application.Queries;

public class GetPermissionTypesHandler : IRequestHandler<GetPermissionTypesQuery, List<PermissionTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPermissionTypesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PermissionTypeDto>> Handle(GetPermissionTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _unitOfWork.PermissionTypes
            .GetAll()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<PermissionTypeDto>>(types);
    }
}
