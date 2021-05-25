using MedicalRecords.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using MedicalRecords.Domain.Services;
using MedicalRecords.Domain.Requests.RiskFactor;

namespace MedicalRecords.API.Controllers
{
    [Route("api/riskFactors")]
    [ApiController]
    public class RiskFactorController : Controller
    {
        private readonly IRiskFactorService _riskFactorService;

        public RiskFactorController(IRiskFactorService riskFactorService)
        {
            _riskFactorService = riskFactorService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var model = await _riskFactorService.GetRiskFactorsAsync(pageSize, pageIndex);   

            return Ok(model);

        }

        [HttpGet("{id}")]
        //[RiskFactorExists]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _riskFactorService.GetRiskFactorAsync(new GetRiskFactorRequest { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddRiskFactorRequest request)
        {
            var result = await _riskFactorService.AddRiskFactorAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
        }

        [HttpPut("{id}")]
        //[RiskFactorExists]
        public async Task<IActionResult> Put(int id, EditRiskFactorRequest request)
        {
            request.Id = id;
            var result = await _riskFactorService.EditRiskFactorAsync(request);
            return Ok(result);
        }
    }
}
