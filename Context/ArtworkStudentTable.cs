//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FineArtProject.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArtworkStudentTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArtworkStudentTable()
        {
            this.StaffComment = new HashSet<StaffComment>();
        }
    
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Quotation { get; set; }
        public string Image { get; set; }
        public System.DateTime UploadDate { get; set; }
        public int CompititionId { get; set; }
    
        public virtual Competition Competition { get; set; }
        public virtual student student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffComment> StaffComment { get; set; }
    }
}
