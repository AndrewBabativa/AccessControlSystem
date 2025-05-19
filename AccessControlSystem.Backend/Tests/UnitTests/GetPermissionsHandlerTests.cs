using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AccessControlSystem.Domain.Repositories;  
using AccessControlSystem.Application.DTOs;      
using AccessControlSystem.Application.Queries;    


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
        var permissions = await _unitOfWork.Permissions.GetAllAsync();
        return _mapper.Map<List<PermissionDto>>(permissions);
    }
}
