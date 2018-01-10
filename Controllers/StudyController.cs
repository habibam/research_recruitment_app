using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using loginregister.Models;


namespace loginregister.Controllers
{
    public class StudyController : Controller
    {

        private MainContext _context;

        public StudyController(MainContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("home")]

        public IActionResult LandingPage()
        {

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if (loggedperson == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                System.Console.WriteLine("HEYYY" + loggedperson);
                User findtheperson = _context.users.SingleOrDefault(str => str.UserId == loggedperson);

                System.Console.WriteLine("FOUND PESON " + findtheperson);

                List<Study> allstudies = _context.studies.ToList();

                ViewBag.User = findtheperson;

                ViewBag.allstudies = allstudies;

                return View("Dashboard");
            }

        }

        [HttpGet]
        [Route("StudyForm")]

        public IActionResult StudyForm()
        {
            return View("StudyForm");
        }

        [HttpPost]
        [HttpGet]
        [Route("createstudy")]

        public IActionResult CreateStudy(Study study)
        {
            if (ModelState.IsValid)
            {

                int? owner = HttpContext.Session.GetInt32("loggedperson");

                if (owner == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Study NewStudy = new Study

                    {
                        StudyName = study.StudyName,
                        PrincipleInvestigator = study.PrincipleInvestigator,
                        StartDate = study.StartDate,
                        EndDate = study.EndDate,
                        Description = study.Description,
                        UserId = (int)owner

                    };

                    _context.Add(NewStudy);
                    _context.SaveChanges();
                    System.Console.WriteLine("NEW STUDY", NewStudy.StudyName);
                    return RedirectToAction("LandingPage");
                }
            }

            ViewBag.Error = "Your Product was not added, please fill out the form correctly!";
            return View("StudyForm");
        }

        [HttpGet]
        [Route("participantform/{StudyId}")]

        public IActionResult ParticipantForm(int StudyId)
        {

            int? owner = HttpContext.Session.GetInt32("loggedperson");

            if (owner == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.currentstudy = _context.studies.Where(x => x.StudyId == StudyId).SingleOrDefault();
                return View("ParticipantForm");


            }


        }

        [HttpPost]
        [Route("categorizeparticipant")]

        public IActionResult CategorizeParticipant(Participant participant, int studyid, int gender, int status, int age)
        {

            if (ModelState.IsValid)
            {

                System.Console.WriteLine("RIGHT BELOW THIS ***************************************");
                System.Console.WriteLine(participant.SubjectId);
                System.Console.WriteLine(gender + age + status);
                System.Console.WriteLine(studyid);
                int? owner = HttpContext.Session.GetInt32("loggedperson");

                if (owner == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var foundparticipant = _context.participants.SingleOrDefault(r => r.SubjectId == participant.SubjectId);
                    if (foundparticipant == null)
                    {
                        var category = gender + age + status;

                        Participant NewParticipant = new Participant

                        {
                            SubjectId = participant.SubjectId,
                            CategoryId = category,
                            StudyId = studyid,


                        };


                        _context.Add(NewParticipant);
                        _context.SaveChanges();
                        return RedirectToAction("LandingPage");
                    }
                    else
                    {
                        ViewBag.Error = "Your Participant already exists";
                        return View("ParticipantForm");
                    }

                }
            }
            return View("ParticipantForm");
        }

        [HttpGet]
        [Route("demographics/{StudyId}")]
        public IActionResult Demographics(int StudyId)
        {
            List<Participant> totallistofparticipants = _context.participants.Where(x => x.StudyId == StudyId).ToList();
            List<Participant> CategoryA = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 29).ToList();
            List<Participant> CategoryB = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 32).ToList();
            List<Participant> CategoryC = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 34).ToList();
            List<Participant> CategoryD = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 37).ToList();
            List<Participant> CategoryE = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 30).ToList();
            List<Participant> CategoryF = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 33).ToList();
            List<Participant> CategoryG = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 35).ToList();
            List<Participant> CategoryH = _context.participants.Where(x => x.StudyId == StudyId).Where(r => r.CategoryId == 38).ToList();

            @ViewBag.participant = totallistofparticipants;
            @ViewBag.CategoryA = CategoryA;
            @ViewBag.CategoryB = CategoryB;
            @ViewBag.CategoryC = CategoryC;
            @ViewBag.CategoryD = CategoryD;
            @ViewBag.CategoryE = CategoryE;
            @ViewBag.CategoryF = CategoryF;
            @ViewBag.CategoryG = CategoryG;
            @ViewBag.CategoryH = CategoryH;
        
            Study studyname = _context.studies.SingleOrDefault(c => c.StudyId == StudyId);
            @ViewBag.studyname = studyname;
            return View("Demographics");
        }


        [HttpGet]
        [Route("editstudyform/{StudyId}")]
        public IActionResult EditStudyForm(int StudyId)
        {
            Study curstudy = _context.studies.SingleOrDefault(r => r.StudyId == StudyId);
            @ViewBag.curstudy = curstudy;
            return View("EditStudy");
        }

        [HttpPost]
        [Route("editstudy")]

        public IActionResult EditStudy(Study Study, int studyid)
        {
            if (ModelState.IsValid)
            {

                int? owner = HttpContext.Session.GetInt32("loggedperson");

                if (owner == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Study foundstudy = _context.studies.SingleOrDefault(x => x.StudyId == studyid);
                    foundstudy.StudyName = Study.StudyName;
                    foundstudy.PrincipleInvestigator = Study.PrincipleInvestigator;
                    foundstudy.StartDate = Study.StartDate;
                    foundstudy.EndDate = Study.EndDate;
                    foundstudy.Description = Study.Description;
                    _context.SaveChanges();
                    return RedirectToAction("LandingPage");
                }
            }

            ViewBag.Error = "Your Product was not added, please fill out the form correctly!";
            return View("StudyForm");
        }

        [HttpPost]
        [HttpGet]
        [Route("delete/{StudyId}")]

        public IActionResult DeleteStudy(int StudyId)
        {

            int? owner = HttpContext.Session.GetInt32("loggedperson");

            if (owner == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Study deletestudy = _context.studies.SingleOrDefault(x => x.StudyId == StudyId);
                _context.Remove(deletestudy);
                _context.SaveChanges();
                return RedirectToAction("LandingPage");
            
            }

        }
    }

}


