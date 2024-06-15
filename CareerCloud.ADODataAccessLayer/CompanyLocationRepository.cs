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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {

        protected readonly string _connStr = string.Empty;

        public CompanyLocationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into dbo.Company_Locations(Id, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code, Company)" +
                       "values(@Id, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code, @Company)";   

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Company_Locations", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<CompanyLocationPoco>();



                while (reader.Read())
                {
                    pocos.Add(new CompanyLocationPoco()
                    {
                        Id = (Guid)reader["Id"],
                        CountryCode = reader.IsDBNull("Country_Code") ? null : (string)reader["Country_Code"], 
                        Province = reader.IsDBNull("State_Province_Code") ? null : (string)reader["State_Province_Code"],
                        Street = reader.IsDBNull("Street_Address") ? null : (string)reader["Street_Address"],
                        City = reader.IsDBNull("City_Town") ? null : (string)reader["City_Town"],
                        PostalCode = reader.IsDBNull("Zip_Postal_Code") ? null : (string)reader["Zip_Postal_Code"], 
                        Company =  (Guid)reader["Company"],
                        
                    }
                    );
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] naviagtionProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Company_Locations WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Company_Locations SET Country_Code = @Country_Code, State_Province_Code = @State_Province_Code, Street_Address = @Street_Address, " +
                        "City_Town=@City_Town, Zip_Postal_Code = @Zip_Postal_Code, Company = @Company WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                   
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
