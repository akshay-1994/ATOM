using Aurigo.Atom.Common.GeneratorObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace Aurigo.Atom.Generator.Core.CodeSolutionBuilder
{
    public class TestSuiteXmlFileGenerator : AbstractFileGeneratorBase
    {
        private string _ModuleId { get; set; }
        private string _DateTimeStamp { get; set; }

        public TestSuiteConfigFile TestSuiteConfigFileObject { get; private set; }

        public TestSuiteXmlFileGenerator(string moduleId, string dateTimeStampAppend, TestSuiteConfigFile testSuiteConfigFileObject)
        {
            this._ModuleId = moduleId;
            this._DateTimeStamp = dateTimeStampAppend;
            this.TestSuiteConfigFileObject = testSuiteConfigFileObject;
        }

        protected override string FileName { get { return $"{_ModuleId}_TestSuite{this._DateTimeStamp}.xml"; } }

        protected override string GetFileContent()
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = false,
                OmitXmlDeclaration = true
            };

            var stream = new MemoryStream();
            using (XmlWriter xw = XmlWriter.Create(stream, settings))
            {
                //this avoids xml namespace declaration
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces(
                                   new[] { XmlQualifiedName.Empty });
                XmlSerializer x = new XmlSerializer(typeof(TestSuiteConfigFile), "");
                x.Serialize(xw, this.TestSuiteConfigFileObject, ns);
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
