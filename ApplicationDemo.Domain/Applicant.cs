




namespace ApplicationDemo.Domain
{
    public class Applicant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public List<ComputerSkills> ComputerSkill { get; set; } = new List<ComputerSkills>();

    }
}
