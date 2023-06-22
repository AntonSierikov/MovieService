using RestSharp;
using RestSharp.Serializers.Json;
using TmbdClient.Entities;

namespace TmbdClient;

public class MovieAgent
{
    private readonly RestClient _client;

    public MovieAgent()
    {
        _client = new RestClient("https://api.themoviedb.org/3/", configureSerialization: s => s.UseSystemTextJson());
        _client.AddDefaultHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJkN2M2NzExYTRlOTUzYmJkOWJmYzk2ZTdiOTUzYmFmOCIsInN1YiI6IjY0NzY2OWNhMDA1MDhhMDEzM2VjNWQ1YSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.WjENHyJ6IvJQj2qWHHlLCCNCNZjMhq57uKALugyyZxg");
    }

    //----------------------------------------------------------------//

    public Task<ProductionCompanyDto> LoadCompany(string companyId)
    {
        var request = new RestRequest($"/company/{companyId}");
        return _client.GetAsync<ProductionCompanyDto>(request);
    }

    //----------------------------------------------------------------//

    public Task<GenreDto[]> LoadGenres()
    {
        var request = new RestRequest("/genre/movie/list");
        return _client.GetAsync<GenreDto[]>(request);

    }

    //----------------------------------------------------------------//

    public Task<MovieDto> LoadMovieAsync(string movieId)
    {
        var request = new RestRequest($"/movie/{movieId}");
        return _client.GetAsync<MovieDto>(request);
    }

    //----------------------------------------------------------------//

    public Task<PeopleDto> LoadPeople(string peopleId)
    {
        var request = new RestRequest($"/person/{peopleId}");
        return _client.GetAsync<PeopleDto>(request);

    }

    //----------------------------------------------------------------//

    public Task<TopMoviesResult> LoadTopMovieIdsByPageAsync(int page)
    {
        var request = new RestRequest($"/movie/top_rated?page={page}");
        return _client.GetAsync<TopMoviesResult>(request);
    }

    //----------------------------------------------------------------//

    public Task<CreditsDto> LoadCreditsByMovieId(int movieId)
    {
        var request = new RestRequest($"/movie/{movieId}/credits");
        return _client.GetAsync<CreditsDto>(request);
    }

    //----------------------------------------------------------------//

    public Task<DepartmentDto[]> LoadDepartments()
    {
        var request = new RestRequest("/configuration/jobs");
        return _client.GetAsync<DepartmentDto[]>(request);
    }

    //----------------------------------------------------------------//

}