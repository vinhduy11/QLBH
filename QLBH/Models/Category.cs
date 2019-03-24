using MySql.Data.MySqlClient;
using QLBH.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QLBH.Models
{
    public class Category
    {
        private int? category_id;
        private string category_name;
        shopEntities db = new shopEntities();

        public Category() { }
        public Category(int category_id, string category_name)
        {
            this.Category_id = category_id;
            this.Category_name = category_name;
        }

        [Display(Name = "ID")]
        public int? Category_id { get => category_id; set => category_id = value; }
        [Required(ErrorMessage = "Phân loại sản phẩm không được để trống")]
        [StringLength(250, ErrorMessage = "Phân loại không được vượt quá 255 ký tự")]
        [Display(Name = "Phân loại" )]
        public string Category_name { get => category_name; set => category_name = value; }

        //Get Data
        public List<category> getCategoryList()
        {
            var category = db.categories.SqlQuery("SELECT * FROM categories").ToList();
            return category;
        }

        public Category getCategorybyId(int? category_id)
        {
            this.Category_id = category_id;
            var getData = db.categories.SqlQuery("select * from categories where category_id = @id", new MySqlParameter("id", this.Category_id))
                .FirstOrDefault();

            Category category = new Category(getData.category_id, getData.category_name);

            return category;
        }
        //Check Data
        public bool checkCategoryExisted(string name)
        {
            this.Category_name = name;
            var getData = db.categories.SqlQuery("select * from categories where category_name = @name", new MySqlParameter("name", this.Category_name))
                .FirstOrDefault();

            if (getData != null)
                return true;

            return false;
        }

        //Update Data
        public int insertData(string category_name)
        {
            this.Category_name = category_name;

            MySqlParameter category_name_param = new MySqlParameter("category_name", this.Category_name);

            string commandText = "INSERT categories (category_name) " +
                                        "VALUES (@category_name)";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, category_name_param);

            return noOfRowInserted;
        }

        public int updateData(int? id, string category_name)
        {
            this.Category_id = id;
            this.Category_name = category_name;

            MySqlParameter id_param = new MySqlParameter("id", this.Category_id);
            MySqlParameter category_name_param = new MySqlParameter("category_name", this.Category_name);

            string commandText = "UPDATE categories SET category_name = @category_name WHERE category_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, category_name_param, id_param);

            return noOfRowInserted;
        }

        public int deleteData(int? id)
        {
            this.Category_id = id;
            MySqlParameter id_param = new MySqlParameter("id", this.Category_id);
            string commandText = "DELETE FROM categories WHERE category_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, id_param);

            return noOfRowInserted;
        }
    }
}