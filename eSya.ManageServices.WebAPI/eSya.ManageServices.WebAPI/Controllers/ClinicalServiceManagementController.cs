using eSya.ManageServices.DL.Repository;
using eSya.ManageServices.DO;
using eSya.ManageServices.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ManageServices.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClinicalServiceManagementController : ControllerBase
    {
        private readonly ClinicalServiceManagementRepository _clinicalServiceManagementRepository;

        public ClinicalServiceManagementController(ClinicalServiceManagementRepository clinicalServiceManagementRepository)
        {
            _clinicalServiceManagementRepository = clinicalServiceManagementRepository;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetSpecialtiesByBusinessKey(int businessKey)
        {
            var ac = await _clinicalServiceManagementRepository.GetSpecialtiesByBusinessKey(businessKey);
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetClinicAndConsultationTypebySpecialty(int businessKey, int specialtyId)
        {
            var ac = await _clinicalServiceManagementRepository.GetClinicAndConsultationTypebySpecialty(businessKey, specialtyId);
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorsbySpecialtyClinicAndConsultation(int businessKey, int specialtyId, int clinicId, int consultationId)
        {
            var ac = await _clinicalServiceManagementRepository.GetDoctorsbySpecialtyClinicAndConsultation(businessKey, specialtyId, clinicId, consultationId);
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctordaySchedulebySearchCriteria(int businessKey, int specialtyId, int clinicId, int consultationId, int doctorId, DateTime scheduleFromDate, DateTime scheduleToDate)
        {
            var ac = await _clinicalServiceManagementRepository.GetDoctordaySchedulebySearchCriteria(businessKey, specialtyId, clinicId, consultationId, doctorId, scheduleFromDate, scheduleToDate);
            return Ok(ac);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctordaySchedule(DO_DoctordaySchedule obj)
        {
            var msg = await _clinicalServiceManagementRepository.InsertIntoDoctordaySchedule(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDoctordaySchedule(DO_DoctordaySchedule obj)
        {
            var msg = await _clinicalServiceManagementRepository.UpdateDoctordaySchedule(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> ActiveOrDeActiveDoctordaySchedule(DO_DoctordaySchedule objdel)
        {
            var msg = await _clinicalServiceManagementRepository.ActiveOrDeActiveDoctordaySchedule(objdel);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> ImpotedDoctorScheduleList(List<DO_DoctordaySchedule> obj)
        {
            var msg = await _clinicalServiceManagementRepository.ImpotedDoctorScheduleList(obj);
            return Ok(msg);
        }
    }
}
