
using System.Collections.Generic;



namespace Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        private Genre() { }

        public Genre(Guid id, string name)
        {

            Id = id;

            Name = name;
        }
    }

}

