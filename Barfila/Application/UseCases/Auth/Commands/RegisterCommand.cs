using MediatR;

namespace Application.UseCases.Auth.Commands
{
    public record RegisterCommand : IRequest<bool>
    {
        public string Name { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public DateTime DateOfBirth { get; init; }
    }
}