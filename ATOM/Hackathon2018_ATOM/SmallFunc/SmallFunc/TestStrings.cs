using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

namespace SmallFunc
{
    public class TestStrings{

        List<int> auxList = new List<int>();
        List<int> listRunnableTestCases = new List<int>();
        public List<string> getScenariosList() {

            XmlDocument doc = new XmlDocument();
            doc.Load("ModuleXYZ_TestSuite.xml");

             XmlNodeList configList = doc.GetElementsByTagName("Config");

            List<string> methods = new List<string>();
            foreach (XmlNode node in configList)
            {
                string value = node.Attributes["Run"].Value;
                List<int>runnableIDs = getRangeofRunnableTestCases(value);

                List<string>runnableIDsToMatch = runnableIDs.ConvertAll<string>(x => x.ToString());

                foreach (string id in runnableIDsToMatch)
                {
                    XmlNode result = doc.SelectSingleNode("//Tests/Test[@Id='" + id + "']");
                    if (result != null)
                    {
                        string method = result.Attributes["Method"].Value;
                        if (!methods.Contains(method))
                            methods.Add(method);
                    }
                }
            }

           




            return methods;
        }

        public List<int> getRangeofRunnableTestCases(string value) {
            String[] req_val = value.Split(',');
            foreach (string values in req_val)
            {
                if (values.Contains("-"))
                {

                    String[] vals1 = values.Split('-');
                    int count = (Int32.Parse(vals1[1]) - Int32.Parse(vals1[0]))+1;
                    auxList =Enumerable.Range(Int32.Parse(vals1[0]), count).ToList();
                    listRunnableTestCases.AddRange(auxList);
                    auxList.Clear();
                }

                else
                {
                    listRunnableTestCases.Add(Int32.Parse(values));
                }

            }

            

            return listRunnableTestCases;


        }

        public static void Main(String[] args)
        {
            TestStrings ts = new TestStrings();
          
        }
    
    }


}
