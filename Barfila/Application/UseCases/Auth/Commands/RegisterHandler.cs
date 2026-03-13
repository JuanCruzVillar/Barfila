using Application.Interfaces;
using Domain.Entities;
using MediatR;
using BCrypt.Net;

namespace Application.UseCases.Auth.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            
            var exists = await _userRepository.ExistsEmailAsync(request.Email);
            if (exists)
                return false;
            
            // la pw nunca toca la base de datos, directamente llega el hash
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            
            var user = new User(
                request.Name,
                request.LastName,
                request.UserName,
                request.Email,
                hashedPassword,
                request.DateOfBirth
            );

           
            await _userRepository.AddUserAsync(user);
            return true;
        }
    }
}