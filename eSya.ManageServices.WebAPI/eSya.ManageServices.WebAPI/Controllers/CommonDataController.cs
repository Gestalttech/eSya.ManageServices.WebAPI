using eSya.ManageServices.DL.Repository;
using eSya.ManageServices.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ManageServices.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonDataController : ControllerBase
    {
        private readonly ICommonMethodRepository _commonMethodRepository;

        public CommonDataController(ICommonMethodRepository commonMethodRepository)
        {
            _commonMethodRepository = commonMethodRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBusinessKey()
        {
            var ac = await _commonMethodRepository.GetBusinessKey();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetApplicationCodesByCodeType(int codetype)
        {
            var ac = await _commonMethodRepository.GetApplicationCodesByCodeType(codetype);
            return Ok(ac);
        }
        
    }
}
