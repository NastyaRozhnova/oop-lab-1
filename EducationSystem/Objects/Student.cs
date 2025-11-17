using System;

namespace EducationSystem
{
    public class Student
    {
        public int Id { get; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; } 

        public Student(int id, string name, int age, string gender)
        {
            Id = id;
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            Name = name;
            Age = age;
            if (gender == null)
                throw new ArgumentNullException(nameof(gender));
            Gender = gender;
        }

        public override string ToString()
            => $"Студент: Id = {Id}, Имя = {Name}, Возраст = {Age}, Пол = {Gender}";
    }
}