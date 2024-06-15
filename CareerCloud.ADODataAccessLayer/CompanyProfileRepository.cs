using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        protected readonly string _connStr = string.Empty;

        public CompanyProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into dbo.Company_Profiles(Id, Company_Website, Registration_Date, Contact_Phone, Contact_Name, Company_Logo )" +
                       "values(@Id, @Company_Website, @Registration_Date, @Contact_Phone, @Contact_Name, @Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                    cmd.ExecuteNonQuery();


                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Company_Profiles", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<CompanyProfilePoco>();



                while (reader.Read())
                {
                    pocos.Add(new CompanyProfilePoco()
                    {
                        Id = (Guid)reader["Id"],
                        CompanyWebsite = reader.IsDBNull("Company_Website") ? null : (string)reader["Company_Website"],
                        RegistrationDate = (DateTime)reader["Registration_Date"],
                        ContactPhone = reader.IsDBNull("Contact_Phone") ? null : (string)reader["Contact_Phone"],
                        ContactName = reader.IsDBNull("Contact_Name") ? null: (string)reader["Contact_Name"],
                        CompanyLogo = reader.IsDBNull("Company_Logo") ? null :(byte[])reader["Company_Logo"]
                    }
                    );
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] naviagtionProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Company_Profiles WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Company_Profiles SET " +

                        "Company_Website = @Company_Website, " +
                        "Registration_Date = @Registration_Date, " +
                        "Contact_Phone = @Contact_Phone, " +
                        "Contact_Name =@Contact_Name ," +
                        "Company_Logo = @Company_Logo " +
                        "WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
