using Aurigo.Atom.Common.GeneratorObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Aurigo.Atom.Generator.Core.CodeSolutionBuilder
{
    class TestSuiteAppConfigGenerator : AbstractFileGeneratorBase
    {
        private string _ModuleId { get; set; }
        private string _DateTimeStamp { get; set; }


        public AppSettings TestSuiteAppConfigObject { get; private set; }

        public TestSuiteAppConfigGenerator(string moduleId, string dateTimeStampAppend, AppSettings testSuiteAppConfigObject)
        {
            this._ModuleId = moduleId;
            this._DateTimeStamp = dateTimeStampAppend;
            this.TestSuiteAppConfigObject = testSuiteAppConfigObject;
        }

        protected override string FileName { get { return $"{_ModuleId}_TestSuite{this._DateTimeStamp}_App.config"; } }

        protected override string GetFileContent()
        {
            //this avoids xml document declaration
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
                XmlSerializer x = new XmlSerializer(typeof(AppSettings), "");
                x.Serialize(xw, this.TestSuiteAppConfigObject, ns);
            }

            return Encoding.UTF8.GetString(stream.ToArray());

        }
    }
}
