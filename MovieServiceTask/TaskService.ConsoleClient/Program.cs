using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MovieDomain.Common.Extensions;
using TaskService.Domain;
using TaskService.Domain.ServiceInterface;
using Microsoft.Extensions.DependencyInjection;

namespace TaskService.ConsoleClient
{
    public class Program
    {
        private const string _settingsFile = "appsettings.json";

        //----------------------------------------------------------------//

        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            TaskServiceConfigure configuration = new TaskServiceConfigure(serviceCollection);
            configuration.ConfigureDependencies(GetGeneralDirectory(), _settingsFile);
            IServiceProvider provider = configuration.Provider;
            ICinemaTaskService taskService = provider.GetRequiredService<ICinemaTaskService>();
            Task<string> task = taskService.RunTasks().ContinueWith(c => c.GetExceptionLog());
            task.Wait();
            Console.WriteLine(task.Result);
        }

        //----------------------------------------------------------------//

        public static string GetGeneralDirectory()
        {
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            string projName = AppDomain.CurrentDomain.FriendlyName;
            Regex regex = new Regex($@"(.*\\{projName})");
            return regex.Match(binPath).Groups.FirstOrDefault()?.Value;
        }
    }
}
