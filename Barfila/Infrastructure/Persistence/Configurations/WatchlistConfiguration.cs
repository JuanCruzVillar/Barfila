using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
    {

        public void Configure(EntityTypeBuilder<Watchlist> builder)
        {
            builder.HasKey(um => um.Id);

            builder.Property(um => um.AddedAt)
                .IsRequired();

          

            builder.HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(um => um.Movie)
                .WithMany()
                .HasForeignKey(um => um.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
