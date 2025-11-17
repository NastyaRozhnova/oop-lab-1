using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationSystem
{
    public sealed class CourseManagementSystem
    {
        private static readonly Lazy<CourseManagementSystem> _instance =
            new Lazy<CourseManagementSystem>(() => new CourseManagementSystem());

        public static CourseManagementSystem Instance => _instance.Value;

        private CourseManagementSystem() { }

        private readonly List<Course> _courses = new List<Course>();

        public void Reset()
        {
            _courses.Clear();
        }


        public void RegisterCourse(Course course)
        {
            if (course == null)    
                throw new ArgumentNullException(nameof(course));
            _courses.Add(course);
        }

        public bool RemoveCourse(int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return false;

            _courses.Remove(course);
            return true;
        }

        public bool AssignTeacherToCourse(int courseId, Teacher teacher)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return false;

            course.AssignTeacher(teacher);
            return true;
        }

        public bool RemoveTeacherFromCourse(int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            return false;

            course.RemoveTeacher();
            return true;
        }

        public bool EnrollStudentToCourse(int courseId, Student student)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return false;

            course.EnrollStudent(student);
            return true;
        }

        public bool RemoveStudentFromCourse(int courseId, int studentId)
        {       
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return false;

            course.RemoveStudent(studentId);
                return true;
        }

        public IReadOnlyCollection<Student> GetStudentsForCourse(int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            return course?.Students ?? Array.Empty<Student>();
        }

        public IReadOnlyCollection<Course> GetAllCourses() => _courses.AsReadOnly();

        public IReadOnlyCollection<Course> GetCoursesByTeacher(int teacherId)
        {
            var result = new List<Course>();

            foreach (var course in _courses)
            {
                if (course.Teacher != null && course.Teacher.Id == teacherId)
                {
                    result.Add(course);
                }
            }

            return result.AsReadOnly();
        }

        public IReadOnlyCollection<Course> GetCoursesByTeacher(Teacher teacher)
        {
            if (teacher == null) 
                throw new ArgumentNullException(nameof(teacher));
            return GetCoursesByTeacher(teacher.Id);
        }

        public IReadOnlyCollection<Course> GetCoursesForStudent(int studentId)
        {
            var result = new List<Course>();

            foreach (var course in _courses)
            {
                foreach (var student in course.Students)
                {
                    if (student.Id == studentId)
                    {
                        result.Add(course);
                        break; 
                    }
                }
            }

            return result.AsReadOnly();
        }
    }
}