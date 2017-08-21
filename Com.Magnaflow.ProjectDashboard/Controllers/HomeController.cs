using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//AA dt 8/4/17
//Change object definition
using System.Data.OracleClient;
//using Oracle.ManagedDataAccess.Client;
using ProjectManagement.Models;
using Newtonsoft.Json.Converters;

namespace ProjectManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string[] lstProjects = new string[] { "Project A", "Project B", "Project C", "Project D" };
            string[] lstStages = new string[] { "Done", "Pending", "Blocked", "Delayed", "" };
            string[] lstDepartments = new string[] { "Sales Department", "Data Team", "Marketing" };
            ManagementModel model = new ManagementModel();

            //Columns
           
            model.gridThisMonth = new GridModel();
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "Project", title = "This Month (" + DateTime.Now.ToString("MMM") + ")", width = "300px" });
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "Owner", title = "Requestor", width = "100px" });
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "Department", title = "Department", width = "120px" });
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "Summary", title = "Summary", width = "200px" });
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "Status", title = "Status", width = "110px" });
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "DueDate", title = "Due Date", width = "110px", format = "{0:d}" });
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "Contact", title = "Contact", width = "100px" });
            model.gridThisMonth.columns.Add(new ColumnModel() { field = "Priority", title = "Priority", width = "100px" });
            try
            {
                //System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection();
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = csavsdbt2.magnaflow.com)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = Testpim2)));UID=PIM2;PWD=test";
                //conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));UID=conMandarDaoo;PWD=iONsensedER3";
                conn.Open();
                OracleCommand command = conn.CreateCommand();
                //command.CommandText = " SELECT * FROM PIM2.PROJ_PROJECT_TAB ";
                command.CommandText = " SELECT IID, NAME, REQUESTOR, REQUEST_DEPARTMENT, SUMMARY, ";
                command.CommandText = command.CommandText + " STATUS, DUE_DATE, IT_OWNER, PRIORITY ";
                //command.CommandText = command.CommandText + " FROM PIM2.PROJ_PROJECT_TAB ";
                command.CommandText = command.CommandText + " FROM PIM2.TEMPDASHBOARDTABLE";
                OracleDataReader oDDReader = command.ExecuteReader();
                if (oDDReader.HasRows)
                {
                    while (oDDReader.Read())
                    {
                        GridDataModel oDataModel = new GridDataModel();
                        if (!Convert.IsDBNull(oDDReader["IID"]))
                            oDataModel.IID = Convert.ToInt64(oDDReader["IID"]);
                        else
                            continue;
                        oDataModel.Project = Convert.ToString(oDDReader["NAME"]);
                        oDataModel.Owner = Convert.ToString(oDDReader["REQUESTOR"]);
                        oDataModel.Department = Convert.ToString(oDDReader["REQUEST_DEPARTMENT"]);
                        oDataModel.Summary = Convert.ToString(oDDReader["SUMMARY"]);
                        oDataModel.Status = Convert.ToString(oDDReader["STATUS"]);



                        if (!Convert.IsDBNull(oDDReader["DUE_DATE"]))
                            oDataModel.DueDate = Convert.ToDateTime(oDDReader["DUE_DATE"]);
                        oDataModel.Contact = Convert.ToString(oDDReader["IT_OWNER"]);
                        oDataModel.Priority = Convert.ToString(oDDReader["PRIORITY"]);

                        model.gridThisMonth.data.Add(oDataModel);

                    }
                }
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    oDDReader.Close();
                }
                #region hardcoaded futur projects
                //Data
                Random rnd = new Random();
                model.gridNextMonth = new GridModel();
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "Project", title = "Next Month (" + DateTime.Now.ToString("MMM") + ")", width = "300px" });
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "Owner", title = "Requestor", width = "100px" });
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "Department", title = "Department", width = "120px" });
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "Summary", title = "Summary", width = "200px" });
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "Status", title = "Status", width = "110px" });
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "DueDate", title = "Due Date", width = "110px", format = "{0:d}" });
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "Contact", title = "Contact", width = "100px" });
                model.gridNextMonth.columns.Add(new ColumnModel() { field = "Priority", title = "Priority", width = "100px" });

                //Data
                Random rndNew = new Random();
                int ID = 1;
                foreach (string project in lstProjects)
                {
                    string comment = null;
                    GridDataModel oDataModel = new GridDataModel();
                    oDataModel.IID = ID;
                    oDataModel.Project = project;
                    oDataModel.Owner = "user";
                    oDataModel.Department = lstDepartments[rnd.Next(0, lstDepartments.Length)];
                    oDataModel.Summary = "summary";
                    oDataModel.Status = "Status";

                    comment = "Comment for" + project;
                    oDataModel.Comments.Add(comment);

                    oDataModel.DueDate = new DateTime(2017, 4, 7);
                    oDataModel.Contact = "Contact";
                    oDataModel.Priority = "Priority";

                    model.gridNextMonth.data.Add(oDataModel);
                    ID++;

                }
                #endregion

                return View(model);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult PushView()
        {
            string[] AcitveProjects = new string[] { "Project A", "Project B", "Project C", "Project D" };
            string[] PushedProjects = new string[] { "Project E" };
            string[] QueuedProjects = new string[] { "Project F", "Project G" };

            ManagementModel model = new ManagementModel();

            model.gridPushView = new GridModel();
            model.gridPushView.columns.Add(new ColumnModel() { field = "ActiveProjects", title = "Active Projects", width = "120px" });
            model.gridPushView.columns.Add(new ColumnModel() { field = "PushedProjects", title = "Pushed Project", width = "300px" });
            model.gridPushView.columns.Add(new ColumnModel() { field = "QueuedProjects", title = "Queued Project", width = "120px" });
            model.gridPushView.columns.Add(new ColumnModel() { field = "Status", title = "Status", width = "100px" });


            List<ActiveProjectModel> lstAcitveProjects = new List<ActiveProjectModel>();
            List<PushedProjectModel> lstPushedProjects = new List<PushedProjectModel>();
            List<QueuedProjectModel> lstQueuedProjects = new List<QueuedProjectModel>();


            //System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));UID=conMandarDaoo;PWD=iONsensedER3";
            //conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));user id=conMandarDaoo;password=iONsensedER3";
            conn.Open();
            OracleCommand command = conn.CreateCommand();
            command.CommandText = " SELECT * FROM PIM2.PROJ_PROJECT_STATUS_ACTIVE ";
            OracleDataReader oDDReader = command.ExecuteReader();
            if (oDDReader.HasRows)
            {
                while (oDDReader.Read())
                {
                    ActiveProjectModel oModel = new ActiveProjectModel();
                    oModel.Name = Convert.ToString(oDDReader["Name"]);
                    lstAcitveProjects.Add(oModel);
                }
            }
            if (!oDDReader.IsClosed)
                oDDReader.Close();

            command.CommandText = " SELECT * FROM PIM2.PROJ_PROJECT_STATUS_PUSHED ";
            oDDReader = command.ExecuteReader();
            if (oDDReader.HasRows)
            {
                while (oDDReader.Read())
                {
                    PushedProjectModel oModel = new PushedProjectModel();
                    oModel.Pushed = Convert.ToString(oDDReader["Pushed"]);
                    lstPushedProjects.Add(oModel);
                }
            }
            if (!oDDReader.IsClosed)
                oDDReader.Close();

            command.CommandText = " SELECT * FROM PIM2.PROJ_PROJECT_STATUS_QUEUED ";
            oDDReader = command.ExecuteReader();
            if (oDDReader.HasRows)
            {
                while (oDDReader.Read())
                {
                    QueuedProjectModel oModel = new QueuedProjectModel();
                    oModel.Name = Convert.ToString(oDDReader["Name"]);
                    oModel.Status = Convert.ToString(oDDReader["Status"]);
                    lstQueuedProjects.Add(oModel);
                }
            }

            if (!oDDReader.IsClosed)
                oDDReader.Close();
            conn.Close();

            int count = 0;
            int lstActiveProjectsCount = lstAcitveProjects.Count;
            int lstPushedProjectsCount = lstPushedProjects.Count;
            int lstQueuedProjectsCount = lstQueuedProjects.Count;

            if (lstActiveProjectsCount > lstPushedProjectsCount)
            {
                if (lstActiveProjectsCount > lstQueuedProjectsCount)
                    count = lstActiveProjectsCount;
                else if (lstQueuedProjectsCount > lstPushedProjectsCount)
                    count = lstQueuedProjectsCount;
                else
                    count = lstPushedProjectsCount;
            }
            else if (lstPushedProjectsCount > lstQueuedProjectsCount)
                count = lstPushedProjectsCount;
            else
                count = lstQueuedProjectsCount;

            for (int i = 0; i < count; i++)
            {
                GridDataModel oDataModelNew = new GridDataModel();
               
                if (i < lstActiveProjectsCount)
                    oDataModelNew.ActiveProjects = lstAcitveProjects[i].Name;

                if (i < lstPushedProjectsCount)
                    oDataModelNew.PushedProjects = lstPushedProjects[i].Pushed;

                if (i < lstQueuedProjectsCount)
                {
                    oDataModelNew.QueuedProjects = lstQueuedProjects[i].Name;
                    oDataModelNew.Status = lstQueuedProjects[i].Status;
                }

                model.gridPushView.data.Add(oDataModelNew);
            }

            return View(model);
        }

        public ActionResult GetCommentsAndHistory(int id)
        {
            string comment = null;
            string history = null;
            ManagementModel oModel = new ManagementModel();
            GridDataModel oDataModel = new GridDataModel();
            oModel.gridPushView = new GridModel();

            //System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));UID=conMandarDaoo;PWD=iONsensedER3";
            //conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));user id=conMandarDaoo;password=iONsensedER3";
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = " SELECT * FROM PIM2.PROJ_COMMENTS WHERE IID = " + id;
            cmd.CommandText = cmd.CommandText + " order by RECORD_DATE desc ";
            OracleDataReader oDDReaderNew = cmd.ExecuteReader();
            if (oDDReaderNew.HasRows)
            {
                while (oDDReaderNew.Read())
                {
                    comment = Convert.ToString(oDDReaderNew["RECORD_DATE"]) + " " + Convert.ToString(oDDReaderNew["COMMENTS"]);
                    oDataModel.Comments.Add(comment);
                }
            }

            oDDReaderNew.Close();

            cmd = conn.CreateCommand();
            cmd.CommandText = " SELECT * FROM PIM2.PROJ_HISTORY WHERE IID = " + id;
            cmd.CommandText = cmd.CommandText + " order by RECORD_DATE desc ";
            oDDReaderNew = cmd.ExecuteReader();
            if (oDDReaderNew.HasRows)
            {
                while (oDDReaderNew.Read())
                {
                    history = Convert.ToString(oDDReaderNew["RECORD_DATE"]) + " " + Convert.ToString(oDDReaderNew["HISTORY"]);
                    oDataModel.History.Add(history);
                }
            }
            oDDReaderNew.Close();
            conn.Close();
            oModel.gridPushView.data.Add(oDataModel);
            return Json(oModel, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddComment(GridDataModel oModel)
        {
            string comment = null;
            string history = null;
            ManagementModel oManagementModel = new ManagementModel();
            GridDataModel oDataModel = new GridDataModel();
            oManagementModel.gridPushView = new GridModel();
            //System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));UID=conMandarDaoo;PWD=iONsensedER3";
            //conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));user id=conMandarDaoo;password=iONsensedER3";
            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            OracleTransaction trans = conn.BeginTransaction();
            cmd.Transaction = trans;
            cmd.CommandText = " INSERT INTO PIM2.PROJ_COMMENTS(IID, COMMENTS, RECORD_DATE ";
            cmd.CommandText = cmd.CommandText + " ) values ( " + oModel.IID;
            cmd.CommandText = cmd.CommandText + ", '" + oModel.NewComment + "'";
            //cmd.CommandText = cmd.CommandText + ", 'Mandar'";
            cmd.CommandText = cmd.CommandText + ", TO_DATE('" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS')";
            cmd.CommandText = cmd.CommandText + ") ";
            try
            {
                int iRecAffctd = cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {

            }
            conn.Close();
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = " SELECT * FROM PIM2.PROJ_COMMENTS WHERE IID = " + oModel.IID;
            cmd.CommandText = cmd.CommandText + " order by RECORD_DATE desc ";

            OracleDataReader oDDReaderNew = cmd.ExecuteReader();

            if (oDDReaderNew.HasRows)
            {
                while (oDDReaderNew.Read())
                {
                    comment = Convert.ToString(oDDReaderNew["RECORD_DATE"]) + " " + Convert.ToString(oDDReaderNew["COMMENTS"]);
                    oDataModel.Comments.Add(comment);
                }
            }

            oDDReaderNew.Close();

            cmd = conn.CreateCommand();
            cmd.CommandText = " SELECT * FROM PIM2.PROJ_HISTORY WHERE IID = " + oModel.IID;
            cmd.CommandText = cmd.CommandText + " order by RECORD_DATE desc ";
            oDDReaderNew = cmd.ExecuteReader();
            if (oDDReaderNew.HasRows)
            {
                while (oDDReaderNew.Read())
                {
                    history = Convert.ToString(oDDReaderNew["RECORD_DATE"]) + " " + Convert.ToString(oDDReaderNew["HISTORY"]);
                    oDataModel.History.Add(history);
                }
            }

            oManagementModel.gridPushView.data.Add(oDataModel);
            conn.Close();
            return Json(oManagementModel, JsonRequestBehavior.AllowGet);

        }

        #region new method
        [HttpPost]
        public ActionResult AddRow(ProjectAdd projectAddModel)
        {
            try
            {
                var context = new DashboardEntities();

                if(String.IsNullOrWhiteSpace( projectAddModel.Name) ||
                    //String.IsNullOrWhiteSpace(projectAddModel.Summary) ||
                    String.IsNullOrWhiteSpace(projectAddModel.Status) ||
                    String.IsNullOrWhiteSpace(projectAddModel.Priority) ||
                    String.IsNullOrWhiteSpace(projectAddModel.Requestor) ||
                    String.IsNullOrWhiteSpace(projectAddModel.IT_Owner) ||
                    String.IsNullOrWhiteSpace(projectAddModel.Request_Department)
                    )
                {
                    return new HttpStatusCodeResult(500, "One or more Required Values is Null or empty");
                }

                var tempDashboardTable = new TEMPDASHBOARDTABLE()
                {
                    NAME = projectAddModel.Name,
                    SUMMARY = projectAddModel.Summary,
                    STATUS = projectAddModel.Status,
                    PRIORITY = projectAddModel.Priority,
                    REQUESTOR = projectAddModel.Requestor,
                    IT_OWNER = projectAddModel.IT_Owner,
                    REQUEST_DEPARTMENT = projectAddModel.Request_Department,
                    DUE_DATE = projectAddModel.Due_Date
                };
                context.TEMPDASHBOARDTABLEs.Add(tempDashboardTable);
                context.SaveChanges();
                //return Json(tempDashboardTable, JsonRequestBehavior.AllowGet);
                
                #region front end model
                GridDataModel oDataModel = new GridDataModel();
                oDataModel.IID = (long)tempDashboardTable.IID;
                oDataModel.Project = tempDashboardTable.NAME;
                oDataModel.Owner = tempDashboardTable.REQUESTOR;
                oDataModel.Department = tempDashboardTable.REQUEST_DEPARTMENT;
                oDataModel.Summary = tempDashboardTable.SUMMARY;
                oDataModel.Status = tempDashboardTable.STATUS;

                oDataModel.DueDate = Convert.ToDateTime(tempDashboardTable.DUE_DATE);
                oDataModel.Contact = tempDashboardTable.IT_OWNER;
                oDataModel.Priority = tempDashboardTable.PRIORITY;
                return Json(oDataModel, JsonRequestBehavior.AllowGet);
                #endregion
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(500, "Failed to commit Project");
            }
        }
        #endregion

        /*public ActionResult PopulateDropDowns()
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));UID=conMandarDaoo;PWD=iONsensedER3";
            //conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.4.135)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = TESTPIM2)));user id=conMandarDaoo;password=iONsensedER3";
            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            OracleTransaction trans = conn.BeginTransaction();
            cmd.Transaction = trans;
            cmd.CommandText = " INSERT INTO PIM2.PROJ_COMMENTS(IID, COMMENTS, RECORD_DATE ";
            cmd.CommandText = cmd.CommandText + " ) values ( " + oModel.IID;
            cmd.CommandText = cmd.CommandText + ", '" + oModel.NewComment + "'";
            //cmd.CommandText = cmd.CommandText + ", 'Mandar'";
            cmd.CommandText = cmd.CommandText + ", TO_DATE('" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS')";
            cmd.CommandText = cmd.CommandText + ") ";
            try
            {
                int iRecAffctd = cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {

            }
            conn.Close();
            return Json(null, JsonRequestBehavior.AllowGet);
        }*/
       

    }
}