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
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {

        protected readonly string _connStr = string.Empty;

        public SystemCountryCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into dbo.System_Country_Codes( Code, Name) values( @Code, @Name)";
                   // cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.System_Country_Codes", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<SystemCountryCodePoco>();



                while (reader.Read())
                {
                    pocos.Add(new SystemCountryCodePoco()
                    {
                        //Id = (Guid)reader["Id"],
                        Code = (string)reader["Code"],
                        Name = (string)reader["Name"]
                    }
                    );
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] naviagtionProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.System_Country_Codes WHERE @Code = Code";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.System_Country_Codes SET Name = @Name WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    //cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
