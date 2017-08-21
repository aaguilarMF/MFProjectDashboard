using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class ActiveProjectModel
    {
        private string _name = null;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}