using BDS.Application.Commands.Users.RegisterUser;
using BDS.Communication.Requests.Users;
using BDS.Communication.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BDS.Api.Controllers;

[Route("minha-conta")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    [HttpPost("cadastro")]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RequestRegisterUserJson request)
    {
        var result = await _mediator.Send(new RegisterUserCommand(request));
        return Created(string.Empty, result);
    }
}