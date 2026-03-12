using Application.DTOs;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public record GetUserQuery : IRequest<UserDto>
    {
        public Guid UserId { get; init; }
    }
}