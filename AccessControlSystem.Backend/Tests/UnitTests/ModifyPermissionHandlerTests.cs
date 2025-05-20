using Xunit;
using Moq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using AccessControlSystem.Application.Commands;
using AccessControlSystem.Application.DTOs;
using AccessControlSystem.Application.External;
using AccessControlSystem.Domain.Entities;
using AccessControlSystem.Domain.Repositories;
using System;
using System.Collections.Generic;

public class ModifyPermissionHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IElasticsearchService> _elasticServiceMock;
    private readonly Mock<IKafkaProducer> _kafkaProducerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ModifyPermissionHandler _handler;

    public ModifyPermissionHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _elasticServiceMock = new Mock<IElasticsearchService>();
        _kafkaProducerMock = new Mock<IKafkaProducer>();
        _mapperMock = new Mock<IMapper>();

        _handler = new ModifyPermissionHandler(
            _unitOfWorkMock.Object,
            _elasticServiceMock.Object,
            _kafkaProducerMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsPermissionDto()
    {
        var command = new ModifyPermissionCommand
        {
            Id = 1,
            EmployeeFirstName = "John",
            EmployeeLastName = "Doe",
            PermissionTypeId = 2
        };

        var permission = new Permission
        {
            Id = command.Id,
            EmployeeFirstName = "OldName",
            EmployeeLastName = "OldLastName",
            PermissionTypeId = 1,
            PermissionDate = DateTime.Today.AddDays(-1)
        };

        var dto = new PermissionDto
        {
            Id = permission.Id,
            EmployeeFirstName = command.EmployeeFirstName,
            EmployeeLastName = command.EmployeeLastName,
            PermissionTypeId = command.PermissionTypeId
        };

        _unitOfWorkMock.Setup(x => x.Permissions.GetByIdAsync(command.Id))
                       .ReturnsAsync(permission);

        _mapperMock.Setup(x => x.Map<PermissionDto>(permission))
                   .Returns(dto);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(command.EmployeeFirstName, result.EmployeeFirstName);
        Assert.Equal(command.EmployeeLastName, result.EmployeeLastName);
        Assert.Equal(command.PermissionTypeId, result.PermissionTypeId);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _elasticServiceMock.Verify(x => x.IndexPermissionAsync(dto), Times.Once);
        _kafkaProducerMock.Verify(x => x.SendMessageAsync(It.IsAny<PermissionEventDto>()), Times.Once);
    }

    [Fact]
    public async Task Handle_PermissionNotFound_ThrowsKeyNotFoundException()
    {
        var command = new ModifyPermissionCommand { Id = 99 };
        _unitOfWorkMock.Setup(x => x.Permissions.GetByIdAsync(command.Id))
                       .ReturnsAsync((Permission?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None)
        );
    }
}
