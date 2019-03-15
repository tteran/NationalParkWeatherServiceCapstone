using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;

namespace WebApplication.Tests.DAL
{
    public class SurveySqlDAOTests : CapstoneTest
    {
        [TestMethod]
        public void SaveSurvey_Should_Return1Moresurvey()
        {
            SurveySqlDAO dao = new SurveySqlDAO(ConnectionString);
            int startingRows = GetRowCount("survey_result");
            SurveyForm survey = new SurveyForm();
            survey.ParkCode = parkCode;
            survey.Email = "";
            survey.State = "OH";
            survey.ActivityLevel = "Active";

            dao.SaveSurvey(survey);
            int endingRows = GetRowCount("survey-Result");

            Assert.AreEqual(startingRows + 1, endingRows);
        }

    }
}
