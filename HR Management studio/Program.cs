using HR_Management_studio.Enums;
using HR_Management_studio.Models;
using HR_Management_studio.Services.EmployeeService;
using HR_Management_studio.Services.EnumServices;
using System.Diagnostics;

EmployeeService res = new();


//res.GetAllData(out List<Employee> employeeCollection);


var result = res.GetEmploies();

//Employee emp = new()
//{
//    Id = 1,
//    Name = "Nick",
//    LastName = "Bara",
//    Age = 17,
//    PersonalId = "5545896544",
//    DateOfEmployment = DateTime.Now,
//    Salary = 1500
//};

//res.AddEmployee(emp);
//res.AddEmployee(emp);
//res.AddEmployee(emp);

//res.RemoveEmployee(3, out bool isSuccessful);

res.RemoveEmployee(x => x.Name == "Nick");

res.PrintEmploiesData();

Console.ReadLine();

//Console.ReadLine();

//EmployeePosition pos = EmployeePosition.Project_Manager;

//Console.WriteLine(pos);

//Console.WriteLine(pos.FormatPositionEnum());