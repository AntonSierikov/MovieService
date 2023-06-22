using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using MovieService.Persistence.Repositories;

namespace MovieService.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieContext _movieContext;

    public IDbContextTransaction StartTransaction() => _movieContext.Database.BeginTransaction();


    public IMovieRepository MovieRepository { get; }

    public IJobStateRepository JobStateRepository { get; }

    public UnitOfWork(MovieContext movieContext)
    {
        _movieContext = movieContext;
        MovieRepository = new MovieRepository(movieContext);
        JobStateRepository = new JobStateRepositiory(movieContext);
    }

    public Task SaveChangesAsync() => _movieContext.SaveChangesAsync();
}