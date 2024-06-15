using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        protected readonly string _connStr = string.Empty;

        public ApplicantJobApplicationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
           using(var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach(var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO dbo.Applicant_Job_Applications(Id, Application_Date,  Applicant, Job)" +
                        "VALUES(@Id, @Application_Date, @Applicant, @Job)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
                  
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using(var conn =    new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Applicant_Job_Applications", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<ApplicantJobApplicationPoco>();
                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = (Guid)reader["Id"];
                    poco.ApplicationDate = (DateTime)reader["Application_Date"];
              
                    poco.Applicant = (Guid)reader["Applicant"];
                    poco.Job = (Guid)reader["Job"];
                    pocos.Add(poco);
                    
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] naviagtionProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
            
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using(SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach(var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"DELETE FROM dbo.Applicant_Job_Applications WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using(SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach(var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Applicant_Job_Applications SET  Application_Date = @ApplicationDate, Applicant = @Applicant, Job = @Job WHERE Id = @Id ";
                    cmd.Parameters.AddWithValue("@ApplicationDate", item.ApplicationDate);
                 
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
        }
    }
}
