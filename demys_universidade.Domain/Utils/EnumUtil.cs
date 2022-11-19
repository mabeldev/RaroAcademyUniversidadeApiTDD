using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Utils
{
    public static class EnumUtil
    {
        public static string Description(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description ?? string.Empty;
        }
    }
}
