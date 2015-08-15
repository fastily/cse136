//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class course_schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public course_schedule()
        {
            this.enrollments = new HashSet<enrollment>();
            this.TeachingAssistants = new HashSet<TeachingAssistant>();
        }
    
        public int schedule_id { get; set; }
        public int course_id { get; set; }
        public int year { get; set; }
        public string quarter { get; set; }
        public string session { get; set; }
        public Nullable<int> schedule_day_id { get; set; }
        public Nullable<int> schedule_time_id { get; set; }
        public Nullable<int> instructor_id { get; set; }
    
        public virtual course course { get; set; }
        public virtual instructor instructor { get; set; }
        public virtual schedule_day schedule_day { get; set; }
        public virtual schedule_time schedule_time { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enrollment> enrollments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeachingAssistant> TeachingAssistants { get; set; }
    }
}
