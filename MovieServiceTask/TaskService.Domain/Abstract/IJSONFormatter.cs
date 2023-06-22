using System;
using System.Collections.Generic;
using System.Text;

namespace TaskService.Domain.Abstract
{
    public interface IJSONFormatter
    {
        T Deserialize<T>(string json) where T : class, new();

        List<T> DeserializeCollection<T>(string json) where T : class, new();

    }
}
