using System.Collections.Generic;

namespace EducationSystem
{
    public class OfflineCourseBuilder : ICourseBuilder
    {
        private OfflineCourse _course = new OfflineCourse();

        public OfflineCourseBuilder()
        {
            Reset();
        }

        public void Reset() => _course = new OfflineCourse();

        public void SetId(int id) => _course.Id = id;

        public void SetTitle(string title) => _course.Title = title;

        public void SetLocation(string location) => _course.Location = location;

        public void SetTeacher(Teacher teacher) => _course.AssignTeacher(teacher);

        public void AddStudent(Student student) => _course.EnrollStudent(student);

        public Course GetResult() => _course;
    }
}