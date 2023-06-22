using Newtonsoft.Json.Converters;

namespace TaskService.Domain.Converters
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {

        //----------------------------------------------------------------//

        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }

        //----------------------------------------------------------------//

    }
}
