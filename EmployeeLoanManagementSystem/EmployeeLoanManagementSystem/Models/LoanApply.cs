//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeLoanManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoanApply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanApply()
        {
            this.Installments = new HashSet<Installment>();
        }
    
        public int LoanApplyId { get; set; }
        public int EmployeeId { get; set; }
        public int LoanCategory { get; set; }
        public decimal LoanMoney { get; set; }
        public int NoOfInstallments { get; set; }
        public Nullable<System.DateTime> InstallmentStartDate { get; set; }
        public Nullable<System.DateTime> InstallmentEndDate { get; set; }
        public byte[] Template { get; set; }
        public System.DateTime RequestDate { get; set; }
        public byte[] LoanAgreement { get; set; }
        public string LoanForProperty { get; set; }
        public string LoanForAutomobile { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Installment> Installments { get; set; }
        public virtual LoanCategory LoanCategory1 { get; set; }
        public virtual LoanDocumentVerify LoanDocumentVerify { get; set; }
        public virtual LoanRequestStatu LoanRequestStatu { get; set; }
    }
}
