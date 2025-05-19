using AccessControlSystem.Application.Commands;
using AccessControlSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("modify")]
    public async Task<IActionResult> ModifyPermission([FromBody] ModifyPermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        var query = new GetPermissionsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
