using Blog.Common.DAL;
using Blog.Common.Rsp;
using Blog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Blog.DAL
{
    public class UserRep : GenericRep<BlogContext, User>
    {
        static string k = "A!9HHhi%XjjYY4YP2@Nob009Xghsr";

        public SingleRsp CreateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new BlogContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Users.FirstOrDefault(x => x.UserName.Equals(user.UserName));
                        if (p != null)
                        {
                            res.SetError("Username already exists!!!");
                        }
                        else
                        {
                            User d = user;
                            d.Password = Encrypt(user.Password);
                            var u = context.Users.Add(d);
                            context.SaveChanges();
                            tran.Commit();
                            res.SetMessage("Register Success!!!");
                        }

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp LoginUser(User u)
        {
            var res = new SingleRsp();
            using (var context = new BlogContext())
            {
                try
                {
                    var r = Encrypt(u.Password);
                    var p = context.Users.SingleOrDefault(x => x.UserName.Equals(u.UserName) && x.Password.Equals(r));
                    if (p == null)
                    {
                        res.SetError("Invalid username or password!!!");
                        return res;
                    }
                    else
                    {
                        res.SetMessage("Logged in successfully!!!");
                        res.Data = p;
                    }                    
                }
                catch (Exception ex)
                {

                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        public SingleRsp DeleteUser(int id)
        {
            var res = new SingleRsp();
            using (var context = new BlogContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Users.SingleOrDefault(x => x.UserId == id);
                        if (p != null)
                        {
                            Context.Users.Remove(p);
                            Context.SaveChanges();
                            tran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp UpdateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new BlogContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                            Context.Users.Update(user);
                            Context.SaveChanges();
                            tran.Commit();
                            res.SetMessage("Update Success!!!");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public List<User> ListUsers()
        {
            var context = new BlogContext();
            var p = context.Users.ToList();
            return p;
        }

        public User FindUserByID(int id)
        {
            var p = All.FirstOrDefault(p => p.UserId == id);
            return p;
        }

        public List<Report> ReportUser(int month, int year)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-82EJ2NL2\\SQLEXPRESS;Initial Catalog=Blog;Integrated Security=True");
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand("spThongKeSoLuongNguoiDung", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@Thang", SqlDbType.Int).Value = month;
            com.Parameters.Add("@Nam", SqlDbType.Int).Value = year;
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            List<Report> reports = new List<Report>();
            foreach (DataRow dr in dt.Rows)
            {
                Report r = new Report();
                r.year = int.Parse(dr[0].ToString());
                r.month = int.Parse(dr[1].ToString());
                r.count = int.Parse(dr[2].ToString());
                reports.Add(r);
            }
            return reports;
        }

        //Function Ma Hoa Mat Khau
        public string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(k));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(k));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }

        }
    }
}
