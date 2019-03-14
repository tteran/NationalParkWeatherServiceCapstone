using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;

namespace WebApplication.Web.Controllers
{
    public class UserController : Controller
    {
        private IParkDAO parkDAO;
        private ISurveyDAO surveyDAO;
        public UserController(IParkDAO parkDAO, ISurveyDAO surveyDAO)
        {
            this.parkDAO = parkDAO;
            this.surveyDAO = surveyDAO;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Survey()
        {
            SurveyForm survey = new SurveyForm();
            survey.Parks = parkDAO.GetAllParks();
            return View(survey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Survey(SurveyForm survey)
        {
            bool wasSaved = surveyDAO.SaveSurvey(survey);

            if (wasSaved)
            {
                TempData["SurveySaved"] = "Thank you for participating in our survey!";
            }

            return RedirectToAction("Results");
        }

        [HttpGet]
        public IActionResult Results()
        {
            IList<SurveyResult> results = surveyDAO.GetSurveyResults();

            return View(results);
        }
    }
}