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
    
    public partial class AwardsIssued
    {
        public int Id { get; set; }
        public int student_Id { get; set; }
        public int competition_Id { get; set; }
        public System.DateTime AwardDate { get; set; }
        public string Remarks { get; set; }
    
        public virtual Competition Competition { get; set; }
        public virtual student student { get; set; }
    }
}