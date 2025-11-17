using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationSystem
{
    public abstract class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Teacher? Teacher { get; private set; }

        private readonly List<Student> _students = new List<Student>();
        public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

        public abstract string CourseType { get; }
        protected Course()
        {
            Id = 0;
            Title = "Без названия";
        }

        protected Course(int id, string title)
        {
            Id = id;
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            Title = title;
        }

        public void AssignTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            Teacher = teacher;
        }

        public void RemoveTeacher()
        {
            Teacher = null;
        }

        public void EnrollStudent(Student student)
        {
            if (student == null) 
                throw new ArgumentNullException(nameof(student));

            if (_students.Any(s => s.Id == student.Id))
                return;
            _students.Add(student);
        }

        public void RemoveStudent(int studentId)
        {
            var student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
                _students.Remove(student);
        }

        public override string ToString()
        {
            string teacherName;

            if (Teacher != null && Teacher.Name != null)
            {
                teacherName = Teacher.Name;
            }
            else
            {
                teacherName = "Преподаватель не назначен";
            }
            
            return $"{Title} [{CourseType}] (Id: {Id}), преподаватель: {teacherName}, студентов: {_students.Count}";
        }
    }
}