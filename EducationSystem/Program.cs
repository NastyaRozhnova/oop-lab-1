using System;

namespace EducationSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var system = CourseManagementSystem.Instance;

            var teacherOnline = new Teacher(4, "Иван Викторович Смирнов");
            var teacherOffline = new Teacher(5, "Мария Сергеевна Афанасьева");

            var student1 = new Student(1, "Петр Гуменник", 18, "Мужской");
            var student2 = new Student(2, "Елизавета Ломакина", 20, "Женский");
            var student3 = new Student(3, "Анна Ямалеева", 19, "Женский");

            var onlineBuilder1 = new OnlineCourseBuilder();
            onlineBuilder1.Reset();
            onlineBuilder1.SetId(1);
            onlineBuilder1.SetTitle("C# для начинающих");
            onlineBuilder1.SetLocation("Stepik");
            onlineBuilder1.SetUrl("https://stepik.org/course/csharp");
            onlineBuilder1.SetTeacher(teacherOnline);
            onlineBuilder1.AddStudent(student1);
            onlineBuilder1.AddStudent(student2);
            var onlineCourse1 = onlineBuilder1.GetResult();

            var onlineBuilder2 = new OnlineCourseBuilder();
            onlineBuilder2.Reset();
            onlineBuilder2.SetId(2);
            onlineBuilder2.SetTitle("ООП на C#");
            onlineBuilder2.SetLocation("Backboost");
            onlineBuilder2.SetUrl("https://backboost.courses.itmo.ru/members/courses/course463359935502");
            onlineBuilder2.SetTeacher(teacherOnline);
            onlineBuilder2.AddStudent(student2);
            var onlineCourse2 = onlineBuilder2.GetResult();

            var offlineBuilder = new OfflineCourseBuilder();
            offlineBuilder.Reset();
            offlineBuilder.SetId(3);
            offlineBuilder.SetTitle("Алгоритмы и структуры данных");
            offlineBuilder.SetLocation("Ломоносова, 9М, ауд. 1222");
            offlineBuilder.SetTeacher(teacherOffline);
            offlineBuilder.AddStudent(student2);
            offlineBuilder.AddStudent(student3);
            var offlineCourse = offlineBuilder.GetResult();

            system.RegisterCourse(onlineCourse1);
            system.RegisterCourse(onlineCourse2);
            system.RegisterCourse(offlineCourse);

            Console.WriteLine("=== Методы в моей системе ===");

            // Показать всех студентов для курса onlineCourse1
            Console.WriteLine($"\nСтуденты, записанные на курс \"{onlineCourse1.Title}\":");
            foreach (var student in system.GetStudentsForCourse(onlineCourse1.Id))
                Console.WriteLine($"- {student}");

            // Записать ещё одного студента на курс onlineCourse1 через систему
            Console.WriteLine($"\nЗаписываем {student3.Name} на курс \"{onlineCourse1.Title}\" через систему...");
            system.EnrollStudentToCourse(onlineCourse1.Id, student3);

            Console.WriteLine($"Обновлённый список студентов курса \"{onlineCourse1.Title}\":");
            foreach (var student in system.GetStudentsForCourse(onlineCourse1.Id))
                Console.WriteLine($"- {student}");

            // Снять преподавателя с оффлайн-курса
            Console.WriteLine($"\nСнимаем преподавателя с курса \"{offlineCourse.Title}\"...");
            system.RemoveTeacherFromCourse(offlineCourse.Id);
            Console.WriteLine(offlineCourse);

            // Назначить другого преподавателя курсу onlineCourse2
            Console.WriteLine($"\nПереназначаем преподавателя курса \"{onlineCourse2.Title}\" на {teacherOffline.Name}...");
            system.AssignTeacherToCourse(onlineCourse2.Id, teacherOffline);
            Console.WriteLine(onlineCourse2);

            // Удалить студента с оффлайн-курса
            Console.WriteLine($"\nУдаляем студента {student2.Name} с курса \"{offlineCourse.Title}\"...");
            system.RemoveStudentFromCourse(offlineCourse.Id, student2.Id);
            Console.WriteLine($"Студенты курса \"{offlineCourse.Title}\" после удаления:");
            foreach (var student in system.GetStudentsForCourse(offlineCourse.Id))
                Console.WriteLine($"- {student}");

            // Показать курсы по Id преподавателя
            Console.WriteLine($"\nКурсы, которые ведет преподаватель с Id = {teacherOnline.Id}:");
            foreach (var course in system.GetCoursesByTeacher(teacherOnline.Id))
                Console.WriteLine($"- {course.Title} [{course.CourseType}]");

            // Удалить курс из системы
            Console.WriteLine($"\nУдаляем курс \"{onlineCourse2.Title}\" из системы...");
            system.RemoveCourse(onlineCourse2.Id);

            Console.WriteLine("Все курсы в системе:");
            foreach (var course in system.GetAllCourses())
                Console.WriteLine(course);

            Console.WriteLine("\nКурсы, которые ведет " + teacherOnline.Name + ":");
            foreach (var course in system.GetCoursesByTeacher(teacherOnline))
                Console.WriteLine($"- {course.Title} [{course.CourseType}]");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}