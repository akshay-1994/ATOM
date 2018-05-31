using System;
using System.Linq;
using OpenQA.Selenium;
using AurigoTest.Toolkit.Common;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using RelevantCodes.ExtentReports;
using System.Data;

namespace AurigoTest.Toolkit.Core
{
    public abstract partial class AutomationBase<TSelf, V>
    {

        //public string GetLastCreatedIdForTable(string tableName, string idFieldName, string hintFieldName = null, string hintFieldValue = null)
        //{
        //    return DBHelper.GetLastCreatedIdForTable(tableName, idFieldName, hintFieldName, hintFieldValue);
        //}

        protected DataTable BeginDatabaseVerification_Using_LastId(string tableName, string idFieldName, string hintFieldName = null, string hintFieldValue = null)
        {
            DataTable dt = DBHelper.GetDataTable_With_LastCreatedRow(tableName, idFieldName, hintFieldName, hintFieldValue);

            if (dt.Rows.Count > 0)
                return dt;
            else
                throw new Exception(string.Format("Last created Row using ID not available for {0}.{1}", tableName, idFieldName));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionIfIdExists">string id, reference to list page</param>
        /// <returns></returns>
        public TSelf ExecuteCustom_Using_LastId(string tableName, string idFieldName, string hintFieldName, string hintFieldValue, EnumHintFieldSearchTechnique hintFieldSearchTechnique, Action<string, TSelf> actionIfIdExists)
        {
            string id = DBHelper.GetLastCreatedIdForTable(tableName, idFieldName, hintFieldName, hintFieldValue, hintFieldSearchTechnique);

            if (!string.IsNullOrEmpty(id))
                actionIfIdExists.Invoke(id, this as TSelf);
            else
                throw new Exception(string.Format("Last ID not available for {0}.{1}", tableName, idFieldName));

            return this as TSelf;
        }

        public TSelf ExecuteCustom_Using_LastId(HintSetting hintFieldSetting, Action<string, TSelf> actionIfIdExists)
        {
            return ExecuteCustom_Using_LastId(hintFieldSetting.TableName, hintFieldSetting.IdField, hintFieldSetting.HintField, hintFieldSetting.HintFieldValue, hintFieldSetting.HintFieldSearchTechnique, actionIfIdExists);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="idFieldName"></param>
        /// <param name="actionIfIdExists"></param>
        /// <returns></returns>
        public TSelf ExecuteCustom_Using_LastId(string tableName, string idFieldName, Action<string, TSelf> actionIfIdExists)
        {
            string id = DBHelper.GetLastCreatedIdForTable(tableName, idFieldName);

            if (!string.IsNullOrEmpty(id))
                actionIfIdExists.Invoke(id, this as TSelf);
            else
                throw new Exception(string.Format("Last ID not available for {0}.{1}", tableName, idFieldName));
            //else if (actionIf_NO_Id != null)
            //    actionIf_NO_Id.Invoke(this);

            return this as TSelf;
        }



        public TSelf VerifyInDB_Using_LastId(string verificationTestId, string tableName, string idFieldName, string hintFieldName, string hintFieldValue, Action<DataRowVerifier<TSelf>, TSelf> actionIfIdExists)
        {
            string testName = Helper_GetActualTestName(verificationTestId + " (DB Verifier)");
            var extentTest = Helpers.Report.StartTest(testName);

            try
            {
                var dt = this.BeginDatabaseVerification_Using_LastId(tableName, idFieldName, hintFieldName, hintFieldValue);

                var dataVerifier = new DataRowVerifier<TSelf>(dt.Rows[0], string.Empty);

                actionIfIdExists.Invoke(dataVerifier, this as TSelf);

                this.LogTestSteps(extentTest, testName, dataVerifier.VerificationStepsTrackerList);
            }
            catch (Exception ex)
            {
                extentTest.Log(LogStatus.Error, ex);
            }

            Helpers.Report.EndTest(extentTest);

            return this as TSelf;
        }

        public TSelf VerifyInDB_Using_LastId(string verificationIdentification, HintSetting hintFieldSetting, Action<DataRowVerifier<TSelf>, TSelf> actionIfIdExists)
        {
            return VerifyInDB_Using_LastId(verificationIdentification, hintFieldSetting.TableName, hintFieldSetting.IdField, hintFieldSetting.HintField, hintFieldSetting.HintFieldValue, actionIfIdExists);
        }

        public TSelf VerifyInDB_Using_LastId(string verificationIdentification, string tableName, string idFieldName, Action<DataRowVerifier<TSelf>, TSelf> actionIfIdExists)
        {
            return VerifyInDB_Using_LastId(verificationIdentification, tableName, idFieldName, null, null, actionIfIdExists);
        }

        public TSelf DB_Run_SelectStatement(string selectStatement, Action<DataTable, TSelf> callback)
        {
            DataTable dt = DBHelper.Execute_SelectStatement(selectStatement);

            if (callback != null)
                callback.Invoke(dt, this as TSelf);

            return this as TSelf;
        }

        public TSelf DB_Run_StoredProcedure(string spName, object[] spParams, Action<DataSet, TSelf> callback)
        {
            DataSet ds = DBHelper.Execute_StoredProcedure(spName, spParams);

            if (callback != null)
                callback.Invoke(ds, this as TSelf);

            return this as TSelf;
        }

        public TSelf DB_Snapshot_Create(string snapshotName, bool continueIfExist = false)
        {
            DBHelper.Snapshot_Create(snapshotName, continueIfExist);
            return this as TSelf;
        }

        public TSelf DB_Snapshot_Restore(string snapshotName, bool continueIfFailed = false)
        {
            DBHelper.Snapshot_Restore(snapshotName, continueIfFailed);
            return this as TSelf;
        }

        public TSelf DB_Snapshot_Delete(string snapshotName, bool continueIfFailed = false)
        {
            DBHelper.Snapshot_Delete(snapshotName, continueIfFailed);
            return this as TSelf;
        }
    }
}
