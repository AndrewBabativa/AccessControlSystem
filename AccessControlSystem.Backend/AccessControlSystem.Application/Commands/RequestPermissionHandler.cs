using MediatR;
using AutoMapper;
using AccessControlSystem.Application.DTOs;
using AccessControlSystem.Application.External;
using AccessControlSystem.Domain.Entities;
using AccessControlSystem.Domain.Enums;
using AccessControlSystem.Domain.Repositories;

namespace AccessControlSystem.Application.Commands;

public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand, PermissionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElasticsearchService _elasticService;
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IMapper _mapper;

    public RequestPermissionHandler(
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

    public async Task<PermissionDto> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new Permission
        {
            EmployeeFirstName = request.EmployeeFirstName,
            EmployeeLastName = request.EmployeeLastName,
            PermissionTypeId = request.PermissionTypeId,
            PermissionDate = DateTime.UtcNow
        };

        await _unitOfWork.Permissions.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();

        var dto = _mapper.Map<PermissionDto>(permission);

        await _elasticService.IndexPermissionAsync(dto);

        var kafkaEvent = new PermissionEventDto
        {
            Id = Guid.NewGuid(),
            NameOperation = OperationType.Request.ToString(),
            Timestamp = DateTime.UtcNow
        };

        await _kafkaProducer.SendMessageAsync(kafkaEvent);

        return dto;
    }
}
