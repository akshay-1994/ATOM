using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AurigoTest.Toolkit.Core;
using System.Runtime.CompilerServices;
using System.Data;

namespace AurigoTest.Toolkit.Core
{
    public class DataRowVerifier<A> : VerifierBase<DataRowVerifier<A>, DataRow>
    {
        private DataRow DataRowRef { get { return base.PageRef; } }

        public DataRowVerifier(DataRow dr, string formContext) : base(dr)
        {
        }

        //public DataRowVerifier<A> Assert_Data(string fieldName, object expectedValue)
        //{
        //    var actual = this.DataRowRef[fieldName];
        //    return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, actual));
        //}

        public DataRowVerifier<A> Assert_Data(string fieldName, decimal expectedValue)
        {
            var actual = this.DataRowRef[fieldName] as decimal?;
            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, actual.Value));
        }

        public DataRowVerifier<A> Assert_Data(string fieldName, string expectedValue)
        {
            var actual = this.DataRowRef[fieldName] as string;
            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, actual));
        }

        public DataRowVerifier<A> Assert_Data(string fieldName, bool expectedValue)
        {
            var actual = this.DataRowRef[fieldName] as bool?;

            if (actual.HasValue)
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, actual.Value));
            else
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, null));
        }

        public DataRowVerifier<A> Assert_Data(string fieldName, int expectedValue)
        {
            var actual = this.DataRowRef[fieldName] as int?;

            if (actual.HasValue)
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, actual.Value));
            else
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, null));
        }

        public DataRowVerifier<A> Assert_Data(string fieldName, double expectedValue)
        {
            var actual = this.DataRowRef[fieldName] as double?;

            if (actual.HasValue)
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, actual.Value));
            else
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, null));
        }
    }
}