using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MovieDomain.Entities;
using MovieDomain.DAL.Abstract;
using MovieDomain.DAL.IQueries;
using MovieDomain.DAL.ICommands;
using TaskService.Domain.TaskBaseInfo;
using TaskService.Domain.ServiceInterface;
using MovieTaskFactory = TaskService.Domain.Factories.TaskFactory;


namespace TaskService.Domain.Service
{
    public class CinemaTaskService : ICinemaTaskService
    {
        private readonly IQueryFactory _queryFactory;
        private readonly ICommandFactory _commandFactory;
        private readonly ISessionFactory _sessionFunc;
        private readonly IServiceProvider _serviceProvider;

        //----------------------------------------------------------------//

        public CinemaTaskService(IServiceProvider provider)
        {
            _queryFactory = provider.GetRequiredService<IQueryFactory>();
            _commandFactory = provider.GetRequiredService<ICommandFactory>();
            _sessionFunc = provider.GetRequiredService<ISessionFactory>();
            _serviceProvider = provider;
        }

        //----------------------------------------------------------------//

        public async Task RunTasks()
        {

            IDbConnection connection = _serviceProvider.GetRequiredService<IDbConnection>();
            connection.Open();
            using (connection)
            {
                try
                {
                    ITaskQuery taskQuery = _queryFactory.CreateQuery<ITaskQuery>(connection);
                    IEnumerable<TaskInfo> enumeration = taskQuery.GetTaskForRun();
                    List<Task> tasks = new List<Task>();
                    foreach (TaskInfo info in enumeration)
                    {
                        TaskBase taskBase = MovieTaskFactory.CreateTask(info, _serviceProvider);
                        tasks.Add(RunTask(taskBase, connection));
                    }

                    await Task.WhenAll(tasks);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //----------------------------------------------------------------//

        public async Task RunTask(TaskBase taskBase, IDbConnection connection)
        {
            bool isSuccess = await Task.Run(() => taskBase.SafeExecute(connection));
            if (isSuccess)
            {
                _commandFactory.CreateCommand<ITaskInfoCommand>(connection)
                               .Update(taskBase.taskInfo);
            }
        }

        //----------------------------------------------------------------//

    }
}
