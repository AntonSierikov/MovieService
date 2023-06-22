using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TaskService.Domain.Formatters
{
    public class GenreJSONFormatter : GeneralJSONFormatter
    {
        private const string GENRE_KEY_COLLECTION = "genres";

        //----------------------------------------------------------------//

        public override List<GenreDto> DeserializeCollection<GenreDto>(string json)
        {
            List<GenreDto> list = null;

            JObject obj = JObject.Parse(json);
            JToken token = null;
            if(obj.TryGetValue(GENRE_KEY_COLLECTION, out token))
            {
                list = base.DeserializeCollection<GenreDto>(token.ToString());
            }
            return list;
        }

        //----------------------------------------------------------------//

    }
}
