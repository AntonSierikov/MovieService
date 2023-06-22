using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TaskService.Domain.Abstract;

namespace TaskService.Domain.Formatters
{
    public class GeneralJSONFormatter : IJSONFormatter
    {

        //----------------------------------------------------------------//

        public virtual T Deserialize<T>(string json) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        //----------------------------------------------------------------//

        public virtual List<T> DeserializeCollection<T>(string json) where T : class, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        //----------------------------------------------------------------//

    }

}
