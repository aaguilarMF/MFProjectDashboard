using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class ManagementModel
    {
        private List<TreeViewModel> _menuTree=new List<TreeViewModel>();
        public List<TreeViewModel> menuTree
        {
            get { return _menuTree; }
            set { _menuTree = value; }
        }
        
        public string pageTitle { get; set; }
        public string pageDescription { get; set; }
        public GridModel gridThisMonth { get; set; }
        public GridModel gridNextMonth { get; set; }
        public GridModel gridPushView { get; set; }
    }
}