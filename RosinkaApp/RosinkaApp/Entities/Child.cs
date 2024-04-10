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
    
    public partial class Child
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Child()
        {
            this.ChildGroup = new HashSet<ChildGroup>();
            this.ChildParent = new HashSet<ChildParent>();
        }
    
        public int ChildId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string BirthCertificateId { get; set; }
        public Nullable<int> Status { get; set; }
        public byte[] ChildPhoto { get; set; }
        public Nullable<int> HealthCardId { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Nationality { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
    
        public virtual BirthCertificate BirthCertificate { get; set; }
        public virtual HealthCard HealthCard { get; set; }
        public virtual Status Status1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChildGroup> ChildGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChildParent> ChildParent { get; set; }
    }
}