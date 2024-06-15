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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        protected readonly string _connStr = string.Empty;

        public SecurityLoginRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into Security_Logins(Id, Created_Date, Password_Update_Date, Agreement_Accepted_Date, Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language, Login, Password)" +
                       "values(@Id, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language, @Login, @Password)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Login", item.Login); 
                    cmd.Parameters.AddWithValue("@Password", item.Password);

                    cmd.ExecuteNonQuery();


                }
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Security_Logins", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                var pocos = new List<SecurityLoginPoco>();



                while (reader.Read())
                {
                    pocos.Add(new SecurityLoginPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Created = (DateTime)reader["Created_Date"],
                        PasswordUpdate = reader.IsDBNull("Password_Update_Date") ? null : (DateTime)reader["Password_Update_Date"],
                        AgreementAccepted = reader.IsDBNull("Agreement_Accepted_Date") ? null : (DateTime)reader["Agreement_Accepted_Date"],
                        IsLocked = (bool)reader["Is_Locked"],
                        IsInactive = (bool)reader["Is_Inactive"],
                        EmailAddress = reader.IsDBNull("Email_Address") ? null : (string)reader["Email_Address"], //
                        PhoneNumber = reader.IsDBNull("Phone_Number") ? null : (string)reader["Phone_Number"], //
                        FullName = reader.IsDBNull("Full_Name") ? null : (string)reader["Full_Name"], //
                        ForceChangePassword = (bool)reader["Force_Change_Password"],
                        PrefferredLanguage = reader.IsDBNull("Prefferred_Language") ? null : (string)reader["Prefferred_Language"], //
                        Login = reader.IsDBNull("Login") ? null : (string)reader["Login"],
                        Password = reader.IsDBNull("Password") ? null : (string)reader["Password"],
                      
                    }
                    );
                }
                conn.Close();
                return pocos;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] naviagtionProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM dbo.Security_Logins WHERE @Id = Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                foreach (var item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE dbo.Security_Logins SET Created_Date = @Created_Date, Password_Update_Date = @Password_Update_Date, " +
                        "Agreement_Accepted_Date = @Agreement_Accepted_Date, Is_Locked = @Is_Locked, Is_Inactive = @Is_Inactive, Email_Address = @Email_Address, " +
                        "Phone_Number = @Phone_Number, " +
                        "Full_Name=@Full_Name, Force_Change_Password=@Force_Change_Password, Prefferred_Language = @Prefferred_Language, Login = @Login, Password = @Password WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
