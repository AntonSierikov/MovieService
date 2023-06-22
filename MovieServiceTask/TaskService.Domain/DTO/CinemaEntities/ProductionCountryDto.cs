using System;
using System.Collections.Generic;
using System.Text;

namespace TaskService.Domain.DTO.CinemaEntities
{
    public class ProductionCountryDto
    {
        public int Id { get; set; }

        public string iso_3166_1 { get; set; }

        public string name { get; set; }

    }
}
