
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
    
public partial class schedule_time
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public schedule_time()
    {

        this.course_schedule = new HashSet<course_schedule>();

    }


    public int schedule_time_id { get; set; }

    public string schedule_time1 { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<course_schedule> course_schedule { get; set; }

}

}
