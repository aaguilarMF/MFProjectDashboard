using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class GridModel
    {
        private List<ColumnModel> _columns=new List<ColumnModel>();
        //private List<Dictionary<string, object>> _data = new List<Dictionary<string, object>>();
        private List<GridDataModel> _data = new List<GridDataModel>();
        public List<ColumnModel> columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
        //public List<Dictionary<string,object>> data
        //{
        //    get { return _data; }
        //    set { _data = value; }
        //}
        public List<GridDataModel> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}