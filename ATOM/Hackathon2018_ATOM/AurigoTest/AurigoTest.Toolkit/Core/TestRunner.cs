using AurigoTest.Toolkit.MW;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public static class TestRunner
    {
        private static Type[] attributesForExecution = new Type[] { typeof(RunAttribute) };

        public static void InitRunOnlyAttributes(params Type[] attributesArg)
        {
            attributesForExecution = attributesArg;
        }

        //public static void RunFunc(string testID, string description, params Func<string, MasterworksScreen>[] testMethodChain)
        //{
        //    string curMethodName = "";
        //    int methodChainNumber = 0;

        //    var testObj = Helpers.Report.StartTest(testID, description);

        //    bool hasExceptionOccurred = false;

        //    foreach (var method in testMethodChain)
        //    {
        //        curMethodName = method.Method.Name;
        //        methodChainNumber++;

        //        testObj.Log(LogStatus.Info, "START => " + methodChainNumber + " " + curMethodName);
        //        try
        //        {
        //            if (!hasExceptionOccurred)
        //            {
        //                method.Invoke(testID);
        //                testObj.Log(LogStatus.Pass, "DONE  => " + methodChainNumber + " " + curMethodName);
        //            }
        //            else
        //            {
        //                testObj.Log(LogStatus.Skip, methodChainNumber + " " + curMethodName);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            hasExceptionOccurred = true;
        //            string errorDetails = "ERROR => " + methodChainNumber + " " + curMethodName + " " + ex.Message;
        //            //string errorDetails = string.Format("Method Chain {0} of {1} failed at location '{2}'", methodChainNumber, testMethodChain.Count(), curMethodName);

        //            testObj.Log(LogStatus.Fail, new Exception(errorDetails, ex));
        //        }
        //        finally
        //        {
        //            //testObj.Log(LogStatus.Info, "END => " + methodChainNumber + " " + curMethodName);
        //        }
        //    }

        //    Helpers.Report.EndTest(testObj);
        //}
        public static void Run(string testID, string description, params Action<string, string>[] testMethodChain)
        {
            Run(testID, description, testMethodChain.ToList());
        }

        public static void Run(string testID, string description, List<Action<string, string>> testMethodChain)
        {
            string curMethodName = "";
            int methodChainNumber = 0;

            var testObj = Helpers.Report.StartTest(testID, description);

            bool hasExceptionOccurred = false;

            foreach (var method in testMethodChain)
            {
                curMethodName = method.Method.Name;
                methodChainNumber++;

                testObj.Log(LogStatus.Info, "START => " + methodChainNumber + " " + curMethodName);
                try
                {
                    if (!hasExceptionOccurred)
                    {
                        try
                        {
                            method.Invoke(testID, description);
                            testObj.Log(LogStatus.Pass, "DONE  => " + methodChainNumber + " " + curMethodName);
                        }
                        catch (Exception e)
                        {
                            testObj.Log(LogStatus.Error, "ERROR => " + methodChainNumber + " " +
                                curMethodName + " Exception:" + e.Message);
                        }
                    }
                    else
                    {
                        testObj.Log(LogStatus.Skip, methodChainNumber + " " + curMethodName);
                    }
                }
                catch (Exception ex)
                {
                    hasExceptionOccurred = true;
                    string errorDetails = "ERROR => " + methodChainNumber + " " + curMethodName + " " + ex.Message;
                    //string errorDetails = string.Format("Method Chain {0} of {1} failed at location '{2}'", methodChainNumber, testMethodChain.Count(), curMethodName);

                    //testObj.AddScreenCapture(Helpers.GetImageLogFileWithFullPath());
                    testObj.Log(LogStatus.Fail, ex.Message, new Exception(errorDetails, ex));

                    if (ex is AurigoTestException)
                        testObj.Log(LogStatus.Info, "Snapshot below: " + testObj.AddScreenCapture((ex as AurigoTestException).ScreenshotPath));
                }
                finally
                {
                    //testObj.Log(LogStatus.Info, "END => " + methodChainNumber + " " + curMethodName);
                }
            }

            Helpers.Report.EndTest(testObj);
        }

        private static string GetDefault_AdditionalInformation(TestClassBase testClassReference)
        {
            string desc = (testClassReference != null && !string.IsNullOrEmpty(testClassReference.AdditionalRunInfo)) ? "(" + testClassReference.AdditionalRunInfo + ")" : string.Empty;
            return desc;
        }

        private static string GeNewLineChar(bool isHtmlReport = true)
        {
            if (isHtmlReport)
                return "<br/>";

            return Environment.NewLine;
        }

        public static void RunWithDesc(string testID, string description, List<Action<string, string>> testMethodChain, TestClassBase testClassReference)
        {
            string curMethodName = "";
            int methodChainNumber = 0;

            Type methodReportAttribute = typeof(MethodReportAttribute);

            var testObj = Helpers.Report.StartTest(testID, description);

            bool hasExceptionOccurred = false;

            string newline = GeNewLineChar();

            foreach (var method in testMethodChain)
            {
                curMethodName = method.Method.Name;
                methodChainNumber++;

                string methodDescMetadata = string.Empty;
                string methodId = string.Empty;

                var methodAttribute = method.Method.GetCustomAttributes(methodReportAttribute, true).FirstOrDefault();
                if (methodAttribute != null)
                {
                    MethodReportAttribute mra = methodAttribute as MethodReportAttribute;

                    if (mra != null)
                        methodDescMetadata = mra.Description;
                }

                methodDescMetadata = methodDescMetadata.Replace(Environment.NewLine, GeNewLineChar());

                testObj.Log(LogStatus.Info, $"START => [{methodChainNumber}] {curMethodName}() {newline} {methodDescMetadata}");
                try
                {
                    if (!hasExceptionOccurred)
                    {
                        try
                        {
                            method.Invoke(testID, description);
                            testObj.Log(LogStatus.Pass, $"DONE  => [{methodChainNumber}]  {curMethodName}() {GetDefault_AdditionalInformation(testClassReference)}");
                        }
                        catch (Exception e)
                        {
                            testObj.Log(LogStatus.Error, $"ERROR  => [{methodChainNumber}]  {curMethodName}() {GetDefault_AdditionalInformation(testClassReference)} {newline} Exception :  {e.Message}");
                        }
                    }
                    else
                    {
                        testObj.Log(LogStatus.Skip, methodChainNumber + " " + curMethodName, description);
                    }
                }
                catch (Exception ex)
                {
                    hasExceptionOccurred = true;

                    string errorDetails = $"ERROR  => [{methodChainNumber}]  {curMethodName}() {GetDefault_AdditionalInformation(testClassReference)} {newline} Exception :  {ex.Message}";
                    //string errorDetails = string.Format("Method Chain {0} of {1} failed at location '{2}'", methodChainNumber, testMethodChain.Count(), curMethodName);

                    //testObj.AddScreenCapture(Helpers.GetImageLogFileWithFullPath());
                    testObj.Log(LogStatus.Fail, ex.Message, new Exception(errorDetails, ex));

                    if (ex is AurigoTestException)
                        testObj.Log(LogStatus.Info, "Snapshot below: " + testObj.AddScreenCapture((ex as AurigoTestException).ScreenshotPath));
                }
                finally
                {
                    //testObj.Log(LogStatus.Info, "END => " + methodChainNumber + " " + curMethodName);
                }
            }

            Helpers.Report.EndTest(testObj);
        }

        //public static void Run(string testID, string description, Action<string, string> methodToRun, params object[] args)
        //{
        //}

        /// <summary>
        /// Runs all method that are marked with MWTestMethodAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="testID"></param>
        /// <param name="description"></param>
        /// <param name="testMethodContainerClassObject"></param>
        public static void Run<T>(string testID, string description, T testMethodContainerClassObject)
        {
            Type typeOfClass = testMethodContainerClassObject.GetType();

            Type typeToIgnore = typeof(IgnoreRun);

            foreach (MethodInfo methodRef in typeOfClass.GetMethods())
            {
                bool isMethodRunRequired = false;

                if (methodRef.GetCustomAttributes(typeToIgnore, true).Length > 0)
                {
                    isMethodRunRequired = false;
                    continue;
                }

                foreach (var at in attributesForExecution)
                {
                    if (methodRef.GetCustomAttributes(at, true).Length > 0)
                    {
                        isMethodRunRequired = true;
                        break;
                    }
                }

                if (isMethodRunRequired)
                {
                    var testObj = Helpers.Report.StartTest(testID, description);

                    string curMethodName = methodRef.Name;

                    testObj.Log(LogStatus.Info, "START => " + curMethodName);
                    try
                    {
                        methodRef.Invoke(testMethodContainerClassObject, new object[] { testID, description });
                        testObj.Log(LogStatus.Pass, "DONE  => " + curMethodName);
                    }
                    catch (Exception ex)
                    {
                        testObj.Log(LogStatus.Fail, "FAILED  => " + curMethodName, ex);
                    }
                    finally
                    {
                        //testObj.Log(LogStatus.Info, "END => " + methodChainNumber + " " + curMethodName);
                    }
                }
            }
        }

        //public static void RunGeneric<T>(string testID, string description, params T[] testMethodChain) where T : Func<string, AutomateScreen> //, Action<string>
        //{
        //}
    }
}