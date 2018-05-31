using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Core;
using ModuleXYZ_TestSuite.AutoGenTests;
using System;
using System.Collections.Generic;

namespace ModuleXYZ_TestSuite
{
    public class MasterRunner_AutoGenTests : ITestSuiteBase
    {
        public string SuiteName { get { return "ModuleXYZ_TestSuite_AutoGenTests"; } }

        public void RunTest(List<string> testMethodsToRun = null)
        {
            throw new NotImplementedException();
        }

        //public void RunTest(List<string> testMethodsToRun = null)
        //{
        //    TC tc = new TC();

        //    List<Action<string, string>> testMethodChain = new List<Action<string, string>>();

        //    //based on methods to run
        //    if (testMethodsToRun == null || testMethodsToRun.Contains("Senario_00001")) testMethodChain.Add(tc.Senario_00001);
        //    if (testMethodsToRun == null || testMethodsToRun.Contains("Senario_00002")) testMethodChain.Add(tc.Senario_00001);
        //    if (testMethodsToRun == null || testMethodsToRun.Contains("Senario_00003")) testMethodChain.Add(tc.Senario_00001);
        //    if (testMethodsToRun == null || testMethodsToRun.Contains("Senario_00004")) testMethodChain.Add(tc.Senario_00001);
        //    if (testMethodsToRun == null || testMethodsToRun.Contains("Senario_00005")) testMethodChain.Add(tc.Senario_00001);
        //    if (testMethodsToRun == null || testMethodsToRun.Contains("Senario_00006")) testMethodChain.Add(tc.Senario_00001);

        //    TestRunner.Run("{AutoGenTestsId}", "{AutoGenTestsDescription}", testMethodChain);
        //}

        //public void RunTest(List<string> testMethodsToRun = null)
        //{
        //    TC tc = new TC();

        //    List<Action<string, string>> testMethodChain = new List<Action<string, string>>();

        //    foreach (var methodName in testMethodsToRun)
        //    {
        //        var methodRef = GetMethodReference(methodName, ref tc);
        //        if (methodRef != null)
        //            testMethodChain.Add(methodRef);
        //    }

        //    var testRunDescription = "ALL";

        //    if (testMethodsToRun == null)
        //        testMethodChain = GetAllMethodReference(ref tc);
        //    else
        //        testRunDescription = string.Join(",", testMethodsToRun);

        //    TestRunner.Run("{AutoGenTestsId}", "Running: " + testRunDescription, testMethodChain);
        //}

        //protected Action<string, string> GetMethodReference(string methodName, ref TC tc)
        //{
        //    switch (methodName)
        //    {
        //        case "Senario_00001": return tc.Senario_00001;
        //        case "Senario_00002": return tc.Senario_00002;
        //        case "Senario_00003": return tc.Senario_00001;
        //        case "Senario_00004": return tc.Senario_00001;
        //        case "Senario_00005": return tc.Senario_00001;
        //    }
        //    return null;
        //}

        //protected List<Action<string, string>> GetAllMethodReference(ref TC tc)
        //{
        //    List<Action<string, string>> testMethodChain = new List<Action<string, string>>();

        //    testMethodChain.Add(tc.Senario_00001);
        //    testMethodChain.Add(tc.Senario_00001);
        //    testMethodChain.Add(tc.Senario_00001);
        //    testMethodChain.Add(tc.Senario_00001);
        //    testMethodChain.Add(tc.Senario_00001);
        //    testMethodChain.Add(tc.Senario_00001);

        //    return testMethodChain;
        //}
    }
}
