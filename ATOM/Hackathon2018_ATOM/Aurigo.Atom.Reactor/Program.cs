using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using AurigoTest.Toolkit;
using Aurigo.Atom.Common.GeneratorObjects;

namespace Aurigo.Atom.Reactor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<string> xmlConfigFileNameArray = new List<string>();

            string pathOfTestSuites = Path.Combine(AppContext.BaseDirectory, "TestSuites");

            if (!Directory.Exists(pathOfTestSuites))
                Directory.CreateDirectory(pathOfTestSuites);

            if (args.Length > 1)
            {
                xmlConfigFileNameArray = args.Skip(1).ToList();
            }
            else
            {
                xmlConfigFileNameArray = Directory.GetFiles(pathOfTestSuites, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            }

            foreach (var moduleXmlFilePath in xmlConfigFileNameArray)
            {
                //moduleXmlFilePath = "D:\\TFS_REPO\\Hackathon2018_ATOM\\AurigoTest\\ModuleXYZ_TestSuite\\ModuleXYZ_TestSuite.xml";

                List<string> methodsToRun = null;

                TestSuiteConfigFile tsConfigFile = ParseConfigXmlAndExtractTestcasesToRun(moduleXmlFilePath, out methodsToRun);

                var dllPath = Path.Combine(pathOfTestSuites, tsConfigFile.LibraryAssemblyName);

                if (!dllPath.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
                    dllPath += ".dll";

                if (!File.Exists(dllPath))
                {
                    Console.WriteLine($"Missing dll {tsConfigFile.LibraryAssemblyName}");
                    continue;
                }

                try
                {
                    //Load the assembly from the specified path.
                    var MyAssembly = Assembly.LoadFrom(dllPath);

                    var obj = MyAssembly.CreateInstance(tsConfigFile.LibraryName);

                    ITestSuiteBase tsbase = obj as ITestSuiteBase;

                    tsbase.RunTest(methodsToRun);
                }
                catch (Exception e)
                {
                }
            }
        }

        #region Helper Methods

        private static TestSuiteConfigFile ParseConfigXmlAndExtractTestcasesToRun(string filenameWithFullPath, out List<string> methodsToRun)
        {
            methodsToRun = new List<string>();

            TestSuiteConfigFile tsConfigFile = null;
            try
            {
                var xs = new XmlSerializer(typeof(TestSuiteConfigFile));

                var fReader = new System.IO.FileStream(filenameWithFullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                tsConfigFile = xs.Deserialize(fReader) as TestSuiteConfigFile;
            }
            catch (Exception e)
            {
                throw e;
            }

            List<int> run_IdList = (!string.IsNullOrWhiteSpace(tsConfigFile.RunConfig.Run)) ? ParseNumberRangeFormat(tsConfigFile.RunConfig.Run) : new List<int>();
            List<int> no_run_IdList = (!string.IsNullOrWhiteSpace(tsConfigFile.RunConfig.NoRun)) ? ParseNumberRangeFormat(tsConfigFile.RunConfig.NoRun) : new List<int>();

            List<int> final_run_IdList = new List<int>();
            List<int> final_noRun_IdList = new List<int>();

            if (run_IdList.Any())
                if (no_run_IdList.Any())
                    final_run_IdList = run_IdList.Where(t => !no_run_IdList.Contains(t)).ToList();
                else
                    final_run_IdList = run_IdList;
            else
            {
                if (no_run_IdList.Any())
                    final_noRun_IdList = no_run_IdList;
            }

            //Now extract method names
            if (final_run_IdList.Any())
            {
                foreach (var id in final_run_IdList)
                {
                    var testItem = tsConfigFile.TestDefinitionList.FirstOrDefault(t => t.Id == id);
                    if (testItem != null)
                        methodsToRun.Add(testItem.Method);
                }
            }
            else
            {
                if (final_noRun_IdList.Any())
                    methodsToRun = tsConfigFile.TestDefinitionList.Where(t => !final_noRun_IdList.Contains(t.Id)).Select(t => t.Method).ToList();
                else
                    methodsToRun = tsConfigFile.TestDefinitionList.Select(t => t.Method).ToList();
            }

            return tsConfigFile;
        }

        //private static void ParseConfigXmlAndExtractTestcasesToRun(XmlDocument doc, out List<string> testCasesToRun, out bool isRunUser)
        //{
        //    testCasesToRun = new List<string>();
        //    isRunUser = false;

        //    XmlNodeList configList = doc.GetElementsByTagName("Config");
        //    if (configList.Count == 0)
        //        return;

        //    XmlNode node = configList.Item(0);

        //    string run_Config = node.Attributes["Run"]?.Value;
        //    string runExclude_Config = node.Attributes["NoRun"]?.Value;

        //    List<int> run_IdList = (!string.IsNullOrWhiteSpace(run_Config)) ? ParseNumberRangeFormat(run_Config) : new List<int>();
        //    List<int> no_run_IdList = (!string.IsNullOrWhiteSpace(run_Config)) ? ParseNumberRangeFormat(runExclude_Config) : new List<int>();

        //    List<int> final_run_IdList = new List<int>();
        //    List<int> final_noRun_IdList = new List<int>();

        //    if (run_IdList.Any())
        //        if (no_run_IdList.Any())
        //            final_run_IdList = run_IdList.Where(t => !no_run_IdList.Contains(t)).ToList();
        //        else
        //            final_run_IdList = run_IdList;
        //    else
        //    {
        //        if (no_run_IdList.Any())
        //            final_noRun_IdList = no_run_IdList;
        //    }

        //    //XmlNodeList testCaseNodes =.GetElementsByTagName("//Tests/Test");
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="configValue">of format : 1-10,20-23,25</param>
        /// <returns></returns>
        private static List<int> ParseNumberRangeFormat(string configValue)
        {
            List<int> listRunnableTestCases = new List<int>();

            configValue = configValue.Trim();

            String[] req_val = configValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string values in req_val)
            {
                if (!values.Contains("-"))
                    listRunnableTestCases.Add(Int32.Parse(values));
                else
                {
                    String[] vals1 = values.Split('-').Select(t => t.Trim()).ToArray();

                    int startRange = int.Parse(vals1[0]);
                    int endRange = int.MaxValue;

                    if (vals1.Length > 1)
                        endRange = int.Parse(vals1[1]);

                    //swap if required
                    if (endRange < startRange) { int x = startRange; startRange = endRange; endRange = x; }

                    int count = (endRange - startRange) + 1;

                    listRunnableTestCases.AddRange(Enumerable.Range(startRange, count));
                }
            }

            return listRunnableTestCases.ToList();
        }

        #endregion Helper Methods
    }
}