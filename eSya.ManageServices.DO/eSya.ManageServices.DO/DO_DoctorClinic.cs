using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DO
{
    public class DO_DoctorClinic
    {
        public int BusinessKey { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyDesc { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int ClinicId { get; set; }
        public string ClinicDesc { get; set; }
        public int ConsultationId { get; set; }
        public string ConsultationDesc { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
}
