using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class TreeViewModel
    {
        private string _text = string.Empty;
        private bool _expanded = true;
        private List<TreeViewModel> _items = new List<TreeViewModel>();

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }
        public bool expanded
        {
            get { return _expanded; }
            set { _expanded = value; }
        }
        public List<TreeViewModel> items
        {
            get { return _items; }
            set { _items = value; }
        }

        //Conditional serilization
        public bool ShouldSerializeexpanded()
        {
            return _items.Count > 0;
        }
        public bool ShouldSerializeitems()
        {
            return _items.Count > 0;
        }

    }
}