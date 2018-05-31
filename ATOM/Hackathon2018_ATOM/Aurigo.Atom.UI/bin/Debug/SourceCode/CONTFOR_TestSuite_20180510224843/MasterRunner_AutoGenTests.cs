

using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Core;
using CONTFOR_TestSuite.AutoGenTests;
using System;
using System.Collections.Generic;

namespace CONTFOR_TestSuite
{
    public class MasterRunner_AutoGenTests : ITestSuiteBase
    {
        public string SuiteName { get { return "CONTFOR_TestSuite"; } }

        public void RunTest(List<string> testMethodsToRun = null)
        {
            TC tc = new TC();

            List<Action<string, string>> testMethodChain = new List<Action<string, string>>();

            foreach (var methodName in testMethodsToRun)
            {
                var methodRef = GetMethodReference(methodName, ref tc);
                if (methodRef != null)
                    testMethodChain.Add(methodRef);
            }

            var testRunDescription = "ALL";

            if (testMethodsToRun == null)
                testMethodChain = GetAllMethodReference(ref tc);
            else
                testRunDescription = string.Join(",", testMethodsToRun);

			try
			{
	            TestRunner.RunWithDesc("CONTFOR_TestSuite", "Running: " + testRunDescription, testMethodChain, tc);
			}
			catch(Exception e)
			{
			}
			finally
            {
                Helpers.Report.Close(); //THIS HAS TO BE DONE so report is created

                if (System.IO.File.Exists(Helpers.ReportFileFullPath))
                    System.Diagnostics.Process.Start(Helpers.ReportFileFullPath);
            } 
        }

        protected Action<string, string> GetMethodReference(string methodName, ref TC tc)
        {
            switch (methodName)
            {
							case "POSITIVE_1": return tc.POSITIVE_1;
							case "NEGATIVE_1": return tc.NEGATIVE_1;
			            }
            return null;
        }

        protected List<Action<string, string>> GetAllMethodReference(ref TC tc)
        {
            List<Action<string, string>> testMethodChain = new List<Action<string, string>>();

							testMethodChain.Add(tc.POSITIVE_1);
							testMethodChain.Add(tc.NEGATIVE_1);
			
            return testMethodChain;
        }
    }
}
