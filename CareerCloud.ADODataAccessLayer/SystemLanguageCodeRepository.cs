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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        protected readonly string _connStr = string.Empty;

        public SystemLanguageCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into dbo.System_Language_Codes(LanguageID, Native_Name, Name) values(@LanguageID, @NativeName, @Name)";
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.Parameters.AddWithValue("@NativeName", item.NativeName);
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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.System_Language_Codes", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<SystemLanguageCodePoco>();



                while (reader.Read())
                {
                    pocos.Add(new SystemLanguageCodePoco()
                    {
                        LanguageID = (string)reader["LanguageID"],
                        NativeName = (string)reader["Native_Name"],
                        Name = (string)reader["Name"]
                    }
                    );
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] naviagtionProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.System_Language_Codes WHERE @LanguageID = LanguageID";
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        //  NET.12-2-Ambroze-Muhumuza-N01686079

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.System_Language_Codes SET Native_Name = @Native_Name, Name = @Name WHERE LanguageID = @LanguageID";
                    cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
