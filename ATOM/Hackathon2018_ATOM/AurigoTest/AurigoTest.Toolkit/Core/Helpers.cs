using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public static class Helpers
    {
        public static string DateTimeNow()
        {
            return DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }

        public static string NewLogFileNameWithDateTime(string fileNamePrefix, string fileExt)
        {
            if (!fileExt.StartsWith("."))
                fileExt = "." + fileExt;

            string fileName = (fileNamePrefix ?? "NewFile") + DateTimeNow() + fileExt;
            return fileName;
        }

        public static string GetLogFileWithFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NewLogFileNameWithDateTime("TestRun", ".html"));
        }
        public static string GetImageLogFileWithFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NewLogFileNameWithDateTime("Img", ".png"));
        }

        public static string GetUniqueData(string str)
        {
            return str + DateTimeNow();
        }

        public static string ReportFileFullPath { get; } = GetLogFileWithFullPath();

        public static ExtentReports Report = new ExtentReports(ReportFileFullPath);

        public static string EscapeForHTML(string str)
        {
            if (str == null) return str;
            return str.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;");
        }


        public static string GetJavascript_DateObject(DateTime dt)
        {
            string dateValueObj = //" new Date(year, month, day, hours, minutes, seconds, milliseconds = 0)"
                          string.Format(" new Date({0},   {1},  {2},  {3},   {4},     {5},     0)", dt.Year, dt.Month - 1, dt.Day, dt.Hour, dt.Minute, dt.Second);//monte is zero based in javascript

            return dateValueObj;
        }

        public static string ObjectHelper_GetPropertyNameString<A, TField>(A thisRef, Expression<Func<A, TField>> propertyLambda)
        {
            Type type = typeof(A);

            MemberExpression memberExpr = propertyLambda.Body as MemberExpression;

            if (memberExpr == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));

            PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()));

            //This condition may not be required
            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyLambda.ToString(), type));

            return memberExpr.Member.Name;
        }

        public static A ObjectHelper_InjectValueToProperty<A>(A thisRef, string propertyName, object value)
        {
            var properties = thisRef.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            PropertyInfo propertyInfo = properties.FirstOrDefault(t => t.Name == propertyName);

            if (propertyInfo == null)
                throw new NotImplementedException(string.Format("Property '{0}' not found in class.", propertyName));

            propertyInfo.SetValue(thisRef, value);

            return thisRef;
        }

        public static object ObjectHelper_ExtractValueFromProperty<A>(A thisRef, string propertyName)
        {
            var properties = thisRef.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            PropertyInfo propertyInfo = properties.FirstOrDefault(t => t.Name == propertyName);

            if (propertyInfo == null)
                throw new NotImplementedException(string.Format("Property '{0}' not found in class.", propertyName));

            return propertyInfo.GetValue(thisRef);
        }

        public static string GetPropertyAndObject<T>(Expression<Action<T>> wrapperMethod)
        {
            StringBuilder sbr = new StringBuilder();

            var methodCallExpr = wrapperMethod.Body as MethodCallExpression;
            var dict = new Dictionary<string, object>();
            var parameterExpressions = methodCallExpr.Arguments;
            int counter = 0;

            foreach (var param in methodCallExpr.Method.GetParameters())
            {
                var parameterExpression = parameterExpressions[counter++];
                var paramValueAccessor = Expression.Lambda(parameterExpression);
                var paramValue = paramValueAccessor.Compile().DynamicInvoke();
                dict[param.Name] = paramValue;

                sbr.AppendFormat(" [{0}={1}] ", param.Name, paramValue);
            }

            return sbr.ToString();
        }

    }
}
