using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MovieDomain.Common;
using MovieDomain.Common.Constans;
using MovieDomain.Entities;
using MovieDomain.DAL.Abstract;
using TaskService.Domain.Entities;
using TaskService.Domain.Helpers;

namespace TaskService.Domain.TaskBaseInfo
{
    public abstract class TaskBase
    {
        public TaskInfo taskInfo { get; private set; }

        public Option option { get; private set; }

        protected readonly IServiceProvider _serviceProvider;

        //----------------------------------------------------------------//

        public TaskBase(TaskInfo info, IServiceProvider serviceProvider )
        {
            taskInfo = info;
            _serviceProvider = serviceProvider;
            option = DeserializeOption();
        }

        //----------------------------------------------------------------//    

        public abstract Task Execute(IDbConnection session);

        //----------------------------------------------------------------//
        
        public virtual Option DeserializeOption()
        {
            Option option = new Option();
            string[] constituents = taskInfo.Options.Split(StringConstants.VERTICAL_LINE);
            foreach(string constituent in constituents)
            {
                string[] pair = constituent.Split(StringConstants.EQUAL);
                OptionHelper.TryMap(pair.FirstOrDefault(), pair.LastOrDefault(), ref option);
            }
            return option;
        }

        //----------------------------------------------------------------//

        public virtual string SerializeOption()
        {
            string[] constituents = taskInfo.Options.Split(StringConstants.VERTICAL_LINE);
            StringBuilder builder = new StringBuilder();
            foreach (string constituent in constituents)
            {
                string[] pair = constituent.Split(StringConstants.EQUAL);
                string key = pair.FirstOrDefault();
                string value = OptionHelper.GetOptionValueByKey(key, option);
                builder.Append(String.Join(StringConstants.EQUAL, key, value));
                builder.Append(StringConstants.VERTICAL_LINE);
            }
            return builder.ToString();
        }

        //----------------------------------------------------------------//

        public async virtual Task<bool> SafeExecute(IDbConnection connection)
        {
            OnStart();
            bool IsSuccess = false;
            try
            {
                await Execute(connection);
                IsSuccess = true;
                OnSuccess();
            }
            catch (Exception e)
            {
                OnFailure(e);
            }
            taskInfo.Options = SerializeOption();
            return IsSuccess;
        }

        //----------------------------------------------------------------//

        

        public virtual void OnStart()
        {
            taskInfo.TaskStarting();
            Logger.Log.Info($"Task with Id = {taskInfo.Id} starts");
        }

        //----------------------------------------------------------------//

        public virtual void OnSuccess()
        {
            taskInfo.TaskEnding(true);
            Logger.Log.Info($"Task with id = {taskInfo.Id} success finished");
        }

        //----------------------------------------------------------------//

        public virtual void OnFailure(Exception exception)
        {
            taskInfo.TaskEnding(false);
            Logger.Log.Info($"Task with Id = {taskInfo.Id} failure finished");
            Logger.Log.Error(exception.Message);
        }

        //----------------------------------------------------------------//
    }
}
