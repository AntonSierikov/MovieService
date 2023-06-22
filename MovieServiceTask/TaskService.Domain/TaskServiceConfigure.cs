using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MovieDomain.Constans;
using MovieDomain.Common.Infrastructure;
using MovieDomain.DAL.Abstract;
using MovieDomain.DAL.Factories;
using TaskService.Domain.Service;
using TaskService.Domain.ServiceInterface;

namespace TaskService.Domain
{
    public class TaskServiceConfigure 
    {
        private readonly IServiceCollection _serviceCollection;

        //----------------------------------------------------------------//

        public IServiceProvider Provider
        {
            get { return _serviceCollection.BuildServiceProvider(); }
        }

        //----------------------------------------------------------------//

        public TaskServiceConfigure(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        //----------------------------------------------------------------//

        public void ConfigureDependencies(string pathFolder, string fileName)
        {
            _serviceCollection.AddSingleton(c => new ConfigurationFactory(pathFolder, fileName));
            _serviceCollection.AddSingleton<ISessionFactory, SessionFactory>(p => new SessionFactory(p.GetRequiredService<ConfigurationFactory>().GetConnectionString(ConfigurationConstans.MainDb)));
            _serviceCollection.AddSingleton<IQueryFactory, QueryFactory>();
            _serviceCollection.AddSingleton<ICommandFactory, CommandFactory>();
            _serviceCollection.AddSingleton<ILoadDataService, LoadDataServiceTmdb>();
            _serviceCollection.AddSingleton<ICinemaTaskService, CinemaTaskService>();
            _serviceCollection.AddTransient<IDbConnection, SqlConnection>(p => new SqlConnection(p.GetRequiredService<ConfigurationFactory>().GetConnectionString(ConfigurationConstans.MainDb)));
        }


        //----------------------------------------------------------------//



    }
}
