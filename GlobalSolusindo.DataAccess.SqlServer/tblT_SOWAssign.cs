//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlobalSolusindo.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblT_SOWAssign
    {
        public int SOWAssign_PK { get; set; }
        public Nullable<int> SOW_FK { get; set; }
        public Nullable<int> User_FK { get; set; }
        public Nullable<int> KategoriJabatan_FK { get; set; }
        public Nullable<System.DateTime> TglMulai { get; set; }
        public Nullable<System.DateTime> TglSelesai { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Status_FK { get; set; }
    }
}
