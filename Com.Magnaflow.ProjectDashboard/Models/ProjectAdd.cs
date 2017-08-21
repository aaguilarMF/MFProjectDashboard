using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class ProjectAdd
    {
        public string Name { get; set; }
        public string Requestor { get; set; }
        public string Request_Department { get; set; }
        public string Status { get; set; }
        public DateTime Due_Date { get; set; }
        public string IT_Owner { get; set; }
        public string Summary { get; set; }
        public string Priority { get; set; }

    }
}