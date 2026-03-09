using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Actor
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Nationality { get; private set; }

        public string ProfileImagePath { get; private set; }

        private Actor() { }

        public Actor(string name, string lastName, DateTime dateOfBirth, string nationality, string profileImagePath)
        {
            Id = Guid.NewGuid();
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Nationality = nationality;
            ProfileImagePath = profileImagePath;
        }
    }
}
