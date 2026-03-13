using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.UseCases.Users.Commands
{
    public record DeleteUserCommand : IRequest<bool>
    {
        public Guid UserId { get; init; }
    }
}