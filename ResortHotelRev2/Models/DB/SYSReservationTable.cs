//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ResortHotelRev2.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYSReservationTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYSReservationTable()
        {
            this.SYSRoomReservedTables = new HashSet<SYSRoomReservedTable>();
            this.SYSOccupiedRoomTables = new HashSet<SYSOccupiedRoomTable>();
        }
    
        public int Id { get; set; }
        public System.DateTime DateIN { get; set; }
        public System.DateTime DateOUT { get; set; }
        public string Status { get; set; }
        public string MadeBy { get; set; }
        public Nullable<int> ReservedByUserId { get; set; }
        public Nullable<System.DateTime> ReservationPlaced { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYSRoomReservedTable> SYSRoomReservedTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYSOccupiedRoomTable> SYSOccupiedRoomTables { get; set; }
    }
}