
namespace ATPBlog.Domain
{
    using SharpArch.Domain.DomainModel;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
 
    public class Blog : Entity
    {
        [DisplayName("Blog")]
        [Required(ErrorMessage = "Missing blog text!")]
        
        public virtual string BlogText { get; set; }
         
        [DisplayName("Time")]
        public virtual DateTime BlogTime { get; set; }


    }
}