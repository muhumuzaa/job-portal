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
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        protected readonly string _connStr = string.Empty;

        public ApplicantEducationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantEducationPoco[] items)  
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO [dbo].[Applicant_Educations]([Id],[Certificate_Diploma],[Start_Date]," +
                        "[Completion_Date],[Completion_Percent],[Applicant],[Major]) " +
                        "VALUES (@Id,@Certificate_Diploma,@Start_Date,@Completion_Date,@Completion_Percent,@Applicant,@Major)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Applicant_Educations]", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<ApplicantEducationPoco>();
                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = (Guid)reader["Id"];
                    poco.CertificateDiploma = (String)reader["Certificate_Diploma"];
                    poco.StartDate = (DateTime)reader["Start_Date"];
                    poco.CompletionDate = (DateTime)reader["Completion_Date"];
                    poco.CompletionPercent = (Byte)reader["Completion_Percent"];
                    poco.Applicant = (Guid)reader["Applicant"];
                    poco.Major = (String)reader["Major"];
                    pocos.Add(poco);
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Educations] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations] SET 
                                        [Certificate_Diploma] = @Certificate_Diploma,
                                        [Start_Date] = @Start_Date,
                                        [Completion_Date] = @Completion_Date,
                                        [Completion_Percent] = @Completion_Percent,
                                        [Applicant] = @Applicant,
                                        [Major] = @Major
                                        WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}