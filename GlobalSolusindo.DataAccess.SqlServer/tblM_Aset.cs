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
    
    public partial class tblM_Aset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblM_Aset()
        {
            this.tblT_AsetHistori = new HashSet<tblT_AsetHistori>();
        }
    
        public int Aset_PK { get; set; }
        public int KategoriAset_FK { get; set; }
        public string AsetID { get; set; }
        public string Name { get; set; }
        public byte[] FilePhoto { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Status_FK { get; set; }
    
        public virtual tblM_AsetKategori tblM_AsetKategori { get; set; }
        public virtual tblM_Status tblM_Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblT_AsetHistori> tblT_AsetHistori { get; set; }
    }
}
