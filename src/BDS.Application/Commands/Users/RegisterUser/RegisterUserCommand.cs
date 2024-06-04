using BDS.Communication.Requests.Users;
using BDS.Communication.Responses.Users;
using MediatR;

namespace BDS.Application.Commands.Users.RegisterUser;

public class RegisterUserCommand : IRequest<ResponseRegisterUserJson>
{
    public RegisterUserCommand(RequestRegisterUserJson request)
    {
        Request = request;
    }

    public RequestRegisterUserJson Request { get; set; }
}