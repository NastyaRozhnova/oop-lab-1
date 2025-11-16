using Xunit;

namespace EducationSystem.Tests;

    public class StudentTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            int id = 1;
            string name = "Петр Гуменник";
            int age = 18;
            string gender = "Мужской";

            var student = new Student(id, name, age, gender);

            Assert.Equal(id, student.Id);
            Assert.Equal(name, student.Name);
            Assert.Equal(age, student.Age);
            Assert.Equal(gender, student.Gender);
        }
    }

    public class OfflineCourseTests
    {
        [Fact]
        public void RemoveStudent_RemovesById()
        {
            var course = new OfflineCourse();
            var s1 = new Student(1, "Петя", 18, "М");
            var s2 = new Student(2, "Аня", 19, "Ж");

            course.EnrollStudent(s1);
            course.EnrollStudent(s2);

            course.RemoveStudent(1);

            Assert.Single(course.Students);
            Assert.DoesNotContain(course.Students, s => s.Id == 1);
        }

        [Fact]
        public void AssignAndRemoveTeacher_WorksCorrectly()
        {
            var course = new OfflineCourse();
            var teacher = new Teacher(10, "Иван Иванович");

            course.AssignTeacher(teacher);
            Assert.Equal(teacher, course.Teacher);

            course.RemoveTeacher();
            Assert.Null(course.Teacher);
        }
    }

    public class OfflineCourseBuilderTests
    {
        [Fact]
        public void Builder_CreatesCourseWithGivenProperties()
        {
            var teacher = new Teacher(5, "Мария Сергеевна");
            var student = new Student(1, "Аня", 20, "Женский");

            var builder = new OfflineCourseBuilder();
            builder.Reset();
            builder.SetId(100);
            builder.SetTitle("Матанализ");
            builder.SetLocation("ауд. 1224");
            builder.SetTeacher(teacher);
            builder.AddStudent(student);

            var course = (OfflineCourse)builder.GetResult();

            Assert.Equal(100, course.Id);
            Assert.Equal("Матанализ", course.Title);
            Assert.Equal("ауд. 1224", course.Location);
            Assert.Equal(teacher, course.Teacher);
            Assert.Single(course.Students);
            Assert.Contains(student, course.Students);
        }
    }

    public class CourseManagementSystemTests
    {
        [Fact]
        public void RegisterCourse_AddsCourseToSystem()
        {
            var system = CourseManagementSystem.Instance;
            system.Reset();

            var course = new OfflineCourse();
            course.Id = 1;
            course.Title = "Тестовый курс";

            system.RegisterCourse(course);

            var allCourses = system.GetAllCourses();
            Assert.Single(allCourses);
            Assert.Contains(allCourses, c => c.Id == 1);
        }

        [Fact]
        public void RemoveCourse_RemovesExistingCourse()
        {
            var system = CourseManagementSystem.Instance;
            system.Reset();

            var course = new OfflineCourse { Id = 1, Title = "Курс" };
            system.RegisterCourse(course);

            var removed = system.RemoveCourse(1);

            Assert.True(removed);
            Assert.Empty(system.GetAllCourses());
        }

        [Fact]
        public void EnrollStudentToCourse_EnrollsViaSystem()
        {
            var system = CourseManagementSystem.Instance;
            system.Reset();

            var course = new OfflineCourse { Id = 1, Title = "Курс" };
            system.RegisterCourse(course);

            var student = new Student(1, "Петя", 18, "Мужской");

            var result = system.EnrollStudentToCourse(1, student);

            Assert.True(result);
            var students = system.GetStudentsForCourse(1);
            Assert.Single(students);
            Assert.Contains(student, students);
        }

        [Fact]
        public void GetCoursesByTeacher_ReturnsCorrectCourses()
        {
            var system = CourseManagementSystem.Instance;
            system.Reset();

            var teacher = new Teacher(10, "Иван Иванович");
            var course1 = new OfflineCourse { Id = 1, Title = "Курс 1" };
            var course2 = new OnlineCourse { Id = 2, Title = "Курс 2" };

            course1.AssignTeacher(teacher);
            course2.AssignTeacher(teacher);

            system.RegisterCourse(course1);
            system.RegisterCourse(course2);

            var courses = system.GetCoursesByTeacher(teacher.Id);

            Assert.Equal(2, courses.Count);
        }
    }
