namespace MovieService.Persistence.Repositories;

public interface IJobStateRepository
{
    Task<TState?> GetStateAsync<TState>(string key);

    Task UpsertAsync(string key, object @object);
}