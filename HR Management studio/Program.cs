using HR_Management_studio.Enums;
using HR_Management_studio.Models;
using HR_Management_studio.Services.EmployeeService;
using HR_Management_studio.Services.EnumServices;
using System.Diagnostics;

EmployeeService res = new();

Employee emp = res.GetEmployee(x => x.Name == "nick");
Console.ReadLine();
//Employee emp = new()
//{
//    Id = 1,
//    Name = "nick",
//    LastName = "Bara",
//    Age = 0,
//    PersonalId = "64654654654",
//    Salary = 1500,
//    EmployeePosition = EmployeePosition.Marketing_Specialist
//};

//res.PrintEmploiesData();