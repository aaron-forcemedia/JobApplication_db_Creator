using ApplicationDemo.Data;
using ApplicationDemo.Domain;

namespace ApplicationDemo.UI
{
    class Program
    {
        public static ApplicationContext _context = new ApplicationContext();

        private static void Main(string[] args)
        {
            //Creates Database & Displays Date
            _context.Database.EnsureCreated();
            Console.WriteLine("Welcome to the Application Database");
            ApplicationCount();
            AddDefaultJobSkills();
            string dateString = GetDate();
            Console.WriteLine($"Today's Date is {dateString}");

            //Menu Options 
            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("|   <E>nter Applicant     |");
                Console.WriteLine("|   <R>emove Applicant    |");
                Console.WriteLine("|   <L>ist Applicants     |");
                Console.WriteLine("|   <V>iew Application    |");
                Console.WriteLine("|   View <S>kills         |");
                Console.WriteLine("|   <A>dd Skills          |");
                Console.WriteLine("|   Re<m>ove Skills       |");
                Console.WriteLine("|   <Q>uit                |");
                string menuOption = Console.ReadLine();
                List<Applicant> applications = _context.Applicants.ToList();
                if (menuOption.ToUpper() == "E")
                {
                    Console.Clear();
                    EnterApplicant(dateString);
                }
                if (menuOption.ToUpper() == "L")
                {
                    ListApplicants();
                }
                if (menuOption.ToUpper() == "A")
                {
                    Console.Clear();
                    ViewJobSkills();
                    AddJob();
                }
                if (menuOption.ToUpper() == "V")
                {
                    Console.Clear();
                    ListApplicants();
                    Console.WriteLine("Enter a Name to view");
                    string queryName = Console.ReadLine();
                    Console.Clear();
                    GetInfo(queryName.ToUpper());
                }
                if (menuOption.ToUpper() == "R")
                {
                    Console.Clear();
                    ListApplicants();
                    if (applications.Count > 0)
                    {
                        Console.WriteLine("Enter a Name to remove");
                        string removeName = Console.ReadLine();
                        Console.Clear();
                        RemoveApplicant(removeName.ToUpper());
                    }
                    else
                    {
                        Console.WriteLine("No applicants left to remove. Press Enter to return to menut.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                if (menuOption.ToUpper() == "S")
                {
                    Console.Clear();
                    ViewJobSkills();
                    Console.WriteLine("Press Any Key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (menuOption.ToUpper() == "M")
                {
                    Console.Clear();
                    ViewJobSkills();
                    Console.WriteLine("Enter a Name to remove or Press Enter to return to Menu");
                    string removeName = Console.ReadLine();
                    while (removeName != "")
                    {
                        Console.Clear();
                        RemoveJob(removeName.ToUpper());
                        removeName = "";
                    }
                    Console.Clear();
                }
                if (menuOption.ToUpper() == "Q")
                {
                    quit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Selection, Please try again.");
                }
            }
        }

        /// <summary>
        /// Sets JobSkills Table if there is no info in table
        /// </summary>
        private static void AddDefaultJobSkills()
        {
            List<JobSkills> jobSkills = _context.JobSkills.ToList();
            if (jobSkills.Count < 1)
            {
                Console.WriteLine("Default Job Skills Added. View & Change in Menu.");
                string[] skills = File.ReadAllLines("skillListText.txt");
                int i = 0;
                foreach (string skill in skills)
                {
                    JobSkills job = new JobSkills();
                    job.JobId = i;
                    job.SkillName = skill;
                    _context.JobSkills.Add(job);
                    i++;
                }
            }
            _context.SaveChanges();           
        }

        /// <summary>
        /// Views list of Job Skills
        /// </summary>
        private static void ViewJobSkills()
        {
            List<JobSkills> jobSkillsList = _context.JobSkills.ToList();
            Console.Clear();
            foreach (JobSkills job in jobSkillsList)
            {
                Console.WriteLine(job.SkillName);
            }
        }

        /// <summary>
        /// Recieves Application Input
        /// </summary>
        private static void EnterApplicant(string dateString)
        {
            List<Applicant> applicantList = _context.Applicants.ToList();
            Applicant newApplicant = new Applicant();
            bool nameValid = false;
            while (!nameValid)
            {
                Console.WriteLine("Name: ");
                newApplicant.Name = Console.ReadLine();
                if (applicantList.Any(a => a.Name.ToUpper() == newApplicant.Name.ToUpper()))
                {
                    Console.WriteLine("Name already exists. Input another name:");
                    nameValid = false;
                }
                else
                {
                    nameValid = true;
                }
            }
            Console.WriteLine("Address: ");
            newApplicant.Address = Console.ReadLine();
            Console.WriteLine("City: ");
            newApplicant.City = Console.ReadLine();
            Console.WriteLine("State: ");
            newApplicant.State = Console.ReadLine();
            Console.WriteLine("Zip: ");
            newApplicant.Zip = Console.ReadLine();
            _context.Applicants.Add(newApplicant);
            _context.SaveChanges();

            int newId = newApplicant.Id;

            Console.WriteLine();
            List<JobSkills> jobSkillList = _context.JobSkills.ToList();
            if (jobSkillList.Count > 0)
            {
                Console.WriteLine("For the following Computer Skills please enter 'Y' if these apply, if not enter any other key");
            }
            string skillResponse = "";

            foreach(JobSkills skill in jobSkillList)
            {
                ComputerSkills newComputerSkills = new ComputerSkills();
                Console.Write($"{skill.SkillName}: ");
                skillResponse = Console.ReadLine();
                if (skillResponse.ToUpper() == "Y")
                {
                    newComputerSkills.ApplicantId = newId;
                    newComputerSkills.SkillName = skill.SkillName;
                    _context.ComputerSkills.Add(newComputerSkills);
                }
            }
            _context.SaveChanges();

            dateString = GetDate();
            Console.WriteLine($"Application Submitted on {dateString}. Press Enter");
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Gets Date from external source
        /// </summary>
        /// <returns>Today's Date</returns>
        public static string GetDate()
        {
            DateTime thisDay = DateTime.Today;
            string dateString = thisDay.ToString("D");
            return dateString;
        }

        /// <summary>
        /// Counts total amount of applications
        /// </summary>
        private static void ApplicationCount()
        {
            List<Applicant> applications = _context.Applicants.ToList();
            Console.WriteLine($"Application Count is {applications.Count}");
        }

        /// <summary>
        /// Lists the applicant's name
        /// </summary>
        private static void ListApplicants()
        {
            List<Applicant> applications = _context.Applicants.ToList();
            Console.Clear();
            foreach (Applicant application in applications)
            {
                Console.WriteLine(application.Name);
            }
            Console.WriteLine("");
            ApplicationCount();
            Console.WriteLine("");
        }

        /// <summary>
        /// Adds job to Database list
        /// </summary>
        private static void AddJob()
        {
            List<JobSkills> addSkill = _context.JobSkills.ToList();
            bool quit = false;
            while (!quit)
            {
                bool jobValid = false;
                while (!jobValid)
                {
                    string job = "";
                    Console.Clear();
                    ViewJobSkills();
                    Console.WriteLine("Add a job to list: ");
                    JobSkills jobtoadd = new JobSkills();
                    job = Console.ReadLine();
                    
                    if (addSkill.Any(a => a.SkillName.ToUpper() == job.ToUpper()) || string.IsNullOrWhiteSpace(job))
                    {
                        Console.WriteLine("Name already exists or Invalid. Press Enter to continue..");
                        Console.ReadLine();
                        jobValid = false;
                    }
                    else
                    {
                        JobSkills jobToAdd = new JobSkills();
                        jobToAdd.SkillName = job;
                        Console.WriteLine($"{job} added to list");
                        _context.JobSkills.Add(jobToAdd);
                        _context.SaveChanges();
                        jobValid = true;
                    }
                }
                Console.WriteLine("Add another? (Enter N to Exit)");
                string quitString = "";
                quitString = Console.ReadLine();
                if (quitString.ToUpper() == "N")
                {
                    Console.Clear();
                    quit = true;
                }
            }                         
        }

        /// <summary>
        /// Removes JobSkill from Database list
        /// </summary>
        /// <param name="removeName"></param>
        private static void RemoveJob(string removeName)
        {
            
            List<JobSkills> remSkill = _context.JobSkills.Where(a => a.SkillName.ToUpper() == removeName).ToList();

            while (remSkill.Count == 0)
            {
                Console.WriteLine("Invalid Input. Try again.");
                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
                ViewJobSkills();
                Console.WriteLine("Enter Job Skill to remove: ");
                removeName = Console.ReadLine();
                remSkill = _context.JobSkills.Where(a => a.SkillName.ToUpper() == removeName).ToList();
            }

            foreach (JobSkills skill in remSkill)
            {
                string stringName = skill.SkillName;
                Console.WriteLine($"{stringName} has been removed. Press Enter to continue");
                _context.JobSkills.Remove(skill);
            }
            Console.ReadLine();
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes applicant based on name input
        /// </summary>
        /// <param name="removeName"></param>
        private static void RemoveApplicant(string removeName)
        {
            List<Applicant> remApplicant = _context.Applicants.Where(a => a.Name.ToUpper() == removeName).ToList();
            List<Applicant> applications = _context.Applicants.ToList();

            while (remApplicant.Count == 0 && applications.Count > 0)
            {
                Console.WriteLine("Invalid Input. Try again.");
                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
                ListApplicants();
                Console.WriteLine("Enter Name to remove: ");
                removeName = Console.ReadLine();
                remApplicant = _context.Applicants.Where(a => a.Name.ToUpper() == removeName).ToList();
            }

            foreach (Applicant application in remApplicant)
            {
                string stringName = application.Name;
                Console.WriteLine($"{stringName} has been removed.");

                List<ComputerSkills> remComputerSkill = _context.ComputerSkills.Where(a => a.Id == application.Id).ToList();
                foreach (ComputerSkills remApplication in remComputerSkill)
                {
                    _context.ComputerSkills.Remove(remApplication);
                }
                _context.Applicants.Remove(application);
            }

            _context.SaveChanges();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Retrieves applicant information from inputed name
        /// </summary>
        /// <param name="queryName"></param>
        private static void GetInfo(string queryName)
        {
            List<Applicant> applicant = _context.Applicants.Where(a => a.Name.ToUpper() == queryName).ToList();
            foreach (Applicant app in applicant)
            {
                Console.WriteLine(app.Name);
                Console.WriteLine(app.Address);
                Console.WriteLine(app.City);
                Console.WriteLine(app.State);
                Console.WriteLine(app.Zip);

                List<ComputerSkills> skillApplicant = _context.ComputerSkills.Where(a => a.ApplicantId == app.Id).ToList();
                Console.WriteLine("");
                Console.WriteLine("Job Skills");
                Console.WriteLine("---------------   ");
                int i = 0;
                foreach (ComputerSkills skill in skillApplicant)
                {
                    i++;
                    if (i == skillApplicant.Count && skillApplicant.Count > 1)
                    {
                        Console.Write(" & ");
                    }
                    Console.Write(skill.SkillName);
                    if (i < (skillApplicant.Count - 1))
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}