using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATPBlog.Web.Mvc
{
    public class CustomDateProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;

            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!(arg is DateTime)) throw new NotSupportedException();

            var dt = (DateTime)arg;

            string suffix;

            if (dt.Day % 10 == 1)
            {
                suffix = "st";
            }
            else if (dt.Day % 10 == 2)
            {
                suffix = "nd";
            }
            else if (dt.Day % 10 == 3)
            {
                suffix = "rd";
            }
            else
            {
                suffix = "th";
            }
            string strTime=String.Format("{0:t}", dt).ToLower ();
            string strDate=string.Format(" {0:MMM} {1}{2}, {0:yyyy}", arg, dt.Day, suffix );
            return strTime + strDate;
        }
    }
    
}