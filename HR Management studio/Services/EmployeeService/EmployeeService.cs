using CsvHelper;
using HR_Management_studio.Models;
using System.Globalization;
using System.Text;
using HR_Management_studio.Services.DirectoryService;

namespace HR_Management_studio.Services.EmployeeService
{
    public class EmployeeService
    {
        private readonly string _filePath = @$"{Directory.GetCurrentDirectory()}\HRMS";

        /// <summary>
        /// Adds single Employee instance to a ~/Employee.csv file
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            string directoryPath = Path.Combine(_filePath, "Employee");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "Employee.csv");

            bool fileExists = File.Exists(filePath);
            bool writeHeader = !fileExists || new FileInfo(filePath).Length == 0;

            employee.Id = DirectoryService.DirectoryService.GetCurrentId() + 1;

            using (var sWriter = new StreamWriter(filePath, append: true))
            using (var csvWriter = new CsvWriter(sWriter, CultureInfo.InvariantCulture))
            {
                if (writeHeader)
                {
                    csvWriter.WriteHeader<Employee>();
                    csvWriter.NextRecord();
                }

                csvWriter.WriteRecord(employee);
                csvWriter.NextRecord();
            }
        }

        /// <summary>
        /// Adds List of Employee instances to a ~/Employee.csv file
        /// </summary>
        /// <param name="emploies"></param>
        public void AddEmploies(List<Employee> emploies) => emploies.ForEach(x => AddEmployee(x));

        /// <summary>
        /// Gets every employee from the ~/Employee.csv file.
        /// </summary>
        /// <returns>
        /// A <b>List&lt;Employee&gt;</b> if no exceptions occur; otherwise, an empty list (<b>List&lt;Employee&gt;</b>).
        /// </returns>
        /// <param name="filePath">The path to the CSV file.</param>
        public List<Employee> GetEmploies()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        /// <summary>
        /// Get single employee with specified Personal Id
        /// </summary>
        /// <param name="PersonalId"></param>
        /// <returns>Employee</returns>
        public Employee GetEmployee(string PersonalId) => 
            GetEmploies().Where(x => x.PersonalId.Equals(PersonalId)).First();

        /// <summary>
        /// Gets every employee from the ~/Employee.csv file which satisfies lambda statement.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns>List<Employee></returns>
        public List<Employee> GetEmploies(Func<Employee, bool> statement) => GetEmploies().Where(statement).ToList();

        /// <summary>
        /// Removes Employee from a collection using [ *primary key* ] id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSuccessful"></param>
        public void RemoveEmployee(int id, out bool isSuccessful)
        {
            try
            {
                string directoryPath = Path.Combine(_filePath, "Employee");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, "Employee.csv");
                
                List<Employee> filteredEmploies = GetEmploies().Where(x => x.Id != id).ToList();
                
                if (filteredEmploies.Count != 0)
                {
                    File.Delete(filePath);
                    AddEmploies(filteredEmploies);
                    isSuccessful = true;
                }
                else isSuccessful = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                isSuccessful = false;
            }
        }

        /// <summary>
        /// Removes Employee from a collection depending on lambda expression
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="isSuccessful"></param>
        public void RemoveEmployee(Func<Employee, bool> statement, out bool isSuccessful)
        {
            try
            {
                string directoryPath = Path.Combine(_filePath, "Employee");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, "Employee.csv");

                List<Employee> filteredEmploies = GetEmploies().Where(emp => !statement(emp)).ToList();

                if (filteredEmploies.Count != 0 || GetEmploies().Count == 1)
                {
                    File.Delete(filePath);
                    AddEmploies(filteredEmploies);
                    isSuccessful = true;
                }
                else isSuccessful = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                isSuccessful = false;
            }
        }

        public void EditEmployee(string personalId, Employee employee)
        {
            try
            {
                string directoryPath = Path.Combine(_filePath, "Employee");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, "Employee.csv");

                Employee filteredEmployee = GetEmploies()
                    .FirstOrDefault(emp => emp.PersonalId.Equals(personalId))!;

                if (filteredEmployee != null)
                {
                    filteredEmployee = employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }

                RemoveEmployee(x => x.PersonalId.Equals(personalId), out bool isSuccessful);

                if (isSuccessful)
                {
                    AddEmployee(filteredEmployee);
                    List<Employee> employeesCollection = GetEmploies().OrderBy(x => x.Id).ToList();
                    AddEmploies(employeesCollection);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// Print every single employe info to console
        /// </summary>
        public void PrintEmploiesData()
        {
            string directoryPath = Path.Combine(_filePath, "Employee");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, "Employee.csv");


            bool fileExists = File.Exists(filePath);

            if (!fileExists) File.Create(filePath).Close();

            using (StreamReader sReader = new(filePath))
            using (CsvReader csvReader = new(sReader, CultureInfo.InvariantCulture))
            {
                List<Employee> employeeList = csvReader.GetRecords<Employee>().ToList();

                employeeList.ForEach(emp => Console.WriteLine(emp.ToString()));
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