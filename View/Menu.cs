using System;
using System.Collections.Generic;
using CourseManagement.Controller;
using CourseManagement.Framework;

namespace CourseManagement.View
{
    public class Menu
    {
        private readonly CourseController controller = new();

        // Dictionary chứa route và delegate tương ứng
        private readonly Dictionary<string, Action<Dictionary<string, string>>> Routes;

        public Menu()
        {
            Routes = new Dictionary<string, Action<Dictionary<string, string>>>
            {
                { "create", _ => CreateCourse() },
                { "list", _ => DisplayCourses() },
                { "remove", p => RemoveCourse(p) },
                { "exit", _ => Exit() }
            };
        }

        public void Start()
        {
            InitializeCourses();

            while (true)
            {
                Console.WriteLine("\nCourse Management System");
                Console.WriteLine("Commands: create, list, remove?id=course_name, exit");
                Console.Write("Enter command: ");

                string input = Console.ReadLine();
                var (command, parameters) = ParseCommand(input);

                if (Routes.TryGetValue(command, out var action))
                {
                    action(parameters); // Gọi delegate xử lý
                }
                else
                {
                    Console.WriteLine("Invalid command. Try again.");
                }
            }
        }

        private void InitializeCourses()
        {
            controller.CreateCourse("Introduction to Programming", 3, "Basics of programming using C#", "Fall 2025");
            controller.CreateCourse("Data Structures", 4, "In-depth study of data structures and algorithms", "Spring 2025");
            controller.CreateCourse("Database Management", 3, "Introduction to relational databases and SQL", "Summer 2025");
            controller.CreateCourse("Web Development", 3, "Front-end and back-end web technologies", "Fall 2025");
            controller.CreateCourse("Machine Learning Basics", 3, "Fundamentals of machine learning and AI", "Winter 2025");

            Console.WriteLine("Predefined courses have been added!");
        }

        private void CreateCourse()
        {
            string name = Utility.GetInput("Enter course name: ");
            int credit = int.Parse(Utility.GetInput("Enter course credit: "));
            string description = Utility.GetInput("Enter course description: ");
            string semester = Utility.GetInput("Enter semester: ");

            controller.CreateCourse(name, credit, description, semester);
            Console.WriteLine("Course added successfully!");
        }

        private void DisplayCourses()
        {
            var courses = controller.GetCourses();
            Console.WriteLine("\nList of Courses:");
            if (courses.Count == 0)
            {
                Console.WriteLine("No courses available.");
            }
            else
            {
                foreach (var course in courses)
                {
                    Console.WriteLine($"Name: {course.Name}, Credit: {course.Credit}, Description: {course.Description}, Semester: {course.Semester}");
                }
            }
        }

        private void RemoveCourse(Dictionary<string, string> parameters)
        {
            if (parameters.TryGetValue("id", out string courseName))
            {
                if (controller.DeleteCourse(courseName))
                {
                    Console.WriteLine($"Course '{courseName}' removed successfully!");
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }
            }
            else
            {
                Console.WriteLine("Please specify a course name using 'remove?id=course_name'.");
            }
        }

        private void Exit()
        {
            Console.WriteLine("Exiting program...");
            Environment.Exit(0);
        }

        // Helper method to parse command and parameters
        private (string command, Dictionary<string, string> parameters) ParseCommand(string input)
        {
            Dictionary<string, string> parameters = new();
            var parts = input.Split("?", StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToLower();

            if (parts.Length > 1)
            {
                var paramPairs = parts[1].Split("&", StringSplitOptions.RemoveEmptyEntries);
                foreach (var pair in paramPairs)
                {
                    var keyValue = pair.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    if (keyValue.Length == 2)
                    {
                        parameters[keyValue[0]] = keyValue[1];
                    }
                }
            }

            return (command, parameters);
        }
    }
}
