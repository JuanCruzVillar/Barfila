
using System.Collections.Generic;


namespace Domain.Entities
{
    public class User
    {

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public string UserName { get; private set; }
        public string Email { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Password { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private User () { }

        public User (string name, string lastName, string userName, string email, string password, DateTime dateOfBirth)
        {
            Id = Guid.NewGuid();
            Name = name;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            CreatedAt = DateTime.UtcNow;


        }

        public void Update(string name, string lastName, string userName, string email)
        {
            Name = name;
            LastName = lastName;
            UserName = userName;
            Email = email;
        }
    }
}
