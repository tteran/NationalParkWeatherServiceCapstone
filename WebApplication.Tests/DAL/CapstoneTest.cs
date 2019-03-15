using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace WebApplication.Tests.DAL
{
    [TestClass]
    public class CapstoneTest
    {
        protected string ConnectionString { get; } = "Server =.\\SQLEXPRESS;Initial Catalog = NPGeek; Integrated Security = True";

        private TransactionScope transaction;


        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();

            string sql = File.ReadAllText("test-script.sql");

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"select count(*) from {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }

    }

}
}
