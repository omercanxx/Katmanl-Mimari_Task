//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public string WhyRejected { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Project Project { get; set; }
    }
}