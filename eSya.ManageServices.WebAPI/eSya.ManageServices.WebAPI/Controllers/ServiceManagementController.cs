using eSya.ManageServices.DO;
using eSya.ManageServices.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ManageServices.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceManagementController : ControllerBase
    {
        private readonly IServiceManagementRepository _ServiceManagementRepository;
        public ServiceManagementController(IServiceManagementRepository serviceManagementRepository)
        {
            _ServiceManagementRepository = serviceManagementRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceTypes()
        {
            var ac = await _ServiceManagementRepository.GetServiceTypes();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceGroups()
        {
            var ac = await _ServiceManagementRepository.GetServiceGroups();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceClasses()
        {
            var ac = await _ServiceManagementRepository.GetServiceClasses();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceCodes()
        {
            var ac = await _ServiceManagementRepository.GetServiceCodes();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceCodeByID(int ServiceID)
        {
            var ac = await _ServiceManagementRepository.GetServiceCodeByID(ServiceID);
            return Ok(ac);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateServiceCode(DO_ServiceCode obj)
        {
            var msg = await _ServiceManagementRepository.AddOrUpdateServiceCode(obj);
            return Ok(msg);
        }
        [HttpGet]
        public async Task<IActionResult> GetBusinessLocationServices(int businessKey)
        {
            var ac = await _ServiceManagementRepository.GetBusinessLocationServices(businessKey);
            return Ok(ac);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateBusinessLocationServices(List<DO_ServiceBusinessLink> obj)
        {
            var msg = await _ServiceManagementRepository.AddOrUpdateBusinessLocationServices(obj);
            return Ok(msg);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceBusinessLocations(int ServiceId)
        {
            var ac = await _ServiceManagementRepository.GetServiceBusinessLocations(ServiceId);
            return Ok(ac);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateServiceBusinessLocations(List<DO_ServiceBusinessLink> obj)
        {
            var msg = await _ServiceManagementRepository.UpdateServiceBusinessLocations(obj);
            return Ok(msg);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceBusinessLink(int businessKey)
        {
            var ac = await _ServiceManagementRepository.GetServiceBusinessLink(businessKey);
            return Ok(ac);
        }
       
    }
}
