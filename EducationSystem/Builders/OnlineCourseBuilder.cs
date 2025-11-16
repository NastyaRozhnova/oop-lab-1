using System.Collections.Generic;

namespace EducationSystem
{
    public class OnlineCourseBuilder : ICourseBuilder
    {
        private OnlineCourse _course = new OnlineCourse();

        public OnlineCourseBuilder()
        {
            Reset();
        }

        public void Reset() => _course = new OnlineCourse();

        public void SetId(int id) => _course.Id = id;

        public void SetTitle(string title) => _course.Title = title;

        public void SetLocation(string platform) => _course.Platform = platform;

        public void SetUrl(string url) => _course.Url = url;

        public void SetTeacher(Teacher teacher) => _course.AssignTeacher(teacher);

        public void AddStudent(Student student) => _course.EnrollStudent(student);

        public Course GetResult() => _course;
    }
}