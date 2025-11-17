using System;

namespace EducationSystem
{
    public class Teacher
    {
        public int Id { get; }
        public string Name { get; }

        public Teacher(int id, string name)
        {
            Id = id;
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            Name = name; 
        }

        public override string ToString() => $"{Name} ({Id})";
    }
}