using HR_Management_studio.Enums;
using HR_Management_studio.Models;
using HR_Management_studio.Services.DirectoryService;
using HR_Management_studio.Services.EmployeeService;
using HR_Management_studio.Services.EnumServices;
using System.Diagnostics;

EmployeeService res = new();

//List<Employee> emp = res.GetEmploies(x => x.Name == "nick");
//Console.ReadLine();
Employee emp = new()
{
    Name = "nick",
    LastName = "Bara",
    Age = 0,
    PersonalId = "64654654654",
    Salary = 1500,
    EmployeePosition = EmployeePosition.Marketing_Specialist
};

Employee emp1 = new()
{
    Name = "nick",
    LastName = "Bara",
    Age = 0,
    PersonalId = "64654654654",
    Salary = 1500,
    EmployeePosition = EmployeePosition.Marketing_Specialist
};

Employee emp2 = new()
{
    Name = "nick",
    LastName = "Bara",
    Age = 0,
    PersonalId = "64654654654",
    Salary = 1500,
    EmployeePosition = EmployeePosition.Marketing_Specialist
};

List<Employee> ns = [emp, emp1, emp2];
res.AddEmploies(ns);

//res.PrintEmploiesData();

Console.ReadLine();


// Csv file filler data
//Id, Name, LastName, Age, PersonalId, EmployeePosition, DateOfEmployment, Salary
//1, nick, Bara,0,64654654654, Marketing_Specialist,04/08/2025 20:41:20,1500
//2,karlo, Bara,0,64654654654, Marketing_Specialist,04/08/2025 20:42:57,1500
//3,saba, Bara,0,64654654654, Marketing_Specialist,04/08/2025 20:42:59,1500
//4,vaso, Bara,0,64654654654, Marketing_Specialist,04/08/2025 20:43:00,1500
//5,nick, Bara,0,64654654654, Marketing_Specialist,04/11/2025 01:42:50,1500
//6,nick, Bara,0,64654654654, Marketing_Specialist,04/11/2025 01:42:50,1500
//7,nick, Bara,0,64654654654, Marketing_Specialist,04/11/2025 01:42:50,1500