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

    public partial class tblT_CheckIn
    {
        public int CheckIn_PK { get; set; }
        public int SOWAssign_FK { get; set; }
        public byte[] File { get; set; }
        public System.DateTime WaktuCheckIn { get; set; }
        public string LongitudeCheckIn { get; set; }
        public string LatitudeCheckIn { get; set; }
        public string CellIDCheckIn { get; set; }
        public Nullable<System.DateTime> WaktuCheckOut { get; set; }
        public string LongitudeCheckOut { get; set; }
        public string LatitudeCheckOut { get; set; }
        public string CellIDCheckOut { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Status_FK { get; set; }
    }
}
