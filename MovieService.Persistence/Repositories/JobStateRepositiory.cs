using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MovieService.Persistence.Entities;

namespace MovieService.Persistence.Repositories;

public class JobStateRepositiory : IJobStateRepository
{
    private readonly MovieContext _movieContext;

    public JobStateRepositiory(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    public async Task<TState?> GetStateAsync<TState>(string key)
    {
        var jobState = await _movieContext.JobStates.FirstOrDefaultAsync(p => p.JobId == key);
        return jobState is not null
            ? JsonSerializer.Deserialize<TState>(jobState.State)
            : default;
    }

    public Task UpsertAsync(string key, object @object)
    {
        return _movieContext.Upsert(new JobState()
        {
            JobId = key,
            State = JsonSerializer.Serialize(@object),
        }).RunAsync();
    }
}