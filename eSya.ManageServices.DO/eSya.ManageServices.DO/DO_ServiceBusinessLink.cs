using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DO
{
    public class DO_ServiceBusinessLink
    {
        public int BusinessKey { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceDesc { get; set; }
        public string? ServiceClassDesc { get; set; }
        public string? LocationDescription { get; set; }
        public decimal? ServiceCost { get; set; }
        public string? InternalServiceCode { get; set; }
        public decimal NightLinePercentage { get; set; }
        public decimal HolidayPercentage { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TerminalID { get; set; }
    }
}
