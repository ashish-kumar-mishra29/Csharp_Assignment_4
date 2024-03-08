using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Program
{
    IList<Employee> employeeList;
    IList<Salary> salaryList;

    public Program()
    {
        employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

        salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
        Program program = new Program();

        program.Task1();

        program.Task2();

        program.Task3();
    }

    public void Task1()
    {
        // Print total Salary of all the employees with their corresponding names in ascending order of their salary.
        var totalSalaries = from emp in employeeList join sal in salaryList on emp.EmployeeID equals sal.EmployeeID
                            group sal.Amount by new { emp.EmployeeFirstName, emp.EmployeeLastName } into total
                            orderby total.Sum() ascending
                            select new
                            {
                                Name = $"{total.Key.EmployeeFirstName} {total.Key.EmployeeLastName}",
                                TotalSalary = total.Sum()
                            };

        Console.WriteLine("Total Salaries:");
        foreach (var item in totalSalaries)
        {
            Console.WriteLine($"{item.Name}: {item.TotalSalary}");
        }
        Console.WriteLine();
    }

    public void Task2()
    {
        // Print Employee details of 2nd oldest employee including his/her total monthly salary.
        var secondOldest = employeeList.OrderBy(x => x.Age).Skip(1).FirstOrDefault();
        if (secondOldest != null)
        {
            var totalMonthlySalary = salaryList.Where(sal => sal.EmployeeID == secondOldest.EmployeeID && sal.Type == SalaryType.Monthly).Sum(s => s.Amount);
            Console.WriteLine($"Details of Second Oldest Employee:");
            Console.WriteLine($"Name: {secondOldest.EmployeeFirstName} {secondOldest.EmployeeLastName}");
            Console.WriteLine($"Age: {secondOldest.Age}");
            Console.WriteLine($"Total Monthly Salary: {totalMonthlySalary}");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("No second oldest employee found.");
        }
    }

    public void Task3()
    {
        // Print means of Monthly, Performance, Bonus salary of employees whose age is greater than 30.
        var meanSalaries = from emp in employeeList
                           join sal in salaryList on emp.EmployeeID equals sal.EmployeeID
                           where emp.Age > 30
                           group sal.Amount by sal.Type into meanSal
                           select new
                           {
                               SalaryType = meanSal.Key,
                               MeanSalary = meanSal.Average()
                           };

        Console.WriteLine("Mean Salaries for Employees over 30:");
        foreach (var item in meanSalaries)
        {
            Console.WriteLine($"{item.SalaryType} Mean Salary: {item.MeanSalary}");
        }
    }
}

public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; } 
    public string EmployeeLastName { get; set; }
    public int Age { get; set; }
}

public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}