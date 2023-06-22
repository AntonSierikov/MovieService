using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MovieDomain.Common;
using MovieDomain.Common.Extensions;
using MovieDomain.Common.Enums;
using MovieDomain.Common.Constans;
using MovieDomain.DAL.Abstract;
using MovieDomain.DAL.Helpers;
using MovieDomain.DAL.ICommands;
using MovieDomain.DAL.IQueries;
using MovieDomain.Entities;
using MovieDomain.Abstract;
using TaskService.Domain.ServiceInterface;
using TaskService.Domain.DTO.CinemaEntities;
using TaskService.Domain.Mappers;

namespace TaskService.Domain.Managers
{
    internal class FeedManager
    {

        //----------------------------------------------------------------//

        private readonly ICommandFactory _commandFactory;
        private readonly IQueryFactory _queryFactory;

        //----------------------------------------------------------------//

        private readonly IMovieCommand _movieCommand;
        private readonly IGenreCommand _genreCommand;
        private readonly ICastCommand _castCommand;
        private readonly ICrewCommand _crewCommand;
        private readonly IPeopleCommand _peopleCommad;
        private readonly ICountryCommand _countryCommand;
        private readonly ICompanyCommand _companyCommand;
        private readonly IJobCommand _jobCommand;
        private readonly IDepartmentCommand _departmentCommand;


        private readonly IMovieGenreCommand _movieGenreCommand;
        private readonly IMovieCompanyCommand _movieCompanyCommand;
        private readonly IMovieCountryCommand _movieCountryCommand;

        //----------------------------------------------------------------//

        private readonly IMovieQuery _movieQuery;
        private readonly IGenreQuery _genreQuery;
        private readonly ICrewQuery _crewQuery;
        private readonly ICastQuery _castQuery;
        private readonly IPeopleQuery _peopleQuery;
        private readonly ICompanyQuery _companyQuery;
        private readonly ICountryQuery _countryQuery;
        private readonly IMovieCountryQuery _movieCountryQuery;
        private readonly IMovieCompanyQuery _movieCompanyQuery;
        private readonly IMovieGenreQuery _movieGenreQuery;
        private readonly IJobQuery _jobQuery;
        private readonly IDepartmentQuery _departmentQuery;

        //----------------------------------------------------------------//

        private readonly ILoadDataService _loadDataService;

        //----------------------------------------------------------------//

        public FeedManager(ISession session, IServiceProvider provider)
        {
            _commandFactory = provider.GetService<ICommandFactory>();
            _queryFactory = provider.GetService<IQueryFactory>();

            _genreCommand = _commandFactory.CreateCommand<IGenreCommand>(session);
            _companyCommand = _commandFactory.CreateCommand<ICompanyCommand>(session);
            _countryCommand = _commandFactory.CreateCommand<ICountryCommand>(session);
            _peopleCommad = _commandFactory.CreateCommand<IPeopleCommand>(session);
            _movieCommand = _commandFactory.CreateCommand<IMovieCommand>(session);
            _castCommand = _commandFactory.CreateCommand<ICastCommand>(session);
            _crewCommand = _commandFactory.CreateCommand<ICrewCommand>(session);
            _jobCommand = _commandFactory.CreateCommand<IJobCommand>(session);
            _departmentCommand = _commandFactory.CreateCommand<IDepartmentCommand>(session);

            _movieGenreCommand = _commandFactory.CreateCommand<IMovieGenreCommand>(session);
            _movieCompanyCommand = _commandFactory.CreateCommand<IMovieCompanyCommand>(session);
            _movieCountryCommand = _commandFactory.CreateCommand<IMovieCountryCommand>(session);

            _movieCountryQuery = _queryFactory.CreateQuery<IMovieCountryQuery>(session);
            _movieCompanyQuery = _queryFactory.CreateQuery<IMovieCompanyQuery>(session);
            _movieGenreQuery = _queryFactory.CreateQuery<IMovieGenreQuery>(session);

            _movieQuery = _queryFactory.CreateQuery<IMovieQuery>(session);
            _genreQuery = _queryFactory.CreateQuery<IGenreQuery>(session);
            _companyQuery = _queryFactory.CreateQuery<ICompanyQuery>(session);
            _countryQuery = _queryFactory.CreateQuery<ICountryQuery>(session);
            _castQuery = _queryFactory.CreateQuery<ICastQuery>(session);
            _crewQuery = _queryFactory.CreateQuery<ICrewQuery>(session);
            _peopleQuery = _queryFactory.CreateQuery<IPeopleQuery>(session);
            _jobQuery = _queryFactory.CreateQuery<IJobQuery>(session);
            _departmentQuery = _queryFactory.CreateQuery<IDepartmentQuery>(session);

            _loadDataService = provider.GetRequiredService<ILoadDataService>();
        }

        //----------------------------------------------------------------//

        public async Task FeedMovieWithSubEntities(int movieId)
        {
            Task<MovieDto> t_movie = _loadDataService.LoadMovie(movieId.ToString());
            Movie movie = CinemaDtoMapper.MapMovie(t_movie.Result);
            List<Task> tasks = new List<Task>();

            //--save genres--companies--countries if not exists
            Task<List<Genre>> t_genres = SaveOrUpdateCollectionsAsync(movie.Genres, _genreCommand, _genreQuery, g => g.genre);
            Task<List<ProductionCompany>> t_companies = SaveOrUpdateCollectionsAsync(movie.ProductionCompanies, _companyCommand, _companyQuery, c => c.Name);
            Task<List<ProductionCountry>> t_countries = SaveOrUpdateCollectionsAsync(movie.ProductionCountries, _countryCommand, _countryQuery, c => c.Name);

            //bind movie with genres--companies-countries
            await SaveMovie(movie);
            tasks.Add(SaveMovieGenresAsync(movie, await t_genres));
            tasks.Add(SaveMovieCompanies(movie, await t_companies));
            tasks.Add(SaveMovieCountriesAsync(movie, await t_countries));
            
            //credits
            tasks.Add(SaveMovieCreditsByMovieAsync(movie));
            await Task.WhenAll(tasks);
        }

        //----------------------------------------------------------------//

        public async Task SaveMovie(Movie movie)
        {
            DbCommandResult? result = await _movieCommand.SaveOrUpdateAsync(_movieQuery, movie);
            if (result.HasValue)
            {
                Logger.Log.Info($"Movie with {movie.Id} was {result.ToString()}");
            }
        }

        //----------------------------------------------------------------//

        public async Task<People> SavePeopleAsync(string peopleId)
        {
            PeopleDto peopleDto = await _loadDataService.LoadPeople(peopleId);
            People people = CinemaDtoMapper.MapPeople(peopleDto);
            if(people != null)
            {
                DbCommandResult? result = await _peopleCommad.SaveOrUpdateAsync(_peopleQuery, people);
                if (result.HasValue)
                {
                    Logger.Log.Info($"People ({people.Id} - {people.Name}) was {result.ToString()}");
                }
            }
            return people;
        }

        //----------------------------------------------------------------//

        public async Task<List<T>> SaveOrUpdateCollectionsAsync<T, TId>(IEnumerable<T> enumerable,
                                                                   ICommand<T, TId> command, 
                                                                   IQuery<T, TId> query,
                                                                   Func<T, string> select) where T: DbObject<TId>
        {
            var resultDictionary = await command.SaveOrUpdateCollectionAsync(query, enumerable);
            string typeName = typeof(T).Name;
            List<T> changedEntities = new List<T>();

            foreach (var pair in resultDictionary)
            {
                string str = String.Join(StringConstants.COMA_SPACE, pair.Value.Select(select));
                changedEntities.AddRange(pair.Value);
                Logger.Log.Info($"{typeName} {str} were {pair.Key.ToString()}");
            }

            return changedEntities;
        }

        //----------------------------------------------------------------//

        public async Task<List<MovieCountry>> SaveMovieCountriesAsync(Movie movie, IEnumerable<ProductionCountry> countries)
        {
            List<MovieCountry> movieCountries = new List<MovieCountry>();

            foreach(ProductionCountry country in countries)
            {
                MovieCountry movieCountry = new MovieCountry(movie, country);
                movie.ProductionCountries.Add(country);
                country.Movies.Add(movieCountry);
                if(await _movieCountryCommand.SaveIfNotExist(_movieCountryQuery, movieCountry))
                {
                    movieCountries.Add(movieCountry);
                }
            }

            string countriesBinded = String.Join(StringConstants.COMA_SPACE, countries.Select(c => c.Name));
            Logger.Log.Info($"Binded countries ({countriesBinded} for movie with id = {movie.Id}");
            return movieCountries;
        }

        //----------------------------------------------------------------//

        public async Task<List<MovieCompany>> SaveMovieCompanies(Movie movie, IEnumerable<ProductionCompany> companies)
        {
            List<MovieCompany> movieCompanies = new List<MovieCompany>();

            foreach(ProductionCompany company in companies)
            {
                MovieCompany movieCompany = new MovieCompany(movie, company);
                movie.ProductionCompanies.Add(company);
                company.Movies.Add(movie);
                if(await _movieCompanyCommand.SaveIfNotExist(_movieCompanyQuery, movieCompany))
                {
                    movieCompanies.Add(movieCompany);
                }
                
            }
            return movieCompanies;
        }

        //----------------------------------------------------------------//

        public async Task<List<MovieGenre>> SaveMovieGenresAsync(Movie movie, IEnumerable<Genre> genres)
        {
            List<MovieGenre> movieGenres = new List<MovieGenre>();

            foreach(Genre genre in genres)
            {
                MovieGenre movieGenre = new MovieGenre(movie, genre);
                movie.Genres.Add(genre);
                genre.Movies.Add(movie);
                if(await _movieGenreCommand.SaveIfNotExist(_movieGenreQuery, movieGenre))
                {
                    movieGenres.Add(movieGenre);
                }
            }

            return movieGenres;
        }

        //----------------------------------------------------------------//

        public async Task SaveMovieCreditsByMovieAsync(Movie movie)
        {
            CreditsDto creditsDto = await _loadDataService.LoadCreditsByMovieId(movie.Tmdb_ID);
            List<Task> tasks = new List<Task>();

            creditsDto.Cast.ForEach(c => tasks.Add(SaveMovieCastAsync(c, movie)));
            creditsDto.Crew.ForEach(c => tasks.Add(SaveMovieCrewAsync(c, movie)));

            await Task.WhenAll(tasks);
        }

        //----------------------------------------------------------------//

        public async Task<Cast> SaveMovieCastAsync(CastDto castDto, Movie movie)
        {
            Cast cast = CinemaDtoMapper.MapCast(castDto);
            cast.Movie = movie;
            cast.MovieId = movie.Id;

            if (castDto.PeopleId != null)
            {
                People people = await SavePeopleAsync(castDto.PeopleId);
                cast.People = people;
                cast.PeopleId = people?.Id;
            }

            if(await _castCommand.SaveIfNotExist(_castQuery, cast))
            {
                Logger.Log.Info($"Cast {cast.Id} was saved");
            }
            return cast;
        }

        //----------------------------------------------------------------//

        public async Task<Crew> SaveMovieCrewAsync(CrewDto crewDto, Movie movie)
        {
            Task<People> t_people = SavePeopleAsync(crewDto.Id.ToString());
            Department department = await SaveDepartmentAsync(crewDto.department);
            Job job = await SaveJobAsync(crewDto.job, department);
            Crew crew = new Crew(movie, await t_people, job);
            await _crewCommand.SaveOrUpdateAsync(crew);
            return crew;
        }

        //----------------------------------------------------------------//

        public async Task<Department> SaveDepartmentAsync(string s_department)
        {
            Department department = new Department(s_department);
            await _departmentCommand.SaveOrUpdateAsync(_departmentQuery, department);
            return department;
        }

        //----------------------------------------------------------------//

        public async Task<Job> SaveJobAsync(string s_job, Department department)
        {
            Job job = new Job(s_job, department);
            await _jobCommand.SaveOrUpdateAsync(_jobQuery, job);
            return job;
        }

        //----------------------------------------------------------------//
    }
}
