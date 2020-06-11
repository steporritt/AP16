using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AP16
{
    class Program
    {
        static void Main(string[] args)
        {
            // We could replace these with parameters, passed in via 'args'
            var sourceFileLocation = "Employees.txt";
            var recordsToDeleteFileLocation = "EmployeesToDelete.txt";
            var outputFileLocation = "Results.txt";

            // We'll give the user some feedback so they know what's going on.
            Console.WriteLine($"Looking for input files {sourceFileLocation} and {recordsToDeleteFileLocation}");

            IEnumerable<Employee> employees = null;
            IEnumerable<string> idsToDelete = null;

            // Load data from our two files
            try
            {
                employees = LoadEmployees(sourceFileLocation);
                idsToDelete = LoadIdsToDelete(recordsToDeleteFileLocation);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Oh no - this application hit an error looking for a required input file.\nThe error was {ex.Message}");
                Console.WriteLine($"The operation has been terminated. Press any key to exit the program.");
                Console.Read();

                Environment.Exit(-1);
            }

            // This is the line where we code the logic for our deletion.
            // TODO: If time allows, this is the line to optimize.
            var results = employees.Where(e => !idsToDelete.Contains(e.Id));

            // Save our new employee list, following our deletes
            Console.WriteLine("The application is now creating a new list of employees." +
                "\nPlease be patient as this operation may take some time.");

            SaveEmployeeList(outputFileLocation, results);

            Console.WriteLine($"All done. You can now view your new employee list in the file '{outputFileLocation}'");
            Console.WriteLine("Have a nice day. Press any key to exit");
            Console.Read();
        }

        /// <summary>
        /// Reads a list of employees from a file and returns them as a List of Employee
        /// </summary>
        /// <param name="InputFilePath">The source file containing the list of employees</param>
        /// <returns>A List of Employee</returns>
        private static IEnumerable<Employee> LoadEmployees(string InputFilePath)
        {
            // Some basic error checking here
            if (!File.Exists(InputFilePath))
                throw new FileNotFoundException($"{System.Reflection.MethodBase.GetCurrentMethod().Name}() could not find the specified file: \"{InputFilePath}\".");

            // Create an object to store and return our employee data
            var results = new List<Employee>();
            var employees = File.ReadAllLines(InputFilePath);

            // Parallel version of the below sequential code
            // This code was throwing an Exception ("Destination array was not long enough.") when we called the Add method.
            // For the purposes of this exercise, I felt I would use the sequential code for the time being and consider
            // debugging the parallel version if the sequential load took too long or if I had time left.
            //Parallel.ForEach(employees, employee =>
            //{
            //    var employeeData = employee.Split(',', 2);
            //    results.Add(new Employee { Id = employeeData[0], AdditionalInformation = employeeData[1] });
            //});

            foreach (var employee in employees)
            {
                var employeeData = employee.Split(',', 2);
                results.Add(new Employee { Id = employeeData[0], AdditionalInformation = employeeData[1] });
            };

            return results;
        }

        /// <summary>
        /// Reads the contents of a file that should be a list of employee Ids
        /// </summary>
        /// <param name="InputFilePath">The location of the file of employee Ids to be deleted</param>
        /// <returns>An IEnumerable of string - one for each line of the input file</returns>
        private static IEnumerable<string> LoadIdsToDelete(string InputFilePath)
        {
            // Some basic error checking here
            if (!File.Exists(InputFilePath))
                throw new FileNotFoundException($"{System.Reflection.MethodBase.GetCurrentMethod().Name}() could not find the specified file: \"{InputFilePath}\".");

            // Create an object to store and return our employee data
            var employees = File.ReadAllLines(InputFilePath);

            return employees;
        }

        /// <summary>
        /// Writes employee data to a specified file
        /// </summary>
        /// <param name="OutputFilePath">The location of the resultant file</param>
        /// <param name="Employees">An IEnumerable of Employee to be written to an output file</param>
        private static void SaveEmployeeList(string OutputFilePath, IEnumerable<Employee> Employees)
        {
            // We could add a check here to warn the user / halt the operation,
            // depending on if the output file already exists.

            // This is a very simple way of serializing Employee objects as strings
            var outputArray = Employees.Select(item => string.Concat(item.Id, ',', item.AdditionalInformation));
            File.WriteAllLines(OutputFilePath, outputArray);
        }
    }
}
