using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCV_Demo.Models;

namespace MyCV_Demo.Controllers
{
    public class CVController : Controller
    {
        private static CVModel _cv = new CVModel
        {
            FullName = "Conrad Mitchell",
            Title = "Software Developer",
            Summary = "An enthusiastic developer with a passion for building clean web apps. using ASP.NET",
            Email = "conradmitchell@outlook.com",
            Location = "Dorchester",
            Skills = new() { "C#", "ASP.NET Core", "SQL", "JavaScript" },
            WorkExperiences = new()
        {
            new WorkExperience { Company = "Bincom", Position = "Trainee C# Volunteer", Responsibilities = new List<string> {"Assisting with C# backend development tasks", "Attending daily standups"}, Duration = "Current", Description = "Currently building real work experience within Bincom, working under the PHP and C# team." },

            new WorkExperience { Company = "OneStop", Position = "Retail Sales Assistant", Duration = "Current", Description = "Currently working as a retail sales assistant " }
        },
            Educations = new()
        {
            new Education { Institution = "University of Bournemouth", Degree = "BSc Software Engineering", Year = 2023 }
        }
        };
        // Show CV
        [HttpGet]
        public ActionResult Index()
        {
            return View(_cv);
        }
        //Edit Form
        [HttpGet]
        public ActionResult Edit()
        {
            return View("Edit", _cv);
        }

        // Update: save changes
        [HttpPost]
        public ActionResult Edit(CVModel updatedCv)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedCv);
            }
            _cv = updatedCv;
            return RedirectToAction(nameof(Index));
        }
    }
}
