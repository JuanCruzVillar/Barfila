

using System.Collections.Generic;


namespace Domain.Entities
{
    public class Director
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Nationality { get; private set; }

        public string ProfileImagePath { get; private set; }

        private Director () { }

        public Director (Guid id, string name, string lastName, DateTime dateOfBirth, string nationality, string profileImagePath)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Nationality = nationality;
            ProfileImagePath = profileImagePath;
        }
    }
}
