using System;
using System.Collections.Generic;
using System.Text;

namespace TaskService.Domain.DTO.CinemaEntities
{
    public class PeopleDto
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public string Imdb_id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; } = null;
        public int Gender { get; set; }
        public string Homepage { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public string PlaceOfBirth { get; set; }
        public double Popularity { get; set; }
        public string ProfilePath { get; set; }
    }
}
