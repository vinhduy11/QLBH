using MySql.Data.MySqlClient;
using QLBH.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH.Models
{
    public class OrderDetails
    {
        private int? order_detail_id;
        private int order_id;
        private int product_id;
        private int price;
        private int quantity;
        private string product_name;
        private int subtotal;
        private string product_description;
        shopEntities db = new shopEntities();

        public OrderDetails() { }

        public OrderDetails(int? order_detail_id, string product_name, string product_description, int price, int quantity, int subtotal)
        {
            this.Order_detail_id = order_detail_id;
            this.Product_name = product_name;
            this.Product_description = product_description;
            this.Price = price;
            this.Quantity = quantity;
            this.Subtotal = subtotal;

        }

        public int? Order_detail_id { get => order_detail_id; set => order_detail_id = value; }
        public int Order_id { get => order_id; set => order_id = value; }
        public int Product_id { get => product_id; set => product_id = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Product_name { get => product_name; set => product_name = value; }
        public int Subtotal { get => subtotal; set => subtotal = value; }
        public string Product_description { get => product_description; set => product_description = value; }

        public List<OrderDetails> getOderDetailsbyId(int id)
        {
            this.Order_id = id;

            MySqlParameter id_param = new MySqlParameter("id", this.Order_id);
            //return orders;
            string sql = " SELECT od.order_detail_id, od.price, od.quantity,p.product_name, p.product_description, (price * quantity) Subtotal" +
                        "   FROM order_details od " +
                        "   JOIN products p on p.product_id = od.product_id " +
                        " WHERE od.order_id = @id";
            var query = db.Database.SqlQuery<OrderDetails>(sql, id_param).ToList();

            List<OrderDetails> ods = new List<OrderDetails>();
            foreach (var item in query)
            {
                ods.Add(new OrderDetails(
                    item.Order_detail_id,
                    item.Product_name,
                    item.Product_description,
                    item.Price,
                    item.Quantity,
                    item.Subtotal
                ));
            }

            return ods;
        }

        public int deleteData(int? id)
        {
            this.Order_id = Order_id;
            MySqlParameter p_id = new MySqlParameter("id", this.Order_id);
            string command1 = "DELETE FROM order_details WHERE order_id = @id";
            int noOfRow1 = db.Database.ExecuteSqlCommand(command1, p_id);

            return noOfRow1;
        }
    }
}