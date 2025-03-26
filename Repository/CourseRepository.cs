using System.Collections.Generic;
using CourseManagement.Model;

namespace CourseManagement.Repository
{
    public class CourseRepository
    {
        private readonly List<Course> courses = new();

        public void AddCourse(Course course)
        {
            courses.Add(course);
        }

        public List<Course> GetAllCourses()
        {
            return new List<Course>(courses);
        }

        public bool RemoveCourse(string name)
        {
            var course = courses.Find(c => c.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            if (course != null)
            {
                courses.Remove(course);
                return true;
            }
            return false;
        }
    }
}
