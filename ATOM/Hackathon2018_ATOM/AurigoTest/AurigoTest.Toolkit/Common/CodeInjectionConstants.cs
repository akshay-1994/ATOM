using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Common
{
    public abstract class CodeInjectionConstants
    {
        public const string DateFormatMethod = @"

window.getZeroPaddedNumber = function(num){
    if(num<10)
        return '0' + num;
    return ''+ num;
}

window.getFormattedDate = function(date) {
    if(typeof date.getMonth === 'function'){
        var str = date.getFullYear() + '-' 
                    + getZeroPaddedNumber(date.getMonth() + 1) + '-' 
                    + getZeroPaddedNumber(date.getDate()) + ' ' 
                    + getZeroPaddedNumber(date.getHours()) + ':' 
                    + getZeroPaddedNumber(date.getMinutes()) + ':' 
                    + getZeroPaddedNumber(date.getSeconds());
        return str;
    }

    return dateObj;
};";

    }
}
