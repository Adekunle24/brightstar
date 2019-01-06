namespace Aurora.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        
        public int Id { get; set; }
       
        public int floor { get; set; }

        public DateTime created_at { get; set; }
        
        public DateTime? updated_at { get; set; }

        [StringLength(128)]
        public string created_by { get; set; }
        public virtual AspNetUser CreatedBy { get; set; }
    }
}
