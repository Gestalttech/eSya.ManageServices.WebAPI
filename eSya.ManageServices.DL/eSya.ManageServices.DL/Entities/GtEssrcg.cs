using System;
using System.Collections.Generic;

namespace eSya.ManageServices.DL.Entities
{
    public partial class GtEssrcg
    {
        public int ServiceClassId { get; set; }
        public string IntScpattern { get; set; } = null!;
        public int IntSccode { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
