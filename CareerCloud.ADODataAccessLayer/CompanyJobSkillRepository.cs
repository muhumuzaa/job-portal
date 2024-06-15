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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {

        protected readonly string _connStr = string.Empty;

        public CompanyJobSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyJobSkillPoco[] items)
        {
           

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into dbo.Company_Job_Skills(Id, Skill_Level, Skill, Importance, Job)" +
                       "values(@Id, @Skill_Level, @Skill, @Importance,  @Job)";    // putting the column names and values in the wrong order was causing this error. Id should be added

                   cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                   // cmd.Parameters.AddWithValue("@Job", Convert.ToInt32(item.Job.ToString()));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            /*

           using (SqlConnection conn = new SqlConnection(_connStr))
           {
               conn.Open();
               foreach (var item in items)
               {
                   SqlCommand cmd = new SqlCommand();
                   cmd.Connection = conn;
                   cmd.CommandText = "Insert into Company_Job_Skills(Id,Skill_Level,Skill,Importance,Job)" +
                      "values(@Id,@Skill_Level,@Skill,@Importance,@Job)";
                   cmd.Parameters.AddWithValue("@Id", item.Id);
                   cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                   cmd.Parameters.AddWithValue("@Skill", item.Skill);
                   cmd.Parameters.AddWithValue("@Importance", item.Importance);
                   cmd.Parameters.AddWithValue("@Job", item.Job);
                   cmd.ExecuteNonQuery();


               }
               conn.Close();
           }*/
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Company_Job_Skills", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<CompanyJobSkillPoco>();



                while (reader.Read())
                {
                    pocos.Add(new CompanyJobSkillPoco()
                    {
                        Id = (Guid)reader["Id"],
                        SkillLevel = (string)reader["Skill_Level"], //is the value : table name
                        Skill = (string)reader["Skill"],
                        Importance = (int)reader["Importance"],
                        Job = (Guid)reader["Job"]
                    }
                    );
                }
                conn.Close();
                return pocos;
            }


        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] naviagtionProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Company_Job_Skills WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Company_Job_Skills SET Skill_Level = @Skill_Level, Skill = @Skill, Importance = @Importance, Job=@Job WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
