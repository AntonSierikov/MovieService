using System;
using System.Collections.Generic;
using System.Text;
using TaskService.Domain.DTO.CinemaEntities;

namespace TaskService.Domain.Formatters
{
    public class CreditJsonFormatter : GeneralJSONFormatter
    {
        private const string CAST_KEYWORD = "cast";

        private const string CREW_KEYWORD = "crew";


        //----------------------------------------------------------------//

        public override CreditsDto Deserialize<CreditsDto>(string json) 
        {
            return null;
        }

        //----------------------------------------------------------------//

    }
}
