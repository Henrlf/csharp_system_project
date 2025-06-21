using FluentValidation;
using Franco.Core.Dto.Messaging;
using Franco.Core.Enum;
using Franco.Sentry.Application.Auth.Dto;
using Franco.Sentry.Application.Auth.Query;
using Franco.Sentry.Application.Auth.Service;
using Franco.Sentry.Infra.Repository;
using MediatR;

namespace Franco.Sentry.Application.Auth.Handler;

public class AuthQueryHandler : IRequestHandler<UserLoginQuery, BaseResponse>
{
    private readonly IValidator<UserLoginQuery> _userLoginValidator;
    private readonly UserRepository _userRepository;
    private readonly JwtService _jwtTokenService;

    public AuthQueryHandler(IValidator<UserLoginQuery> userLoginValidator, UserRepository userRepository, JwtService jwtTokenService)
    {
        _userLoginValidator = userLoginValidator;
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<BaseResponse> Handle(UserLoginQuery query, CancellationToken cancellationToken)
    {
        var result = await _userLoginValidator.ValidateAsync(query, cancellationToken);

        if (!result.IsValid)
        {
            return new BaseResponse()
            {
                Success = false,
                Code = HttpCodeEnum.INVALID_DATA,
                Message = "",
                Errors = result.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var user = await _userRepository.GetByUsernameAsync(query.Username);

        if (user is null || !BCrypt.Net.BCrypt.Verify(query.Password, user.Password))
        {
            return new BaseResponse()
            {
                Success = false,
                Code = HttpCodeEnum.DATA_NOT_FINDED,
                Message = "Usrname or Password invalid!"
            };
        }
        
        var token = _jwtTokenService.GenerateToken(new AuthJwt
        {
            IdUser = user.Id,
            Name = user.Username
        });

        return new BaseResponse
        {
            Message = "Login successful",
            Data = token
        };
    }
}