using HR_Management_studio.Services;
using HR_Management_studio.Assets;
using HR_Management_studio.Models;
using Spectre.Console;
using Figgle;
using System.Reflection;
using HR_Management_studio.Enums;
using System.Linq;
using System.Threading.Channels;
using HR_Management_studio.Services.EnumServices;

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
                    HireEmployee();
                    break;
                case "2. Fire an employee":
                    FireEmployee();
                    break;
                case "3. Get all emploies' data":
                    PrintAllEmploiesData();
                    break;
                case "4. Get an employee data":
                    GetEmployeeData();
                    break;
                case "5. Edit an employee data (via personal id)":
                    EditEmployee();
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

    private static void HireEmployee()
    {

        AnsiConsole.Clear(); // Optional: clears screen for cleaner look
        PrintLogo();

        //var image = new CanvasImage("../../../Assets/nature2.jpg");
        //image.MaxWidth(250);
        //AnsiConsole.Write(image);

        Employee employee = new();

        Console.WriteLine();

        AnsiConsole.Write(
            new Rule("[bold #d7af00]Register employee[/]")
            .RuleStyle(Color.White)
            .Justify(Justify.Left));

        Console.WriteLine();

        employee.Name = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Name) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.LastName = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Last-name) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.Age = AnsiConsole.Prompt(
            new TextPrompt<int>("[red]·[/] [#5fafff]Employee[/] (Age) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.PersonalId = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Personal id) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.EmployeePosition = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]·[/] [bold #5fafff]Choose employee position[/]")
                .PageSize(100)
                .EnableSearch()
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices([
                    "IT_Tech",
                    "Accountant",
                    "HRP",
                    "Marketing_Specialist",
                    "Business_Analyst",
                    "Sales_Manager",
                    "Project_Manager",
                    "VP",
                    "[#CD7F32]President[/]",
                    "[#cda432]COO[/]",
                    "[#FFD700]CEO[/]"
                ])).StringToEnum();

        AnsiConsole.MarkupLine($"[red]·[/] [#5fafff]Employee[/] (Employee position) : [#d7af00]{employee.EmployeePosition}[/]");
        Console.WriteLine();


        employee.DateOfEmployment = AnsiConsole.Prompt(
        new TextPrompt<DateTime>(
            $"[red]·[/] [#5fafff]Employee[/] (Date of employment) " +
            $"[blue underline][[ default: {DateTime.Today} ]][/] :")
            .PromptStyle(Color.Gold3_1)
            .AllowEmpty()
            .DefaultValue(DateTime.Now)
            .HideDefaultValue()
        ); Console.WriteLine();

        employee.Salary = AnsiConsole.Prompt(
            new TextPrompt<int>("[red]·[/] [#5fafff]Employee[/] (Salary) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        //bool isOperationConfirmed = AnsiConsole.Prompt(
        //    new TextPrompt<bool>("Complete [blue]hiring[/] procedure?")
        //    .AddChoices([true, false])
        //    );

        bool isOperationConfirmed = AnsiConsole.Prompt(
            new TextPrompt<bool>("[red]·[/] [#5fafff]Complete procedure?[/] [blue][[ yes / no ]][/] :")
                .AddChoices([true, false])
                .WithConverter(choice => choice ? "yes" : "no")
                .HideChoices()
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        if (isOperationConfirmed)
        {
            empService.AddEmployee(employee);
            AnsiConsole.Clear(); // Optional: clears screen for cleaner look
            PrintLogo();
            AnsiConsole.MarkupLine("[green bold]Employee hired successfully[/]");
        }
        else
        {
            AnsiConsole.Clear(); // Optional: clears screen for cleaner look
            PrintLogo();
            AnsiConsole.MarkupLine("[red bold]Employee was not hired[/]");
        }

    }

    private static void FireEmployee()
    {
        AnsiConsole.Clear(); // Optional: clears screen for cleaner look
        PrintLogo();

        Console.WriteLine();

        AnsiConsole.Write(
            new Rule("[bold #d7af00]Fire employee[/]")
            .RuleStyle(Color.White)
            .Justify(Justify.Left)
        ); Console.WriteLine();

        string employeePersonalId = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Private Id) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        bool isOperationConfirmed = AnsiConsole.Prompt(
            new TextPrompt<bool>("[red]·[/] [#5fafff]Complete procedure?[/] [blue][[ yes / no ]][/] :")
                .AddChoices([true, false])
                .WithConverter(choice => choice ? "yes" : "no")
                .HideChoices()
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        if (isOperationConfirmed)
        {
            empService.RemoveEmployee(employee => employee.PersonalId.Equals(employeePersonalId), out bool isSuccesfull);
            AnsiConsole.Clear(); 
            PrintLogo();
            if (isSuccesfull)
                AnsiConsole.MarkupLine("[green bold]Employee fired successfully[/]");
            else 
                AnsiConsole.MarkupLine("[red bold]Employee was not fired[/]");
        }
        else
        {
            AnsiConsole.Clear(); // Optional: clears screen for cleaner look
            PrintLogo();
            AnsiConsole.MarkupLine("[red bold]Employee was not fired[/]");
        }
    }

    private static void EditEmployee()
    {
        AnsiConsole.Clear(); // Optional: clears screen for cleaner look
        PrintLogo();

        //var image = new CanvasImage("../../../Assets/nature2.jpg");
        //image.MaxWidth(250);
        //AnsiConsole.Write(image);

        Employee employee = new();

        Console.WriteLine();

        AnsiConsole.Write(
            new Rule("[bold #d7af00]Edit employee[/]")
            .RuleStyle(Color.White)
            .Justify(Justify.Left));

        Console.WriteLine();

        employee.Name = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Name) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.LastName = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Last-name) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.Age = AnsiConsole.Prompt(
            new TextPrompt<int>("[red]·[/] [#5fafff]Employee[/] (Age) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.PersonalId = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Personal id) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        employee.EmployeePosition = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]·[/] [bold #5fafff]Choose employee position[/]")
                .PageSize(100)
                .EnableSearch()
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices([
                    "IT_Tech",
                    "Accountant",
                    "HRP",
                    "Marketing_Specialist",
                    "Business_Analyst",
                    "Sales_Manager",
                    "Project_Manager",
                    "VP",
                    "[#CD7F32]President[/]",
                    "[#cda432]COO[/]",
                    "[#FFD700]CEO[/]"
                ])).StringToEnum();

        AnsiConsole.MarkupLine($"[red]·[/] [#5fafff]Employee[/] (Employee position) : [#d7af00]{employee.EmployeePosition}[/]");
        Console.WriteLine();


        employee.DateOfEmployment = AnsiConsole.Prompt(
        new TextPrompt<DateTime>(
            $"[red]·[/] [#5fafff]Employee[/] (Date of employment) " +
            $"[blue underline][[ default: {DateTime.Today} ]][/] :")
            .PromptStyle(Color.Gold3_1)
            .AllowEmpty()
            .DefaultValue(DateTime.Now)
            .HideDefaultValue()
        ); Console.WriteLine();

        employee.Salary = AnsiConsole.Prompt(
            new TextPrompt<int>("[red]·[/] [#5fafff]Employee[/] (Salary) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        //bool isOperationConfirmed = AnsiConsole.Prompt(
        //    new TextPrompt<bool>("Complete [blue]hiring[/] procedure?")
        //    .AddChoices([true, false])
        //    );

        bool isOperationConfirmed = AnsiConsole.Prompt(
            new TextPrompt<bool>("[red]·[/] [#5fafff]Complete procedure?[/] [blue][[ yes / no ]][/] :")
                .AddChoices([true, false])
                .WithConverter(choice => choice ? "yes" : "no")
                .HideChoices()
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();


        if (isOperationConfirmed)
        {
            empService.EditEmployee(employee.PersonalId, employee);
            AnsiConsole.Clear(); // Optional: clears screen for cleaner look
            PrintLogo();
            AnsiConsole.MarkupLine("[green bold]Employee edited successfully[/]");
        }
        else
        {
            AnsiConsole.Clear(); // Optional: clears screen for cleaner look
            PrintLogo();
            AnsiConsole.MarkupLine("[red bold]Employee was not edited[/]");
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

    private static void PrintAllEmploiesData()
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

    public static void GetEmployeeData()
    {
        AnsiConsole.Clear(); // Optional: clears screen for cleaner look
        PrintLogo();

        Console.WriteLine();

        AnsiConsole.Write(
            new Rule("[bold #d7af00]Get employee data[/]")
            .RuleStyle(Color.White)
            .Justify(Justify.Left)
        ); Console.WriteLine();

        string employeePersonalId = AnsiConsole.Prompt(
            new TextPrompt<string>("[red]·[/] [#5fafff]Employee[/] (Private Id) :")
                .PromptStyle(Color.Gold3_1)
        ); Console.WriteLine();

        Employee employee = empService.GetEmployee(employeePersonalId);

        // Get property information for the Employee class
        PropertyInfo[] employeePropertyCollection = typeof(Employee)
            .GetProperties()
            .ToArray();

        // Create the table
        Table table = new();

        // Add columns for each property name
        foreach (PropertyInfo item in employeePropertyCollection)
        {
            table.AddColumn(item.Name);
        }

        // Add the row with the values of each property for the employee
        var employeeValues = employeePropertyCollection
            .Select(prop => prop.GetValue(employee)?.ToString() ?? "N/A") // Get values or "N/A" if null
            .ToArray();

        table.AddRow(employeeValues);

        // Write the table to the console
        AnsiConsole.Write(table);

    }

}
