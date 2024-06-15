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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {

        protected readonly string _connStr = string.Empty;

        public ApplicantSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO dbo.Applicant_Skills(Id, Skill_Level, Start_Month, Start_Year, End_Month, End_Year, Applicant, Skill) " +
                        "VALUES(@Id, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year,  @Applicant, @Skill)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                 
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Applicant_Skills", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<ApplicantSkillPoco>();

                while (reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = (Guid)reader["Id"];
                    poco.SkillLevel = (string)reader["Skill_Level"];
                    poco.StartMonth = (byte)reader["Start_Month"];
                    poco.StartYear = (int)reader["Start_Year"];
                    poco.EndMonth = (byte)reader["End_Month"];
                    poco.EndYear = (int)reader["End_Year"];
                    poco.Applicant = (Guid)reader["Applicant"];
                    poco.Skill = (string)reader["Skill"];
     
                    pocos.Add(poco);

                }
                conn.Close();
                return pocos;

            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] naviagtionProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Applicant_Skills WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Applicant_Skills SET " +
                        "Skill_Level = @Skill_Level, Start_Month = @Start_Month , Start_Year = @Start_Year, End_Month = @End_Month, End_Year = @End_Year, Applicant = @Applicant, Skill = @Skill WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
        }
    }
}
