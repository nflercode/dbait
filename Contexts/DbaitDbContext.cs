using Microsoft.EntityFrameworkCore;
using DbaitArgue.DbModels;

namespace DbaitArgue.Contexts;

public class DbaitDbContext : DbContext
{
    public DbaitDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Opinion> Opinions => Set<Opinion>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Response> Responses => Set<Response>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // User
        builder.Entity<User>(b =>
        {
            b.Property(p => p.Name)
                .HasMaxLength(30)
                .IsRequired();

            b.Property(p => p.Password)
                .IsRequired();

            b.Property(p => p.Email)
                .HasMaxLength(60);
        });

        builder.Entity<User>()
            .HasOne(b => b.Author)
            .WithOne(b => b.User)
            .HasForeignKey<Author>(b => b.UserId);

        // Opinions
        builder.Entity<Opinion>(b =>
        {
            b.Property(p => p.Title)
                .HasMaxLength(250)
                .IsRequired();

            b.Property(p => p.Content)
                .HasMaxLength(3000)
                .IsRequired();
        });

        builder.Entity<Opinion>()
            .HasOne(b => b.Author)
            .WithMany(b => b.Opinions)
            .HasForeignKey(b => b.AuthorId)
            .IsRequired();

        builder.Entity<Opinion>()
            .HasMany(b => b.Responses)
            .WithOne(b => b.Opinion)
            .HasForeignKey(b => b.OpinionId)
            .IsRequired();

        // Author
        builder.Entity<Author>(b =>
        {
            b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Entity<Author>()
            .HasOne(b => b.User)
            .WithOne(b => b.Author)
            .IsRequired();

        builder.Entity<Author>()
            .HasMany(b => b.Opinions)
            .WithOne(b => b.Author)
            .IsRequired();

        builder.Entity<Author>()
            .HasMany(b => b.Responses)
            .WithOne(b => b.Author)
            .IsRequired();

        // Response
        builder.Entity<Response>(b =>
        {
            b.Property(p => p.Content)
                .HasMaxLength(3000)
                .IsRequired();
        });

        builder.Entity<Response>()
            .HasOne(b => b.Author)
            .WithMany(b => b.Responses)
            .HasForeignKey(b => b.AuthorId)
            .IsRequired();

        builder.Entity<Response>()
            .HasOne(b => b.Opinion)
            .WithMany(b => b.Responses)
            .HasForeignKey(b => b.OpinionId)
            .IsRequired();
    }
}