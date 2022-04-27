namespace ApplicationDemo.Domain
{
    public class ComputerSkills
    {
        public int Id { get; set; }

        public int ApplicantId { get; set; }

        public string SkillName { get; set; }

        public List<Applicant> Applicant { get; set; } = new List<Applicant>();


    }
}
