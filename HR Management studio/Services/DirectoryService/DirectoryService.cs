using System.Globalization;
using CsvHelper;
using HR_Management_studio.Models;

namespace HR_Management_studio.Services.DirectoryService;

public static class DirectoryService
{
    private static readonly string _filePath = @$"{Directory.GetCurrentDirectory()}\HRMS\Employee";
    
    /// <summary>
    /// Gets Id of the last Employee added to ~/Employee.csv
    /// </summary>
    /// <returns>int</returns>
    public static int GetCurrentId()
    {
		try
		{
            string csvFilePath = Path.Combine(_filePath, "Employee.csv");
            bool fileExists = File.Exists(csvFilePath);

            using (StreamReader sReader = new(csvFilePath))
            using (CsvReader csvReader = new(sReader, CultureInfo.InvariantCulture))
            {
                if(fileExists && csvReader.Read())
                {
                    csvReader.ReadHeader();
                    return csvReader.GetRecords<Employee>().Last().Id;
                }
                else if (fileExists && !csvReader.Read())
                    return 0;
                else
                    throw new Exception("Error finding file");
            }
        }
		catch (Exception ex)
		{
            Console.WriteLine(ex.Message);
            return -1;
		}
    }
}