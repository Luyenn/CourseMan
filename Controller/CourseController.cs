using System.Collections.Generic;
using CourseManagement.Model;
using CourseManagement.Repository;

namespace CourseManagement.Controller
{
    public class CourseController
    {
        private readonly CourseRepository repository = new();

        public void CreateCourse(string name, int credit, string description, string semester)
        {
            var course = new Course
            {
                Name = name,
                Credit = credit,
                Description = description,
                Semester = semester
            };
            repository.AddCourse(course);
        }

        public List<Course> GetCourses()
        {
            return repository.GetAllCourses();
        }

        public bool DeleteCourse(string name)
        {
            return repository.RemoveCourse(name);
        }
    }
}
