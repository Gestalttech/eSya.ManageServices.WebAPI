using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DO
{
    public class DO_ServiceClass
    {
        public int ServiceClassId { get; set; }
        public int ServiceGroupId { get; set; }
        public string ServiceClassDesc { get; set; }
        public bool IsBaseRateApplicable { get; set; }
        public int ParentId { get; set; }
        public int PrintSequence { get; set; }
        public bool ActiveStatus { get; set; }
       
    }
}
