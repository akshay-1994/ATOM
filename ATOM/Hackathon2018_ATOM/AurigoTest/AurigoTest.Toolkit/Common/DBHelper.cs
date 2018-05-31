using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Common.Dto;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

////Microsoft.SqlServer.Smo.dll
//using Microsoft.SqlServer.Management.Smo;
////Microsoft.SqlServer.ConnectionInfo.dll
//using Microsoft.SqlServer.Management.Common;


namespace AurigoTest.Toolkit.Core
{
    public static class DBHelper
    {
        private static Dictionary<string, XmlDocument> ModuleToXmlLookup { get; set; } = new Dictionary<string, XmlDocument>();

        public static string TryGetDatabaseConnectionString()
        {
            string connString = null;

            string tryGetConnectionString = ConfigurationManager.AppSettings.Get("SiteConnectionString");

            if (!string.IsNullOrEmpty(tryGetConnectionString))
                connString = tryGetConnectionString;//1st priority
            else if (ConfigurationManager.ConnectionStrings[AurigoAppSettings.ConnectionStringSource] == null)
                throw new AurigoConfigException("Connection string not defined");
            else
                connString = ConfigurationManager.ConnectionStrings[AurigoAppSettings.ConnectionStringSource].ConnectionString;
            
            return connString;
        }

        #region ConnectionString related
        public static string GetDB_Name_FromConnectionString()
        {
            string connString = TryGetDatabaseConnectionString();
            
            Regex regex = new Regex("[Database][/s]*=[/s]*(.*?)[;]");//Must use non greedy ie use (.*?) instead of (.*) 
            Match v = regex.Match(connString);
            string s = v.Groups[1].ToString();

            return s;
        }

        public static SqlConnection GetNewConnection()
        {

            //if (UrlConstants.SiteUrl.Contains("localhost"))
            //    if (ConfigurationManager.ConnectionStrings["AMP3ConnectionString_Local"] != null)
            //        return new SqlConnection(ConfigurationManager.ConnectionStrings["AMP3ConnectionString_Local"].ConnectionString);

            string connString = TryGetDatabaseConnectionString();

            if (!string.IsNullOrEmpty(connString))
                return new SqlConnection(connString);

            throw new AurigoConfigException("Connection string not defined");
        }

        public static SqlConnection GetNewConnection_As_MasterDatabase()
        {
            string dbName = GetDB_Name_FromConnectionString();

            string connString = TryGetDatabaseConnectionString();

            string newConnString_2_Master = connString.Replace(dbName, "master");

            //Regex regex = new Regex("[Database][/s]*=[/s]*(.*?)[;]");//Must use non greedy ie use (.*?) instead of (.*) 
            //string s = regex.Replace(connString, "master");

            return new SqlConnection(newConnString_2_Master);
        }
        #endregion ConnectionString related

        #region Last created Id related
        public static string GetLastCreatedIdForTable(string tableName, string idFieldName, string hintFieldName = null, string hintFieldValue = null, EnumHintFieldSearchTechnique hintFieldSearchTechnique = EnumHintFieldSearchTechnique.Contains)
        {
            string retValue = string.Empty;

            string sSQL_Template = "SELECT TOP 1 {0} from {1} ORDER BY 1 DESC";//"WHERE SettingName= 'CurrentTimeZone'";

            string sSQL;
            if (hintFieldName != null && hintFieldValue != null)
            {
                string searchString = "";
                switch (hintFieldSearchTechnique)
                {
                    case EnumHintFieldSearchTechnique.BeginWith: searchString = string.Format(" LIKE '%{0}' ", hintFieldValue); break;
                    case EnumHintFieldSearchTechnique.EndsWith: searchString = string.Format(" LIKE '{0}%' ", hintFieldValue); break;
                    case EnumHintFieldSearchTechnique.ExactMatch: searchString = string.Format(" = '{0}' ", hintFieldValue); break;

                    case EnumHintFieldSearchTechnique.NotContains: searchString = string.Format(" NOT LIKE '%{0}%' ", hintFieldValue); break;
                    default:
                    case EnumHintFieldSearchTechnique.Contains: searchString = string.Format(" LIKE '%{0}%' ", hintFieldValue); break;
                }
                sSQL_Template = "SELECT TOP 1 {0} from {1} WHERE {2} {3} ORDER BY 1 DESC";

                sSQL = string.Format(sSQL_Template, idFieldName, tableName, hintFieldName, searchString);
            }
            else
                sSQL = string.Format(sSQL_Template, idFieldName, tableName);


            SqlConnection con;
            SqlDataReader reader;
            try
            {
                con = DBHelper.GetNewConnection();
                con.Open();


                reader = new SqlCommand(sSQL, con).ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        retValue = reader[0].ToString();
                        break;
                    }
                }
                else
                    throw new ArgumentException("LastCreatedId coulud not be determined.");

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (string.IsNullOrWhiteSpace(retValue))
                throw new ArgumentException("LastCreatedId coulud not be determined.");

            return retValue;
        }

        public static string GetLastCreatedIdForTable(HintSetting hintFieldSetting)
        {
            return GetLastCreatedIdForTable(hintFieldSetting.TableName, hintFieldSetting.IdField, hintFieldSetting.HintField, hintFieldSetting.HintFieldValue, hintFieldSetting.HintFieldSearchTechnique);
        }
        #endregion Last created Id related

        #region DataTable related
        public static DataTable GetDataTable_With_LastCreatedRow(string tableName, string idFieldName, string hintFieldName = null, string hintFieldValue = null)
        {
            string retValue = string.Empty;

            string sSQL_Template = "SELECT TOP 1 {0} as ID_ID_ID ,* from {1} ORDER BY ID_ID_ID DESC";//"WHERE SettingName= 'CurrentTimeZone'";

            string sSQL;
            if (hintFieldName != null && hintFieldValue != null)
            {
                sSQL_Template = "SELECT TOP 1 {0} as ID_ID_ID ,* from {1} WHERE {2} LIKE '%{3}%' ORDER BY ID_ID_ID DESC";

                sSQL = string.Format(sSQL_Template, idFieldName, tableName, hintFieldName, hintFieldValue);
            }
            else
                sSQL = string.Format(sSQL_Template, idFieldName, tableName);

            try
            {
                using (var con = DBHelper.GetNewConnection())
                using (var adapter = new SqlDataAdapter(sSQL, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (string.IsNullOrWhiteSpace(retValue))
                throw new ArgumentException("LastCreatedId coulud not be determined.");

            return null;
        }


        public static DataTable GetDataForTable(string table, string schema = "dbo", int? pageNumber = null, int? maxRecords = null)
        {

            //string keyFields = (tableInfo.PrimaryKeysCSV ?? tableInfo.ProxyPrimaryKeyCSV);
            //string dataFields = string.Join(",", tableInfo.ColumnsToMigrate.Select(t => "[" + t.ColumnName + "]"));
            //string query = string.Format("SELECT {0} , {1} FROM {2} WHERE 1=1 ", keyFields, dataFields, tableInfo.TableNameWithSchema);

            //try
            //{
            //    using (var con = DAL.GetNewConnection())
            //    using (var adapter = new SqlDataAdapter(query, con))
            //    {
            //        DataTable dt = new DataTable();
            //        adapter.Fill(dt);
            //        return dt;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logText.AppendLine("ERROR for table" + tableInfo.TableNameWithSchema + "    " + ex.Message);
            //}

            return null;
        }

        public static DataTable Execute_SelectStatement(string selectStatement)
        {
            string retValue = string.Empty;

            string refinedSQL = selectStatement;

            try
            {
                using (var con = DBHelper.GetNewConnection())
                using (var adapter = new SqlDataAdapter(refinedSQL, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public static DataSet Execute_StoredProcedure(string spName, object[] spParams)
        {
            throw new NotImplementedException();
        }
        #endregion DataTable related

        #region XML Form Related
        public static XmlDocument GetXML_Form(string moduleId)
        {
            moduleId = moduleId.ToUpper();

            if (ModuleToXmlLookup.ContainsKey(moduleId))
                return ModuleToXmlLookup[moduleId];

            string fileName = moduleId + ".xml";
            string xmlType = "Form";
            try
            {
                SqlDataReader reader = null;
                using (var con = DBHelper.GetNewConnection())
                using (SqlCommand cmd = new SqlCommand("usp_XMLFORMCUDGetForm", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ModuleID", SqlDbType.VarChar).Value = moduleId;
                    cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = fileName;
                    cmd.Parameters.Add("@FileContents", SqlDbType.VarBinary).Value = null;
                    cmd.Parameters.Add("@XMLType", SqlDbType.NVarChar).Value = xmlType;// "Form";
                    cmd.Parameters.Add("@IsTemplate", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@Mode", SqlDbType.Char).Value = 'G';

                    con.Open();
                    reader = cmd.ExecuteReader(CommandBehavior.SingleRow);


                    byte[] file = null;
                    if (reader.Read())
                    {
                        file = (byte[])reader["FileContents"];

                        XmlDocument xmlDoc = new XmlDocument();
                        using (MemoryStream memStream = new MemoryStream(file))// Convert.FromBase64String(extractedBaseString)))
                        {
                            xmlDoc.Load(memStream);
                        }

                        ModuleToXmlLookup[moduleId] = xmlDoc;
                        return xmlDoc;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
        private static XControl GetXML_FromControlObject_ByAttributeName(string moduleId, string fieldName, string attributeName)
        {
            XmlDocument xmlDoc = DBHelper.GetXML_Form(moduleId);

            if (xmlDoc == null)
                return null;

            XmlElement xmlEle = xmlDoc.GetElementsByTagName("Control").Cast<XmlElement>().FirstOrDefault(t => t.Attributes[attributeName]?.Value == fieldName);

            if (xmlEle == null)
                return null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XControl));

            using (TextReader textReader = new StringReader(xmlEle.OuterXml))
            {
                return (XControl)xmlSerializer.Deserialize(new NamespaceIgnorantXmlTextReader(textReader));
            }
        }

        public static XControl GetXML_FromControlObject_ByName(string moduleId, string fieldName)
        {
            return GetXML_FromControlObject_ByAttributeName(moduleId, fieldName, "Name");
        }

        public static object GetXML_GetStaticGrid(string moduleId, string gridId)
        {
            throw new NotImplementedException();
        }

        public static XControl GetXML_FromControlObject_ByCaption(string moduleId, string fieldName)
        {
            return GetXML_FromControlObject_ByAttributeName(moduleId, fieldName, "Caption");
        }

        #endregion XML Form Related

        #region Snapshot Related
        public static bool Snapshot_Create(string snapshotName, bool ignoreCreateIfExists = false)
        {
            if (string.IsNullOrWhiteSpace(snapshotName))
                throw new AurigoConfigException("Snapshot name cannot be empty");

            //Limitation: https://technet.microsoft.com/en-us/library/ms189940(v=sql.105).aspx

            string dbName = GetDB_Name_FromConnectionString();
            string dbSnapshotName = GetSnapshotNameInDB(snapshotName);

            string queryString_CheckExistingSnapshot = string.Format("SELECT TOP 1 Name FROM sys.databases WHERE name = '{0}'", dbSnapshotName);

            string ss_timeStamp = Helpers.DateTimeNow();
            string ss_dirname = AurigoAppSettings.DatabaseSnapshotPath;


            if (string.IsNullOrEmpty(ss_dirname))
                throw new AurigoConfigException("File path for Snapshot is not specified in the configfile");
            else if (!ss_dirname.EndsWith("\\"))
                ss_dirname += "\\";

            using (SqlConnection connection = GetNewConnection())
            {
                bool isExistAlready = Snapshot_CheckIfItExists(connection, snapshotName);

                if (isExistAlready && !ignoreCreateIfExists)
                    throw new AurigoConfigException("Snapshot Already exists. Delete it our reuse this create snapshot");

                if (!isExistAlready)
                {
                    //string  query_ss_partialFiles = "select OnCmd = '(NAME=''' + [name] + ''', FILENAME=''' + @ssdirname + [name] + '-' + @timestamp + '.ss'')' from sys.database_files	where [type] = 0"
                    string query_ss_partialFiles = string.Format("select OnCmd = '(NAME=''' + [name] + ''', FILENAME=''{0}' + [name] + '_{1}_{2}.ss'')' from sys.database_files	where [type] = 0", ss_dirname, snapshotName, ss_timeStamp);

                    string str_OnCommand = string.Empty;
                    using (var adapter = new SqlDataAdapter(query_ss_partialFiles, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                            throw new AurigoConfigException("Snapshot Error in retrieving partial files list in DB");
                        else
                        {
                            bool isfirst = false;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (!isfirst)
                                {
                                    str_OnCommand += row["OnCmd"];
                                    isfirst = true;
                                }
                                else
                                    str_OnCommand += "," + row["OnCmd"];
                            }
                        }
                    }

                    //-- CREATE DATABASE PlanningDBSS ON (NAME = 'MWProduct_Planning', FILENAME = 'D:\MSSnapShots\MWProduct_Planning-20160907T162824473.ss') AS SNAPSHOT OF PlanningDB
                    //--"CREATE DATABASE {snapshotDBName} ON (NAME='{current_db name}', FILENAME='{fullpathofsnapshot.ss}') AS SNAPSHOT OF {originalDB}"
                    string queryString_CreateSnapshot = string.Format("CREATE DATABASE {0} ON {1} AS SNAPSHOT OF {2}", dbSnapshotName, str_OnCommand, dbName);

                    //
                    SqlCommand command = new SqlCommand(queryString_CreateSnapshot, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }

        public static bool Snapshot_Restore(string snapshotName, bool continueIfFailed = false)
        {
            if (string.IsNullOrWhiteSpace(snapshotName))
                throw new AurigoConfigException("Snapshot name cannot be empty");

            string dbName = GetDB_Name_FromConnectionString();
            string dbSnapshotName = GetSnapshotNameInDB(snapshotName);

            try
            {
                using (SqlConnection masterDB_Conn = GetNewConnection_As_MasterDatabase())
                {
                    if (!Snapshot_CheckIfItExists(masterDB_Conn, snapshotName))
                        throw new AurigoConfigException(string.Format("Snapshot named '{0}' does not exists.", snapshotName));

                    //url : http://weblogs.asp.net/jongalloway/Handling-_2200_GO_2200_-Separators-in-SQL-Scripts-_2D00_-the-easy-way
                    //Server server = new Server(new ServerConnection(connection));
                    //server.ConnectionContext.ExecuteNonQuery(queryString_CheckExistingSnapshot);

                    SqlCommand command1 = new SqlCommand(string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", dbName, dbSnapshotName), masterDB_Conn);
                    command1.Connection.Open();
                    command1.ExecuteNonQuery();

                    try
                    {
                        SqlCommand command2 = new SqlCommand(string.Format("RESTORE DATABASE {0} FROM DATABASE_SNAPSHOT = '{1}';", dbName, dbSnapshotName), masterDB_Conn);
                        command2.ExecuteNonQuery();
                    }
                    finally
                    {
                        //must restore it mandatorily
                        SqlCommand command3 = new SqlCommand(string.Format("ALTER DATABASE {0} SET MULTI_USER", dbName, dbSnapshotName), masterDB_Conn);
                        command3.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!continueIfFailed)
                    throw;
            }

            return true;
        }

        public static bool Snapshot_Delete(string snapshotName, bool continueIfFailed = false)
        {
            if (string.IsNullOrWhiteSpace(snapshotName))
                throw new AurigoConfigException("Snapshot name cannot be empty");

            string dbSnapshotName = GetSnapshotNameInDB(snapshotName);
            string queryString_CheckExistingSnapshot = string.Format("SELECT TOP 1 Name FROM sys.databases WHERE name = '{0}'", dbSnapshotName);
            try
            {
                using (SqlConnection connection = GetNewConnection())
                {
                    if (!Snapshot_CheckIfItExists(connection, snapshotName))
                        throw new AurigoConfigException(string.Format("Snapshot named '{0}' does not exists.", snapshotName));

                    //string queryString_DeleteSnapshot = string.Format("USE master; ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{0}];", dbSnapshotName);
                    string queryString_DeleteSnapshot = string.Format("DROP DATABASE [{0}];", dbSnapshotName);

                    //
                    SqlCommand command = new SqlCommand(queryString_DeleteSnapshot, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                if (!continueIfFailed)
                    throw;
            }

            return true;
        }

        private static bool Snapshot_CheckIfItExists(SqlConnection connection, string snapshotName)
        {
            string dbSnapshotName = GetSnapshotNameInDB(snapshotName);
            string queryString_CheckExistingSnapshot = string.Format("SELECT TOP 1 Name FROM sys.databases WHERE name = '{0}'", dbSnapshotName);

            using (var adapter = new SqlDataAdapter(queryString_CheckExistingSnapshot, connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                    return true;
            }

            return false;
        }

        public static string GetSnapshotNameInDB(string snapshotName)
        {
            return GetDB_Name_FromConnectionString() + "_SS_" + snapshotName;
        }
        #endregion Snapshot Related

        #region Data checker
        public static bool Check_DataExist(string tableName, string idFieldName, string hintFieldName, string hintFieldValue, EnumHintFieldSearchTechnique hintFieldSearchTechnique)
        {
            try
            {
                string id = GetLastCreatedIdForTable(tableName, idFieldName, hintFieldName, hintFieldValue, hintFieldSearchTechnique);
                if (!string.IsNullOrEmpty(id))
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public static bool Check_DataExist(HintSetting hintFieldSetting)
        {
            return Check_DataExist(hintFieldSetting.TableName, hintFieldSetting.IdField, hintFieldSetting.HintField, hintFieldSetting.HintFieldValue, hintFieldSetting.HintFieldSearchTechnique);
        }
        #endregion Data checker
    }

    internal class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(System.IO.TextReader reader) : base(reader) { }

        public override string NamespaceURI
        {
            get { return ""; }
        }
    }
}