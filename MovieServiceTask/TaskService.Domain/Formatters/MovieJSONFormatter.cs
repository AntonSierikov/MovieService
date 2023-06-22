using TaskService.Domain.Formatters; 

namespace TaskService.Domain.Formatters
{
    public class MovieJSONFormatter : GeneralJSONFormatter 
    {

        //----------------------------------------------------------------//

        public override MovieDto Deserialize<MovieDto>(string json)
        {
            return base.Deserialize<MovieDto>(json);
        }

        //----------------------------------------------------------------//

    }
}
