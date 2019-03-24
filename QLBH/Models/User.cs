using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using QLBH.DatabaseModels;
using System.Data.Entity.Infrastructure;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace QLBH.Models
{
    public class User
    {
        private int? user_id;
        private string username;
        private string password;
        private string fullname;
        private string email;
        shopEntities db = new shopEntities();

        public User() { }
        public User(int? user_id, string username, string password, string fullname, string email)
        {
            this.User_id = user_id;
            this.Username = username;
            this.Password = password;
            this.Fullname = fullname;
            this.Email = email;
        }

        public int? User_id { get => user_id; set => user_id = value; }
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [StringLength(50, ErrorMessage = "Tài khoản không được vượt quá 50 ký tự")]
        public string Username { get => username; set => username = value; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get => password; set => password = value; }
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string Fullname { get => fullname; set => fullname = value; }
        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        public string Email { get => email; set => email = value; }


        //Get Data
        public List<user> getUserList()
        {
            var users = db.users.SqlQuery("SELECT * FROM USERS").ToList();
            return users;
        }

        public User getUserbyId(int? user_id)
        {
            this.User_id = user_id;
            var getData = db.users.SqlQuery("select * from users where user_id = @id", new MySqlParameter("id", this.User_id))
                .FirstOrDefault();

            User userdata = new User(getData.user_id, getData.username, getData.password, getData.fullname, getData.email);

            return userdata;
        }

        //Session
        public bool doLogin(string username, string password)
        {
            return db.users.SingleOrDefault(c => c.username == username && c.password == password) != null;
        }

        public User getFullUserInfo(string username)
        {
            this.Username = username;
            var getData = db.users.SqlQuery("select * from users where username = @id", new MySqlParameter("id", this.Username))
                .FirstOrDefault();

            User userdata = new User(getData.user_id, getData.username, getData.password, getData.fullname, getData.email);

            return userdata;
        }


        //Check Value Condition
        public bool checkUserExisted(string username)
        {
            this.Username = username;
            var getData = db.users.SqlQuery("select * from users where username = @username", new MySqlParameter("username", this.Username))
                .FirstOrDefault();

            if (getData != null)
                return true;

            return false;
        }


        public bool checkEmailExisted(string email)
        {
            this.Email = email;
            var getData = db.users.SqlQuery("select * from users where email = @email", new MySqlParameter("email", this.Email))
                .FirstOrDefault();

            if (getData != null)
                return true;

            return false;
        }
        //Change Value

        public int insertData(string username, string password, string fullname, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Fullname = fullname;
            this.Email = email;

            MySqlParameter username_param = new MySqlParameter("username", this.Username);
            MySqlParameter password_param = new MySqlParameter("password", this.Password);
            MySqlParameter fullname_param = new MySqlParameter("fullname", this.Fullname);
            MySqlParameter email_param = new MySqlParameter("email", this.Email);

            string commandText = "INSERT Users (username, password, fullname, email) " +
                                        "VALUES (@username, @password,  @fullname,  @email)";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, username_param, password_param, fullname_param, email_param);

            return noOfRowInserted;
        }

        public int updateData(int? user_id, string fullname, string email)
        {
            this.User_id = user_id;
            this.Fullname = fullname;
            this.Email = email;

            MySqlParameter id_param = new MySqlParameter("id", this.User_id);
            MySqlParameter fullname_param = new MySqlParameter("fullname", this.Fullname);
            MySqlParameter email_param = new MySqlParameter("email", this.Email);

            string commandText = "UPDATE users SET fullname = @fullname, email = @email WHERE user_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, fullname_param, email_param, id_param);

            return noOfRowInserted;
        }

        public int deleteData(int? user_id)
        {
            this.User_id = user_id;
            MySqlParameter id_param = new MySqlParameter("id", this.User_id);
            string commandText = "DELETE FROM users WHERE user_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, id_param);

            return noOfRowInserted;
        }
    }
}