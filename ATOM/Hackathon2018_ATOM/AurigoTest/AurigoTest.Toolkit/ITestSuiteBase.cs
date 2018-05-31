using System;
using System.Collections.Generic;

namespace AurigoTest.Toolkit
{
    public interface ITestSuiteBase
    {
        string SuiteName { get; }

        void RunTest(List<string> testMethodsToRun = null);
    }
}
