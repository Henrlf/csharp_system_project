using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Franco.Core.Controller;

public class ControllerApi : ControllerBase
{
    private readonly List<string> _errors = [];

    private ActionResult CustomResponse(object? result = null)
    {
        if (IsOperationValid())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            {
                "Messages", _errors.ToArray()
            }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            AddError(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected bool IsOperationValid()
    {
        return _errors.Count == 0;
    }

    protected void AddError(string erro)
    {
        _errors.Add(erro);
    }

    protected void ClearErrors()
    {
        _errors.Clear();
    }

    protected string GetCustomerFromToken()
    {
        var customer = Request.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "cliente");

        if (customer == null)
        {
            throw new Exception("Não sei o que fazer ainda");
        }

        return customer.Value.ToString();
    }

    protected string GetKeyFromJwt(string key)
    {
        // TODO: REVER AVISOS!!!
        var handler = new JwtSecurityTokenHandler();
        string authHeader = Request.Headers["Authorization"];

        authHeader = authHeader.Replace("Bearer ", "");

        var jsonToken = handler.ReadToken(authHeader);
        var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

        return tokenS.Claims.First(claim => claim.Type == key).Value;
    }
}