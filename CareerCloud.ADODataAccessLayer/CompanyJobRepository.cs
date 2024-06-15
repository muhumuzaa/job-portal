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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {

        protected readonly string _connStr = string.Empty;

        public CompanyJobRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into Company_Jobs(Id, Profile_Created, Is_Inactive, Is_Company_Hidden, Company)" +
                       "values(@Id,@Profile_Created,@Is_Inactive,@Is_Company_Hidden, @Company)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                    cmd.Parameters.AddWithValue("@Company", item.Company);

                    cmd.ExecuteNonQuery();


                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Company_Jobs", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<CompanyJobPoco>();



                while (reader.Read())
                {
                    pocos.Add(new CompanyJobPoco()
                    {
                        Id = (Guid)reader["Id"],
                        ProfileCreated = (DateTime)reader["Profile_Created"],
                        IsInactive = (bool)reader["Is_Inactive"],
                        IsCompanyHidden = (bool)reader["Is_Company_Hidden"],
                        Company = (Guid)reader["Company"]

                    }
                    );
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] naviagtionProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Company_Jobs WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Company_Jobs SET Profile_Created = @Profile_Created, Is_Inactive = @Is_Inactive, Is_Company_Hidden = @Is_Company_Hidden, Company=@Company WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
