using bouvet_fagkaffe_repository.Models;
using Microsoft.EntityFrameworkCore;

namespace bouvet_fagkaffe_repository.Context;

public class FagkaffeContext : DbContext
{
    public FagkaffeContext(DbContextOptions options) 
        :base(options){ }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(c =>
        {
            c.Property(c => c.Type)
            .HasConversion<string>();

            c.Property(c => c.Status)
            .HasConversion<string>();

            c.Property(c => c.Format)
            .HasConversion<string>();

            c.HasOne(c => c.ProposedBy);

            c.HasMany(c => c.RegisteredPresenters)
            .WithMany(u => u.RegisteredOnCandidates);

            c.HasMany(c => c.VotedOnBy)
            .WithMany(u => u.VotedOn);
        });


        modelBuilder.Entity<Lecture>(l =>
        {
            l.OwnsMany(l => l.MeetingLinks);

            l.Property(l => l.Status)
            .HasConversion<string>();

            l.HasMany(l => l.HeldBy)
            .WithMany(u => u.PresentsLectures);

            l.HasMany(l => l.Tags)
            .WithMany(t => t.UsedOn);
        });

        base.OnModelCreating(modelBuilder);
    }
}
