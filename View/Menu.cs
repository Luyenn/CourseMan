using System;
using System.Collections.Generic;
using CourseManagement.Controller;
using CourseManagement.Framework;

namespace CourseManagement.View
{
    public class Menu
    {
        private readonly CourseController controller = new();
        private readonly Dictionary<string, Action<Dictionary<string, string>>> Routes;

        public Menu()
        {
            Routes = new()
            {
                ["create"] = _ => CreateCourse(),
                ["list"] = _ => DisplayCourses(),
                ["remove"] = RemoveCourse,
                ["exit"] = _ => Exit()
            };
        }

        public void Start()
        {
            InitializeCourses();

            while (true)
            {
                Console.Write("\nCourse Management System\nCommands: create, list, remove?id=course_name, exit\nEnter command: ");
                var (command, parameters) = ParseCommand(Console.ReadLine());

                if (Routes.TryGetValue(command, out var action))
                    action(parameters);
                else
                    Console.WriteLine("Invalid command. Try again.");
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
            var name = Utility.GetInput("Enter course name: ");
            var credit = int.Parse(Utility.GetInput("Enter course credit: "));
            var description = Utility.GetInput("Enter course description: ");
            var semester = Utility.GetInput("Enter semester: ");

            controller.CreateCourse(name, credit, description, semester);
            Console.WriteLine("Course added successfully!");
        }

        private void DisplayCourses()
        {
            var courses = controller.GetCourses();
            Console.WriteLine("\nList of Courses:");
            if (courses.Count == 0)
                Console.WriteLine("No courses available.");
            else
                courses.ForEach(c =>
                    Console.WriteLine($"Name: {c.Name}, Credit: {c.Credit}, Description: {c.Description}, Semester: {c.Semester}")
                );
        }

        private void RemoveCourse(Dictionary<string, string> parameters)
        {
            if (parameters.TryGetValue("id", out string courseName))
            {
                Console.WriteLine(controller.DeleteCourse(courseName)
                    ? $"Course '{courseName}' removed successfully!"
                    : "Course not found.");
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

        private (string command, Dictionary<string, string>) ParseCommand(string input)
        {
            var parts = input.Split('?', StringSplitOptions.RemoveEmptyEntries);
            var parameters = new Dictionary<string, string>();

            if (parts.Length > 1)
            {
                foreach (var pair in parts[1].Split('&'))
                {
                    var kv = pair.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if (kv.Length == 2) parameters[kv[0]] = kv[1];
                }
            }

            return (parts[0].ToLower(), parameters);
        }
    }
}
