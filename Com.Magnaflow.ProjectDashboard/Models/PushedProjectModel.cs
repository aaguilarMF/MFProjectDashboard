using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class PushedProjectModel
    {
        private string _pushed = null;
        public string Pushed
        {
            get { return _pushed; }
            set { _pushed = value; }
        }
    }
}