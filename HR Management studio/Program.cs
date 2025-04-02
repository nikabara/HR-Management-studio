using HR_Management_studio.Enums;
using HR_Management_studio.Models;
using HR_Management_studio.Services.EmployeeService;
using HR_Management_studio.Services.EnumServices;

EmployeeService res = new();


//res.GetAllData(out List<Employee> employeeCollection);

Console.ReadLine();

EmployeePosition pos = EmployeePosition.Project_Manager;

Console.WriteLine(pos);

Console.WriteLine(pos.FormatPositionEnum());