using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App.Helpers;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo!.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        return (attributes != null && attributes.Length > 0) ? attributes.First().Description : value.ToString();
    }
}