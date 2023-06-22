using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using MovieService.Persistence.Repositories;

namespace MovieService.Persistence;

public interface IUnitOfWork
{
    public IDbContextTransaction StartTransaction();

    public IMovieRepository MovieRepository { get; }

    public IJobStateRepository JobStateRepository { get; }

    Task SaveChangesAsync();
}