using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH.Models
{
    public class Notification
    {
        private bool show;
        private string message;
        private string type;

        public Notification() { }
        public Notification(bool show, string type, string message)
        {
            this.Show = show;
            this.Type = type;
            this.Message = message;
        }

        public bool Show { get => show; set => show = value; }
        public string Message { get => message; set => message = value; }
        public string Type { get => type; set => type = value; }

        
    }
}