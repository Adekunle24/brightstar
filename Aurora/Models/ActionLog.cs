namespace Aurora.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActionLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(128)]
        public string ModelName { get; set; }

        [StringLength(50)]
        public string ActionType { get; set; }

        public int? ModelId { get; set; }

        [Column(TypeName = "text")]
        public string ActionPayload { get; set; }
        public virtual AspNetUser CreatedBy { get; set; }
    }
}
