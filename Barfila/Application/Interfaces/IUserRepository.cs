using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository 
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user); // devolver el objeto completo ya q la entidad tiene todo lo q necesita dentro

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(Guid id);

        Task<bool> ExistsEmailAsync(string email);






    }
}
