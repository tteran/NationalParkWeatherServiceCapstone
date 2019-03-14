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


        public IList<SurveyResult> GetSurveyResults()
        {
            IList<SurveyResult> results = new List<SurveyResult>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"select survey_result.parkCode as parkCode, park.parkName as parkNAME, count(*) as 'count'
                                                        from survey_result
                                                        join park on survey_result.parkCode = park.parkCode
                                                        group by survey_result.parkCode, park.parkName
                                                        order by count DESC, park.parkName;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SurveyResult survey = ConvertReaderToSurvey(reader);
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

        private SurveyResult ConvertReaderToSurvey(SqlDataReader reader)
        {
            SurveyResult survey = new SurveyResult()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                Votes = Convert.ToInt32(reader["count"])
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
