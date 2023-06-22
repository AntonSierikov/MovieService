using Microsoft.EntityFrameworkCore;
using MovieService.Persistence.Entities;

namespace MovieService.Persistence;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
    {
        
    }

    public DbSet<ActorDal> Actors { get; set; }

    public DbSet<MovieDal> Movies { get; set; }

    public DbSet<CastDal> Casts { get; set; }

    public DbSet<MovieMarkDal> MovieMarks { get; set; }

    public DbSet<MovieReviewDal> MovieReviews { get; set; }

    public DbSet<UserDal> Users { get; set; }

    public DbSet<JobState> JobStates { get; set; }

    public DbSet<GenreDal> Genres { get; set; }

    public DbSet<MovieGenreDal> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MovieMarkDal>()
            .HasKey(a => new {a.MovieId, a.UserId});
        modelBuilder.Entity<CastDal>()
            .HasOne(x => x.Actor)
            .WithMany(x => x.Casts);
        modelBuilder.Entity<CastDal>()
            .HasOne(x => x.Movie)
            .WithMany(x => x.Casts);
        modelBuilder.Entity<MovieDal>()
            .Property(x => x.Id)
            .ValueGeneratedNever();
        modelBuilder.Entity<ActorDal>()
            .Property(x => x.Id)
            .ValueGeneratedNever();
        modelBuilder.Entity<CastDal>()
            .Property(x => x.Id)
            .ValueGeneratedNever();
        modelBuilder.Entity<MovieReviewDal>()
            .Property(x => x.ReviewId)
            .ValueGeneratedNever();
        modelBuilder.Entity<MovieDal>()
            .HasIndex(x => x.MovieExtraId)
            .IsUnique();
        modelBuilder.Entity<ActorDal>()
            .HasIndex(x => x.ExtraId)
            .IsUnique();
        modelBuilder.Entity<CastDal>()
            .HasIndex(x => x.ExtraId)
            .IsUnique();
        modelBuilder.Entity<GenreDal>()
            .HasIndex(x => x.Genre)
            .IsUnique();
        modelBuilder.Entity<MovieGenreDal>()
            .HasKey(g => new {g.MovieId, g.GenreId});
        modelBuilder.Entity<GenreDal>()
            .Property(g => g.GenreId)
            .ValueGeneratedNever();
        modelBuilder.Entity<MovieGenreDal>()
            .Property(g => g.GenreId)
            .ValueGeneratedNever();
        modelBuilder.Entity<MovieGenreDal>()
            .Property(g => g.MovieId)
            .ValueGeneratedNever();
    }
}