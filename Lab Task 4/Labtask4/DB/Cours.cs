//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Labtask4.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cours
    {
        public Cours()
        {
            this.CourseStudents = new HashSet<CourseStudent>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string PreReq { get; set; }
    
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
