using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DO
{
    public class DO_ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string ServiceTypeDesc { get; set; }
        public int PrintSequence { get; set; }
        public bool ActiveStatus { get; set; }
        
    }
}
