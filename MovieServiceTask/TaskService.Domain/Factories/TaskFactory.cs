using System;
using MovieDomain.Entities;
using MovieDomain.DAL.Abstract;
using TaskService.Domain.TaskBaseInfo;
using TaskService.Domain.Enums;
using TaskService.Domain.Tasks.PartialFeedTasks;

namespace TaskService.Domain.Factories
{
    public static class TaskFactory
    {
        private const string TASK_NOT_EXIST = "Task isn't exist";

        //----------------------------------------------------------------//
        
        public static TaskBase CreateTask(TaskInfo info, IServiceProvider provider)
        {
            TaskBase taskBase = null;
            CinemaServiceTask taskId = (CinemaServiceTask)Enum.ToObject(typeof(CinemaServiceTask), info.Id);
            switch (taskId)
            {
                case CinemaServiceTask.FeedTopMovies:
                    taskBase = new FeedTopMovies(info, provider);
                    break;
                default:
                    throw new Exception(TASK_NOT_EXIST);
            }
            return taskBase;
        }
    }
}
