namespace EducationSystem
{
    public interface ICourseBuilder
    {
        void Reset();
        void SetId(int id);
        void SetTitle(string title);
        void SetLocation(string location);
        void SetTeacher(Teacher teacher);
        void AddStudent(Student student);
        Course GetResult();
    }
}