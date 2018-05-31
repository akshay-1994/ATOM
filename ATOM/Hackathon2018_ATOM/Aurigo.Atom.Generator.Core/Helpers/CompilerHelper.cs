using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Aurigo.Atom.Generator.Core.Helpers
{
    public static class CompilerHelper
    {

        public static bool CompilerProject(string srcPath, string projectFilePath, string targetPath, TextWriter wStream, out string dllFileNameWithFullPath)
        {
            dllFileNameWithFullPath = string.Empty;

            bool hasError = false;
            try
            {
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);

                //src : http://ww0ww.blogspot.in/2014/07/automated-build-csharp-code-using.html
                //https://stackoverflow.com/questions/42260915/codedomprovider-compileassemblyfromsource-cant-find-roslyn-csc-exe

                System.CodeDom.Compiler.CompilerParameters cp = new CompilerParameters();


                string _projectName = Path.GetFileNameWithoutExtension(projectFilePath);
                XmlDocument _project = new XmlDocument();
                _project.Load(projectFilePath);
                XmlNamespaceManager ns = new XmlNamespaceManager(_project.NameTable);
                ns.AddNamespace("msbld", "http://schemas.microsoft.com/developer/msbuild/2003");
                XmlNode _settings = _project.SelectSingleNode("//msbld:Project/msbld:PropertyGroup/msbld:AssemblyName", ns);
                string _asmName = _settings.InnerText;
                XmlNodeList _files = _project.SelectNodes("//msbld:Project/msbld:ItemGroup/msbld:Compile", ns);
                ArrayList _sourceNames = new ArrayList();
                // Add compilable files  
                foreach (XmlNode _file in _files)
                {
                    string _codeFilename = _file.Attributes["Include"].Value.ToLower();
                    if (_codeFilename != "assemblyinfo.cs" && !_codeFilename.EndsWith("vsa.cs"))
                    {
                        // Read the source code  
                        int _idx = _sourceNames.Add(_file.Attributes["Include"].Value);
                    }
                }
                XmlNodeList _reference = _project.SelectNodes("//msbld:Project/msbld:ItemGroup/msbld:Reference", ns);
                foreach (XmlNode _asmRef in _reference)
                {
                    // Try to use the AssemblyName attribute if it exists  
                    string _refAsmName;
                    if (_asmRef.Attributes["AssemblyName"] != null)
                        _refAsmName = _asmRef.Attributes["AssemblyName"].Value + ".dll";
                    else if (_asmRef.Attributes["Name"] != null)
                        _refAsmName = _asmRef.Attributes["Name"].Value + ".dll";
                    else
                    {
                        if (_asmRef.HasChildNodes && _asmRef["HintPath"] != null)
                        {
                            _refAsmName = Path.Combine(srcPath, _asmRef["HintPath"].InnerText);
                        }
                        else
                        {
                            _refAsmName = _asmRef.Attributes["Include"].Value + ".dll";
                            if (_refAsmName.IndexOf(",") > 0)
                            {
                                _refAsmName = _refAsmName.Substring(0, _refAsmName.IndexOf(",")) + ".dll";

                            }
                        }
                    }

                    cp.ReferencedAssemblies.Add(_refAsmName);
                }
                cp.GenerateExecutable = false;   // result is a .DLL  
                cp.IncludeDebugInformation = true;
                String[] _sourcefiles = (String[])_sourceNames.ToArray(typeof(string));

                string[] fullPath_sourcefiles = _sourcefiles.Select(t => Path.Combine(srcPath, t)).ToArray();

                //-------------------------------------------------------------

                CodeDomProvider provider = new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider();
                //CodeDomProvider.CreateProvider("CSharp", options);

                dllFileNameWithFullPath = Path.Combine(targetPath, _asmName + ".dll");
                cp.CompilerOptions = " /out:" + dllFileNameWithFullPath;

                CompilerResults cr = provider.CompileAssemblyFromFile(cp, fullPath_sourcefiles);
                if (cr.Errors.Count > 0) //if (_compErrs.Length > 0)  
                {
                    hasError = true;

                    bool _error = false;
                    foreach (CompilerError _err in cr.Errors)
                    {
                        // Error or warning?  
                        if (!_err.IsWarning)//( _err.ErrorLevel != Microsoft.CSharp.ErrorLevel.Warning )  
                            _error = true;
                        if (_error)
                        {
                            string errorMsg = "CompileAndDeploy:CompileComponentAssemblies: Error compiling " + _projectName + ".\nPlease rectify then redeloy.";
                            wStream.WriteLine(errorMsg);
                            Console.WriteLine(errorMsg);
                        }
                        else
                        {
                            string errorMsg = "CompileAndDeploy:CompileComponentAssemblies: Warning compiling " + _projectName + ".\nPlease rectify then redeloy.";
                            wStream.WriteLine(errorMsg);
                            Console.WriteLine(errorMsg);
                        }
                        wStream.WriteLine(_err.ErrorText);
                        Console.WriteLine(_err.ErrorText);
                    }
                    if (_error)
                    {
                        wStream.WriteLine("Compile errors occurred. Rectify first.");
                        Console.WriteLine("Compile errors occurred. Rectify first.");
                        //throw new Exception("Compile errors occurred. Rectify first.");
                    }
                }

                var v = cr.Output;

                foreach (var line in cr.Output)
                    wStream.WriteLine(line);

            }
            catch (Exception e)
            {
                hasError = true;
                wStream.WriteLine(e.Message);
                Console.WriteLine(e.Message);
            }

            wStream.Flush();

            return hasError;
        }
    }
}
