//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyFreeShare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class File
    {
        public int Id { get; set; }
        public string file_path { get; set; }
        public string nama_file { get; set; }
        public string pengguna { get; set; }
        public Nullable<int> terunduh { get; set; }
    
        public virtual pengguna pengguna1 { get; set; }
    }
}