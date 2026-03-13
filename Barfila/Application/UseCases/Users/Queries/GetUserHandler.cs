using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            if (user == null)
                return null;

            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName
            };
        }
    }
}