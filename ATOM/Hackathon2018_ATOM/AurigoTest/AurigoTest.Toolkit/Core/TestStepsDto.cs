using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public enum EnumStepStatus { Unknown = 0, Passed, Failed, Skipped };
    public class TestStepsDto
    {
        public string StepName { get; set; }
        public string StepDescription { get; set; }

        public Exception ExceptionRef { get; private set; }
        public string ErrorMsg { get; set; }
        public string ErrorStackTrace { get; set; }

        public EnumStepStatus StepStatus { get; set; } = EnumStepStatus.Unknown;

        private TestStepsDto() { }

        public string GetStepNameAndDescription()
        {
            return Helpers.EscapeForHTML(this.StepName + " : " + this.StepDescription + " : ");
        }

        public static TestStepsDto Success(string stepName, string desc = null)
        {
            return new TestStepsDto() { StepStatus = EnumStepStatus.Passed, StepName = Helpers.EscapeForHTML(stepName), StepDescription = Helpers.EscapeForHTML(desc) };
        }

        public static TestStepsDto Failed(string stepName, Exception ex, string desc = null)
        {
            return new TestStepsDto()
            {
                StepStatus = ex != null ? EnumStepStatus.Failed : EnumStepStatus.Unknown,
                StepName = Helpers.EscapeForHTML(stepName),
                StepDescription = Helpers.EscapeForHTML(desc),
                ErrorMsg = Helpers.EscapeForHTML(ex?.Message),
                ErrorStackTrace = Helpers.EscapeForHTML(ex?.StackTrace),
                ExceptionRef = ex
            };
        }
        public static TestStepsDto Failed(string stepName, string errMsg, string desc = null)
        {
            return new TestStepsDto()
            {
                StepStatus = EnumStepStatus.Failed,
                StepName = Helpers.EscapeForHTML(stepName ?? string.Empty),
                StepDescription = Helpers.EscapeForHTML(desc),
                ErrorMsg = Helpers.EscapeForHTML(errMsg ?? string.Empty),
            };
        }

        public static TestStepsDto Skipped(string stepName, string desc = null)
        {
            return new TestStepsDto()
            {
                StepStatus = EnumStepStatus.Skipped,
                StepName = Helpers.EscapeForHTML(stepName ?? string.Empty),
                StepDescription = Helpers.EscapeForHTML(desc)
            };
        }

        public RelevantCodes.ExtentReports.LogStatus GetTranslatedStatusForReports()
        {
            switch (this.StepStatus)
            {
                case EnumStepStatus.Passed:
                    return RelevantCodes.ExtentReports.LogStatus.Pass;
                case EnumStepStatus.Failed:
                    return RelevantCodes.ExtentReports.LogStatus.Fail;
                case EnumStepStatus.Skipped:
                    return RelevantCodes.ExtentReports.LogStatus.Skip;
                default:
                    return RelevantCodes.ExtentReports.LogStatus.Unknown;
            }
        }
    }
}
