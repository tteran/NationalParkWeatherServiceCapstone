using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class SurveyForm
    {
        /// <summary>
        /// The code of the park voted for
        /// </summary>
        public string ParkCode { get; set; }

        /// <summary>
        /// The email address of the voter
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The home state of the Voter
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The Voter's activity level
        /// </summary>
        public string ActivityLevel { get; set; }
    }
}
