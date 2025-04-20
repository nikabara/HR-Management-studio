using HR_Management_studio.Enums;
using System.Linq;

namespace HR_Management_studio.Services.EnumServices;

public static class EmployeePositionHandler
{
    public static string FormatPositionEnum<T>(this T _enum) where T : Enum =>
        string.Join(' ', _enum!.ToString()!.Split('_'));

    public static EmployeePosition StringToEnum(this string enumString) =>
        enumString switch
        {
            "IT_Tech" => EmployeePosition.IT_Tech,
            "Accountant" => EmployeePosition.Accountant,
            "HRP" => EmployeePosition.HRP,
            "Marketing_Specialist" => EmployeePosition.Marketing_Specialist,
            "Business_Analyst" => EmployeePosition.Business_Analyst,
            "Sales_Manager" => EmployeePosition.Sales_Manager,
            "Project_Manager" => EmployeePosition.Project_Manager,
            "VP" => EmployeePosition.VP,
            "President" => EmployeePosition.President,
            "COO" => EmployeePosition.COO,
            "CEO" => EmployeePosition.CEO,
            _ => EmployeePosition._Unidentified,
        };
}
