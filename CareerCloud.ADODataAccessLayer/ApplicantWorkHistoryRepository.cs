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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        protected readonly string _connStr = string.Empty;

        public ApplicantWorkHistoryRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO dbo.Applicant_Work_History" +
                        "(" +
                        "Id, " +
                        "Company_Name, " +
                        "Country_Code, " +
                        "Job_Title, " +
                        "Job_Description, " +
                        "Start_Month, " +
                        "Start_Year, " +
                        "End_Month, " +
                        "End_Year, " +
                        "Applicant, " +
                        "Location" +
                        ") " +
                        "VALUES" +
                        "(" +
                        "@Id, " +
                        "@Company_Name, " +
                        "@Country_Code, " +
                        "@Job_Title, " +
                        "@Job_Description, " +
                        "@Start_Month, " +
                        "@Start_Year, " +
                        "@End_Month, " +
                        "@End_Year, " +
                        "@Applicant, " +
                        "@Location" +
                        ")";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                   
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Location", item.Location);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Applicant_Work_History", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<ApplicantWorkHistoryPoco>();

                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = (Guid)reader["Id"];
                    poco.CompanyName = (string)reader["Company_Name"];
                    poco.CountryCode = (string)reader["Country_Code"];
                    poco.JobTitle = (string)reader["Job_Title"];
                    poco.JobDescription = (string)reader["Job_Description"];
                    poco.StartMonth = (short)reader["Start_Month"];
                    poco.StartYear = (int)reader["Start_Year"];
                    poco.EndMonth = (short)reader["End_Month"];
                    poco.EndYear = (int)reader["End_Year"];
                    //poco.Applicant = (string)reader["Applicant"].ToString(); //needed explicit conversion
                    poco.Applicant = (Guid)reader["Applicant"]; //changed to this coz it was giving an error
                    poco.Location = (string)reader["Location"];

                    pocos.Add(poco);

                }
                conn.Close();
                return pocos;
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] naviagtionProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Applicant_Work_History WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Applicant_Work_History SET " +
                      
                        "Company_Name = @Company_Name, " +
                        "Country_Code = @Country_Code, " +
                        "Job_Title = @Job_Title, " +
                        "Job_Description =@Job_Description, " +
                        "Start_Month = @Start_Month, " +
                        "Start_Year = @Start_Year, " +
                        "End_Month = @End_Month, " +
                        "End_Year = @End_Year, " +
                        "Applicant = @Applicant, " +
                        "Location = @Location " +
                        "WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Location", item.Location);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
        }
    }
}
