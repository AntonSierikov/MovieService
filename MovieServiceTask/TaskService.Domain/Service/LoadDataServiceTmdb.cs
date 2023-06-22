using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using TaskService.Domain.DTO.CinemaEntities;
using TaskService.Domain.ServiceInterface;
using System.Net.Http;
using MovieDomain.Common.Helpers;
using System.Threading.Tasks;
using TaskService.Domain.Helpers;
using TaskService.Domain.Formatters;
using TaskService.Domain.Abstract;

namespace TaskService.Domain.Service
{
    public class LoadDataServiceTmdb : ILoadDataService
    {
        private const string TmdbAPIUrl = "https://api.themoviedb.org/3/";
        private const string ApiKey = "api_key";
        private const string Credential = "915ba5ac74f317e1048dfb39d3ebcdbd";
        private KeyValuePair<string, string> ApiKeyParam = new KeyValuePair<string, string>(ApiKey, Credential);


        //----------------------------------------------------------------//

        public async Task<ProductionCompanyDto> LoadCompany(string companyId)
        {
            string url = UrlHelper.AddTerm($"{TmdbAPIUrl}/company/", companyId);
            url = UrlHelper.AddParam(url, ApiKeyParam);
            string json = await GeneralLoadDataHelper.GetMessageAsString(url);
            IJSONFormatter formatter = new GeneralJSONFormatter();
            return formatter.Deserialize<ProductionCompanyDto>(json);
        }

        //----------------------------------------------------------------//

        public async Task<List<GenreDto>> LoadGenres()
        {
            string url = UrlHelper.AddParam($"{TmdbAPIUrl}genre/movie/list", ApiKeyParam);
            string json = await GeneralLoadDataHelper.GetMessageAsString(url);
            IJSONFormatter formatter = new GenreJSONFormatter();
            return formatter.DeserializeCollection<GenreDto>(json);
        }

        //----------------------------------------------------------------//

        public async Task<MovieDto> LoadMovie(string movieId)
        {
            string url = UrlHelper.AddTerm($"{TmdbAPIUrl}movie", movieId);
            url = UrlHelper.AddParam(url, ApiKeyParam);
            string json = await GeneralLoadDataHelper.GetMessageAsString(url);
            IJSONFormatter formatter = new MovieJSONFormatter();
            return formatter.Deserialize<MovieDto>(json);
        }

        //----------------------------------------------------------------//

        public async Task<PeopleDto> LoadPeople(string peopleId)
        {
            string url = UrlHelper.AddTerm($"{TmdbAPIUrl}person", peopleId);
            url = UrlHelper.AddParam(url, ApiKeyParam);
            string json = await GeneralLoadDataHelper.GetMessageAsString(url);
            IJSONFormatter formatter = new GeneralJSONFormatter();
            return formatter.Deserialize<PeopleDto>(json);
        }

        //----------------------------------------------------------------//

        public async Task<List<int>> LoadTopMovieIdsByPage(int page)
        {
            string url = UrlHelper.AddParam($"{TmdbAPIUrl}movie/top_rated", ApiKeyParam);
            url = UrlHelper.AddParam(url, new KeyValuePair<string, string>("page", page.ToString()));
            string jsonPageTopMovies = await GeneralLoadDataHelper.GetMessageAsString(url);
            return GeneralLoadDataHelper.GetMovieIds(jsonPageTopMovies);
        }

        //----------------------------------------------------------------//

        public async Task<CreditsDto> LoadCreditsByMovieId(int movieId)
        {
            string url = UrlHelper.AddParam($"{TmdbAPIUrl}movie/{movieId}/credits", ApiKeyParam);
            string json = await GeneralLoadDataHelper.GetMessageAsString(url);
            IJSONFormatter formatter = new GeneralJSONFormatter();
            return formatter.Deserialize<CreditsDto>(json);
        }

        //----------------------------------------------------------------//

        public async Task<List<DepartmentDto>> LoadDepartments()
        {
            string url = UrlHelper.AddParam($"{TmdbAPIUrl}configuration/jobs", ApiKeyParam);
            string json = await GeneralLoadDataHelper.GetMessageAsString(url);
            IJSONFormatter formatter = new GeneralJSONFormatter();
            return formatter.DeserializeCollection<DepartmentDto>(json);
        }

        //----------------------------------------------------------------//

    }
}
