using System;
using TaskService.Domain.Abstract;
using TaskService.Domain.Enums;
using TaskService.Domain.Formatters;
using TaskService.Domain.DTO.CinemaEntities;

namespace TaskService.Domain.Factories
{
    public class JSONFormatterFactory
    {

        //----------------------------------------------------------------//

        public static IJSONFormatter CreateJsonFormatter(FormatterType formatterType = FormatterType.Default)
        {
            switch (formatterType)
            {
                case FormatterType.Genre:
                    return new GenreJSONFormatter();
                default:
                    return new GeneralJSONFormatter();
            }

        }

        //----------------------------------------------------------------//

        public static IJSONFormatter CreateJsonFormatter<T>()
        {
            if (typeof(MovieDto).Equals(typeof(T))) return new MovieJSONFormatter();
            else if (typeof(GenreDto).Equals(typeof(T))) return new GenreJSONFormatter();
            else return new GeneralJSONFormatter();
        }

    }
}
