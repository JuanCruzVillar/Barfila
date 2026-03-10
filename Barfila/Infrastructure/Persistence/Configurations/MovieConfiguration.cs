using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Synopsis)
                .HasMaxLength(1000);

            builder.Property(m => m.PosterPath)
                .HasMaxLength(500);

            builder.HasMany(m => m.Genres)
                .WithMany()
                .UsingEntity("MovieGenres");

            builder.HasMany(m => m.Directors)
                .WithMany()
                .UsingEntity("MovieDirectors");

            builder.HasMany(m => m.Actors)
                .WithMany()
                .UsingEntity("MovieActors");
        }
    }
}