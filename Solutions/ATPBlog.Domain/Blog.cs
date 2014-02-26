
namespace ATPBlog.Domain
{
    using SharpArch.Domain.DomainModel;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    class CustomFormatter : IFormatProvider, ICustomFormatter
    {
        // The GetFormat method of the IFormatProvider interface.
        // This must return an object that provides formatting services for the specified type.
        public object GetFormat(System.Type type)
        {
            return this;
        }
        // The Format method of the ICustomFormatter interface.
        // This must format the specified value according to the specified format settings.
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string formatValue = arg.ToString();
            if (format == "u")
                return formatValue.ToUpper();
            else if (format == "l")
                return formatValue.ToLower();
            else return formatValue;
        }
    }
   
    public class Blog : Entity
    {
        [DisplayName("Blog")]
        [Required(ErrorMessage = "Missing blog text!")]
        
        public virtual string BlogText { get; set; }
         
        [DisplayName("Time")]
        [DisplayFormat( DataFormatString="{0:hh:mm} th {0:MMM}")]
        public virtual DateTime BlogTime { get; set; }


    }
}