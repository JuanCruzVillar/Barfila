using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Auth.Commands
{
    public record LoginCommand : IRequest<string?>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
