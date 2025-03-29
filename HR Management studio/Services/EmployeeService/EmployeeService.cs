using CsvHelper;
using HR_Management_studio.Models;
using System.Globalization;
using System.Text;

namespace HR_Management_studio.Services.EmployeeService
{
    public class EmployeeService
    {
        private readonly string _filePath = @$"{Directory.GetCurrentDirectory()}\HRMS";

        public void AddEmployee(Employee employee)
        {
            string directoryPath = Path.Combine(_filePath, "Employee");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "Employee.csv");


            bool fileExists = File.Exists(filePath);


            using (StreamWriter sWriter = new(filePath, append: true))
            using (CsvWriter csvWriter = new(sWriter, CultureInfo.InvariantCulture))
            {

                if (!fileExists)
                {
                    csvWriter.WriteHeader<Employee>();
                    csvWriter.NextRecord();
                }
                
                csvWriter.WriteRecord(employee);
                csvWriter.NextRecord();
            }

        }

        public List<Employee> GetEmploies()
        {
            string directoryPath = Path.Combine(_filePath, "Employee");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "Employee.csv");

            using (StreamReader sReader = new(filePath))
            using (CsvReader csvReader = new(sReader, CultureInfo.InvariantCulture))
            {

                return csvReader.GetRecords<Employee>().ToList();
            }
        }



        [Obsolete("Dont use this method. Use [AddEmployee(Employee employee)] instead")]
        public void AddEmployee(Employee employee, out bool isOperationSuccessful)
        {
            try
            {
                string directoryPath = Path.Combine(_filePath, "Employee");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, "Employee.csv");

                // Write the data
                WriteToFile(filePath, employee);

                isOperationSuccessful = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();

                isOperationSuccessful = false;
            }
        }
        private void WriteToFile(string filePath, Employee employee)
        {
            try
            {
                bool fileExists = File.Exists(filePath);

                using (StreamWriter writer = new(filePath, append: true, Encoding.UTF8))
                {
                    // Write header if file is new, this is needed for CsvReader
                    if (!fileExists)
                    {
                        writer.WriteLine("Id,Name,LastName,Age,PersonalId,DateOfEmployment,Salary");
                    }

                    // Write employee data in CSV format
                    writer.WriteLine($"{employee.Id},{employee.Name},{employee.LastName},{employee.Age},{employee.PersonalId},{employee.DateOfEmployment},{employee.Salary}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

    }
}