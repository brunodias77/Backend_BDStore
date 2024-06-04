using BDS.Communication.Responses.Users;
using MediatR;

namespace BDS.Application.Commands.Users.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseRegisterUserJson>
{
    public Task<ResponseRegisterUserJson> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}