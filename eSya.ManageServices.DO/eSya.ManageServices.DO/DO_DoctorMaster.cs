using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DO
{
    public class DO_DoctorMaster
    {
        public int BusinessKey { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorShortName { get; set; }
        public string Gender { get; set; }
        public string DoctorRegnNo { get; set; }
        public string EMailId { get; set; }
        public int ISDCode { get; set; }
        public string MobileNumber { get; set; }
        public int DoctorClass { get; set; }
        public int DoctorCategory { get; set; }
        //public bool AllowConsultation { get; set; }
        //public bool AllowSMS { get; set; }
        public string TraiffFrom { get; set; }
        //public int PayoutType { get; set; }
        public string Password { get; set; }
        public int SeniorityLevel { get; set; }
        public string FormID { get; set; }
        public bool ActiveStatus { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        //for displaying properties
        public string DoctorClassDesc { get; set; }
        public string DoctorCategoryDesc { get; set; }
        //public string PayoutTypeDesc { get; set; }
        public string SeniorityLevelDesc { get; set; }

        ////for Image
        //public byte[] Photo { get; set; }
        //public string userimage { get; set; }

        //public List<DO_DoctorParameter> l_DoctorParameter { get; set; }

    }
}
