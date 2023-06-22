using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MovieDomain.Common;
using MovieDomain.Entities;
using MovieDomain.DAL.Abstract;
using TaskService.Domain.TaskBaseInfo;
using TaskService.Domain.ServiceInterface;
using TaskService.Domain.Managers;

namespace TaskService.Domain.Tasks.PartialFeedTasks
{
    public sealed class FeedTopMovies : TaskBase
    {

        //----------------------------------------------------------------//

        private readonly ILoadDataService _loadDataService;
        private readonly ISessionFactory _sessionFactory;

        //----------------------------------------------------------------//

        public FeedTopMovies(TaskInfo info, IServiceProvider provider)
            : base(info, provider)
        {
            _loadDataService = provider.GetRequiredService<ILoadDataService>();
            _sessionFactory = provider.GetRequiredService<ISessionFactory>();
        }

        //----------------------------------------------------------------//

        public async override Task Execute(IDbConnection connection)
        {
            List<int> movieIds = null;

            while (movieIds?.Count != 0)
            {
                await SaveTopMoviePage(++option.LastPage, connection);
                Logger.Log.Info($"Page {option.LastPage} was loaded");
            }
        }

        //----------------------------------------------------------------//
        private async Task<List<int>> SaveTopMoviePage(int pageNumber, IDbConnection connection)
        {
            List<int> loadTopMovieIds = await _loadDataService.LoadTopMovieIdsByPage(pageNumber);
            foreach (int movieId in loadTopMovieIds)
            {
                await LoadMovieWithEntities(movieId, connection);
            }
            return loadTopMovieIds;
        }

        //----------------------------------------------------------------//

        public async Task LoadMovieWithEntities(int movieId, IDbConnection connection)
        {
            using(ISession session = _sessionFactory.CreateSession(connection))
            {
                FeedManager feedManager = new FeedManager(session, _serviceProvider);
                await feedManager.FeedMovieWithSubEntities(movieId);
                Logger.Log.Info($"Entities for movie with id {movieId} was loaded");
                session.SaveChanges();
            }
        }

    }
}
