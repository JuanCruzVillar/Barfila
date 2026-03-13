using Application.Interfaces;
using MediatR;
using BCrypt.Net;

namespace Application.UseCases.Auth.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, string?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // buscar usuario por mail, si no existe null
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                return null;

            // comparar la pw guardada con el hash
            var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isValid)
                return null;

            // devuelve el token
            return _jwtService.GenerateToken(user);
        }
    }
}