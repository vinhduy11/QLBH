using MySql.Data.MySqlClient;
using QLBH.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QLBH.Models
{
    public class Order
    {
        private int? order_id;
        private int customer_id;
        private string customer_name;
        private int total;
        private int payment;
        private string address;
        private DateTime date;
        private int status;
        shopEntities db = new shopEntities();

        public Order() { }
        public Order(int? order_id, int customer_id, string customer_name, int total, int payment, string address, DateTime date, int status)
        {
            this.Order_id = order_id;
            this.Customer_id = customer_id;
            this.Customer_name = customer_name;
            this.Total = total;
            this.Payment = payment;
            this.Address = address;
            this.Date = date;
            this.Status = status;
        }

        [Display(Name = "Mã đơn hàng")]
        public int? Order_id { get => order_id; set => order_id = value; }

        public int Customer_id { get => customer_id; set => customer_id = value; }

        [Display(Name = "Tên Khách hàng")]
        public string Customer_name { get => customer_name; set => customer_name = value; }

        [Display(Name = "Loại thanh toán")]
        public int Payment { get => payment; set => payment = value; }
        [Display(Name = "Địa chỉ")]
        public string Address { get => address; set => address = value; }
        [Display(Name = "Ngày đặt")]
        public DateTime Date { get => date; set => date = value; }
        [Display(Name = "Trạng thái đơn hàng")]
        public int Status { get => status; set => status = value; }
        [Display(Name = "Tổng cộng")]
        public int Total { get => total; set => total = value; }
        

        //Get Data
        public List<Order> getList()
        {
            List<Order> orders = new List<Order>();

            string sql = "SELECT " +
                           " o.order_id, " +
                           " o.customer_id, " +
                           " c.cus_name customer_name, " +
                           " b.total," +
                           " o.payment," +
                           " o.address," +
                           " o.date," +
                           " o.STATUS status" +
                        " FROM" +
                        "    orders o" +
                        " JOIN (" +
                        "   SELECT od.order_id, sum(od.quantity * od.price) total " +
                        "   FROM order_details od " +
                        "   GROUP BY od.order_id) b ON b.order_id = o.order_id" +
                        " JOIN customers c on c.cus_id = o.customer_id";
            var query = db.Database.SqlQuery<Order>(sql).ToList();

            foreach (var item in query)
            {
                orders.Add(new Order(
                    item.order_id,
                    item.customer_id,
                    item.customer_name,
                    item.total,
                    item.payment,
                    item.address,
                    item.date,
                    item.status
                ));
            }

            return orders;
        }

        public Order getOrderbyId(int id)
        {
            this.Order_id = id;

            MySqlParameter id_param = new MySqlParameter("id", this.Order_id);
            //return orders;
            string sql = "SELECT " +
                           " o.order_id, " +
                           " o.customer_id, " +
                           " c.cus_name customer_name, " +
                           " b.total," +
                           " o.payment," +
                           " o.address," +
                           " o.date," +
                           " o.STATUS status" +
                        " FROM" +
                        "    orders o" +
                        " JOIN (" +
                        "   SELECT od.order_id, sum(od.quantity * od.price) total " +
                        "   FROM order_details od " +
                        "   GROUP BY od.order_id) b ON b.order_id = o.order_id" +
                        " JOIN customers c on c.cus_id = o.customer_id" +
                        " WHERE o.order_id = @id";
            var query = db.Database.SqlQuery<Order>(sql, id_param).SingleOrDefault();
            Order orders = new Order(query.order_id, query.customer_id, query.customer_name, query.total, query.payment, query.address, query.date, query.status);

            return orders;
        }

        //Check Data
        //public bool checkCustomerExisted(string email)
        //{
        //    this.Cus_email = email;
        //    var getData = db.customers.SqlQuery("select * from customers where cus_email = @email", new MySqlParameter("email", this.Cus_email))
        //        .FirstOrDefault();

        //    if (getData != null)
        //        return true;

        //    return false;
        //}

        //Update Data
        //public int insertData(int customer_id, int total, int payment, string address, DateTime date, int status)
        //{
        //    this.Customer_id = customer_id;
        //    this.Payment = payment;
        //    this.Address = address;
        //    this.Date = date;
        //    this.Status = status;

        //    MySqlParameter p_name = new MySqlParameter("p_name", this.Cus_name);
        //    MySqlParameter p_pass = new MySqlParameter("p_pass", this.Cus_password);
        //    MySqlParameter p_fullname = new MySqlParameter("p_fullname", this.Cus_fullname);
        //    MySqlParameter p_email = new MySqlParameter("p_email", this.Cus_email = cus_email);
        //    MySqlParameter p_phone = new MySqlParameter("p_phone", this.Cus_phone);

        //    string commandText = "INSERT customers (cus_name, cus_password, cus_fullname, cus_email, cus_phone) " +
        //                                "VALUES (@p_name, @p_pass, @p_fullname, @p_email, @p_phone)";
        //    int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_name, p_pass, p_fullname, p_email, p_phone);

        //    return noOfRowInserted;
        //}

        //public int updateData(int id, string cus_name, string cus_password, string cus_fullname, string cus_email, string cus_phone)
        //{
        //    this.Order_id = order_id;
        //    this.Customer_id = customer_id;
        //    this.Customer_name = customer_name;
        //    this.Total = total;
        //    this.Payment = payment;
        //    this.Address = address;
        //    this.Date = date;
        //    this.Status = status;

        //    MySqlParameter p_id = new MySqlParameter("id", this.Cus_id);
        //    MySqlParameter p_name = new MySqlParameter("p_name", this.Cus_name);
        //    MySqlParameter p_pass = new MySqlParameter("p_pass", this.Cus_password);
        //    MySqlParameter p_fullname = new MySqlParameter("p_fullname", this.Cus_fullname);
        //    MySqlParameter p_email = new MySqlParameter("p_email", this.Cus_email);
        //    MySqlParameter p_phone = new MySqlParameter("p_phone", this.Cus_phone);

        //    string commandText = "UPDATE customers SET cus_name = @p_name, cus_password=@p_pass, cus_fullname=@p_fullname, cus_email=@p_email, cus_phone=@p_phone WHERE cus_id = @id";
        //    int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_name, p_pass, p_fullname, p_email, p_phone, p_id);

        //    return noOfRowInserted;
        //}

        public int deleteData(int? id)
        {
            this.Order_id = order_id;
            MySqlParameter p_id = new MySqlParameter("id", this.Order_id);
            string commandText = "DELETE FROM orders WHERE order_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_id);

            return noOfRowInserted;
        }
    }
}