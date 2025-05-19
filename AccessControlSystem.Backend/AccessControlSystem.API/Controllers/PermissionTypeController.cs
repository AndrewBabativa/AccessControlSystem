using AccessControlSystem.Application.Commands;
using AccessControlSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetPermissionTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
