using System.ComponentModel.DataAnnotations;

namespace HR_Management_studio.Models;

public class Employee
{
	private int id;
	[Key]
	public int Id
	{
		get { return id; }
		set { id = value; }
	}

	private string name = string.Empty;
	[Required]
	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	private string lastName = string.Empty;
	[Required]
	public string LastName
	{
		get { return lastName; }
		set { lastName = value; }
	}

	private int age;
	[Required]
	public int Age
	{
		get { return age; }
		set { age = value; }
	}

	private string personalId = string.Empty;
	[Required, Length(11, 11, ErrorMessage = "Personal number must be 11 characters")]
	public string PersonalId
    {
		get { return personalId; }
		set { personalId = value; }
	}

	private DateTime dateOfEmployment = DateTime.Now;
    /// <summary>
    /// If not assigned default value of DateOfEmployment property will be DateTime.Now
    /// </summary>
	[Required]
	public DateTime DateOfEmployment
    {
		get { return dateOfEmployment; }
		set { dateOfEmployment = value; }
	}

	private decimal salary;
	[Required]
	public decimal Salary
	{
		get { return salary; }
		set { salary = value; }
	}
}
