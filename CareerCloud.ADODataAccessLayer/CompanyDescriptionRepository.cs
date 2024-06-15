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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {

        protected readonly string _connStr = string.Empty;

        public CompanyDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into Company_Descriptions(Id,LanguageId,Company,Company_Name,Company_Description)" +
                       "values(@Id,@LanguageId,@Company,@Company_Name,@Company_Description)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageId", item.LanguageId);
                    cmd.ExecuteNonQuery();

           
                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Company_Descriptions", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<CompanyDescriptionPoco>();

              

                while (reader.Read())
                {
                    pocos.Add(new CompanyDescriptionPoco()
                    {
                        Id = (Guid)reader["Id"],
                        LanguageId = (string)reader["LanguageId"],
                        Company = (Guid)reader["Company"],
                        CompanyName = (string)reader["Company_Name"],
                        CompanyDescription = (string)reader["Company_Description"]
                    }
                    );
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] naviagtionProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Company_Descriptions WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Company_Descriptions SET " +

                        "Company_Name = @Company_Name, " +
                        "Company_Description = @Company_Description, " +
                        "Company = @Company, " +
                        "LanguageId =@LanguageId " +
                      
                        "WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageId", item.LanguageId);
               
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
        }
    }
}
