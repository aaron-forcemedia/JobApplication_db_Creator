using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDemo.Domain
{
    public class JobSkills
    {
        
        public int Id { get; set; }
        public int JobId { get; set; }

        public string? SkillName { get; set; }

    }
}
