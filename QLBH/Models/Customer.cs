using MySql.Data.MySqlClient;
using QLBH.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QLBH.Models
{

    public class Customer
    {
        private int? cus_id;
        private string cus_name;
        private string cus_password;
        private string cus_fullname;
        private string cus_email;
        private string cus_phone;
        shopEntities db = new shopEntities();

        public Customer() { }
        public Customer(int? cus_id, string cus_name, string cus_password, string cus_fullname, string cus_email, string cus_phone)
        {
            this.Cus_id = cus_id;
            this.Cus_name = cus_name;
            this.Cus_password = cus_password;
            this.Cus_fullname = cus_fullname;
            this.Cus_email = cus_email;
            this.Cus_phone = cus_phone;
        }
        [Display(Name = "ID")]
        public int? Cus_id { get => cus_id; set => cus_id = value; }

        [Required(ErrorMessage = "Tài khoản sản phẩm không được để trống")]
        [StringLength(50, ErrorMessage = "Tài khoản không được vượt quá 255 ký tự")]
        [Display(Name = "Tài khoản")]
        public string Cus_name { get => cus_name; set => cus_name = value; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(50, ErrorMessage = "Mật khẩu không được vượt quá 50 ký tự")]
        [Display(Name = "Mật khẩu")]
        public string Cus_password { get => cus_password; set => cus_password = value; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(50, ErrorMessage = "Họ tên không được vượt quá 50 ký tự")]
        [Display(Name = "Họ tên")]
        public string Cus_fullname { get => cus_fullname; set => cus_fullname = value; }

        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        [Display(Name = "Email")]
        public string Cus_email { get => cus_email; set => cus_email = value; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        [Display(Name = "Số điện thoại")]
        public string Cus_phone { get => cus_phone; set => cus_phone = value; }

        //Get Data
        public List<customer> getList()
        {
            var customers = db.customers.SqlQuery("SELECT * FROM customers").ToList();
            return customers;
        }

        public Customer getDatabyId(int? id)
        {
            this.Cus_id = id;
            var getData = db.customers.SqlQuery("select * from customers where cus_id = @id", new MySqlParameter("id", this.Cus_id))
                .FirstOrDefault();

            Customer customer = new Customer(getData.cus_id, getData.cus_name, getData.cus_password, getData.cus_fullname, getData.cus_email, getData.cus_phone);

            return customer;
        }
        //Check Data
        public bool checkCustomerExisted(string email)
        {
            this.Cus_email = email;
            var getData = db.customers.SqlQuery("select * from customers where cus_email = @email", new MySqlParameter("email", this.Cus_email))
                .FirstOrDefault();

            if (getData != null)
                return true;

            return false;
        }

        //Update Data
        public int insertData(string cus_name, string cus_fullname, string cus_password, string cus_email, string cus_phone)
        {
            this.Cus_name = cus_name;
            this.Cus_password = cus_password;
            this.Cus_fullname = cus_fullname;
            this.Cus_email = cus_email;
            this.Cus_phone = cus_phone;

            MySqlParameter p_name = new MySqlParameter("p_name", this.Cus_name);
            MySqlParameter p_pass = new MySqlParameter("p_pass", this.Cus_password);
            MySqlParameter p_fullname = new MySqlParameter("p_fullname", this.Cus_fullname);
            MySqlParameter p_email = new MySqlParameter("p_email", this.Cus_email = cus_email);
            MySqlParameter p_phone = new MySqlParameter("p_phone", this.Cus_phone);

            string commandText = "INSERT customers (cus_name, cus_password, cus_fullname, cus_email, cus_phone) " +
                                        "VALUES (@p_name, @p_pass, @p_fullname, @p_email, @p_phone)";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_name, p_pass, p_fullname, p_email, p_phone);

            return noOfRowInserted;
        }

        public int updateData(int? id, string cus_name, string cus_password ,string cus_fullname, string cus_email, string cus_phone)
        {
            this.Cus_id = id;
            this.Cus_name = cus_name;
            this.Cus_password = cus_password;
            this.Cus_fullname = cus_fullname;
            this.Cus_email = cus_email;
            this.Cus_phone = cus_phone;

            MySqlParameter p_id = new MySqlParameter("id", this.Cus_id);
            MySqlParameter p_name = new MySqlParameter("p_name", this.Cus_name);
            MySqlParameter p_pass = new MySqlParameter("p_pass", this.Cus_password);
            MySqlParameter p_fullname = new MySqlParameter("p_fullname", this.Cus_fullname);
            MySqlParameter p_email = new MySqlParameter("p_email", this.Cus_email);
            MySqlParameter p_phone = new MySqlParameter("p_phone", this.Cus_phone);

            string commandText = "UPDATE customers SET cus_name = @p_name, cus_password=@p_pass, cus_fullname=@p_fullname, cus_email=@p_email, cus_phone=@p_phone WHERE cus_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_name, p_pass, p_fullname, p_email, p_phone, p_id);

            return noOfRowInserted;
        }

        public int deleteData(int? id)
        {
            this.Cus_id = id;
            MySqlParameter p_id = new MySqlParameter("id", this.Cus_id);
            string commandText = "DELETE FROM customers WHERE cus_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_id);

            return noOfRowInserted;
        }
    }
}