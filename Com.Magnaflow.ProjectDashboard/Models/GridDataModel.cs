using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectManagement.Models
{
    [DataContract]
    public class GridDataModel
    {
        private long _iID = 0;


        private string _project = null;
        private string _owner = null;
        private string _department = null;
        private string _summary = null;
        private string _status = null;
        private DateTime _dueDate;
        private string _contact = null;
        private string _priority = null;
        List<string> _comments = new List<string>();
        List<string> _history = new List<string>();
        private string _ActiveProjects = null;
        private string _PushedProjects = null;
        private string _QueuedProjects = null;
        [DataMember]
        public string ActiveProjects
        {
            get { return _ActiveProjects; }
            set { _ActiveProjects = value; }
        }
        [DataMember]
        public string PushedProjects
        {
            get { return _PushedProjects; }
            set { _PushedProjects = value; }
        }
        [DataMember]
        public string QueuedProjects
        {
            get { return _QueuedProjects; }
            set { _QueuedProjects = value; }
        }
        [DataMember]
        public List<string> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        [DataMember]
        public List<string> History
        {
            get { return _history; }
            set { _history = value; }
        }
        [DataMember]
        public long IID
        {
            get { return _iID; }
            set { _iID = value; }
        }
        [DataMember]
        public string Project
        {
            get { return _project; }
            set { _project = value; }
        }
        [DataMember]
        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        [DataMember]
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        [DataMember]
        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }
        [DataMember]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        [DataMember]
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }
        [DataMember]
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        [DataMember]
        public string Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        [DataMember]
        private string _newComment = null;
        [DataMember]
        public string NewComment
        {
            get { return _newComment; }
            set { _newComment = value; }
        }
    }
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}