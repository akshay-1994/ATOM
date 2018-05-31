namespace Aurigo.Atom.Common.DTO
{
    /// <summary>
    ///
    /// </summary>
    public class SolutionGenerationConfig
    {
        public bool IsGeneratedDistinctDLL { get; set; }

        public bool IsGeneratedDistinctSuiteXmlConfig { get; set; }

        public bool IsAutoGenerateSolutionName { get; set; } = true;

        public string SolutionName { get; set; }

        public string SolutionTargetPath { get; set; }


        /// <summary>
        ///if not specified then we use the this.SolutionSavePath
        /// </summary>
        public string LastKnownSolutionFolderDirPath { get; set; }

        public bool IsCompileAndGenerateLibrary { get; set; }

        public bool IsAutoDeploy { get; set; }

        public string AutoDeployPath { get; set; }
        
    }
}