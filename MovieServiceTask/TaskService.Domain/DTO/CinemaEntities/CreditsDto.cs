using System;
using System.Collections.Generic;
using System.Text;

namespace TaskService.Domain.DTO.CinemaEntities
{
    public class CreditsDto
    {
        public List<CastDto> Cast { get; set; }

        public List<CrewDto> Crew { get; set; }
    }
}
