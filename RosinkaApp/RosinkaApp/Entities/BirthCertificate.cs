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
    
    public partial class BirthCertificate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BirthCertificate()
        {
            this.Child = new HashSet<Child>();
        }
    
        public string BirthCertificateId { get; set; }
        public Nullable<System.DateTime> BirthCertificateDate { get; set; }
        public string IssuedBy { get; set; }
        public string PlaceOfBirth { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Child> Child { get; set; }
    }
}