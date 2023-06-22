using System;
using System.Collections.Generic;
using System.Text;
using MovieDomain.Entities;
using TaskService.Domain.DTO.CinemaEntities;

namespace TaskService.Domain.Mappers
{
    public class CinemaDtoMapper
    {

        //----------------------------------------------------------------//

        public static Movie MapMovie(MovieDto movieDto)
        {
            Movie movie = new Movie()
            {
                Tmdb_ID = movieDto.Id,
                Adult = movieDto.Adult,
                Backdrop_path = movieDto.Backdrop_path,
                Budget = movieDto.Budget,
                BelongsToCollection = movieDto.BelongsToCollection,
                Imdb_id = movieDto.Imdb_id,
                Original_language = movieDto.Original_language,
                Original_title = movieDto.Original_title,
                Overview = movieDto.Overview,
                Popularity = movieDto.Popularity,
                Poster_path = movieDto.poster_path,
                ReleaseDate = movieDto.release_date,
                Runtime = movieDto.Runtime,
                Status = movieDto.Status,
                Tagline = movieDto.Tagline,
                Title = movieDto.Title,
                Video = movieDto.Video,
                VoteAverage = movieDto.VoteAverage,
                VoteCount = movieDto.VoteCount
            };
            movieDto.Genres.ForEach(g => movie.Genres.Add(MapGenre(g)));
            movieDto.Production_Companies.ForEach(c => movie.ProductionCompanies.Add(MapCompany(c)));
            movieDto.Production_Countries.ForEach(c => movie.ProductionCountries.Add(MapCoutry(c)));
            return movie;
        }

        //----------------------------------------------------------------//
        public static Genre MapGenre(GenreDto genreDto)
        {
            Genre genre = new Genre(genreDto.Name);
            return genre;
        }

        //----------------------------------------------------------------//

        public static ProductionCompany MapCompany(ProductionCompanyDto companyDto)
        {
            ProductionCompany company = new ProductionCompany()
            {
                Name = companyDto.Name,
                //Description = companyDto.Description,
                //Headquaters = companyDto.Headquarters,
                //Origin_Country = companyDto.Origin_country,
                //Parent_Company = companyDto.Parent_Company
            };
            return company;
        }

        //----------------------------------------------------------------//

        public static ProductionCountry MapCoutry(ProductionCountryDto countyDto)
        {
            ProductionCountry country = new ProductionCountry()
            {
                Name = countyDto.name,
                iso_3166_1 = countyDto.iso_3166_1,
            };
            return country;
        }

        //----------------------------------------------------------------//

        public static Cast MapCast(CastDto castDto)
        {
            Cast cast = new Cast()
            {
                CharacterCast = castDto.Character,
                Gender = castDto.Gender,
                Order = castDto.Order
            };
            return cast;
        }

        //----------------------------------------------------------------//

        public static People MapPeople(PeopleDto peopleDto)
        {
            People people = null;

            if (peopleDto != null && peopleDto.Name != null)
            {
                people = new People()
                {
                    Biography = peopleDto.Biography,
                    Deathday = peopleDto.Deathday,
                    Birthday = peopleDto.Birthday,
                    Gender = peopleDto.Gender,
                    Name = peopleDto.Name,
                    PlaceOfBirth = peopleDto.PlaceOfBirth,
                    Popularity = peopleDto.Popularity,
                    Imdb_id = peopleDto.Imdb_id,
                    Homepage = peopleDto.Homepage
                };
            }

            return people;
        }


        //----------------------------------------------------------------//

    }
}
