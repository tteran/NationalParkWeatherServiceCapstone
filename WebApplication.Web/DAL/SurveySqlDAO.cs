using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class SurveySqlDAO : ISurveyDAO
    {
        private string connectionString;
        public SurveySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public IList<SurveyForm> GetSurveyResults()
        {
            IList<SurveyForm> results = new List<SurveyForm>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from survey_result", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SurveyForm survey = ConvertReaderToSurvey(reader);
                        results.Add(survey);
                    }
                }
            }
            catch (SqlException ex)
            {
                //ToDo: actually do something with this catch
                throw;
            }

            return results;
        }

        private SurveyForm ConvertReaderToSurvey(SqlDataReader reader)
        {
            SurveyForm survey = new SurveyForm()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                Email = Convert.ToString(reader["emailAddress"]),
                State = Convert.ToString(reader["state"]),
                ActivityLevel = Convert.ToString(reader["activityLevel"])
            };
            return survey;
        }

        public bool SaveSurvey(SurveyForm survey)
        {
            bool output;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into survey_results (parkCode, emailAddress, state, activityLevel) values @parkCode, @emailAddress, @state, @activityLevel", conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.Email);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();
                    output = true;
                }
            }
            catch(SqlException ex)
            {
                //ToDo - Learn how to deal with exceptions
                output = false;
            }

            return output;
        }
    }
}
