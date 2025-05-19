using MediatR;
using AutoMapper;
using AccessControlSystem.Application.DTOs;
using AccessControlSystem.Application.External;
using AccessControlSystem.Domain.Enums;
using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Application.Commands;

public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, PermissionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElasticsearchService _elasticService;
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IMapper _mapper;

    public ModifyPermissionHandler(
        IUnitOfWork unitOfWork,
        IElasticsearchService elasticService,
        IKafkaProducer kafkaProducer,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _elasticService = elasticService;
        _kafkaProducer = kafkaProducer;
        _mapper = mapper;
    }

    public async Task<PermissionDto> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _unitOfWork.Permissions.GetByIdAsync(request.Id);
        if (permission is null)
            throw new KeyNotFoundException($"Permission with ID {request.Id} not found.");

        permission.EmployeeFirstName = request.EmployeeFirstName;
        permission.EmployeeLastName = request.EmployeeLastName;
        permission.PermissionTypeId = request.PermissionTypeId;
        permission.PermissionDate = DateTime.UtcNow;

        _unitOfWork.Permissions.Update(permission);
        await _unitOfWork.SaveChangesAsync();

        var dto = _mapper.Map<PermissionDto>(permission);

        await _elasticService.IndexPermissionAsync(dto);

        var kafkaEvent = new PermissionEventDto
        {
            Id = Guid.NewGuid(),
            NameOperation = OperationType.Modify.ToString(),
            Timestamp = DateTime.UtcNow
        };

        await _kafkaProducer.SendMessageAsync(kafkaEvent);

        return dto;
    }
}
