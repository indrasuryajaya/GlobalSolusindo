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

    public partial class tblM_User
    {
        public int User_PK { get; set; }
        public int UserDetail_FK { get; set; }
        public int RoleGroup_FK { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Status_FK { get; set; }
    
        public virtual tblM_Status tblM_Status { get; set; }
        public virtual tblM_UserDetail tblM_UserDetail { get; set; }
    }
}
