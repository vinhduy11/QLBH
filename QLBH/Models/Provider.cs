using MySql.Data.MySqlClient;
using QLBH.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH.Models
{
    [Authorize]
    public class Provider
    {
        private int? provider_id;
        private string provider_name;
        private string provider_email;
        private string provider_address;    
        private string provider_phone;
        shopEntities db = new shopEntities();

        public Provider() { }

        public Provider(int? provider_id, string provider_name, string provider_email, string provider_address, string provider_phone)
        {
            this.Provider_id = provider_id;
            this.Provider_name = provider_name;
            this.Provider_email = provider_email;
            this.Provider_address = provider_address;
            this.Provider_phone = provider_phone;
        }
        [Display(Name = "ID")]
        public int? Provider_id { get => provider_id; set => provider_id = value; }

        [Display (Name = "Tên nhà cung cấp")]
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(50, ErrorMessage = "Tên nhà cung cấp không được vượt quá 50 ký tự")]
        public string Provider_name { get => provider_name; set => provider_name = value; }

        [Display(Name = "Email nhà cung cấp")]
        [Required(ErrorMessage = "Email nhà cung cấp không được để trống")]
        [StringLength(250, ErrorMessage = "Email nhà cung cấp không được vượt quá 250 ký tự")]
        public string Provider_email { get => provider_email; set => provider_email = value; }

        [Display(Name = "Địa chỉ nhà cung cấp")]
        [Required(ErrorMessage = "Địa chỉ nhà cung cấp không được để trống")]
        [StringLength(50, ErrorMessage = "Địa chỉ không được vượt quá 50 ký tự")]
        public string Provider_address { get => provider_address; set => provider_address = value; }

        [Display(Name = "Số điện thoại nhà cung cấp")]
        [Required(ErrorMessage = "Số điện thoại nhà cung cấp không được để trống")]
        [StringLength(50, ErrorMessage = "Số điện thoại nhà cung cấp không được vượt quá 50 ký tự")]
        public string Provider_phone { get => provider_phone; set => provider_phone = value; }

        //Get Data
        public List<provider> getProviderList()
        {
            var providers = db.providers.SqlQuery("SELECT * FROM providers").ToList();
            return providers;
        }

        public Provider getProviderbyId(int? id)
        {
            this.Provider_id = id;
            var getData = db.providers.SqlQuery("select * from providers where provider_id = @id", new MySqlParameter("id", this.Provider_id))
                .FirstOrDefault();

            Provider data = new Provider(getData.provider_id, getData.provider_name, getData.provider_email, getData.provider_address, getData.provider_phone);

            return data;
        }

        //Check Value Condition
        public bool checkNameExisted(string provider_name)
        {
            this.Provider_name = provider_name;
            var getData = db.providers.SqlQuery("select * from providers where provider_name = @value", new MySqlParameter("value", this.Provider_name))
                .FirstOrDefault();

            if (getData != null)
                return true;

            return false;
        }


        public bool checkEmailExisted(string email)
        {
            this.Provider_email = email;
            var getData = db.providers.SqlQuery("select * from providers where provider_name = @value", new MySqlParameter("value", this.Provider_email))
                .FirstOrDefault();

            if (getData != null)
                return true;

            return false;
        }

        //Change Value

        public int insertData(string provider_name, string provider_email, string provider_address, string provider_phone)
        {
            this.Provider_name = provider_name;
            this.Provider_email = provider_email;
            this.Provider_address = provider_address;
            this.Provider_phone = provider_phone;

            MySqlParameter provider_name_param = new MySqlParameter("provider_name", this.Provider_name);
            MySqlParameter provider_email_param = new MySqlParameter("provider_email", this.Provider_email);
            MySqlParameter provider_address_param = new MySqlParameter("provider_address", this.Provider_address);
            MySqlParameter provider_phone_param = new MySqlParameter("provider_phone", this.Provider_phone);

            string commandText = "INSERT providers (provider_name, provider_email, provider_address, provider_phone) " +
                                        "VALUES (@provider_name, @provider_email,  @provider_address,  @provider_phone)";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, provider_name_param, provider_email_param, provider_address_param, provider_phone_param);

            return noOfRowInserted;
        }

        public int updateData(int? id, string provider_name, string provider_email, string provider_address, string provider_phone)
        {
            this.Provider_id = id;
            this.Provider_name = provider_name;
            this.Provider_email = provider_email;
            this.Provider_address = provider_address;
            this.Provider_phone = provider_phone;

            MySqlParameter id_param = new MySqlParameter("id", this.Provider_id);
            MySqlParameter provider_name_param = new MySqlParameter("provider_name", this.Provider_name);
            MySqlParameter provider_email_param = new MySqlParameter("provider_email", this.Provider_email);
            MySqlParameter provider_address_param = new MySqlParameter("provider_address", this.Provider_address);
            MySqlParameter provider_phone_param = new MySqlParameter("provider_phone", this.Provider_phone);

            string commandText = "UPDATE providers SET provider_name = @provider_name, provider_email = @provider_email, provider_address = @provider_address, provider_phone = @provider_phone WHERE provider_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, provider_name_param, provider_email_param, provider_address_param, provider_phone_param, id_param);

            return noOfRowInserted;
        }

        public int deleteData(int? id)
        {
            this.Provider_id = id;
            MySqlParameter id_param = new MySqlParameter("id", this.Provider_id);
            string commandText = "DELETE FROM providers WHERE provider_id = @id";
            int noOfRowInserted = db.Database.ExecuteSqlCommand(commandText, id_param);

            return noOfRowInserted;
        }
    }
}