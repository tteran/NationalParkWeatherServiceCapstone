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
        public UserController(IParkDAO parkDAO)
        {
            this.parkDAO = parkDAO;
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
    }
}