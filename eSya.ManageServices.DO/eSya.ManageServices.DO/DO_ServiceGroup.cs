using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DO
{
    public class DO_ServiceGroup
    {
        public int ServiceGroupId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceGroupDesc { get; set; }
        public string ServiceCriteria { get; set; }
        public int PrintSequence { get; set; }
        public bool ActiveStatus { get; set; }
       
    }
}
