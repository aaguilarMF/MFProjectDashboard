using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class ColumnModel
    {
        private string _field;
        private string _title;
        private string _width = string.Empty;
        private string _color;
        private ColumnAttributeModel _attributes = new ColumnAttributeModel();
        private string _format = string.Empty;
        public string field
        {
            get { return _field; }
            set { _field = value; }
        }
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string width
        {
            get { return _width; }
            set { _width = value; }
        }
        public string color
        {
            get { return _color; }
            set { _color = value; }
        }
        public string format
        {
            get { return _format; }
            set { _format = value; }
        }
        public ColumnAttributeModel attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }
    }
}