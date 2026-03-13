using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.UseCases.Users.Commands
{
    public record UpdateUserCommand : IRequest<bool>
    {
        public Guid UserId { get; init; }
        public string Name { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
    }
}
