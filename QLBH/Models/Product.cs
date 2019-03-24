using MySql.Data.MySqlClient;
using QLBH.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QLBH.Models
{
    public class Product
    {
        private int? product_id;
        private int category_id;
        private int provider_id;
        private string category_name;
        private string provider_name;
        private string product_name;
        private string product_image;
        private int product_price;
        private int quantities;
        private string product_description;

        shopEntities db = new shopEntities();

        public Product() { }
        public Product(int? product_id, int category_id, int provider_id, string product_name, string product_image, int product_price, int quantities, string product_description, string category_name, string provider_name)
        {

            this.Product_id = product_id;
            this.Category_id = category_id;
            this.Provider_id = provider_id;
            this.Product_name = product_name;
            this.Product_image = product_image;
            this.Product_price = product_price;
            this.Quantities = quantities;
            this.Product_description = product_description;
            this.Category_name = category_name;
            this.Provider_name = provider_name;
        }

        [Display(Name = "ID")]
        public int? Product_id { get => product_id; set => product_id = value; }

        public int Category_id { get => category_id; set => category_id = value; }

        public int Provider_id { get => provider_id; set => provider_id = value; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(250, ErrorMessage = "Tên nhà cung cấp không được vượt quá 50 ký tự")]
        public string Product_name { get => product_name; set => product_name = value; }

        [Display(Name = "Hình ảnh")]
        public string Product_image { get => product_image; set => product_image = value; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá sản phâm không được để trống")]
        public int Product_price { get => product_price; set => product_price = value; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        public int Quantities { get => quantities; set => quantities = value; }

        [Display(Name = "Mô tả")]
        public string Product_description { get => product_description; set => product_description = value; }

        [Display(Name = "Nhà cung cấp")]
        public string Provider_name { get => provider_name; set => provider_name = value; }

        [Display(Name = "Phân loại")]
        public string Category_name { get => category_name; set => category_name = value; }

        //Get Data
        public List<Category> getCategories()
        {
            List<Category> categories = new List<Category>();

            var getData = from p in db.categories
                          select new
                          {
                              Category_name = p.category_name,
                              Category_id = p.category_id
                          };

            foreach (var t in getData)
            {
                categories.Add(new Category (t.Category_id, t.Category_name)
                );
            }

            return categories;
        }

        public List<Provider> getProviders()
        {
            List<Provider> providers = new List<Provider>();

            var getData = from p in db.providers
                          select new
                          {
                              Provider_name = p.provider_name,
                              Provider_id = p.provider_id
                          };

            foreach (var t in getData)
            {
                providers.Add(new Provider(t.Provider_id, t.Provider_name, "", "",""));
            }

            return providers;
        }

        public List<Product> getProductList()
        {
            List<Product> results = new List<Product>();
            
            var getData = from p in db.products
                          join c in db.categories on p.category_id equals c.category_id
                          join pv in db.providers on p.provider_id equals pv.provider_id
                          select new
                          {
                                Product_id = p.product_id,
                                Category_id = p.category_id,
                                Provider_id = p.provider_id,
                                Product_name = p.product_name,
                                Product_image = p.product_image,
                                Product_price = p.product_price,
                                Quantities = p.quantities,
                                Product_description = p.product_description,
                                Category_name = c.category_name,
                                Provider_name = pv.provider_name
                            };

            foreach (var t in getData)
            {
                results.Add(new  Product (
                    t.Product_id,
                    t.Category_id,
                    t.Provider_id,
                    t.Product_name,
                    t.Product_image,
                    t.Product_price,
                    t.Quantities,
                    t.Product_description,
                    t.Category_name,
                    t.Provider_name));
            }
            
            return results;
        }

        public Product getProductbyId(int? id)
        {
            this.Product_id = id;
            Product results = new Product();

            var getData = from p in db.products
                          join c in db.categories on p.category_id equals c.category_id
                          join pv in db.providers on p.provider_id equals pv.provider_id
                          where p.product_id == this.Product_id
                          select new
                          {
                              Product_id = p.product_id,
                              Category_id = p.category_id,
                              Provider_id = p.provider_id,
                              Product_name = p.product_name,
                              Product_image = p.product_image,
                              Product_price = p.product_price,
                              Quantities = p.quantities,
                              Product_description = p.product_description,
                              Category_name = c.category_name,
                              Provider_name = pv.provider_name
                          };

            foreach (var t in getData)
            {

                results.Product_id = t.Product_id;
                results.Category_id = t.Category_id;
                results.Provider_id = t.Provider_id;
                results.Product_name = t.Product_name;
                results.Product_image = t.Product_image;
                results.Product_price = t.Product_price;
                results.Quantities = t.Quantities;
                results.Product_description = t.Product_description;
                results.Category_name = t.Category_name;
                results.Provider_name = t.Provider_name;
            }

            return results;
        }

        //Check Value Condition
        public bool checkNameExisted(string product_name)
        {
            this.Product_name = product_name;
            var getData = db.products.SqlQuery("select * from products where product_name = @value", new MySqlParameter("value", this.Product_name))
                .FirstOrDefault();

            if (getData != null)
                return true;

            return false;
        }

        //Change Value

        public int insertData(int category_id, int provider_id, string product_name, string product_image, int product_price, int quantities, string product_description)
        {
            this.Category_id = category_id;
            this.Provider_id = provider_id;
            this.Product_name = product_name;
            this.Product_image = product_image;
            this.Product_price = product_price;
            this.Quantities = quantities;
            this.Product_description = product_description;

            MySqlParameter p_category_id = new MySqlParameter("category_id", this.Category_id);
            MySqlParameter p_provider_id = new MySqlParameter("provider_id", this.Provider_id);
            MySqlParameter p_product_name = new MySqlParameter("product_name", this.Product_name);
            MySqlParameter p_product_image = new MySqlParameter("product_image", this.Product_image);
            MySqlParameter p_product_price = new MySqlParameter("product_price", this.Product_price);
            MySqlParameter p_quantities = new MySqlParameter("quantities", this.Quantities);
            MySqlParameter p_product_description = new MySqlParameter("product_description", this.Product_description);

            string commandText = "INSERT products (category_id, provider_id, product_name, product_image, product_price, quantities, product_description) " +
                                        "VALUES (@category_id, @provider_id, @product_name, @product_image, @product_price, @quantities, @product_description)";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_category_id, p_provider_id, p_product_name, p_product_image, p_product_price, p_quantities, p_product_description);

            return noOfRowInserted;
        }

        public int updateData(int? id, int category_id, int provider_id, string product_name, string product_image, int product_price, int quantities, string product_description)
        {
            this.Product_id = id;
            this.Category_id = category_id;
            this.Provider_id = provider_id;
            this.Product_name = product_name;
            this.Product_image = product_image;
            this.Product_price = product_price;
            this.Quantities = quantities;
            this.Product_description = product_description;

            MySqlParameter id_param = new MySqlParameter("id", this.Product_id);
            MySqlParameter p_category_id = new MySqlParameter("category_id", this.Category_id);
            MySqlParameter p_provider_id = new MySqlParameter("provider_id", this.Provider_id);
            MySqlParameter p_product_name = new MySqlParameter("product_name", this.Product_name);
            MySqlParameter p_product_image = new MySqlParameter("product_image", this.Product_image);
            MySqlParameter p_product_price = new MySqlParameter("product_price", this.Product_price);
            MySqlParameter p_quantities = new MySqlParameter("quantities", this.Quantities);
            MySqlParameter p_product_description = new MySqlParameter("product_description", this.Product_description);

            string commandText = "UPDATE products SET category_id = @category_id, provider_id = @provider_id, product_name = @product_name, product_image = @product_image, product_price = @product_price, quantities = @quantities, product_description = @product_description WHERE product_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, p_category_id, p_provider_id, p_product_name, p_product_image, p_product_price, p_quantities, p_product_description, id_param);

            return noOfRowInserted;
        }

        public int deleteData(int? id)
        {
            this.Product_id = id;
            MySqlParameter id_param = new MySqlParameter("id", this.Product_id);
            string commandText = "DELETE FROM products WHERE product_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, id_param);

            return noOfRowInserted;
        }
    }
}