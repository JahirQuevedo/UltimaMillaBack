using System.ComponentModel;
using System.Reflection;

namespace ALOG.Enums;

public class EnumList
{
    public List<EnumNameValue> EnumNamesValues { get; set; }
}

public static class EnumSerializer
{
    public static EnumList EnumToJson<T>() where T : struct, Enum
    {
        var type = typeof(T);
        var values = Enum.GetValues<T>()
            .Select(x => new EnumNameValue
            {
                Id = (int)(object)x,
                Description = type.GetField(x.ToString()).GetCustomAttribute<DescriptionAttribute>().Description
            });
        return new EnumList { EnumNamesValues = values.ToList() };
    }
}