using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TaskService.Domain.Abstract;
using TaskService.Domain.Factories;
using Newtonsoft.Json.Linq;


namespace TaskService.Domain.Helpers
{
    public class GeneralLoadDataHelper
    {
        private const string RESULT_TOP_MOVIES = "results";
        private const string MOVIE_ID = "id";

        //----------------------------------------------------------------//

        public async static Task<T> LoadEntity<T>(string url) where T : class, new()
        {
            T dto = default(T);
            IJSONFormatter formatter = JSONFormatterFactory.CreateJsonFormatter<T>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = await client.GetAsync(url);
                string json = await message.Content.ReadAsStringAsync();
                dto = formatter.Deserialize<T>(json);
            }
            return dto;
        }

        //----------------------------------------------------------------//

        public async static Task<List<T>> LoadCollection<T>(string url) where T : class, new()
        {
            List<T> collectionDTOs = null;
            IJSONFormatter formatter = JSONFormatterFactory.CreateJsonFormatter<T>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = await client.GetAsync(url);
                string json = await message.Content.ReadAsStringAsync();
                collectionDTOs = formatter.DeserializeCollection<T>(json);
            }
            return collectionDTOs;
        }

        //----------------------------------------------------------------//

        public async static Task<string> GetMessageAsString(string url)
        {
            HttpResponseMessage message = null;
            using(HttpClient client = new HttpClient())
            {
                message = await client.GetAsync(url);
            }
            return await message.Content.ReadAsStringAsync();
        }

        //----------------------------------------------------------------//

        public static List<int> GetMovieIds(string jsonMessage)
        {
            List<int> movieIds = new List<int>();

            JObject obj = JObject.Parse(jsonMessage);
            JToken token = null;
            if(obj.TryGetValue(RESULT_TOP_MOVIES, out token))
            {
                foreach(JToken movieToken in token.Children())
                {
                    string strId = movieToken[MOVIE_ID]?.ToString();
                    int movieId = 0;
                    if (int.TryParse(strId, out movieId))
                    {
                        movieIds.Add(movieId);
                    }
                }
            }
            return movieIds;
        }

    }
}
