using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public enum BrowserType { Chrome, IE, Mozilla }

    public enum EnumSearchLocation { MainWindow, IFrame }

    public enum EnumElementCallStatus { Success, ElementNotFound, TimeOutError }

    public enum EnumExceptionType
    {
        Unknown,
        AssertException,
        ElementNotFound,
        MethodCallAverted,
        NoUrlChange,
        UrlChanged,
        WaitOnElementNotMatched,
        ContinueIfFailed
    }
}
