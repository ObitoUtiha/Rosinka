//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RosinkaApp.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Passport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Passport()
        {
            this.Parent = new HashSet<Parent>();
        }
    
        public int PassportId { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Citizenship { get; set; }
        public string RegistrationAddress { get; set; }
        public string ActualAddress { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parent> Parent { get; set; }
    }
}
