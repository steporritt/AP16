using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AP16
{
    class Program
    {
        static void Main(string[] args)
        {
            // We could replace these with parameters to the application in future
            var sourceFileLocation = "Resources\\Employees.txt";
            var recordsToDeleteFileLocation = "Resources\\EmployeesToDelete.txt";


        }

        /// <summary>
        /// Reads a list of employees from a file and returns them as a List of Employee
        /// </summary>
        /// <param name="InputFilePath">The source file containing the list of employees</param>
        /// <returns>A List of Employee</returns>
        private static IEnumerable<Employee> LoadEmployees(string InputFilePath)
        {
            if (!File.Exists(InputFilePath))
                throw new FileNotFoundException($"LoadEmployees() could not find the specified file: \"{InputFilePath}\".");
            
            var employees = File.ReadAllLines(InputFilePath);

            return employees.ToList();

            return employees;
        }

        private
    }
}
