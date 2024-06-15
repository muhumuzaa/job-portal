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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        protected readonly string _connStr = string.Empty;

        public ApplicantResumeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantResumePoco[] items)
        {
            using(SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach(var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO dbo.Applicant_Resumes(Id, Applicant, Last_Updated, Resume) VALUES(@Id, @Applicant, @Last_Updated, @Resume)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using(var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Applicant_Resumes", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<ApplicantResumePoco>();

                while (reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = (Guid)reader["Id"];
                    poco.Applicant = (Guid)reader["Applicant"];
                    poco.Resume = (string)reader["Resume"];
                    poco.LastUpdated = reader.IsDBNull(reader.GetOrdinal("Last_Updated")) ? null : (DateTime?)reader["Last_Updated"];
                    pocos.Add(poco);

                }
                conn.Close();
                return pocos;
               
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] naviagtionProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Applicant_Resumes WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Applicant_Resumes SET " +
                        "Applicant =@Applicant, Last_Updated = @LastUpdated, Resume = @Resume WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@LastUpdated", item.LastUpdated);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    cmd.Parameters.AddWithValue("@Id", item.Id);



                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
        }
    }
}
