using bouvet_fagkaffe_repository.Models;
using Microsoft.EntityFrameworkCore;

namespace bouvet_fagkaffe_repository.Context;

public class FagkaffeContext : DbContext
{
    public FagkaffeContext(DbContextOptions options) 
        :base(options){ }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Lecture> Lectures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .Property(c => c.Type)
            .HasConversion<string>();
        modelBuilder.Entity<Candidate>()
            .Property(c => c.Status)
            .HasConversion<string>();
        modelBuilder.Entity<Candidate>()
            .Property(c => c.Format)
            .HasConversion<string>();

        modelBuilder.Entity<Lecture>()
            .OwnsMany(l => l.MeetingLinks);
        modelBuilder.Entity<Lecture>()
            .Property(l => l.Status)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}
