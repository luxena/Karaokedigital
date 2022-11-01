using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public static class Extension
    {
        public static string ToCapitalize<T>(this T value) where T : class
        {
            var newValue = "";
            if (value != null)
            {
                if (value.ToString().Length > 0)
                {
                    newValue = char.ToUpper(value.ToString()[0]) + value.ToString().Substring(1);
                }
                
            }
          
            return newValue;
        }

    }
}
