using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class QueuedProjectModel
    {
        private string _name = null;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _status = null;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}