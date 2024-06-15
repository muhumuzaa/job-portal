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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        protected readonly string _connStr = string.Empty;

        public ApplicantProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantProfilePoco[] items)
        {
            using(var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach(var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO dbo.Applicant_Profiles(Id, Current_Salary, Current_Rate, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code, Login, Currency)" +
                        "VALUES(@Id, @Current_Salary, @Current_Rate, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code, @Login, @Currency)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using(var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Applicant_Profiles", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<ApplicantProfilePoco>();

                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = (Guid)reader["Id"];
                    poco.CurrentSalary = (decimal)reader["Current_Salary"];
                    poco.CurrentRate = (decimal)reader["Current_Rate"];
                    poco.Country = (string)reader["Country_Code"];
                    poco.Province = (string)reader["State_Province_Code"];
                    poco.Street = (string)reader["Street_Address"];
                    poco.City = (string)reader["City_Town"];
                    poco.PostalCode = (string)reader["Zip_Postal_Code"];
                    poco.Login = (Guid)reader["Login"];
                    poco.Currency = (string)reader["Currency"];
                    pocos.Add(poco);
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] naviagtionProperties)
        {

            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using(SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach(var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Applicant_Profiles WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach(var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Applicant_Profiles SET " +
                        "Current_Salary =@CurrentSalary, " +
                        "Current_Rate = @CurrentRate, " +
                        "Country_Code = @Country, " +
                        "State_Province_Code = @Province, " +
                        "Street_Address = @Street, " +
                        "City_Town = @City, " +
                        "Zip_Postal_Code = @PostalCode, " +
                        "Login = @Login, " +
                        "Currency = @Currency " +
                        "WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Country", item.Country);
                    cmd.Parameters.AddWithValue("@Province", item.Province);
                    cmd.Parameters.AddWithValue("@Street", item.Street);
                    cmd.Parameters.AddWithValue("@City", item.City);
                    cmd.Parameters.AddWithValue("@PostalCode", item.PostalCode);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
        }
    }
}
