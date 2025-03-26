namespace CourseManagement.Framework
{
    public static class Utility
    {
        public static string GetInput(string prompt)
        {
            System.Console.Write(prompt);
            return System.Console.ReadLine();
        }
    }
}
