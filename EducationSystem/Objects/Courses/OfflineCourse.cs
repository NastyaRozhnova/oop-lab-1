using System;

namespace EducationSystem
{
    public class OfflineCourse : Course
    {
        public string Location { get; set; }

        public override string CourseType => "Offline";

        public OfflineCourse()
            : base()
        {
            Location = "Не указано";
        }

        public OfflineCourse(int id, string title, string location)
            : base(id, title)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public override string ToString() =>
            base.ToString() + $", место проведения: {Location}";
    }
}