using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TaskService.Domain.DTO.CinemaEntities
{
    public class CastDto
    {
    
        public int CastId;

        public int Gender;

        public int Order;

        public string Character;

        public string Overview;

        public string MovieId;

        [JsonProperty("id")]
        public string PeopleId;
    }
}
