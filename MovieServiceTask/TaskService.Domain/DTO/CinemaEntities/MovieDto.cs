using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TaskService.Domain.DTO.CinemaEntities
{
    public class MovieDto
    {
        public MovieDto()
        {
            Genres = new List<GenreDto>();
            Production_Companies = new List<ProductionCompanyDto>();
            Production_Countries = new List<ProductionCountryDto>();
        }

        public int Id;

        public Dictionary<string, string> dictionary;

        public bool Adult;

        public string Backdrop_path;

        public int Budget;

        public string BelongsToCollection;

        public string Imdb_id;

        public string Original_language;

        public string Original_title;

        public string Overview;

        public double Popularity;

        public string poster_path;

        public DateTime release_date;

        public int Revenue;

        public int? Runtime;

        public string Status;

        public string Tagline;

        public string Title;

        public bool Video;

        public double VoteAverage;

        public int VoteCount;

        public List<GenreDto> Genres;

        public List<ProductionCountryDto> Production_Countries;

        public List<ProductionCompanyDto> Production_Companies;

    }
}
