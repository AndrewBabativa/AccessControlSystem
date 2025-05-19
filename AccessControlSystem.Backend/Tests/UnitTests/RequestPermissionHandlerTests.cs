using Xunit;
using Moq;
using AutoMapper;
using AccessControlSystem.Application.Commands;
using AccessControlSystem.Application.DTOs;
using AccessControlSystem.Domain.Entities;
using AccessControlSystem.Domain.Repositories;
using AccessControlSystem.Application.External;
using System.Threading.Tasks;
using System;
using System.Threading;

public class RequestPermissionHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreatePermissionAndReturnDto()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockElastic = new Mock<IElasticsearchService>();
        var mockKafka = new Mock<IKafkaProducer>();
        var mockMapper = new Mock<IMapper>();

        var command = new RequestPermissionCommand
        {
            EmployeeFirstName = "Juan",
            EmployeeLastName = "Pérez",
            PermissionTypeId = 1,
            PermissionDate = new DateTime(2024, 1, 1)
        };

        var permission = new Permission
        {
            Id = 1,
            EmployeeFirstName = command.EmployeeFirstName,
            EmployeeLastName = command.EmployeeLastName,
            PermissionTypeId = command.PermissionTypeId,
            PermissionDate = command.PermissionDate
        };

        var dto = new PermissionDto
        {
            Id = 1,
            EmployeeFirstName = "Andres",
            EmployeeLastName = "Babativa",
            PermissionDate = command.PermissionDate
        };

        mockMapper.Setup(m => m.Map<Permission>(command)).Returns(permission);
        mockMapper.Setup(m => m.Map<PermissionDto>(It.IsAny<Permission>())).Returns(dto);

        mockUnitOfWork.Setup(u => u.Permissions.AddAsync(It.IsAny<Permission>()));
        mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        var handler = new RequestPermissionHandler(
            mockUnitOfWork.Object,
            mockElastic.Object,
            mockKafka.Object,
            mockMapper.Object
        );

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(dto.Id, result.Id);
        Assert.Equal(dto.EmployeeFirstName, result.EmployeeFirstName);
    }
}
