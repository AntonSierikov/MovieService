using System.Data;
using System.Threading.Tasks;
using TaskService.Domain.TaskBaseInfo;

namespace TaskService.Domain.ServiceInterface
{
    public interface ICinemaTaskService
    {

        //----------------------------------------------------------------//

        Task RunTasks();

        Task RunTask(TaskBase taskBase, IDbConnection connection);

        //----------------------------------------------------------------//

    }
}
