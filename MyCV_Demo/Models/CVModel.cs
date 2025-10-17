namespace MyCV_Demo.Models
{
    public class CVModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Title { get; set; } = "";
        public string Email { get; set; } = "";

        public List<string> Skills { get; set; } = new();
        public List<WorkExperience> WorkExperiences { get; set; } = new();
        public List<Education> Educations { get; set; } = new();
        public string Summary { get; set; } = "";
        public string Location { get; set; } = "";
    }

    public class WorkExperience
    {
        public int Id { get; set; }
        public string Company { get; set; } = "";
        public string Position { get; set; } = "";
        public List<string> Responsibilities { get; set; } = new();
        public string Duration { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class Education
    {
        public int Id { get; set; }
        public string Institution { get; set; } = "";
        public string Degree { get; set; } = "";
        public int Year { get; set; }
    }
}
