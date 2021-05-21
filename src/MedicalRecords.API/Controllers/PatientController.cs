using MedicalRecords.API.Filters;
using MedicalRecords.API.ResponseModels;
using MedicalRecords.Domain.Requests;
using MedicalRecords.Domain.Requests.Patient;
using MedicalRecords.Domain.Responses;
using MedicalRecords.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRecords.API.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _patientService.GetPatientsAsync();

            var totalPatients = result.Count();

            var patientsOnPage = result
                .OrderBy(p => p.Id)
                //.OrderBy(p => p.PatientSurname )
                //.ThenBy(p => p.PatientName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);

            var model = new PaginatedEntityResponseModel<PatientResponse>(
                pageIndex, pageSize, totalPatients, patientsOnPage);


            return Ok(model);
            
        }

        [HttpGet("{id}")]
        [PatientExists]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _patientService.GetPatientAsync(new GetPatientRequest { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddPatientRequest request)
        {
            var result = await _patientService.AddPatientAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
        }

        [HttpPut("{id}")]
        [PatientExists]
        public async Task<IActionResult> Put(int id, EditPatientRequest request)
        {
            request.Id = id;
            var result = await _patientService.EditPatientAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [PatientExists]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeletePatientRequest{Id = id};
            await _patientService.DeletePatientAsync(request);
            return NoContent();
        }

        //[HttpGet("/riskFactor/{id}/patients")]
        //[PatientExists]
        //public async Task<IActionResult> GetByRiskFactorId(int id)
        //{
        //    var result = await _patientService.GetPatientByRiskFactorIdAsync(id);
        //    return Ok(result);
        //}
    }
}
