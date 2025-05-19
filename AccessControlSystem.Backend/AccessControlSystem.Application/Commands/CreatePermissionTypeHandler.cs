using MediatR;
using AutoMapper;
using AccessControlSystem.Application.DTOs;
using AccessControlSystem.Domain.Entities;
using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Application.Commands;

public class CreatePermissionTypeHandler : IRequestHandler<CreatePermissionTypeCommand, PermissionTypeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePermissionTypeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PermissionTypeDto> Handle(CreatePermissionTypeCommand request, CancellationToken cancellationToken)
    {
        var type = new PermissionType { Description = request.Description };

        await _unitOfWork.PermissionTypes.AddAsync(type);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PermissionTypeDto>(type);
    }
}
