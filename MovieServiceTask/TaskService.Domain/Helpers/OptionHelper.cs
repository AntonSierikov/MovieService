using System;
using System.Collections.Generic;
using System.Text;
using TaskService.Domain.Entities;
using TaskService.Domain.Constans;

namespace TaskService.Domain.Helpers
{
    public static class OptionHelper
    {

        //----------------------------------------------------------------//

        public static bool TryMap(string key, string value, ref Option option)
        {
            bool isMap = false;
            switch (key)
            {
                case TaskOptionConstans.LAST_PAGE:
                    isMap = int.TryParse(value, out option.LastPage);
                    break;
            }
            return isMap;
        }

        //----------------------------------------------------------------//
       
        public static string GetOptionValueByKey(string key, Option option)
        {
            switch (key)
            {
                case TaskOptionConstans.LAST_PAGE:
                    return option.LastPage.ToString();
            }
            return String.Empty;
        }

        //----------------------------------------------------------------//

    }
}
