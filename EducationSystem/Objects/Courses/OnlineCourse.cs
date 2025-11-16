using System;

namespace EducationSystem
{
    public class OnlineCourse : Course
    {
        public string Platform { get; set; }
        public string Url { get; set; }

        public override string CourseType => "Online";

        public OnlineCourse()
            : base()
        {
            Platform = "Не указано";
            Url = "Не указано";
        }

        public OnlineCourse(int id, string title, string platform, string url)
            : base(id, title)
        {
            Platform = platform ?? throw new ArgumentNullException(nameof(platform));
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public override string ToString() =>
            base.ToString() + $", платформа: {Platform}, ссылка: {Url}";
    }
}