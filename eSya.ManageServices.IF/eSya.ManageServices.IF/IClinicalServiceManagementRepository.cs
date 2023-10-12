using eSya.ManageServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.IF
{
    public interface IClinicalServiceManagementRepository
    {
        Task<List<DO_SpecialtyCodes>> GetSpecialtiesByBusinessKey(int businessKey);
        Task<List<DO_DoctorClinic>> GetClinicAndConsultationTypebySpecialty(int businessKey, int specialtyId);
        Task<List<DO_DoctorMaster>> GetDoctorsbySpecialtyClinicAndConsultation(int businessKey, int specialtyId, int clinicId, int consultationId);
        Task<List<DO_DoctordaySchedule>> GetDoctordaySchedulebySearchCriteria(int businessKey, int specialtyId, int clinicId, int consultationId, int doctorId, DateTime scheduleFromDate, DateTime scheduleToDate);
        Task<DO_ReturnParameter> InsertIntoDoctordaySchedule(DO_DoctordaySchedule obj);
        Task<DO_ReturnParameter> UpdateDoctordaySchedule(DO_DoctordaySchedule obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveDoctordaySchedule(DO_DoctordaySchedule objdel);
        Task<DO_ReturnParameter> ImpotedDoctorScheduleList(List<DO_DoctordaySchedule> obj);
    }
}
