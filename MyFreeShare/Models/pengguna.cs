//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyFreeShare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class pengguna
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pengguna()
        {
            this.Table = new HashSet<Table>();
        }
    
        public string username { get; set; }
        public string nama { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table> Table { get; set; }
    }
}