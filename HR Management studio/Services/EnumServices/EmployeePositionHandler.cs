using System.Linq;

namespace HR_Management_studio.Services.EnumServices;

public static class EmployeePositionHandler
{
    public static string FormatPositionEnum<T>(this T _enum) where T : Enum =>
        string.Join(' ', _enum!.ToString()!.Split('_'));
}
