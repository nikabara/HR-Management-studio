﻿using HR_Management_studio.Services;
using HR_Management_studio.Assets;
using HR_Management_studio.Models;
using Spectre.Console;
using Figgle;
using System.Reflection;
using HR_Management_studio.Enums;
using System.Linq;

namespace HR_Management_studio.Services.ConsoleService;

public class ConsoleService
{
    private static EmployeeService.EmployeeService empService = new();

    public static void PrintLogo()
    {
        Console.WriteLine('\n');
        Console.WriteLine(AnsiShadow.LoadAnsiShadow().Render("HRMS"));
    }

    public static void Start() 
    {
        string menuOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\n# [bold]Choose action[/]")
                .PageSize(100)
                .EnableSearch()
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices([
                    "1. Hire a new employee",
                    "2. Fire an employee",
                    "3. Get all emploies' data",
                    "4. Get an employee data",
                    "5. Edit an employee data (via personal id)",
                    "6. Get job position analytics",
                    "[red bold]-- Exit ------>[/]"
                ]));

        CallMethodOnMenuOption(menuOption);
    }

    private static void CallMethodOnMenuOption(string mentuOption)
    {
        try
        {
            switch (mentuOption)
            {
                case "1. Hire a new employee":
                    AnsiConsole.Markup("[bold red]Not implemented[/]");
                    break;
                case "2. Fire an employee":
                    AnsiConsole.Markup("[bold red]Not implemented[/]");
                    break;
                case "3. Get all emploies' data":
                    PrintAllEmploiesData();
                    break;
                case "4. Get an employee data":
                    AnsiConsole.Markup("[bold red]Not implemented[/]");
                    break;
                case "5. Edit an employee data (via personal id)":
                    AnsiConsole.Markup("[bold red]Not implemented[/]");
                    break;
                case "6. Get job position analytics":
                    GetCompanyPositionsAnalytics();
                    break;
                case "[red bold]-- Exit ------>[/]":
                    Environment.Exit(0);
                    break;
                default:
                    throw new NotImplementedException("Function does not exits");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup(ex.Message);
            Start();
        }
    }

    private static void GetCompanyPositionsAnalytics()
    {
        List<Employee> employeeCollection = empService.GetEmploies();

        var employeeGroups = employeeCollection
            .GroupBy(x => x.EmployeePosition)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Responsive width based on console window size
        int chartWidth = Math.Max(40, Console.WindowWidth - 10);

        // Color palette for bars
        Color[] colors =
        {
            Color.Aqua, Color.Green, Color.Blue, Color.Orange1,
            Color.Purple, Color.Teal, Color.CadetBlue, Color.DeepSkyBlue1,
            Color.MediumVioletRed, Color.SkyBlue1
        };

        int index = 0;

        // Bar chart with dynamic width
        BarChart jobPositionBarChart = new BarChart()
            .Width(chartWidth)
            .Label("[bold underline green]Employees per Position[/]")
            .CenterLabel();

        foreach (var item in employeeGroups)
        {
            var color = colors[index++ % colors.Length];
            string label = $"[bold]{item.Key}[/] [grey]({item.Value.Count})[/]";
            jobPositionBarChart.AddItem(label, item.Value.Count, color);
        }

        // Wrap in a fancy panel
        var chartPanel = new Panel(jobPositionBarChart)
            .Header("[yellow bold]📊 Company Position Analytics[/]")
            .Border(BoxBorder.Double)
            .BorderStyle(new Style(Color.White))
            .Padding(1, 1)
            .Expand();

        AnsiConsole.Clear(); // Optional: clears screen for cleaner look
        PrintLogo();
        AnsiConsole.Write(chartPanel);
    }



    public static void PrintAllEmploiesData()
    {
        AnsiConsole.Clear();
        PrintLogo();

        List<Employee> employeeCollection = empService.GetEmploies();

        // Get property names of Employee class
        string[] employeePropertyCollection = typeof(Employee)
            .GetProperties()
            .Select(x => x.Name)
            .ToArray();

        // Create the table and center it
        Table employeeTable = new Table()
            .Border(TableBorder.Rounded)
            .Title("[yellow]Employee Data[/]");

        AnsiConsole.Live(employeeTable)
            .AutoClear(false) // Keep final output visible
            .Overflow(VerticalOverflow.Ellipsis)
            .Cropping(VerticalOverflowCropping.Top)
            .Start(ctx =>
            {
                // Add columns dynamically with animation
                foreach (var prop in employeePropertyCollection)
                {
                    employeeTable.AddColumn(new TableColumn($"[bold]{prop}[/]"));
                    ctx.Refresh();
                    Thread.Sleep(100);
                }

                // Add rows for each employee
                foreach (var employee in employeeCollection)
                {
                    var values = employeePropertyCollection
                        .Select(prop => typeof(Employee).GetProperty(prop)!.GetValue(employee)?.ToString() ?? "N/A")
                        .ToArray();

                    employeeTable.AddRow(values);
                    ctx.Refresh();
                    Thread.Sleep(150);
                }
            });

        // Optional: add a footer or message after
        AnsiConsole.MarkupLine("\n[green]✔ Done loading employee data.[/]");
    }

}
