using Hangfire;
using Hangfire.Common;

namespace MovieService.Jobs;

public class SyncMovieDataJob : BackgroundJob
{
    public SyncMovieDataJob(string id, Job job, DateTime createdAt)
        : base(id, job, createdAt)
    {
    }

    public SyncMovieDataJob(string id, Job job, DateTime createdAt, IReadOnlyDictionary<string, string> parametersSnapshot)
        : base(id, job, createdAt, parametersSnapshot)
    {
    }

    public void Execute()
    {
        
    }
}