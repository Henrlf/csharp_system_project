using Microsoft.AspNetCore.Mvc;
using Franco.Core.Controller;
using Franco.Sentry.Application.Auth.Query;
using MediatR;

namespace Franco.Sentry.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login/")]
    public async Task<IActionResult> Login([FromBody] UserLoginQuery query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // [HttpPost("validate/{token}")]
    // public async Task<IActionResult> Validate(string token, CancellationToken cancellationToken)
    // {
    //     var query = new TokenQuery
    //     {
    //         Token = token
    //     };
    //     var response = await _mediator.Send(query, cancellationToken);
    //
    //     if (!response.Success)
    //     {
    //         return BadRequest(response);
    //     }
    //
    //     return Ok(response);
    // }
}